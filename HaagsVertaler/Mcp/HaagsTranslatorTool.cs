using System.ComponentModel;
using ModelContextProtocol.Server;

namespace HaagsVertaler.Mcp
{
  /// <summary>
  /// Stelt de Haagse vertaler beschikbaar als MCP-tool, zodat AI-agents
  /// (Claude, ChatGPT, etc.) Nederlands naar Haags kunnen laten vertalen
  /// zonder dat ze de website zelf open hoeven te hebben.
  /// </summary>
  [McpServerToolType]
  public static class HaagsTranslatorTool
  {
    [McpServerTool(Name = "vertaal_naar_haags")]
    [Description("Vertaal Nederlandse tekst naar het Haagse dialect (zoals gesproken door Harry uit Den Haag).")]
    public static string VertaalNaarHaags(
      [Description("De Nederlandse tekst die naar het Haags vertaald moet worden.")] string tekst)
    {
      if (string.IsNullOrWhiteSpace(tekst))
        return string.Empty;

      return HaagsTranslator.Translator.Translate(tekst);
    }
  }
}
