using UnityEngine;
using MBS;

public partial class WUPostalDemo : MonoBehaviour {
    public void OneTagByID()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Tags.AddId( 13 );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void OneTagBySlug()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Tags.AddSlug( "cooking" );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    /// Posts that have at least ONE of these tags by slug
    public void TagsFromSlugList()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Tags.AddSlugs( new string [] { "bread", "baking" } );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    /// Posts that have at least ONE of these tags by id
    public void TagsFromIdList()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Tags.AddIdsIn(new int[] { 37, 47 });
        WUPostal.FetchPosts(query.QueryString, PrintResponse, OnError);
    }

    /// Posts that have ALL these tags by slug
    public void MultipleTagsBySlug()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Tags.AddSlug( "bread+baking+recipe" );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    /// Posts that have ALL these tags by id
    public void MultipleTagsById()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Tags.AddIdsAnd( new int [] { 37, 47 } );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void ExcludesPostsWithTheseIds()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Tags.ExcludeMultiple( new int [] { 37,47 } );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }
}
