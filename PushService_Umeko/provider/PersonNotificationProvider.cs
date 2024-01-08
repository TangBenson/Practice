// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using PushService_Umeko.Models;

// namespace PushService_Umeko.provider
// {
//     public class PersonNotificationProvider
//     {
//         private string connString;
		
// 		// = "Password=a123456;Persist Security Info=True;User ID=caip;Initial Catalog=iRentDBV2;Data Source=(local);Encrypt=yes;Max Pool Size=200;TrustServerCertificate=True";

//         SQLHelper<Sync_PersonNotification, SPOutput_Base> db;

// 		public PersonNotificationProvider()
// 		{

// 			db = new SQLHelper<Sync_PersonNotification, SPOutput_Base>(connString);
// 		}
//         public PersonNotificationProvider(string connStr)
//         {
//             this.connString = connStr;
//             db = new SQLHelper<Sync_PersonNotification, SPOutput_Base>(connString);
// 		}

// 		public async Task<List<Sync_PersonNotification>> GetSendList()
//         {

// 			SPOutput_Base spOut = new SPOutput_Base();
// 			List<ErrorInfo > lstErrorInfo = new List<ErrorInfo>();


// 			//string baseSql = "usp_GetPersonNotificationForSend";
// 			string baseSql = "usp_PersonNotification_Q01";	//20210831 ADD BY ADAM REASON.修改sp名稱


// 			Dictionary<string, object> rtnObjects = await db.Base_QueryAsync<Sync_PersonNotification>(baseSql, null, spOut, CommandType.StoredProcedure);

//             var rowCount = -2;
//             var dataObjColl = new List<Sync_PersonNotification>();

// 			if (rtnObjects != null)
//             {
// 				rowCount = rtnObjects.ContainsKey("rowCount") ? (int)rtnObjects["rowCount"] : rowCount;
//                 if (rowCount > 0)
//                 {
//                     dataObjColl = rtnObjects.ContainsKey("results") ? (List<Sync_PersonNotification>)rtnObjects["results"] : dataObjColl;
// 				}

//                 if (rowCount <= 0) WriteErrorLog(rtnObjects, baseSql, rowCount);
//             }

// 			return dataObjColl;
//         }

        
//         public async Task<int> UpdateData(List<Sync_PersonNotification> updateModel)
//         {

//             SPOutput_Base spOut = new SPOutput_Base();

// 			//string baseSql = "usp_UpdatePersonNotificationSendStatus";
// 			string baseSql = "usp_PersonNotification_U01";      //20210831 ADD BY ADAM REASON.修改sp名稱

// 			var updateModelForSP = ConvertToUpdateModel(updateModel);

//             var rtnObjects = await db.Base_Update_multiple<SyncInput_UpdatePersonNotificationSendStatus>(baseSql, updateModelForSP, CommandType.StoredProcedure);

//             var rowCount = -2;

//             if (rtnObjects == null)
//             {
//                 rowCount = -2;
//             }
//             else
//             {
//                 rowCount = rtnObjects.ContainsKey("rowCount") ? (int)rtnObjects["rowCount"] : rowCount;
//             }

//             if (rowCount <= 0) WriteErrorLog(rtnObjects, baseSql, rowCount);

// 			return rowCount;
//             //dataObjColl.Dump();
//         }

//         private void WriteErrorLog(Dictionary<string, object> rtnObjects,string baseSql, int rowCount)
//         {
// 			NLogHelper.logger.Error($"baseSql:{baseSql},rowCount:{rowCount}");

// 			var ErrorList = rtnObjects.ContainsKey("ErrorInfo")
//                 ? (List<ErrorInfo>)rtnObjects["ErrorInfo"]
//                 : new List<ErrorInfo>();

//             ErrorList.ForEach(x => NLogHelper.logger.Error($"ErrorCode:{x.ErrorCode},ErrorMsg:{x.ErrorMsg}"));
//         }


// 		private List<SyncInput_UpdatePersonNotificationSendStatus> ConvertToUpdateModel(List<Sync_PersonNotification> updateModel)=>
//             updateModel.Select(o =>
//                 new SyncInput_UpdatePersonNotificationSendStatus
//                 {
//                     NotificationID = o.NotificationID,
//                     isSend = o.isSend,
//                     NewsID = o.NewsID
//                 }).ToList();
      

// 		/// <summary>
// 		/// 查詢參數組合
// 		/// </summary>
// 		/// <param name="inputModel"></param>
// 		/// <returns></returns>
// 		private (Dapper.DynamicParameters parameters, List<string> sqlConditions) GetQueryConditions(Sync_PersonNotification qryModel)
// 		{

// 			var parameters = new Dapper.DynamicParameters();
// 			List<string> sqlConditions = new List<string>();
// 			if (qryModel != null)
// 			{
// 				if (qryModel.OrderNum > 0)
// 				{
// 					sqlConditions.Add(" OrderNum = @OrderNum");
// 					parameters.Add("OrderNum", qryModel.OrderNum, DbType.Int64);
// 				}
// 				if (!string.IsNullOrEmpty(qryModel.IDNO))
// 				{
// 					sqlConditions.Add(" IDNO = @IDNO ");
// 					parameters.Add("IDNO", qryModel.IDNO, DbType.String);
// 				}
// 				if (qryModel.NType > 0)
// 				{
// 					sqlConditions.Add(" NType = @NType ");
// 					parameters.Add("NType", qryModel.NType, DbType.Int16);
// 				}

// 				if (qryModel.STime.HasValue)
// 				{
// 					sqlConditions.Add(" STime = @STime ");
// 					parameters.Add("STime", qryModel.STime.Value, DbType.DateTime);
// 				}
// 				if (qryModel.PushTime.HasValue)
// 				{
// 					sqlConditions.Add(" PushTime = @PushTime ");
// 					parameters.Add("PushTime", qryModel.PushTime.Value, DbType.DateTime);
// 				}
// 				if (qryModel.PushTime.HasValue)
// 				{
// 					sqlConditions.Add(" PushTime = @PushTime ");
// 					parameters.Add("PushTime", qryModel.PushTime.Value, DbType.DateTime);
// 				}
// 				if (!string.IsNullOrEmpty(qryModel.Title))
// 				{
// 					sqlConditions.Add(" Title = @Title ");
// 					parameters.Add("Title", qryModel.Title, DbType.String);
// 				}
// 				if (!string.IsNullOrEmpty(qryModel.Message))
// 				{
// 					sqlConditions.Add(" Message = @Message ");
// 					parameters.Add("Message", qryModel.Message, DbType.String);
// 				}
// 				if (!string.IsNullOrEmpty(qryModel.url))
// 				{
// 					sqlConditions.Add(" url = @url ");
// 					parameters.Add("url", qryModel.url, DbType.String);
// 				}
// 				if (qryModel.isSend >= 0)
// 				{
// 					sqlConditions.Add(" isSend = @isSend ");
// 					parameters.Add("isSend", qryModel.isSend, DbType.Int16);
// 				}
// 				if (qryModel.MKTime.HasValue)
// 				{
// 					sqlConditions.Add(" MKTime = @MKTime ");
// 					parameters.Add("MKTime", qryModel.MKTime.Value, DbType.DateTime);
// 				}
// 				if (qryModel.NewsID > 0)
// 				{
// 					sqlConditions.Add(" NewsID = @NewsID ");
// 					parameters.Add("NewsID", qryModel.NewsID, DbType.Int32);
// 				}

// 			}

// 			return new ValueTuple<Dapper.DynamicParameters, List<string>>(parameters, sqlConditions);

// 		}
// 	}
// }