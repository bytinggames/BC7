using System;
using System.Collections.Generic;

namespace BC7
{
    public class @Loca
    {
        public static Dictionary<string, string> Dict;
        public static Localization L;
        public static void Initialize(string languageKey)
        {
            L = new Localization("Content/Loca.loca", languageKey);
            Dict = L.GetDictionary();
        }
        public static string hello => Dict["hello"];
        public static string menue => Dict["menue"];
        public static string ingame => Dict["ingame"];
        public class @Menue
        {
            public static string text => Dict["menue.text"];
        }
    }

    public class LocaArray
    {
        private readonly Func<string, string> getLocaValue;
        private readonly string baseKey;
        public int Length { get; }

        public LocaArray(Func<string, string> getLocaValue, string baseKey, int length)
        {
            this.getLocaValue = getLocaValue;
            this.baseKey = baseKey;
            Length = length;
        }

        public string this[int index] => getLocaValue(baseKey + index);
    }
}