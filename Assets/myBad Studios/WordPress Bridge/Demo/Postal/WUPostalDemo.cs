using UnityEngine;
using MBS;

public partial class WUPostalDemo : MonoBehaviour, IPostalDemo
{
    /// <summary>
    /// Since the purpose of this asset is to fetch content from your WordPress website
    /// the demo functions won't work without you modifying them slightly to match the
    /// content on your website
    /// 
    /// See IPostalDemo for the list of demo functions prepared for you to experiment with
    /// Place any of the demo function names inside RunDemo to try it out after tweaking.
    /// Ideally, copy paste the function you want to test into the RunDemo function then
    /// modify the copy to work with your site's content and call the copied, modified function
    /// </summary>
    void RunDemo(CML _) {
        //Examples...        
        /*
        void FindPostWithID7()
        {
            WUPostalQuery query = new WUPostalQuery();
            query.PostBasics.SpecifyPostId(7);        
            WUPostal.FetchPosts(query.QueryString, PrintResponse, OnError);
        }
        FindPostWithId7();

        ...or 
        FindPostByID(7);
        FindPageByID(42);
        From9To5Weekdays();
        
        WUPostal.FindPostsMadeThisWeek();
        WUPostal.FindPostsMadeAfter(2021, EWUPDateMonths.Jan, EWUPDateDays._1, PrintResponse, OnError);
        WUPostal.FindPostsMadeOn(2021, EWUPDateMonths.Sep, EWUPDateDays._1, PrintResponse, OnError);
        WUPostal.FindPostsMadeBetween(2021, EWUPDateMonths.Aug, EWUPDateDays._25, 2021, EWUPDateMonths.Aug, EWUPDateDays._28, true, PrintResponse, OnError);
         */

    }

    void Start() => WULogin.OnLoggedIn += RunDemo;
    void PrintResponse( CML response ) => Debug.Log( response.ToString() );
    void OnError( CMLData response ) => Debug.Log( response.ToString() );

}
