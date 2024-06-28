//WordPress For Unity Bridge: Postal © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 

namespace MBS
{
    public enum EWUPAndOr { AND, OR }
    public enum EWUPOrder { DESC, ASC }
    public enum EWUPBasicCompareOperator { EQUALS, NOT_EQUALS, BIGGER_THAN, BIGGER_OR_EQUAL, LESS_THAN, LESS_THAN_OR_EQUAL }
    public enum EWUPCategoryMethod { None, IdIn, IdAnd, IdExclude, SlugIn, SlugAnd, Multiple }
    public enum EWUPDateCompareOperator { EQUALS, NOT_EQUALS, BIGGER_THAN, BIGGER_OR_EQUAL, LESS_THAN, LESS_THAN_OR_EQUAL, IN, NOT_IN, BETWEEN, NOT_BETWEEN }
    public enum EWUPDateDays { _1 = 1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20, _21, _22, _23, _24, _25, _26, _27, _28, _29, _30, _31 }
    public enum EWUPDateMonths { Jan = 1, Feb, March, Aprl, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec }
    public enum EWUPMetaCompare { EQUALS, NOT_EQUALS, LESS_THAN, LESS_THAN_OR_EQUAL, GREATER_THAN, GREATER_THAN_OR_EQUAN, LIKE, NOT_LIKE, IN, NOT_IN, BETWEEN, NOT_BETWEEN, NOT_EXISTS, REGEXP, NOT_REGEXP, RLIKE }
    public enum EWUPMetaQueryCompare { EQUALS, NOT_EQUALS, LESS_THAN, LESS_THAN_OR_EQUAL, GREATER_THAN, GREATER_THAN_OR_EQUAN, LIKE, NOT_LIKE, IN, NOT_IN, BETWEEN, NOT_BETWEEN, EXISTS, NOT_EXISTS }
    public enum EWUPMetaTypes { String, NUMERIC, BINARY, CHAR, DATE, DATETIME, DECIMAL, SIGNED, TIME, UNSIGNED }
    public enum EWUPOrderBy { none, ID, author, title, name, type, date, modified, parent, rand, comment_count, relevance, menu_order, meta_value, post__in, post_name__in, post_parent__in }
    public enum EWUPPostalStatus { publish, pending, draft, future, private_, trash, any }
    public enum EWUPTaxTerm { term_id, name, slug, term_taxonomy_id }
    public enum EWUPTaxOperator { IN, NOT_IN, AND, EXISTS, NOT_EXISTS }

    /// <summary>
    /// WARNING: PROPER USE OF THIS CLASS ASSUMES YOU HAVE A PROPER UNDERSTANDING OF THE WPQUERY OBJECT
    /// TO LEARN HOW TO USE THIS CLASS PROPERLY, USE THE FOLLOWING LINK AS A TUTORIAL
    /// https://developer.wordpress.org/reference/classes/wp_query/
    /// 
    /// This class contains (nearly ?) all the various parts of a WordPress WP_Query object
    /// When doing a query on the website one would normally create complex queries using either
    /// in-line parameters or by specifying the various parameters as separate variables first and
    /// then passing in those prepared variables when making the query.
    /// 
    /// This class works in much the same way in that it contains all the various parameters one might
    /// want to pass but as separate class members. To build up simple or complex queries, first create
    /// an instance of this class and then call the method(s) of it's respective members to populate
    /// each member with whatever values you require. Any members you don't need, simply leave them be.
    /// 
    /// See the demos for examples. To make life simpler the demos use the exact same queries as the
    /// sample code at the link above. See how they do it on the website then see how to do the same in Unity
    /// </summary>
    public class WUPostalQuery {
               
        bool? ReturnOnlyPostIds;

        public WUPQueryInfoBasics PostBasics { get; protected set; } = new WUPQueryInfoBasics();
        public WUPQueryInfoDate Date { get;protected set;  } = new WUPQueryInfoDate();

        public WUPQueryInfoAuthor Author { get; protected set; } = new WUPQueryInfoAuthor();
        public WUPQueryInfoCategory Category { get; protected set; } = new WUPQueryInfoCategory();
        public WUPQueryInfoTags Tags { get; protected set; } = new WUPQueryInfoTags();
        public WUPTaxQuery Taxonomy { get; protected set; } = new WUPTaxQuery();
        public WUPPagination Pagination { get; protected set; } = new WUPPagination();
        public WUPCaching Caching { get; protected set; } = new WUPCaching();
        public WUPMimeTypes MimeTypes { get; protected set; } = new WUPMimeTypes();

        CMLData _data;
    
        public WUPostalQuery(bool return_ids_only=false)
		{
            if ( return_ids_only )
                ReturnOnlyPostIds = true;
            else
                ReturnOnlyPostIds = null;
		}

        /// <summary>
        /// Join all the various specified query params together and return the
        /// properly formatted query parameters to be sent over to the server
        /// </summary>
        public CMLData QueryString
		{
            get
            {
                _data = new CMLData();
                if ( null != ReturnOnlyPostIds )
                    _data.Set("fields", "ids");

                PostBasics.Output( ref _data );
                Author.Output( ref _data );
                Category.Output( ref _data );
                Tags.Output( ref _data );
                Taxonomy.Output( ref _data );
                Pagination.Output( ref _data );
                Caching.Output( ref _data );
                MimeTypes.Output( ref _data );
                Date.Output( ref _data );

                return _data;
            }
        }

        /// <summary>
        /// Save the query for later re-use
        /// </summary>
        /// <param name="file_name">The PlayerPrefs string name to save queries to</param>
        /// <param name="query_name">A string to identify this query by</param>
        public void SaveQuery(string file_name, string query_name)
		{
            CML save_data = new CML();
            save_data.Load(file_name);
			CMLData query = save_data.GetFirstNodeOfType(query_name);
			if(null != query)
				save_data.RemoveCurrentNode();
			save_data.CopyNode( QueryString );
			save_data.Last.data_type = query_name;
			save_data.Save(file_name);
		}
	}
}