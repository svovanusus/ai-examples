using System.ClientModel;
using Microsoft.Extensions.AI;

Console.WriteLine("== AI Console Chat 001 ==");

var chatClient = new OpenAI.Chat.ChatClient(
    "qwen2.5-7b-instruct-1m",
    new ApiKeyCredential("key"),
    new OpenAI.OpenAIClientOptions()
    {
        Endpoint = new Uri("http://127.0.0.1:1234/v1"),
    }).AsIChatClient();

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

        var response = await chatClient.GetResponseAsync(question);

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
