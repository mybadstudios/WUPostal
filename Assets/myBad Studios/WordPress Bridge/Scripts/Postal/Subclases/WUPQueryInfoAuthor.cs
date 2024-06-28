//WordPress For Unity Bridge: Postal © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 

namespace MBS
{
    public class WUPQueryInfoAuthor : WUPQueryInfo
    {
        public void AddId(int id) => AddInt(EWUPCategoryMethod.Multiple, id);
        public void AddIds(int[] ids) => AddInts(EWUPCategoryMethod.Multiple, ids);
        public void AddIdIn(int id) => AddInt(EWUPCategoryMethod.IdIn, id);
        public void AddIdsIn(int[] ids) => AddInts(EWUPCategoryMethod.IdIn, ids);

        public void Exclude(int id) => ExcludeInt(id);
        public void ExcludeMultiple(int[] ids) => ExcludeInts(ids);

        public void Output(ref CMLData _data) => OutPut(ref _data, "author", "author__in", "", "author__not_in", "author_name");
    }
}