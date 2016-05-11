using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Kava._2
{
    public partial class App
    {
        private static readonly Random random;

        static App()
        {
            random = new Random();
            names = new List<string>();
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "Kava._2.names.txt";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                while (true)
                {
                    var name = reader.ReadLine();
                    if (name == null) break;
                    names.Add(name);
                }
            }
        }

        public static int RandomizerNext(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }

        private static readonly List<string> names;

        public static string GetRandomName()
        {
            return names[RandomizerNext(0, names.Count)];
        }
    }
}