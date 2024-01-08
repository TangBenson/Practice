using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestFulController : ControllerBase
    {
        // 這裡專門用來搞清楚有無[HttpGet]等相關差別
        // 宣告private會導致API無法被呼叫
        // 沒加Attribute或加[HttpGet]的method只能有一個，不然會錯，除非指定Route要抓到action
        
        public int Aa()
        {
            return 99;
        }

        [Route("[Action]")]
        public int Bb()
        {
            return 88;
        }

        [HttpPost]
        public int Cc([FromBody] test api_in) //[FromBody] 不加也可以，那必要性是?
        {
            //string c = JsonConvert.SerializeObject(api_in);
            //test cc = JsonConvert.DeserializeObject<test>(c);
            //return cc.c;

            // 一行等同上面三行，所以Json傳進來直接就可以轉成物件了喔
            return api_in.c;
        }

        //[HttpPost]
        //public int Dd(Dictionary<string, object> value)
        //{
        //    Console.WriteLine(value.GetType());//System.Collections.Generic.Dictionary`2[System.String,System.Object]
        //    test c = new test();
        //    c.c = 5;
        //    Console.WriteLine(c.GetType()); //DotnetCore.Controllers.test
        //    string a = "asdsad";
        //    Console.WriteLine(a.GetType()); //System.String
        //    return 5;
        //}

    }

    // 這邊宣告成private會語法錯誤，why?
    public class test{
        public int c { get; set; }
    }
}
