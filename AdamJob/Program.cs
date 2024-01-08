using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using Dapper;
using Newtonsoft.Json;

namespace AdamJob
{
    class Program
    {
        private static readonly string _connectString = "Data Source=wwwww,1433;Initial Catalog=LS;User ID=xxxxx;Password=aaaaa;";

        static void Main(string[] args)
        {
            // SqlConnection cn = new SqlConnection(_connectString);
            // SqlCommand cmd = new SqlCommand($"UPDATE Customers SET PhoneNo='0099999' where Id=1 ", cn);
            // cn.Open();
            // cmd.ExecuteNonQuery();
            // cn.Close();

            //Dapper：Query Model，查詢回來可以立刻Bind至Model裡享受強型別的好處
            // List<MyModel> results = null;
            // using (SqlConnection conn = new SqlConnection(_connectString)) //C# 8.0 using 語句可不加大括弧
            // {
            //     string strSql = "Select * from Customers";
            //     results = conn.Query<MyModel>(strSql).ToList();
            //     foreach (MyModel i in results){
            //         Console.WriteLine(i.Name);
            //     }
            // }

            //Dapper：Query Anonymous，不宣告強型別也能順利接回來
            // using (SqlConnection conn = new SqlConnection(_connectString))
            // {
            //     string strSql = "Select * from Customers";
            //     var results = conn.Query(strSql).ToList();
            //     foreach (var i in results)
            //     {
            //         Console.WriteLine(i.PhoneNo);
            //     }
            // }

            //Dapper：QueryFirst，如同Lambda的First()，會將符合條件的第一筆回傳回來，如果沒有符合會拋出錯誤
            //        QueryFirstOrDefault，如同Lambda的FirstOrDefault()，會將符合條件的第一筆回傳回來，如果沒有符合回傳null
            //        QuerySingle，如同Lambda的Single()，查詢唯一符合條件的資料，如果沒有符合或符合條件為多筆時會拋出錯誤
            //        QuerySingleOrDefault，如同Lambda的SingleOrDefault()，查詢唯一符合條件的資料，如果沒有符合回傳null，但如果符合條件為多筆時會拋出錯誤
            // MyModel results = null;
            // using (SqlConnection conn = new SqlConnection(_connectString))
            // {
            //     string strSql = "Select * from Customers";
            //     results = conn.QueryFirst<MyModel>(strSql);
            //    // results = conn.QueryFirstOrDefault<MyModel>(strSql);
            //    // results = conn.QuerySingle<MyModel>(strSql);
            //    // results = conn.QuerySingleOrDefault<MyModel>(strSql);
            //     Console.WriteLine(results.Name);
            // }

            //Dapper：QueryMultiple
            // using (SqlConnection conn = new SqlConnection(_connectString))
            // {
            //     string strSql = "Select * from Customers; Select * from Orders;";
            //     using (var results = conn.QueryMultiple(strSql))
            //     {
            //         //第一段SQL
            //         var customers = results.Read<MyModel>().ToList();
            //         //第二段SQL 強型別
            //         var orders = results.Read().ToList();
            //         for(int i=0; i<5; i++){
            //             Console.WriteLine($"{customers[i].Name}___{orders[i].OrderNo}");
            //         }
            //     }
            // }

            //Dapper：防止SQL Injection，避免參數直接丟到 SQL 裡面去串起來
            // var sql = @"SELECT * FROM Customers Where Name = @Name";
            // var parameters = new DynamicParameters();
            // parameters.Add("Name", "Jerry");
            // using (var conn = new SqlConnection(_connectString))
            // {
            //     var result = conn.QueryFirstOrDefault<MyModel>(sql, parameters);
            //     Console.Write(result.Name);
            // }

            //Dapper：Parameter​






            //Dapper：Execute (Stored Procedure會用到的input、output、return都可以用)
            // using (SqlConnection conn = new SqlConnection(_connectString))
            // {
            //     //準備參數
            //     DynamicParameters parameters = new DynamicParameters();
            //     parameters.Add("@a", "abc", DbType.String, ParameterDirection.Input);
            //     parameters.Add("@b", "123", DbType.String, ParameterDirection.Input);
            //     parameters.Add("@c", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //     parameters.Add("@Return", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            //     conn.Execute("testDapper", parameters, commandType: CommandType.StoredProcedure);
            //     //接回Output值
            //     int outputResult = parameters.Get<int>("@c");
            //     //接回Return值
            //     int returnResult = parameters.Get<int>("@Return");
            //     Console.WriteLine($"{outputResult}____{returnResult}");
            // }

            //Dapper：INSERT






            //Dapper：Update
            // var sql =@"
            // UPDATE Customers
            // SET 
            // [PhoneNo] = 'hims987'
            // WHERE 
            // Name = @Name";
            // MyModel parameter = new MyModel();
            // var parameters = new DynamicParameters(parameter);
            // parameters.Add("Name", "Jerry", System.Data.DbType.String);

            // using (var conn = new SqlConnection(_connectString))
            // {
            //     var result = conn.Execute(sql, parameters);
            //     Console.Write(result);
            // }



            DateTime date_1 = new DateTime(2021, 07, 01);
            DateTime date_2 = new DateTime(2021, 12, 31);
            int diffDay = ((date_2 - date_1).Days) + 1;
            string[] arrayDay = new string[diffDay];
            int j = 0;
            for (int i = 20210701; i < 20211232; i++) //順序:2021下 2021上 2020下 2020上
            {
                switch (i)
                {
                    case 20210732:
                        i = 20210801;
                        break;
                    case 20210832:
                        i = 20210901;
                        break;
                    case 20210931:
                        i = 20211001;
                        break;
                    case 20211032:
                        i = 20211101;
                        break;
                    case 20211131:
                        i = 20211201;
                        break;
                }
                if (i != 20211232)
                {
                    arrayDay[j] = i.ToString();
                    j++;
                }
            }

            foreach (var q in arrayDay)
            {
                Console.WriteLine(q);
                using (System.IO.StreamWriter file =  //StreamWriter默認會覆蓋原先文件并重寫一份
                      new System.IO.StreamWriter(@"C:\Users\benson922\Desktop\AdamJobRecord.txt", true))
                {
                    DateTime localDate = DateTime.Now;
                    file.WriteLine($"開始：{q}__{localDate.ToString()}");
                    Console.WriteLine($"開始：{q}__{localDate.ToString()}");

                    // ADO.Net被阻擋，改用ssapi
                    using (SqlConnection conn = new SqlConnection(_connectString))
                    {
                        try
                        {
                            //準備參數
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@DLRCD", "01", DbType.String, ParameterDirection.Input);
                            parameters.Add("@TRDT", q, DbType.String, ParameterDirection.Input);
                            parameters.Add("@MSG", dbType: DbType.Int32, direction: ParameterDirection.Output);
                            conn.Execute("SP_LSE120_CARTYPE_IRENT_JOB", parameters, commandType: CommandType.StoredProcedure);//SP_LSE120_CARTYPE_NOIRENT_JOB
                            //接回Output值
                            // string outputResult = parameters.Get<string>("@MSG"); //一直跳錯INT轉STRING
                        }
                        catch (Exception e)
                        {
                            file.WriteLine($"錯誤：{q}__{localDate.ToString()}__");
                            Console.WriteLine($"錯誤：{q}__{localDate.ToString()}__");
                        }

                        localDate = DateTime.Now;
                        // file.WriteLine($"結束：{q}__{localDate.ToString()}__");
                        // Console.WriteLine($"結束：{q}__{localDate.ToString()}__");
                    }
                }
                Thread.Sleep(30000); //等待60秒
            }
        }
    }

    class MyModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string PhoneNo { set; get; }
    }
}
