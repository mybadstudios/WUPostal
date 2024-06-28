using UnityEngine;
using MBS;

public partial class WUPostalDemo : MonoBehaviour {
    public void FindOnlyPasswordProtectedPosts()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifyPasswordRequired( true );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void FindOnlyPublicPosts()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifyPasswordRequired( false );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void FindAllPostsWithASpecficPassword()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifyPostPassword( "zxcvbn" );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }
}
