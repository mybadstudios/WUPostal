//WordPress For Unity Bridge: Postal © 2024 by Ryunosuke Jansen is licensed under CC BY-ND 4.0. 

namespace MBS
{
    using System.Collections.Generic;

    public class WUPMimeTypes {
        List<string> Mimes;

        public WUPMimeTypes() { Mimes = new List<string>(); }

        public void SpecifyType( string mime )
        {
            mime = mime.ToLower().Trim();
            if ( !Mimes.Contains( mime ) )
                Mimes.Add( mime );
        }

        public void SpecifyTypes( string [] mimes )
        {
            foreach ( string mime in mimes )
                SpecifyType( mime );
        }

        public void Output( ref CMLData _data )
        {
            if ( Mimes.Count > 0 )
                _data.Set( "post_mime_type", string.Join(",", Mimes) );
        }
    }
}