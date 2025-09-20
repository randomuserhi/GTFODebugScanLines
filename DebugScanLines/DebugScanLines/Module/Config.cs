using BepInEx;
using BepInEx.Configuration;

namespace DebugScanLines.BepInEx {
    public static partial class ConfigManager {
        public static ConfigFile configFile;

        static ConfigManager() {
            string text = Path.Combine(Paths.ConfigPath, $"{Module.Name}.cfg");
            configFile = new ConfigFile(text, true);

            debug = configFile.Bind(
                "Debug",
                "enable",
                false,
                "Enables debug messages when true.");

            thickness = configFile.Bind(
                "Settings",
                "Thickness",
                8.0f,
                "Thickness of holopath.");

            r = configFile.Bind(
                "Settings",
                "Red",
                1.0f,
                "Red channel.");

            g = configFile.Bind(
                "Settings",
                "Green",
                1.0f,
                "Green channel.");

            b = configFile.Bind(
                "Settings",
                "Blue",
                1.0f,
                "Blue channel.");

            a = configFile.Bind(
                "Settings",
                "Alpha",
                1.0f,
                "Alpha channel.");
        }

        public static bool Debug {
            get { return debug.Value; }
            set { debug.Value = value; }
        }
        private static ConfigEntry<bool> debug;

        public static float Thickness {
            get { return thickness.Value; }
            set { thickness.Value = value; }
        }
        private static ConfigEntry<float> thickness;

        public static float R {
            get { return r.Value; }
            set { r.Value = value; }
        }
        private static ConfigEntry<float> r;

        public static float G {
            get { return g.Value; }
            set { g.Value = value; }
        }
        private static ConfigEntry<float> g;

        public static float B {
            get { return b.Value; }
            set { b.Value = value; }
        }
        private static ConfigEntry<float> b;

        public static float A {
            get { return a.Value; }
            set { a.Value = value; }
        }
        private static ConfigEntry<float> a;
    }
}