//WordPress For Unity Bridge: Postal © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 

namespace MBS
{
    using System.Collections.Generic;

    public class WUPNestedQueryArray<T> : CMLDataBase where T : WUPTaxQueryEntry
    {
        public List<T> Entries { get; private set; } = null;
        public WUPNestedQueryArray<T> NestedLevel { get; private set; } = null;
        string Relation { get; set; } = "AND";

        public int EntriesCount => Entries?.Count ?? 0;

        public T AddEntry(T value, EWUPAndOr relation)
        {
            Relation = relation.ToString();
            return AddEntry(value);
        }

        public T AddEntry(T value)
        {
            if (null == Entries)
                Entries = new List<T>();
            Entries.Add(value);
            return value;
        }

        public WUPNestedQueryArray<T> CreateNestedEntries()
        {
            NestedLevel = new WUPNestedQueryArray<T>();
            return NestedLevel;
        }

        public string ToString(int nested = 0)
        {
            string newline = "";
            string tabs = "";
            #region DELETE THIS AT SOME POINT
            //if (nested > 0)
                //for (int i = 0; i < nested; i++)
                    //tabs += "\t";
            //newline = "\n";
            #endregion
            string result = tabs;

            if (EntriesCount > 1)
                result += $"array({newline}{tabs}'relation'=>'{Relation}',{newline}";
            else if   (EntriesCount == 1 && NestedLevel?.EntriesCount > 0)
                    result += $"array({newline}'relation'=>'{Relation}',{newline}";
            else
            {
                if (nested == 0)
                {
                    result += $"array({newline}";
                }
                if (EntriesCount == 1 && NestedLevel?.EntriesCount > 0)
                    result += $"'relation'=>'{Relation}',{newline}";
            }
            if (EntriesCount > 0)
            {
                foreach (var entry in Entries)
                {
                    result += $"{tabs}array({newline}{tabs}\t";
                    foreach (var kvp in entry.defined)
                    {
                        var val = (kvp.Key == "operator") ? kvp.Value.Replace("_", " ") : kvp.Value;
                        result += $"'{kvp.Key}' => '{val}',{newline}{tabs}\t";
                    }
                    if (entry.Terms != "")
                        result += $"{tabs}'terms' => {entry.Terms},{newline}{tabs}";
                    result += $"),{newline}";
                }
            }
            if (null != NestedLevel)
                result += NestedLevel.ToString(nested + 1);
            result += $"){newline}";
            result = result.Replace(",)", ")");
            return result;
        }
    }
}