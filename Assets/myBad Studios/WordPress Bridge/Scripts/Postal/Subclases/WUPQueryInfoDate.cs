//WordPress For Unity Bridge: Postal © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 

namespace MBS
{
    using System.Collections.Generic;

    public class WUPYMD {
        readonly int? Year = null;
        readonly EWUPDateMonths? Month = null;
        readonly EWUPDateDays? Day = null;

        public WUPYMD(int? y = null, EWUPDateMonths? m = null, EWUPDateDays? d = null)
        {
            Year = y;
            Month = m;
            Day = d;
        }

        public override string ToString()
        {
            string output = "array(";
            if (null != Year) output += $"'year'=>{Year},";
            if (null != Month) output += $"'month'=>{(int)Month},";
            if (null != Day) output += $"'day'=>{(int)Day},";
            output += ")";
            if (output == "array()") output = string.Empty;
            return output;
        }
    }
    public class WUPDateQueryElement
    {
        public int? Year = null;
        public EWUPDateMonths? Month = null;
        public int? Week;// Week of the year(from 0 to 53).
        public EWUPDateDays? Day = null;
        public int? Hour = null;// from 0 to 23
        public int? Minute = null;//from 0 to 59
        public int? Second = null; // 0 to 59
        public EWUPDateCompareOperator? Compare = null;
        public string Column = "post_date";
        public EWUPAndOr? relation = EWUPAndOr.AND;
        public bool? inclusive;//For after/before, whether exact value should be matched or not

        WUPYMD after = null;
        WUPYMD before = null;
        int[] DaysOfTheWeek = null;

        public void SetBeforeDate(int year, EWUPDateMonths month, EWUPDateDays day) => before = new WUPYMD(year, month, day);
        public void SetAfterDate(int year, EWUPDateMonths month, EWUPDateDays day) => after = new WUPYMD(year, month, day);
        public void SetDaysOfWeek(int[] daysFrom0To6) => DaysOfTheWeek = daysFrom0To6;

        override public string ToString()
        {
            var CommentOperators = new string[] { "=", "!=", ">", ">=", "<", "<=", "IN", "NOT IN", "BETWEEN", "NOT BETWEEN" };
            var output = $"array('column'=>'{Column}',";
            if (null != Year) output += $"'year'=>{Year},";
            if (null != Month) output += $"'month'=>{(int)Month},";
            if (null != Week) output += $"'week'=>{Week},";
            if (null != Day) output += $"'day'=>{(int)Day},";
            if (null != Hour) output += $"'hour'=>{Hour},";
            if (null != Minute) output += $"'minute'=>{Minute},";
            if (null != Second) output += $"'second'=>{Second},";
            if (null != Compare) output += $"'compare'=>'{CommentOperators[(int)Compare]}',";
            if (null != relation) output += $"'relation'=>'{relation}',";
            if (null != DaysOfTheWeek) output += $"'dayofweek'=>array({string.Join(",", DaysOfTheWeek)}),";
            if (null != before && before.ToString() != string.Empty) output += $"'before'=>{before},";
            if (null != after && after.ToString() != string.Empty) output += $"'after'=>{after},";
            if (null != inclusive && (null != after || null != before)) output += $"'inclusive'=>{inclusive},";
            output += "),";

            return output;
        }
    }
    public class WUPQueryInfoDate
    {
        public int? Year = null;
        public EWUPDateMonths? Month = null;
        public int? Week;// Week of the year(from 0 to 53).
        public EWUPDateDays? Day = null;
        public int? Hour = null;// from 0 to 23
        public int? Minute = null;//from 0 to 59
        public int? Second = null; // 0 to 59
        
        int? Yearmonth = null; // ie. 202105
        List<WUPDateQueryElement> DateQuery = null;

        public WUPQueryInfoDate(int? y = null, EWUPDateMonths? m = null, int? w = null, EWUPDateDays? d = null, int? hour = null, int? minutes = null, int? seconds = null)
        { 
            Year = y;
            Month = m;
            Week = w;   
            Day = d;
            Hour = hour;
            Minute = minutes;
            Second = seconds;
        }

        public void SpecifyYearMonth(int year, EWUPDateMonths month) => Yearmonth = (year * 100) + (int)month;
        public void SpecifyYearMonth(int year_and_month) => Yearmonth = year_and_month;

        public void SpecifyDate(int? y = null, EWUPDateMonths? m = null, int? w = null, EWUPDateDays? d = null, int? hour = null, int? minutes = null, int? seconds = null)
        {
            Year = y;
            Month = m;
            Week = w;
            Day = d;
            Hour = hour;
            Minute = minutes;
            Second = seconds;
        }

        public WUPDateQueryElement AddDateQuery(int? y = null, EWUPDateMonths? m = null, int? w = null, EWUPDateDays? d = null, int? hour = null, int? minutes = null, int? seconds = null)
        {
            if (null == DateQuery)
                DateQuery = new List<WUPDateQueryElement>();

            var q = new WUPDateQueryElement()
            {
                Year = y,
                Month = m,
                Week = w,
                Day = d,
                Hour = hour,
                Minute = minutes,
                Second = seconds,
            };
            DateQuery.Add(q);
            return q;
        }

        virtual public void Output(ref CMLData _data)
        {
            if (null != Year) _data.Seti("year", Year.Value);
            if (null != Week) _data.Seti("w", Week.Value);
            if (null != Hour) _data.Seti("hour", Hour.Value);
            if (null != Minute) _data.Seti("minute", Minute.Value);
            if (null != Second) _data.Seti("second", Second.Value);
            if (null != Yearmonth) _data.Seti("m", Yearmonth.Value);

            if (null != Month) _data.Seti("monthnum", (int)Month.Value);
            if (null != Day) _data.Seti("day", (int)Day.Value);

            if (null != DateQuery && DateQuery.Count > 0)
            {
                var datequery_string = "array(";
                foreach (var item in DateQuery)
                    datequery_string += item.ToString();
                datequery_string += "),";
                _data.Set("date_query", Encoder.Base64Encode(datequery_string));
            }
        }
    }
}