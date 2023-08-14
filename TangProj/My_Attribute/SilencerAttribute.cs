using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Attribute
{
    public class SilencerAttribute : Attribute
    {
        public SilencerMode Mode { get; set; }
    }

    public enum SilencerMode
    {
        Normal,
        Strict
    }
}