using System.ClientModel;
using Example.AI.App02;
using Microsoft.Extensions.AI;

Console.WriteLine("== AI Console Chat 002 ==");

var intentSystemMessage = new ChatMessage(ChatRole.System, $"Ты помощник, который определяет тему вопроса пользователя. Твоя задача определить тему запроса из заданного списка.\n{ChatThemes.ThemesDescription}");
var mainSystemMessage = new ChatMessage(ChatRole.System, "Ты полезный помощник!");

var chatClient = new OpenAI.Chat.ChatClient(
    "qwen2.5-7b-instruct-1m",
    new ApiKeyCredential("key"),
    new OpenAI.OpenAIClientOptions()
    {
        Endpoint = new Uri("http://127.0.0.1:1234/v1"),
    })
    .AsIChatClient();

List<ChatMessage> intentHistory = [intentSystemMessage];
List<ChatMessage> globalHistory = [];

while (true)
{
    try
    {
        // Ask user
        Console.Write("Вы > ");
        var question = Console.ReadLine();
        if (string.IsNullOrEmpty(question))
        {
            continue;
        }

        var questionMessage = new ChatMessage(ChatRole.User, question);
        intentHistory.Add(questionMessage);
        globalHistory.Add(questionMessage);

        // Intent
        var intentResponse = await chatClient.GetResponseAsync<IntentModel>(
            intentHistory,
            new()
            {
                Temperature = 0.1f,
            },
            true);

        intentHistory.AddMessages(intentResponse);

        // Generate local history with theme
        List<ChatMessage> history = [mainSystemMessage];

        var themeContent = ChatThemes.GetThemeContent(intentResponse.Result.Theme);
        if (!string.IsNullOrEmpty(themeContent))
        {
            history.Add(new ChatMessage(ChatRole.User, $"Вот мои предпочтения: {themeContent}."));
        }

        history.AddRange(globalHistory);

        // Send Messages
        var response = await chatClient.GetResponseAsync(history, new()
        {
            Temperature = 0.1f,
        });

        // Add Responses to history
        globalHistory.AddMessages(response);

        // Show Result
        foreach (var message in response.Messages)
        {
            if (!string.IsNullOrEmpty(message.Text))
            {
                Console.WriteLine($"Бот > {message.Text}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"В процессе выполнения возникла ошибка: {ex}");
    }
}
