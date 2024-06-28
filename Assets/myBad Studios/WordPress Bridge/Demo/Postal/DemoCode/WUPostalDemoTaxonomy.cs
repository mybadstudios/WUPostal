using UnityEngine;
using System.Collections.Generic;
using MBS;

public partial class WUPostalDemo : MonoBehaviour {

    /// Display posts tagged with bob, under "people" custom taxonomy:
    public void FindBobUnderPeople()
    {
        //create the query object
        WUPostalQuery query = new WUPostalQuery();
        query.PostBasics.SpecifyPostType("post");

        //add a query based on taxonomy, then add the term(s)
        WUPTaxQueryEntry main = query.Taxonomy.Entries.AddEntry(WUPTaxQueryEntry.Generate("people", EWUPTaxTerm.slug), EWUPAndOr.AND);
        main.AddTerms(new List<string>() { "bob" });

        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    /// Display posts from multiple taxonomies
    public void DoMultipleTaxonomySearch()
    {
        //create the query object
        WUPostalQuery query = new WUPostalQuery();

        //add a query based on taxonomy. Set the relation to "first AND second"
        var main = query.Taxonomy.Entries.AddEntry( WUPTaxQueryEntry.Generate( "movie_genre", EWUPTaxTerm.slug ), EWUPAndOr.AND );
        main.AddTerms( new List<string>() { "action", "comedy" } );

        //add a second taxonomy to the query
        main = query.Taxonomy.Entries.AddEntry( WUPTaxQueryEntry.Generate( "actor", EWUPTaxTerm.term_id, _operator: EWUPTaxOperator.NOT_IN ));
        main.AddTerms( new List<int>() { 103, 115, 206 } );

        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    /// Display posts from one taxonomy or another
    public void DoSelectiveTaxonomySearch()
    {
        //create the query object
        WUPostalQuery query = new WUPostalQuery();

        //add a query based on taxonomy. Set the relation to "first OR second"
        var main = query.Taxonomy.Entries.AddEntry( WUPTaxQueryEntry.Generate( "category", EWUPTaxTerm.slug ), EWUPAndOr.OR );
        main.AddTerms( new List<string>() { "quotes" } );

        //add a second taxonomy to the query
        main = query.Taxonomy.Entries.AddEntry( WUPTaxQueryEntry.Generate( "post_format", EWUPTaxTerm.slug ) );
        main.AddTerms( new List<string>() { "post-format-quote" } );

        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }

    public void DoNestedTaxonomySearch()
	{
        //create the query object
        WUPostalQuery query = new WUPostalQuery();

        //add a query based on taxonomy to "query"
        var main = query.Taxonomy.Entries.AddEntry( WUPTaxQueryEntry.Generate( "category", EWUPTaxTerm.slug ), EWUPAndOr.OR );
        main.AddTerms( new List<string>() { "quotes" } );

        //taxonomy can be nested so generate nested taxonomies inside "query"
        var nested = query.Taxonomy.Entries.CreateNestedEntries();
        //then add the first queary based on taxonomy to the "nested" taxonomy
        var first_nested = nested.AddEntry( WUPTaxQueryEntry.Generate( "post_format", EWUPTaxTerm.slug ), EWUPAndOr.AND );
        first_nested.AddTerms(new List<string>() { "post-format-quote" } );

        //add the second taxonomy to the "nested" taxonomy
        var second_nested = nested.AddEntry( WUPTaxQueryEntry.Generate("category", EWUPTaxTerm.slug) );
        second_nested.AddTerms( new List<string>() { "wisdom" } );

        //Send to WordPress...
        WUPostal.FetchPosts( query.QueryString, PrintResponse, OnError );
    }
}
