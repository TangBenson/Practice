using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Attribute
{
    [Silencer(Mode = SilencerMode.Normal)]
    public class InternalBulletin : Bulletin
    {
        public InternalBulletin()
        {
            Description = "內部";
        }

        public string InternalProperty { get; set; }
    }
}