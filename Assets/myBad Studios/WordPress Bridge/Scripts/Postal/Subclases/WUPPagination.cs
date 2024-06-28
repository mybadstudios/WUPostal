//WordPress For Unity Bridge: Postal © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 
namespace MBS
{
    public class WUPPagination
    {
        bool?
            nopaging = null,
            ignore_sticky_posts = null;

        int?
            posts_per_page = null,
            posts_per_archive_page = null,
            offset = null,
            paged = null,
            page = null;

        public void SpecifyUsePaging(bool use = true) => nopaging = use;
        public void IgnoreStickyPosts(bool ignore = true) => ignore_sticky_posts = ignore;
        public void SpecifyPostsPerPage(int count) => posts_per_page = count;
        public void SpecifyPostsPerArchivePage(int count) => posts_per_archive_page = count;
        public void SpecifyOffset(int count) => offset = count;
        public void SpecifyPage(int count) => page = count;
        public void SpecifyPaged(int count) => paged = count;

        public void Output(ref CMLData _data)
        {
            if (null != nopaging)
                _data.Set("nopaging", nopaging.Value.ToString());

            if (null != ignore_sticky_posts)
                _data.Set("ignore_sticky_posts", ignore_sticky_posts.Value.ToString());

            if (null != posts_per_page)
                _data.Seti("posts_per_page", posts_per_page.Value);

            if (null != posts_per_archive_page)
                _data.Seti("posts_per_archive_page", posts_per_archive_page.Value);

            if (null != offset)
                _data.Seti("offset", offset.Value);

            if (null != paged)
                _data.Seti("paged", paged.Value);

            if (null != page)
                _data.Seti("page", page.Value);
        }
    }
}