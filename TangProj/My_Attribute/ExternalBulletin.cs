using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Attribute
{
    [Silencer(Mode = SilencerMode.Strict)]
    public class ExternalBulletin : Bulletin
    {
        public ExternalBulletin()
        {
            Description = "外部";
        }

        public string ExternalProperty { get; set; }
    }
}