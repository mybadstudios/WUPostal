//WordPress For Unity Bridge: Data © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 

namespace MBS
{
    using UnityEngine;
    using System;
    using System.Globalization;
    
    public enum WUPostalAction		{FetchPosts, MakePost, SpecifyFeaturedImage, UploadFeaturedImage }

	static public class WUPostal {
        public enum EImageFormat { jpg, png }

		public const string filepath = "wub_postal/unity_functions.php";
		public const string ASSET = "POSTAL";

        static public void FetchPosts(CMLData data, Action<CML> onSuccess = null, Action<CMLData> onFail = null) =>
            WPServer.ContactServer(WUPostalAction.FetchPosts, filepath, ASSET, data, onSuccess, onFail);

        static public void MakePost(string title, string content, string excerpt, string posttype = "post", CMLData other = null, Action<CML> onSuccess = null, Action<CMLData> onFail = null)
        {
            var data =  other ?? new CMLData();
            data.Set("post_type", posttype);
            data.Set("post_title", Encoder.Base64Encode(title.Trim()));
            data.Set("post_content", Encoder.Base64Encode(content.Trim()));
            data.Set("post_excerpt", Encoder.Base64Encode(excerpt.Trim()));
            MakePost(data,onSuccess,onFail);
        }
        static public void MakePost(CMLData data, Action<CML> onSuccess = null, Action<CMLData> onFail = null) =>
            WPServer.ContactServer(WUPostalAction.MakePost, filepath, ASSET, data, onSuccess, onFail);

        static public void UploadFeaturedImage(int post_id, Texture2D image, string filename, EImageFormat extension, int uid = -1, Action<CML> onSuccess = null, Action<CMLData> onFail = null)
        {
            var content = extension == EImageFormat.jpg ? image.EncodeToJPG() : image.EncodeToPNG();

            if (uid < 0) uid = WULogin.UID;
            if (uid < 1)
            {
                var error = new CMLData();
                error.Set("message", "Invalid user selected");
                onFail?.Invoke(error);
                return;
            }
            var data = new CMLData();
            data.Seti("uid", uid);
            data.Seti("post_id", post_id);
            data.Set("filename", filename);
            data.Set("extension", extension.ToString());
            data.Set("content", Convert.ToBase64String(content));
            WPServer.ContactServer(WUPostalAction.UploadFeaturedImage, filepath, ASSET, data, onSuccess, onFail);
        }

        static public void SpecifyFeaturedImage(int post, int image, Action<CML> onSuccess = null, Action<CMLData> onFail = null)
        {
            CMLData data = new CMLData();
            data.ProcessCombinedFields($"post_id={post};image_id={image}");
            WPServer.ContactServer(WUPostalAction.SpecifyFeaturedImage, filepath, ASSET, data, onSuccess, onFail);
        }

        //SOME COMMON QUERIES PEOPLE MIGHT MAKE, PREMADE FOR CONVENIENCE
        static public void FetchPageById(int id, Action<CML> onSuccess = null, Action<CMLData> onFail = null)
        {
            WUPostalQuery query = new WUPostalQuery();
            query.PostBasics.SpecifyPageId(id);
            FetchPosts(query.QueryString, onSuccess, onFail);
        }

        static public void FetchPageBySlug(string slug, Action<CML> onSuccess = null, Action<CMLData> onFail = null)
        {
            WUPostalQuery query = new WUPostalQuery();
            query.PostBasics.SpecifyPageName(slug);
            FetchPosts(query.QueryString, onSuccess, onFail);
        }

        static public void FetchLastBlogPosts(int category, int count = 5, Action<CML> onSuccess = null, Action<CMLData> onFail = null)
        {
            WUPostalQuery query = new WUPostalQuery();
            query.Category.AddId(category);
            query.Pagination.SpecifyPostsPerPage(count);
            FetchPosts(query.QueryString, onSuccess, onFail);
        }

        static public void FetchLastBlogPostsForUser(int user, int category, int count = 5, Action<CML> onSuccess = null, Action<CMLData> onFail = null)
        {
            WUPostalQuery query = new WUPostalQuery();
            query.Category.AddId(category);
            query.Pagination.SpecifyPostsPerPage(count);
            query.Author.AddId(user);
            FetchPosts(query.QueryString, onSuccess, onFail);
        }

        static public void FindPostsMadeOn(int year, EWUPDateMonths month, EWUPDateDays day, Action<CML> onSuccess = null, Action<CMLData> onFail = null)
        {
            WUPostalQuery query = new WUPostalQuery();
            query.Date.SpecifyDate(year, month, d: day);
            FetchPosts(query.QueryString, onSuccess, onFail);
        }

        static public void FindPostsMadeThisWeek(Action<CML> onSuccess = null, Action<CMLData> onFail = null)
        {
            // Gets the Calendar instance associated with a CultureInfo.
            CultureInfo myCI = CultureInfo.CurrentCulture;
            Calendar myCal = myCI.Calendar;
            var Now = DateTime.Now;
            var year = Now.Year;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
            var week = myCal.GetWeekOfYear(Now, myCWR, myFirstDOW);
            //for some reason the week I get here is 1 more than it should be.
            //If you have this same problem, uncomment this line:
            //week--;

            WUPostalQuery query = new WUPostalQuery();
            query.Date.AddDateQuery(year, w:week);
            FetchPosts(query.QueryString, onSuccess, onFail);
        }

        static public void FindPostsMadeAfter(int year, EWUPDateMonths month, EWUPDateDays day, Action<CML> onSuccess = null, Action<CMLData> onFail = null)
        {
            WUPostalQuery query = new WUPostalQuery();
            query.Date.AddDateQuery().SetAfterDate(year, month, day);
            FetchPosts(query.QueryString, onSuccess, onFail);
        }

        static public void FindPostsMadeBetween(int year, EWUPDateMonths month, EWUPDateDays day, int to_year, EWUPDateMonths to_month, EWUPDateDays to_day, bool inclusive =true, Action<CML> onSuccess = null, Action<CMLData> onFail = null)
        {
            WUPostalQuery query = new WUPostalQuery();
            var dq = query.Date.AddDateQuery();
            dq.SetAfterDate(year, month, day);
            dq.SetBeforeDate(to_year, to_month, to_day);
            dq.inclusive = inclusive;
            FetchPosts(query.QueryString, onSuccess, onFail);
        }

    }
}
