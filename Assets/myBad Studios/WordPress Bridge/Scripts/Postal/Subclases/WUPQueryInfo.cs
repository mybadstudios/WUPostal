//WordPress For Unity Bridge: Postal © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 

namespace MBS
{
    using UnityEngine;
    using System.Collections.Generic;

    public class WUPQueryInfo
    {
        List<int>
            entries,
            entries_in,
            entries_and,
            entries_exclusions;

        List<string>
            entries_slug,
            entries_slug_in,
            entries_slug_and,
            entries_slug_exclusions;

        public WUPQueryInfo()
        {
            entries = new List<int>();
            entries_in = new List<int>();
            entries_and = new List<int>();
            entries_exclusions = new List<int>();

            entries_slug = new List<string>();
            entries_slug_and = new List<string>();
            entries_slug_in = new List<string>();
            entries_slug_exclusions = new List<string>();
        }

        public void ExcludeInt(int id) => AddInt(EWUPCategoryMethod.IdExclude, Mathf.Abs(id));
        public void ExcludeInts(int[] ids)
        {
            if (null != ids)
                foreach (int id in ids)
                    ExcludeInt(id);
        }

        public void AddInt(EWUPCategoryMethod method, int value)
        {
            switch (method)
            {
                case EWUPCategoryMethod.Multiple:
                    if (!entries.Contains(value))
                        entries.Add(value);
                    return;

                case EWUPCategoryMethod.IdIn:
                    if (!entries_in.Contains(value))
                        entries_in.Add(value);
                    return;

                case EWUPCategoryMethod.IdAnd:
                    if (!entries_and.Contains(value))
                        entries_and.Add(value);
                    return;

                case EWUPCategoryMethod.IdExclude:
                    if (!entries_exclusions.Contains(value))
                        entries_exclusions.Add(value);
                    return;

            }
        }

        public void AddInts(EWUPCategoryMethod method, int[] values)
        {
            if (null != values && values.Length > 0)
                foreach (int value in values)
                    AddInt(method, value);
        }

        public void SetNiceName(string slug) => SetSlug(slug); // Alias for SetSlug()
        public void SetSlug(string value) => entries_slug = new List<string>() { value };
        
        public void AddSlug(EWUPCategoryMethod method, string value)
        {
            switch (method)
            {
                case EWUPCategoryMethod.Multiple:
                    if (!entries_slug.Contains(value))
                        entries_slug.Add(value);
                    return;

                case EWUPCategoryMethod.SlugIn:
                    if (!entries_slug_in.Contains(value))
                        entries_slug_in.Add(value);
                    return;

                case EWUPCategoryMethod.SlugAnd:
                    if (!entries_slug_and.Contains(value))
                        entries_slug_and.Add(value);
                    return;

                case EWUPCategoryMethod.IdExclude:
                    if (!entries_slug_exclusions.Contains(value))
                        entries_slug_exclusions.Add(value);
                    return;
            }
        }

        public void AddSlugs(EWUPCategoryMethod method, string[] values)
        {
            if (null != values && values.Length > 0)
                foreach (string value in values)
                    AddSlug(method, value);
        }

        private string ListToString(List<int> items) => string.Join(",", items);
        private string ListToString(List<string> items) => string.Join(",", items);

        public void OutPut(
            ref CMLData data,
            string eEntries = "",
            string eIn = "",
            string eAnd = "",
            string eExclude = "",
            string eSlugEntries = "",
            string eSlugIn = "",
            string eSlugAnd = "",
            string eSlugExclude = ""
            )
        {
            if (eEntries != string.Empty && entries.Count > 0)
                data.Set(eEntries, ListToString(entries));

            if (eIn != string.Empty && entries_in.Count > 0)
                data.Set(eIn, ListToString(entries_in));

            if (eAnd != string.Empty && entries_and.Count > 0)
                data.Set(eAnd, ListToString(entries_and));

            if (eExclude != string.Empty && entries_exclusions.Count > 0)
                data.Set(eExclude, ListToString(entries_exclusions));

            if (eSlugEntries != string.Empty && entries_slug.Count > 0)
                data.Set(eSlugEntries, ListToString(entries_slug));

            if (eSlugIn != string.Empty && entries_slug_in.Count > 0)
                data.Set(eSlugIn, ListToString(entries_slug_in));

            if (eSlugAnd != string.Empty && entries_slug_and.Count > 0)
                data.Set(eSlugAnd, ListToString(entries_slug_and));

            if (eSlugExclude != string.Empty && entries_slug_exclusions.Count > 0)
                data.Set(eSlugExclude, ListToString(entries_slug_exclusions));
        }
    }

}