using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient; // .net3.1使用SqlConnection是引用Microsoft.Data.SqlClient，但這邊卻是System.Data.SqlClient
using Dapper;
using DotnetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using DotnetCore.Service.BonusQuery;
using Newtonsoft.Json;
using System.Net.Http;
using DotnetCore.Service.ExecService;
using System.Net.Http.Headers;
using System.Web;
using System.IO;
using System.Net.Http.Json;

namespace DotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TangApiController : Controller
    {
        private List<Person> _person = new List<Person>();
        private readonly string _connectString = "Data Source=ggggggg.database.windows.net,1433;Initial Catalog=IRENT_V2T;Persist Security Info=True;User ID=hhhhh;Password=eeeee;Application Name=IRentWeb;";
        private readonly IBonusQuery _service;
        private readonly IExecService _service2;
        private HttpClient _client { get; set; }

        public TangApiController(IBonusQuery service, HttpClient client, IExecService service2)
        {
            Console.WriteLine("TangApiController");
            _service = service;
            _service2 = service2;
            _client = client;
            //_person.Clear();
            _person.Add(new Person { Name = "珮綺", Id = 0, Description = "很文靜" });
            _person.Add(new Person { Name = "邱俠", Id = 1, Description = "話很多" });
            _person.Add(new Person { Name = "阿舒", Id = 2, Description = "很活潑" });
            //_person.Add(new Person { Name = "Jerry", Id = 3, Description = "大好人" });
            //_person.Add(new Person { Name = "Adam", Id = 3, Description = "脾氣好" });
            //_person.Add(new Person { Name = "UI三妹", Id = 4, Description = "有吃的" });
            //_person.Add(new Person { Name = "Gordan哥", Id = 4, Description = "謎" });
        }
        #region 隱藏

        //呼叫此api要加上action name才行，不然就是最外層改成[Route("api/[controller]/[action]")]
        [Route("[Action]")]
        public IActionResult Index()
        {
            // return View(); // 這樣會錯
            return Ok("QQ"); // IActionResult的回傳類型似乎要用內建的方式回傳，單傳str會錯
        }

        [Route("[Action]")]
        public int Test()
        {
            return 80345;
        }

        // 加上[HttpGet]就不用在URL加上ACTION名稱，當用GET方式呼叫就會自動抓這裡，而METHOD名稱可任取，用Get只是習慣
        // [HttpGet]
        // public String Get()
        // {
        //     return "KOF XV";
        // }

        // 同時兩個method加上[HttpGet]會衝突，系統不知道要抓哪個，除非加上[Route("{id}")]多一層路由指定
        // [HttpGet]
        // public List<Person> GetList()
        // {
        //    return _person;
        // }

        [HttpGet]
        [Route("{id}")] //https://localhost:5002/api/TangApi/1
        public Person Get([FromRoute] int id) // [FromRoute]屬性來告訴 API 說 int id 這個參數是來自於 Route 上的，還有[FromQuery]、[FromBody]...
        {
            return _person.FirstOrDefault(person => person.Id == id);// 只取第一筆
        }

        [HttpGet]
        [Route("[Action]")]
        public List<Person> Get2() // [FromRoute]屬性來告訴 API 說 int id 這個參數是來自於 Route 上的，還有[FromQuery]、[FromBody]...
        {
            return _person;
        }

        // 資料塞入陣列
        [HttpPost]
        [Route("[Action]")]
        public IActionResult Insert([FromBody] PersonParameter parameter) //我們也不是每次都會回傳 IActionResult，可能也會是自訂的錯誤型別等等
        {
            _person.Add(new Person
            {
                Id = _person.Any()
                    ? _person.Max(card => card.Id) + 1
                    : 0, // 臨時防呆，如果沒東西就從 0 開始
                Name = parameter.Name,
                Description = parameter.Description
            });

            return Ok(parameter.Name);
        }

        [HttpPost]
        [Route("[Action]")]
        public IActionResult Insert55([FromBody] string parameter) //FromBody若要傳入多參數，就要用json格式
        {
            //body設定不要json，用一般文字
            return Ok(parameter);
        }

        // 資料塞入陣列
        [HttpPost]
        [Route("[Action]")] // 呼叫此api要加上action name才行
        public ReturnContent Insert2([FromBody] PersonParameter parameter) //和上面不同的是回傳型別我要自己定義
        {
            _person.Add(new Person
            {
                Id = _person.Any()
                    ? _person.Max(card => card.Id) + 1
                    : 0, // 臨時防呆，如果沒東西就從 0 開始
                Name = parameter.Name,
                Description = parameter.Description
            });

            ReturnContent results = new ReturnContent();
            results.Results = "成功啦";
            results.ErrorCode = "";
            results.ErrorMsg = "無";

            //return Ok();
            return results;
        }

        // ADO.NET連結資料庫測試
        [HttpPost]
        [Route("[Action]")]
        public string Irent([FromBody] IrentMember id)
        {
            #region SQL沒有回傳值
            // SqlConnection cn = new SqlConnection(_connectString);
            // SqlCommand cmd = new SqlCommand($"UPDATE TB_MemberData SET MEMADDR='55688' where MEMIDNO='A129425984'", cn);
            // cn.Open();
            // cmd.ExecuteNonQuery();
            // cn.Close();
            #endregion
            #region SQL傳回單一值(或是只要讀取傳回的第 1 筆資料的第 1 個欄位值)
            // SqlConnection cn = new SqlConnection(_connectString);
            // SqlCommand cmd = new SqlCommand($"select memtel from tb_memberdata where memidno='{id.ID}'", cn);
            // cn.Open();
            // string a = Convert.ToString(cmd.ExecuteScalar());
            // cn.Close();
            #endregion
            #region 呼叫無回傳值的sp
            // using (SqlConnection conn = new SqlConnection(_connectString))
            // {
            //    conn.Open();
            //    SqlTransaction tran;
            //    tran = conn.BeginTransaction();
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.Connection = conn;
            //    cmd.Transaction = tran;
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.CommandText = "SP_EdiBat_Q01_Test";
            //    cmd.Parameters.Add("@info", SqlDbType.VarChar, 20).Value = "100號5樓";
            //    cmd.ExecuteNonQuery();
            //    cmd.Parameters.Clear();
            //    tran.Commit();
            // }
            #endregion
            #region 呼叫無回傳值的sp
            // SqlConnection db = new SqlConnection(_connectString);
            // SqlCommand cmd = new SqlCommand("SP_EdiBat_Q01_Test", db);
            // cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.Add("@info", SqlDbType.VarChar, 20);
            // cmd.Parameters["@info"].Value = "100號5樓";
            // try
            // {
            //     db.Open();
            //     cmd.ExecuteNonQuery();
            // }
            // catch (Exception ex)
            // {
            //     throw ex.GetBaseException();
            // }
            // finally
            // {
            //     db.Close();
            // }
            #endregion
            #region sp有設定Output Parameter的方式回傳資料的情況
            // SqlConnection db = new SqlConnection(_connectString);
            // SqlCommand cmd = new SqlCommand("SP_EdiBat_Q01_Test", db);
            // cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.Add("@info", SqlDbType.VarChar, 20);
            // cmd.Parameters["@info"].Value = "0550";
            // SqlParameter retValParam = cmd.Parameters.Add("@MSG", SqlDbType.VarChar, 250);
            // retValParam.Direction = ParameterDirection.Output;
            // try
            // {
            //     db.Open();
            //     cmd.ExecuteNonQuery();
            //     Console.Write("取得的輸出資料: " + retValParam.Value);
            // }
            // catch (Exception ex)
            // {
            //     throw ex.GetBaseException();
            // }
            // finally
            // {
            //     db.Close();
            // }
            #endregion
            #region sp直接用 RETURN 語法回傳
            // SqlConnection db = new SqlConnection(_connectString);
            // SqlCommand cmd = new SqlCommand("SP_EdiBat_Q01_Test", db);
            // cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.Add("@info", SqlDbType.VarChar, 15);
            // cmd.Parameters["@info"].Value = "0550";
            // SqlParameter retValParam = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.VarChar, 250);
            // retValParam.Direction = ParameterDirection.ReturnValue;
            // try
            // {
            //     db.Open();
            //     cmd.ExecuteNonQuery();
            //     Console.Write("取得的輸出資料: " + retValParam.Value);
            // }
            // catch (Exception ex)
            // {
            //     throw ex.GetBaseException();
            // }
            // finally
            // {
            //     db.Close();
            // }
            #endregion
            #region SQL傳回SELECT表格資料型態，使用DataReader
            // SqlConnection db = new SqlConnection(_connectString);
            // SqlCommand cmd = new SqlCommand("SP_EdiBat_Q01_Test", db); //還有種做法是把sql語法定義在CommandText
            // cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.Add("@info", SqlDbType.VarChar, 15);
            // cmd.Parameters["@info"].Value = "0550";
            // db.Open();
            // SqlDataReader reader = cmd.ExecuteReader(); //SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow); // 只讀取第一個資料集，可結省資源及增進效率
            // if (reader.HasRows)
            // {
            //     while (reader.Read())
            //     {
            //         Console.WriteLine("Last Name：" + reader["LoveName"]);
            //         Console.WriteLine("First Name：" + reader["LoveCode"]);
            //         Console.WriteLine(reader[0] + " - " + reader[1]);
            //     }
            // }
            // cmd.Cancel();
            // reader.Close(); //先呼叫SqlCommand的.Cancel()方法在呼叫DataReader.Close()方法，否則會浪費資源喔
            // db.Close(); //db.Dispose();基本上同Close，一些class只提供Close，Close是為了那些不熟悉Dispose的開發者設計的
            #endregion
            #region SQL傳回SELECT表格資料型態，使用DataReader
            // SqlConnection conn = new SqlConnection(_connectString);
            // SqlCommand cmd = new SqlCommand();
            // try
            // {
            //     conn.Open();
            //     cmd.Connection = conn;
            //     cmd.CommandText = "SELECT top 10 LoveName,LoveCode from TB_LoveCode with(nolock);";
            //     cmd.CommandType = System.Data.CommandType.Text;
            //     // 绑定参数, 方式一
            //     // cmd.Parameters.AddWithValue("@Sex", "Male");
            //     // 绑定参数, 方式二
            //     //cmd.Parameters.Add("@Sex", SqlDbType.NVarChar);
            //     //cmd.Parameters["@Sex"].Value = "Male";
            //     SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // DataReader關閉的時候，也會自動關閉資料庫的連結
            //     if (reader.HasRows)
            //     {
            //         while (reader.Read())
            //         {
            //             Console.WriteLine(reader.GetString(0) + " - " + reader.GetString(1));
            //         }
            //     }
            // }
            // catch
            // {
            //     if (conn.State != ConnectionState.Closed)
            //     {
            //         conn.Close();
            //     }
            //     throw;
            // }
            #endregion
            #region DataAdaper 把資料填到DataSet
            // SqlConnection Conn = new SqlConnection(_connectString);
            // SqlDataAdapter da = new SqlDataAdapter(@"select top 5 * from tb_token where memidno IN('A129425984','A100052345');
            //                                                 select top 5 memidno,MEMTEL,MEMADDR from TB_MEMBERDATA where memidno IN('A129425984','A100052345') ", Conn);
            // DataSet ds = new DataSet(); //可存多個資料表，DataTable只能存一個
            // da.Fill(ds, "token");    //這時候執行SQL指令。取出資料，放進 DataSet。
            // da.Fill(ds, "MEMBERDATA");    //這時候執行SQL指令。取出資料，放進 DataSet。
            // Console.WriteLine(ds.Tables[0].Rows.Count);
            // Console.WriteLine(ds.Tables[1].Rows[0]["MEMADDR"].ToString());
            #endregion
            #region DataAdaper 把資料填到DataSet
            // SqlConnection cn = new SqlConnection(_connectString);
            // SqlCommand cmd = new SqlCommand();
            // cmd.CommandType = System.Data.CommandType.Text;
            // cmd.CommandTimeout = 300;
            // cmd.Connection = cn;
            // cmd.CommandText = "select top 5 memidno,MEMTEL,MEMADDR from TB_MEMBERDATA where memidno IN('A129425984','A100052345') ";
            // SqlDataAdapter da = new SqlDataAdapter(cmd); //SqlDataAdapter是 DataSet和 SQL Server之間的橋接器
            // DataSet ds = new DataSet();
            // da.Fill(ds);//把資料庫資料填入到DataSet
            // Console.WriteLine(ds.Tables[0].Rows[0]["MEMADDR"].ToString());
            #endregion
            #region DataAdaper(呼叫sp)
            // SqlConnection cn = new SqlConnection(_connectString);
            // SqlCommand cmd = new SqlCommand("SP_EdiBat_Q01_Test", cn);
            // cmd.CommandType = CommandType.StoredProcedure;
            // cmd.CommandTimeout = 300;
            // cmd.Parameters.Add("@info", SqlDbType.VarChar, 8).Value = 'Q';
            // SqlDataAdapter da = new SqlDataAdapter(cmd);
            // DataSet ds = new DataSet();
            // da.Fill(ds);

            // for (int i = 1; i < 10; i++)
            // {
            //     Console.WriteLine(ds.Tables[0].Rows[i]["LoveName"].ToString());
            // }
            #endregion
            #region 取得是否要寫LOG(舊版)
            // SqlConnection conn = new SqlConnection(_configuration["IRent"]);
            // SqlCommand cmd = new SqlCommand("usp_GetAPIList_Q1", conn);
            // cmd.CommandType = CommandType.StoredProcedure;
            // cmd.Parameters.Add("@APIName", SqlDbType.VarChar, 50);
            // cmd.Parameters["@APIName"].Value = funName;
            // SqlParameter retValParam = cmd.Parameters.Add("@MSG", SqlDbType.VarChar, 250);
            // retValParam.Direction = ParameterDirection.Output;
            // try
            // {
            //     conn.Open();
            //     SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // DataReader關閉的時候，也會自動關閉資料庫的連結
            //     if (reader.HasRows)
            //     {
            //         // 讀取第一張table的第一個欄位
            //         while (reader.Read())
            //         {
            //             Console.WriteLine(reader[0].ToString());
            //         }
            //         // 跳到下一個table
            //         reader.NextResult();
            //         // 讀取第二張table的第一個欄位
            //         while (reader.Read())
            //         {
            //             Console.WriteLine(reader[0].ToString());
            //         }
            //     }
            //     Console.Write("取得的輸出資料: " + retValParam.Value);
            // }
            // catch (Exception ex)
            // {
            //     conn.Close();
            //     throw ex.GetBaseException();
            // }
            #endregion

            return "s";
        }

        // Dapper連結資料庫測試
        [HttpGet]
        public IEnumerable<Db_Output> TestDapper([FromQuery] string tel)
        {
            if (tel == "" || tel == null)
            {
                tel = "0928250058";
            }
            using (var conn = new SqlConnection(_connectString))
            {
                //除了 Query 之外，Dapper 還準備了一些針對不同查詢的方法給大家使用
                var result = conn.Query<Db_Output>($"SELECT MEMCNAME,MEMIDNO FROM tb_memberdata WHERE MEMTEL='{tel}'");
                return result;
            }
        }

        // API文冰哥式寫法
        [HttpPost]
        [Route("[Action]")]
        public IActionResult Post(Dictionary<string, object> value)
        {
            string a = JsonConvert.SerializeObject(_service.DoBonusQuery(value));
            return Ok(JsonConvert.SerializeObject(_service.DoBonusQuery(value)));
        }

        // API上擎式寫法
        //[EnableCors(origins: "*", headers: "*", methods: "*")] //會錯，應該是.net core不支援了
        //[ResponseType(typeof(queryOrder_Params))]
        //[SwaggerResponse(HttpStatusCode.OK, Type = typeof(ResultDTO<queryOrderDTO>))]
        [HttpPost]
        public HttpResponseMessage QueryLoveCode(loveCode_Params Param)
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    //JsonConvert.SerializeObject(ExecService.reflashToken(Param)), //reflashToken不是static的，不能直接用class名去呼叫，白癡
                    JsonConvert.SerializeObject(_service2.loveCodeList(Param)),
                    System.Text.Encoding.UTF8,
                    "application/json"
                    )
            };
        }
        #endregion 

        // 呼叫API : HttpClient
        [Route("[Action]")]
        public Task<string> TestHttpClientGet()
        {
            return Run();
            async Task<string> Run()
            {
                try
                {
                    _client.Timeout = TimeSpan.FromSeconds(30); //設定 Timeout 屬性，避免資源浪費
                    HttpResponseMessage response = await _client.GetAsync("https://irentcar-app-test.azurefd.net/api/CityList/"); //透過 HttpClient GetAysnc 方法發送非同步請求
                    //HttpResponseMessage response = _client.GetAsync("https://irentcar-app-test.azurefd.net/api/CityList/").GetAwaiter().GetResult(); //同上
                    // HttpResponseMessage response = _client.GetAsync("https://irentcar-app-test.azurefd.net/api/CityList/").Result; // 也行，差別是啥？
                    Console.Write(response.EnsureSuccessStatusCode()); //如果 Status Code 不為 2xx，會丟出HttpRequestException，會進到 Catch 處理寫 error log message
                    string responseBody = await response.Content.ReadAsStringAsync(); //非同步的方式取得 HTTP Response 的 Content

                    //GetStringAsync回傳的是string，直接取代上面寫法，但可能取不到header相關訊息吧?
                    //string responseBody2 = await _client.GetStringAsync("https://irentcar-app-test.azurefd.net/api/CityList/");

                    return responseBody;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                    return "No!!!";
                }
            }
        }

        // 呼叫API : HttpClient
        [Route("[Action]")]
        public string TestHttpClientGet2()
        {
            try
            {
                // 自己宣告HttpClient，不用注入的方式
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30); //設定 Timeout 屬性，避免資源浪費
                    client.BaseAddress = new Uri("https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-D0047-061");
                    // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["Map8ApiKey"]);
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string directionUrl = "?Authorization=CWB-A0B581C8-BDD4-4809-A61D-DD7FB8DD8105";

                    directionUrl += "&limit=1";
                    directionUrl += "&locationName=" + HttpUtility.UrlEncode("內湖區");
                    directionUrl += "&elementName=Wx,PoP12h,T";
                    HttpResponseMessage response = client.GetAsync(directionUrl).Result;
                    response.EnsureSuccessStatusCode(); //如果 Status Code 不為 2xx，會丟出HttpRequestException，會進到 Catch 處理寫 error log message
                    Stream responseBody = response.Content.ReadAsStreamAsync().Result;
                    StreamReader sr = new StreamReader(responseBody);
                    string response1 = sr.ReadToEnd();
                    string response2 = response.Content.ReadAsStringAsync().Result; //直接這樣也可

                    // 這段是Jerry寫的，但直接印在瀏覽器畫面上會全變空的，不懂
                    //JObject reader = JsonConvert.DeserializeObject<JObject>(responseBody);
                    //return (new ResultDTO<JObject>
                    //{
                    //    Message = "",
                    //    Data = reader,
                    //    Result = reader["success"].ToString().Trim() == "true" ? true : false
                    //});

                    return response1;
                }
            }
            catch (Exception ex)
            {
                return "Oh,Shit!";
                //return (new ResultDTO<JObject>
                //{
                //    Message = "FuckingError:" + ex.Message,
                //    Data = { },
                //    Result = false
                //});
            }
        }

        // 呼叫API : HttpClient
        [Route("[Action]")]
        public async Task TestHttpClientGet3()
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30); //設定 Timeout 屬性，避免資源浪費
                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "https://irentcar-app-test.azurefd.net/api/CityList/");
                await client.SendAsync(httpRequest) //透過 SendAysnc 方法將 HttpRequestMessage 內容作為參數發送非同步請求
                .ContinueWith(responseTask => //任務完成後要做的事情，類似 Javascript 中使用 Promise 處理非同步的寫法
                {
                    Console.WriteLine("Response: {0}", responseTask.Result);//印出response的header資訊
                });
                // HttpResponseMessage resp = await client.SendAsync(httpRequest);
                // string response = resp.Content.ReadAsStringAsync().Result;
                // Console.WriteLine(response);
            }
        }

        [Route("[Action]")]
        public void CallPushService()
        {
            _client.Timeout = TimeSpan.FromSeconds(30); //設定 Timeout 屬性，避免資源浪費
            // TakeiRentToken param = new TakeiRentToken();
            // param.limit = 5;
            PushContent param = new PushContent()
            {
                Msg = "test",
                Title = "test2",
                Url = ""
            };
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(param));
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage resp = _client.PostAsync("http://localhost:5160/api/Push/Post", httpContent).Result;
            Stream rsp = resp.Content.ReadAsStreamAsync().Result; //非同步的方式取得 HTTP Response 的 Content
            StreamReader sr = new StreamReader(rsp);
            string response = sr.ReadToEnd();
        }

        // 呼叫API : HttpClient
        [Route("[Action]")]
        public async Task<string> TestHttpClientPost()
        {
            using (HttpClient client = new HttpClient())
            {
                // 作法一
                // 設定Header - Accept的資料型別
                // client.DefaultRequestHeaders.Accept.Clear();
                // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // TakeiRentToken param = new TakeiRentToken();
                // param.limit = 5;
                // HttpResponseMessage resp = await client.PostAsJsonAsync("https://irentcar-app-test.azurefd.net/api/LoveCodeList", param);
                // Stream rsp = resp.Content.ReadAsStreamAsync().Result;
                // StreamReader sr = new StreamReader(rsp);
                // string response = sr.ReadToEnd();

                // 作法二
                TakeiRentToken param = new TakeiRentToken();
                param.limit = 5;
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(param));
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage resp = await client.PostAsync("https://irentcar-app-test.azurefd.net/api/LoveCodeList", httpContent);
                Stream rsp = resp.Content.ReadAsStreamAsync().Result; //非同步的方式取得 HTTP Response 的 Content
                StreamReader sr = new StreamReader(rsp);
                string response = sr.ReadToEnd();

                return response;
            }
        }

        // 呼叫API : HttpClient
        [Route("[Action]")]
        public string TestHttpClientPost2()
        {
            TakeiRentToken s = new TakeiRentToken()
            {
                limit = 5
            };
            string postData = JsonConvert.SerializeObject(s);
            //string postData = "{\"limit\":\"5\"}";

            string url = "https://irentcar-app-test.azurefd.net/api/LoveCodeList";
            string statusCode = "";
            string result = string.Empty;
            //設置Http的正文
            HttpContent httpContent = new StringContent(postData);
            //設置Http的內容標頭
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //設置Http的內容標頭的字符
            httpContent.Headers.ContentType.CharSet = "utf-8";
            using (HttpClient client = new HttpClient())
            {
                //異步Post
                HttpResponseMessage response = client.PostAsync(url, httpContent).Result;
                //輸出Http響應狀態碼
                statusCode = response.StatusCode.ToString();
                //確保Http響應成功
                if (response.IsSuccessStatusCode)
                {
                    //異步讀取json
                    result = response.Content.ReadAsStringAsync().Result;
                }
            }
            return result;
        }

        // 呼叫API : HttpClient 泛型，有錯，不知為何，應該是泛型語法問題
        [Route("[Action]")]
        public T TestHttpClientPost3<T>()
        {
            T result = default(T); //宣告T型別的result，給個初始預設值
            string postData = "{\"limit\":\"5\"}";
            string url = "https://irentcar-app-test.azurefd.net/api/LoveCodeList";

            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            httpContent.Headers.ContentType.CharSet = "utf-8";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;
                    //Newtonsoft.Json
                    string json = JsonConvert.DeserializeObject(s).ToString();
                    result = JsonConvert.DeserializeObject<T>(json);
                }
            }
            return result;
        }

        // 呼叫API : HttpClient 黑大建議寫法，詳見HttpClientTestController，因為寫法較不一樣

        // 收發含 Cookie 的 HTTP Request(黑大範例)
        // [Route("[Action]")]
        // public string TestHttpClientPost4()
        // {
        //     var handler = new HttpClientHandler();
        //     var url = "http://localhost/aspnet/CookieTest/AutoRedirect.aspx";
        //     handler.CookieContainer.Add(new System.Net.Cookie
        //     {
        //         Name = "ReqId",
        //         Value = DateTime.Now.ToString("mm:ss.fff"),
        //         Domain = new Uri(url).Host
        //     });
        //     var client = new HttpClient(handler);
        //     var response = await client.GetAsync(url);
        //     Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        //     Console.WriteLine($"RespId=" + handler.CookieContainer.GetCookies(new Uri(url))["RespId"]?.Value);
        // }

        // 呼叫API : HttpClient (.Net Core專用)


        // 呼叫API : RestClient，好像滿推?!


        // 呼叫API : RestSharp，NuGet 第三方套件


        //呼叫FTP，HttpClient不支援


        // 呼叫API : HttpWebRequest，不推薦使用


        // 預設有的，但不知在幹嘛
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }

    public class TakeiRentToken
    {
        //public string IDNO { get; set; }
        //public string RefrashToken { get; set; }
        //public string DeviceID { get; set; }
        //public string APP { get; set; }
        //public string APPVersion { get; set; }
        //public string PushREGID { get; set; }
        public int limit { get; set; }
    }
    public class PushContent
    {
        public string Msg { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}