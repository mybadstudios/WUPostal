using UnityEngine;
using MBS;

public partial class WUPostalDemo : MonoBehaviour {

    public void From9To5Weekdays()
    {
        WUPostalQuery query = new WUPostalQuery();
        var a = query.Date.AddDateQuery(hour: 9);
        a.Compare = EWUPDateCompareOperator.BIGGER_OR_EQUAL;

        var b = query.Date.AddDateQuery(hour: 17);
        b.Compare = EWUPDateCompareOperator.LESS_THAN_OR_EQUAL;

        var c = query.Date.AddDateQuery();
        c.Compare = EWUPDateCompareOperator.BETWEEN;
        c.SetDaysOfWeek(new int[] { 2, 6 });

        WUPostal.FetchPosts(query.QueryString, PrintResponse, OnError);
    }

}
