namespace Example.AI.App02;

internal static class ChatThemes
{
    private static readonly Dictionary<string, ThemeDescription> themes = new()
    {
        ["common"] = new (
            "К этой теме относятся вопросы на любые темы, которые не входят не входят в другие темы.",
            string.Empty),
        ["wether"] = new (
            "К этой теме относятся вопросы на тему погоды и климата.",
            "Мне нравятся тёплые спокойные вечера, чтобы было не жарко."),
        ["geography"] = new (
            "К этой теме относятся вопросы на тему географических объектов (водоёмы, возвышенности, страны, города и т.д.).",
            "Горы и море сочетаются прекрасно. Получаются очень красивые пейзажи."),
        ["animals"] = new (
            "К этой теме относятся на тему животных и всего, что с ними связано.",
            "Всё-таки собаки - это очень верные животные."),
    };

    public static string ThemesDescription =>
        string.Join("Вот список тем, которые у меня есть:\n", themes.Select(x => $"{x.Key} - {x.Value.Description}"));

    public static string GetThemeContent(string name) =>
        themes.TryGetValue(name, out var value) ? value.Content : string.Empty;

    private sealed record ThemeDescription(string Description, string Content);
}