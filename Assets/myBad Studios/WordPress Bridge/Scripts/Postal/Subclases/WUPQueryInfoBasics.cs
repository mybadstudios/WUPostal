//WordPress For Unity Bridge: Postal © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 

namespace MBS
{
    public class WUPQueryInfoBasics : WUPQueryInfo
    {
        bool IsPost { get; set; } = true;
        int PostId { get; set; } = -1;
        int PostParent { get; set; } = -1;
        int CommentCount { get; set; } = -1;
        string SearchTerm { get; set; } = string.Empty;
        string Slug { get; set; } = string.Empty;
        string Title { get; set; } = string.Empty;
        string Password { get; set; } = string.Empty;
        string Permission { get; set; } = string.Empty;
        string Orderby { get; set; } = string.Empty;
        bool? HasPassword { get; set; } = null;
        EWUPBasicCompareOperator CommentCountOperator { get; set; } = EWUPBasicCompareOperator.EQUALS;

        public void SpecifyPostId(int id)
        {
            IsPost = true;
            PostId = id;
        }

        public void SpecifyPageId(int id)
        {
            IsPost = false;
            PostId = id;
        }

        public void SpecifyCommentCount(int count, EWUPBasicCompareOperator count_operator = EWUPBasicCompareOperator.EQUALS)
        {
            CommentCount = count;
            CommentCountOperator = count_operator;
        }

        public void SpecifyPostPassword(string password) => Password = password.Trim();
        public void SpecifyPasswordRequired(bool required) => HasPassword = required;
        public void SpecifyPermission(string perm) => Permission = perm.Trim();

        public void SpecifySearchTerm(string keyword) => SearchTerm = keyword.Trim();
        public void SpecifyTitle(string title) => Title = title.Trim();

        public void SpecifyPostType(string slug) => AddSlug(EWUPCategoryMethod.Multiple, slug);
        public void SpecifyPostTypes(string[] slugs) => AddSlugs(EWUPCategoryMethod.Multiple, slugs);

        public void SpecifyPostNameIn(string post) => AddSlug(EWUPCategoryMethod.SlugIn, post);
        public void SpecifyPostNamesIn(string[] posts) => AddSlugs(EWUPCategoryMethod.SlugIn, posts);

        public void SpecifyPostStatus(EWUPPostalStatus status) => AddSlug(EWUPCategoryMethod.SlugAnd, status.ToString().Replace("_", ""));
        public void SpecifyPostStatuses(EWUPPostalStatus[] statuses)
        {
            foreach (var status in statuses)
                SpecifyPostStatus(status);
        }

        public void SpecifyPageName(string name)
        {
            IsPost = false;
            Slug = name;
        }

        public void SpecifyPostName(string name)
        {
            IsPost = true;
            Slug = name;
        }

        public void SpecifyPostIn(int post) => AddInt(EWUPCategoryMethod.Multiple, post);
        public void SpecifyPostsIn(int[] posts) => AddInts(EWUPCategoryMethod.Multiple, posts);

        public void SpecifyPostParent(int parent) => PostParent = parent;
        public void SpecifyPostParentIn(int parent) => AddInt(EWUPCategoryMethod.IdIn, parent);
        public void SpecifyPostParentsIn(int[] parents) => AddInts(EWUPCategoryMethod.IdIn, parents);

        public void ExcludePost(int post) => AddInt(EWUPCategoryMethod.IdAnd, post);
        public void ExcludePosts(int[] posts) => AddInts(EWUPCategoryMethod.IdAnd, posts);

        public void ExcludePostParent(int parent) => AddInt(EWUPCategoryMethod.IdExclude, parent);
        public void ExcludePostParents(int[] parents) => AddInts(EWUPCategoryMethod.IdExclude, parents);

        public void SpecifyOrderBy(EWUPOrderBy name, EWUPOrder order = EWUPOrder.DESC) => Orderby += $",{name}={order}";

        virtual public void Output(ref CMLData _data)
        {
            OutPut(ref _data, "post__in", "post_parent__in", "post__not_in", "post_parent__not_in", "post_type", "post_name__in", "post_status");

            if (PostParent > 0)
                _data.Seti("post_parent", PostParent);
            if (PostId > 0)
                _data.Seti(IsPost ? "p" : "page_id", PostId);

            if (CommentCount > 0)
            {
                var CommentOperators = new string[] { "=", "!=", ">", ">=", "<", "<=" };
                _data.Seti("comment_count", CommentCount);
                _data.Set("comment_operator", CommentOperators[(int)CommentCountOperator]);
            }

            if (SearchTerm != string.Empty)
                _data.Set("s", SearchTerm);

            if (Slug != string.Empty)
                _data.Set(IsPost ? "name" : "pagename", Slug);

            if (Title != string.Empty)
                _data.Set("title", Title);

            if (null != HasPassword)
                _data.Set("has_password", HasPassword.Value ? "true" : "false");

            if (Password != string.Empty)
                _data.Set("post_password", Password);

            if (Permission != string.Empty)
                _data.Set("perm", Permission);

            if (Orderby.Length > 1)
                _data.Set("orderby", Orderby.Substring(1));
        }
    }
}