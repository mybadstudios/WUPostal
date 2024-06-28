//WordPress For Unity Bridge: Postal © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 

namespace MBS
{
    public class WUPQueryInfoTags : WUPQueryInfo
    {
        //supports tag (string), tag_id (int), tag_and (int), tag_in (int), tag_not_in (int), tag_slug_and (string), tag_slug_in (string)
        public void AddSlug(string slug) => AddSlug(EWUPCategoryMethod.Multiple, slug);
        public void AddSlugs(string[] slug) => AddSlugs(EWUPCategoryMethod.Multiple, slug);
        public void AddSlugIn(string slug) => AddSlug(EWUPCategoryMethod.SlugIn, slug);
        public void AddSlugsIn(string[] slugs) => AddSlugs(EWUPCategoryMethod.SlugIn, slugs);
        public void AddSlugAnd(string slug) => AddSlug(EWUPCategoryMethod.SlugAnd, slug);
        public void AddSlugsAnd(string[] slugs) => AddSlugs(EWUPCategoryMethod.SlugAnd, slugs);

        public void AddId(int id) => AddInt(EWUPCategoryMethod.Multiple, id);
        public void AddIds(int[] ids) => AddInts(EWUPCategoryMethod.Multiple, ids);
        public void AddIdIn(int id) => AddInt(EWUPCategoryMethod.IdIn, id);
        public void AddIdsIn(int[] ids) => AddInts(EWUPCategoryMethod.IdIn, ids);
        public void AddIdAnd(int id) => AddInt(EWUPCategoryMethod.IdAnd, id);
        public void AddIdsAnd(int[] ids) => AddInts(EWUPCategoryMethod.IdAnd, ids);

        public void Exclude(int id) => ExcludeInt(id);
        public void ExcludeMultiple(int[] ids) => ExcludeInts(ids);
        public void Output(ref CMLData _data) => OutPut(ref _data, "tag_id", "tag__in", "tag__and", "tag__not_in", "tag", "tag_slug__in", "tag_slug__and");
    }
}