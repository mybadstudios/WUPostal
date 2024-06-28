//WordPress For Unity Bridge: Postal © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 

namespace MBS
{
    using System.Collections.Generic;

    public class WUPTaxQueryArray : WUPNestedQueryArray<WUPTaxQueryEntry> { }
    public class WUPTaxQueryEntry : CMLDataBase
    {
        List<string> terms;
        public string Terms { get {
                if (terms.Count == 0) return string.Empty;
                
                var result = "array(";
                var numbers = long.TryParse(terms[0], out long _);
                if (terms.Count == 1) 
                    return result + (numbers ? $"{terms[0]})" : $"'{terms[0]}')") ;

                var separator = numbers ? "," : "','";
                var combined = string.Join(separator, terms);
                var ends = numbers ? string.Empty : "'";
                result += $"{ends}{combined}{ends})";
                return result;
            } 
        }

        public WUPTaxQueryArray QueriesArray { get; private set; } = null;

        static public WUPTaxQueryEntry Generate(string _taxonomy, EWUPTaxTerm _field = EWUPTaxTerm.term_id, bool _include_children = true, EWUPTaxOperator _operator = EWUPTaxOperator.IN)
        {
            WUPTaxQueryEntry result = new WUPTaxQueryEntry();
            result.Set("taxonomy", _taxonomy);

            if (_field != EWUPTaxTerm.term_id)
                result.Set("field", _field.ToString());

            if (_operator != EWUPTaxOperator.IN)
                result.Set("operator", _operator.ToString());

            if (!_include_children)
                result.Set("children", "false");
            result.terms = new List<string>();
            result.data_type = "entry";
            return result;
        }

        public void AddTerms(List<int> values)
        {
            foreach (var value in values)
                terms.Add(value.ToString());
        }

        public void AddTerms(List<string> values)
        {
            foreach (var value in values)
                terms.Add(value);
        }

        public WUPTaxQueryArray AddTermsArray()
        {
            QueriesArray = new WUPTaxQueryArray();
            return QueriesArray;
        }

        override public string ToString()
        {
            string response;

            if (terms.Count > 0)
                Set("terms", Terms);

            if (QueriesArray?.EntriesCount > 0)
                response = QueriesArray.ToString();
            else
            {
                bool isnumber = long.TryParse(defined[Keys[0]], out long _); 
                response = "array(";
                foreach (var d in defined)
                    if (d.Key != "id")
                    {
                        response += $"'{d.Key}' => " + (isnumber ? d.Value : $"'{d.Value}',");
                    }
                response += ")";
            }            
            return response;
        }
    }
}