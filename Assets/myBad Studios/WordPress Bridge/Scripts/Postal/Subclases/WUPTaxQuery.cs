//WordPress For Unity Bridge: Postal © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 

namespace MBS
{
    public class WUPTaxQuery
    {
        public WUPTaxQueryArray Entries { get; private set; } = new WUPTaxQueryArray();

        public void Output(ref CMLData _data)
        {
            if (Entries.EntriesCount == 0)
                return;

            var cml = "$tax_query_arg = " + Entries.ToString(0) + ";";
            _data.Set("tax_query", Encoder.Base64Encode( cml ) );
        }
    }   
}