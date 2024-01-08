using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCore.Service.BonusQuery
{
    public interface IBonusQuery
    {
        Dictionary<string, object> DoBonusQuery([FromBody] Dictionary<string, object> value);
    }
}