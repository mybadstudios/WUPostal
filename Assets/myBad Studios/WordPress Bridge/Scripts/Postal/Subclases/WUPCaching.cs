//WordPress For Unity Bridge: Postal © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 

namespace MBS
{
    public class WUPCaching
    {
        bool?
            cache_results = null,
            update_post_meta_cache = null,
            update_post_term_cache = null;

        public void SetCacheResults( bool use = true ) => cache_results = use;
        public void SetUpdatePostMetaCache( bool update = true ) => update_post_meta_cache = update;
        public void SetUpdatePostTermCache( bool update = true ) => update_post_term_cache = update;

        public void Output( ref CMLData _data )
        {
            if ( null != cache_results )
                _data.Set( "cache_results", cache_results.Value.ToString() );

            if ( null != update_post_meta_cache )
                _data.Set( "update_post_meta_cache", update_post_meta_cache.Value.ToString() );

            if ( null != update_post_term_cache )
                _data.Set( "update_post_term_cache", update_post_term_cache.Value.ToString() );

        }
    }

}