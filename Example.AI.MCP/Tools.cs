using System.ComponentModel;
using ModelContextProtocol.Server;

namespace Example.AI.MCP;

[McpServerToolType]
public class Tools
{
    [McpServerTool, Description("Возвращает текущую дату и время в строковом формате.")]
    public static DateTime GetCurrentDate()
    {
        return DateTime.Now;
    }

    [McpServerTool, Description("Возвращает имя пользователя.")]
    public static string GetUserName(int randomValue)
    {
        return "Владимир";
    }
}
