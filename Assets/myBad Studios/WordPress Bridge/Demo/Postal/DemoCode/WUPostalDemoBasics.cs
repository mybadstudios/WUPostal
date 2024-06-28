using UnityEngine;
using MBS;

public partial class WUPostalDemo : MonoBehaviour {

    public void DoASearch()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifySearchTerm("keyword");
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void FindPostByID(int pid = 7)
    {
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifyPostId(pid);
        WUPostal.FetchPosts(query.QueryString, PrintResponse, OnError);
    }

    public void FindPageByID(int pid = 7)
    {
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifyPageId( pid );
        WUPostal.FetchPosts(query.QueryString, PrintResponse, OnError);
    }

    public void FindPostBySlug()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifyPostName( "about-my-life" );
        //or query.PostBasics.SpecifyPageName( "about-my-life" );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void FindChildViaSlugAndChild()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifyPageName( "contact_us/canada" );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void FindChildViaParentID()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifyPostParent( 93 );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void ShowOnlyTopLevelPages()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifyPostParent( 0 );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void PostsWithParenstInArray()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifyPostParentsIn( new int [] { 2, 5, 12, 14, 20 } );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void ShowSpecificPosts()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifyPostType( "page" );
        query.PostBasics.SpecifyPostsIn( new int [] { 2, 5, 12, 14, 20 } );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void DisplayAllPagesExceptSelected()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifyPostType( "page" );
        query.PostBasics.ExcludePosts( new int [] { 2, 5, 12, 14, 20 } );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

}
