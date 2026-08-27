using System.IO;
using Luminire.Core;

namespace Luminire.Template;

public class TemplateConfig : ConfigBase<TemplateConfig>
{
    public bool ExampleFeatureEnabled { get; set; } = true;
    public string ExampleMessage { get; set; } = "Hello from Luminire Template!";
    public int ExampleNumber { get; set; } = 42;

    public override string ConfigPath => Path.Combine(TShockAPI.TShock.SavePath, "Luminire", "Template.json");
}
