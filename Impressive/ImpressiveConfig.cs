using System.Reflection;
using ResoniteModLoader;

namespace Impressive;

public partial class Impressive : ResoniteMod
{
    [AutoRegisterConfigKey]
    internal static ModConfigurationKey<bool> Enabled_Config = new("Enabled", "When checked, enables Impressive face & eye tracking", () => true);
    public static bool Enabled => Config!.GetValue(Enabled_Config);
    
    [AutoRegisterConfigKey]
    internal static ModConfigurationKey<int> In_Port_Config = new("InPort", "The port on which OSC will listen", () => 9000);

    [AutoRegisterConfigKey]
    internal static ModConfigurationKey<int> Out_Port_Config = new("OutPort", "The port on which OSC will send", () => 9001);

    [AutoRegisterConfigKey]
    internal static ModConfigurationKey<string> Out_Address_Config = new("OutAddress", "The address on which OSC will send", () => "127.0.0.1");
}