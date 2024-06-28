//WordPress For Unity Bridge: Postal © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 

namespace MBS
{
    public class WUPQueryInfoCategory : WUPQueryInfo
    {
        //Only supports cat(int), categorn_name(string), category_and (int), category_in (int), ### category_not_in (int) --- Not used ###
        public void AddSlug( string slug ) => AddSlug( EWUPCategoryMethod.Multiple, slug );
        public void AddSlugs( string [] slug ) => AddSlugs( EWUPCategoryMethod.Multiple, slug );

        public void AddId( int id ) => AddInt( EWUPCategoryMethod.Multiple, id );
        public void AddIdIn( int id ) => AddInt( EWUPCategoryMethod.IdIn, id );
        public void AddIdAnd( int id ) => AddInt( EWUPCategoryMethod.IdAnd, id );
        public void AddIds( int [] ids ) => AddInts( EWUPCategoryMethod.Multiple, ids );
        public void AddIdsIn( int [] ids ) => AddInts( EWUPCategoryMethod.IdIn, ids );
        public void AddIdsAnd( int [] ids ) => AddInts( EWUPCategoryMethod.IdAnd, ids );

        //you can use either cat=-1,-2,-3 OR cat__not_in (1,2,3) by using the "Not" version below
        public void Exclude( int id ) => AddId( -id );
        public void ExcludeNot( int id ) => Exclude( id );
        public void ExcludeMultiple( int [] ids ) { foreach ( int id in ids ) Exclude( id ); }
        public void ExcludeMultipleNot( int [] ids ) => ExcludeMultiple( ids );

        public void Output( ref CMLData _data ) => OutPut( ref _data, "cat", "category__in", "category__and", "category__not_in", "category_name" );
    }
}