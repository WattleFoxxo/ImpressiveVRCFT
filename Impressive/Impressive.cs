using HarmonyLib;
using ResoniteModLoader;
using FrooxEngine;
using System.Net.Http;

namespace Impressive;

public partial class Impressive : ResoniteMod
{
    public override string Name => "Impressive";
    public override string Author => "Cyro";
    public override string Version => typeof(Impressive).Assembly.GetName().Version.ToString();
    public override string Link => "https://github.com/RileyGuy/Impressive";
    public static ModConfiguration? Config;

    public override void OnEngineInit()
    {
        Harmony harmony = new("net.Cyro.Impressive");
        Config = GetConfiguration();
        Config?.Save(true);

        CreateParameters();

        // harmony.PatchAll();
        Msg("Patched successfully!");
        Engine engine = Engine.Current;
        engine.RunPostInit(() => 
        {
            try
            {
                engine.InputInterface.RegisterInputDriver(new SteamLinkDriver());
            }
            catch (Exception ex)
            {
                Msg($"Failed to initialize SteamLink driver! Exception: {ex}");
            }
        });
    }

    // https://github.com/hazre/VRCFTReceiver/blob/main/VRCFTReceiver/ParametersFile.cs
    public static async Task CreateParameters()
    {
        string url = "https://raw.githubusercontent.com/hazre/VRCFTReceiver/main/static/vrc_parameters.json";

        string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "LocalLow", @"VRChat\VRChat\OSC\vrcft\Avatars\vrc_parameters.json");


        if (File.Exists(savePath))
        {
            Msg("JSON file already exists.");
            return;
        };

        // Check if the directory exists, if not, create it
        string directory = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Download and save the JSON file
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    File.WriteAllText(savePath, jsonContent);
                    Msg("JSON file downloaded and saved successfully.");
                }
                else
                {
                    Warn("Failed to download JSON file. HTTP status code: " + response.StatusCode);
                }
            }
            catch (Exception e)
            {
                Error("An error occurred: " + e.Message);
            }
        }
    }
}
