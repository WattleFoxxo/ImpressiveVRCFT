using System.Reflection;
using ResoniteModLoader;

namespace Impressive;

public partial class Impressive : ResoniteMod
{
    [AutoRegisterConfigKey]
    internal static ModConfigurationKey<bool> Enabled_Config = new("Enabled", "When checked, enables listening on the specified OSC port");
    public static bool Enabled => Config!.GetValue(Enabled_Config);
    
    [AutoRegisterConfigKey]
    internal static ModConfigurationKey<int> Port_Config = new("Port", "The port on which OSC will listen", () => 8000);
}