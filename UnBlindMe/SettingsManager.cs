using System;
using System.IO;
using System.Text.Json;

namespace UnBlindMe
{
    public class AppSettings
    {
        public double DimmingOpacity { get; set; } = 0.4; // Default 40%
        public bool AlwaysOnTop { get; set; } = false;
        public bool RememberPosition { get; set; } = true;
        public double WindowTop { get; set; } = 100;
        public double WindowLeft { get; set; } = 100;
        public double WindowWidth { get; set; } = 800;
        public double WindowHeight { get; set; } = 450;
    }

    public static class SettingsManager
    {
        private static readonly string SettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "UnBlindMe",
            "settings.json"
        );

        public static AppSettings LoadSettings()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    string json = File.ReadAllText(SettingsPath);
                    return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                }
            }
            catch (Exception)
            {
                // Fallback to defaults on error
            }
            return new AppSettings();
        }

        public static void SaveSettings(AppSettings settings)
        {
            try
            {
                string directory = Path.GetDirectoryName(SettingsPath)!;
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SettingsPath, json);
            }
            catch (Exception)
            {
                // Handle or log error
            }
        }
    }
}
