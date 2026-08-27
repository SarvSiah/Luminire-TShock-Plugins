using System.IO;
using Luminire.Core;

namespace Luminire.Essentials;

public class EssentialsConfig : ConfigBase<EssentialsConfig>
{
    public bool EnableJoinMessage { get; set; } = true;
    public string JoinMessageFormat { get; set; } = "{player} has joined Luminire!";

    public bool EnableDiscordLogging { get; set; } = false;
    public string DiscordWebhookUrl { get; set; } = "";

    public int MaxHomesPerPlayer { get; set; } = 5;
    public bool EnableHomeCommand { get; set; } = true;
    public bool EnableBackCommand { get; set; } = true;

    public bool EnableClearCommand { get; set; } = true;
    public string ClearPermission { get; set; } = "luminire.essentials.clear";

    public override string ConfigPath => Path.Combine(TShockAPI.TShock.SavePath, "Luminire", "Essentials.json");
}
