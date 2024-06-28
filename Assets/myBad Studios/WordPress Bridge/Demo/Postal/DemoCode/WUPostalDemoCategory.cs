using UnityEngine;
using MBS;

public partial class WUPostalDemo : MonoBehaviour {

    public void OneCategoryAndChildrenById()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Category.AddId( 4 );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void OneCategoryAndChildrenBySlug()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Category.AddSlug( "staff" );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void OneCategoryById()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Category.AddIdIn( 4 );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    /// Returns posts from multiple categories
    public void MultipleCategories()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Category.AddIds( new int [] { 2, 6, 17, 38 } );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void MultipleCategoriesBySlug()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Category.AddSlug( "staff,news" );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void HasAllCategoriesBySlug()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Category.AddSlug( "staff+news" );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    /// Exclude the following categories from the search results
    public void ExcludeCategories()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Category.ExcludeMultiple( new int [] { 12, 34, 56 } );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    /// Returns posts that are in ALL the included categories
    public void HasMultipleCategories()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Category.AddIdsAnd( new int [] { 2, 6 } );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    /// Returns posts that are in ANY of the selected categories
    public void OneOfSeveralCategories()
    {
        WUPostalQuery query = new WUPostalQuery();
        query.Category.AddIdsIn( new int [] { 2, 6, 17, 38 } );
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }
}
