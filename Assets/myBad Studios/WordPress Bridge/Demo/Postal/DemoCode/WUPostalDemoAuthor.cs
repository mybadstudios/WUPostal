using UnityEngine;
using MBS;

 public partial class WUPostalDemo : MonoBehaviour {
    public void OneAuthorByID(int id=123)
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Author.AddId( id );
        WUPostal.FetchPosts( query.QueryString, PrintResponse );
    }

    public void OneAuthorByNiceName( string name="rami")
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Author.SetNiceName( name );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void MultipleSpecificAuthorsByID()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Author.AddIds( new int [] { 2, 6, 17, 38 } );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void AllExcludingOneAuthor()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Author.Exclude( 12 );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void AllExcludingSpecificAuthors()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Author.ExcludeMultiple( new int [] { 2, 6 } );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }
}
