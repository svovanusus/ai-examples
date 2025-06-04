using System.ClientModel;
using Example.AI.App03;
using Microsoft.Extensions.AI;

Console.WriteLine("== AI Console Chat 003 ==");

var chatClient = new OpenAI.Chat.ChatClient(
    "qwen2.5-7b-instruct-1m",
    new ApiKeyCredential("key"),
    new OpenAI.OpenAIClientOptions()
    {
        Endpoint = new Uri("http://127.0.0.1:1234/v1"),
    })
    .AsIChatClient()
    .AsBuilder()
    .UseFunctionInvocation()
    .Build();

IList<AITool> tools = [
    AIFunctionFactory.Create(AppTools.GetCurrentDate, "get_current_date", "Возвращает текущую дату и время в строковом формате. Используй эту функцию, чтобы получить текущие дату и время."),
    AIFunctionFactory.Create(AppTools.GetUserName, "get_username", "Запрашивает у пользователя его имя и возвращает полученное значение."),
];

List<ChatMessage> history = [
    new ChatMessage(ChatRole.System, "Ты профессиональный ментор. Я буду задавать тебе вопросы, твоя задача отвечать на эти вопросы максимально понятным и простым языком."),
];

while (true)
{
    try
    {
        Console.Write("Вы > ");
        var question = Console.ReadLine();
        if (string.IsNullOrEmpty(question))
        {
            continue;
        }

        history.Add(new ChatMessage(ChatRole.User, question));

        var response = await chatClient.GetResponseAsync(history, new()
        {
            Temperature = 0.1f,
            Tools = tools,
        });

        history.AddMessages(response);

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
