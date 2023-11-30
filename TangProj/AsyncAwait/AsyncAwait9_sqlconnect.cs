
using Microsoft.Data.SqlClient;

class AsyncAwait9_sqlconnect
{
    /*
    Q：開啟資料庫連線不是即時的嗎?  這樣感受不到使用非同步的好處阿？
    A：請注意，非同步方式的好處更明顯地體現在需要進行多個非相關的操作或在大型且繁忙的應用程式中。在一些較小的應用程式或單一操作的情況下，可能無法明顯感受到非同步方式的好處。

    對於使用非同步方式的程式碼，並不一定會開啟新的執行緒。
    非同步操作可以使用單一執行緒或工作排程器（例如 .NET 中的工作排程器）來處理。
    這意味著主執行緒可以繼續執行其他工作，而不需要等待非同步操作完成。
    當使用非同步方式時，主執行緒可以在非同步操作等待期間被釋放，以處理其他工作或回應其他請求。
    這種方式稱為非阻塞，因為主執行緒不會被阻塞，可以自由地執行其他任務。
    使用非同步方式的程式碼並不意味著一定需要監控執行緒狀態。非同步程式設計的目標是在等待耗時的操作期間釋放主執行緒，以提高應用程式的效能和反應能力。
    */
    internal static async Task Main()
    {
        string connectionString = "Data Source=sqyhi03azt.database.windows.net,1433;Initial Catalog=IRENT_V2T;Persist Security Info=True;User ID=HIAPIUSER;Password=Xz)0#v,X64Ln;Application Name=IRentWebAPI;";

        // 建立資料庫連線物件
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            /*
            如果在 connection.OpenAsync() 前面不加上 await，則該方法將以非同步方式啟動，但不會等待它完成。
            換句話說，程式將繼續執行後續的指令，而不會等待資料庫連線開啟完成。
            在這種情況下，如果沒有 await，connection.OpenAsync() 方法將返回一個 Task 物件，表示資料庫連線開啟的非同步操作。
            然而，由於沒有等待這個 Task 完成，程式將繼續執行後續的指令，可能在連線還沒有完成的情況下執行其他操作。
            這可能會導致問題，因為在沒有確定連線是否成功的情況下，可能會執行依賴於連線狀態的後續指令。
            這可能會導致錯誤，例如在未成功連線的情況下執行資料庫查詢，或者導致未預期的行為。
            因此，在使用非同步方法時，通常建議在需要等待該方法完成的地方使用 await 關鍵字，以確保在繼續執行後續指令之前，
            等待該非同步操作完成。這樣可以確保在需要連線完成的情況下，才繼續執行後續的操作。
            */
            // 非同步方式開啟資料庫連線
            // await connection.OpenAsync();

            Console.WriteLine("資料庫連線已開啟");

            // 執行其他工作，例如進行其他請求或回應用戶端的操作
            Console.WriteLine("執行其他工作...");

            /*
            如果在 await Task.Delay(2000) 前面不加上 await，則該方法將以非同步方式啟動，但不會等待延遲完成。
            換句話說，程式將繼續執行後續的指令，而不會等待延遲結束。
            在這種情況下，如果沒有 await，Task.Delay(2000) 方法將返回一個 Task 物件，表示延遲的非同步操作。
            然而，由於沒有等待這個 Task 完成，程式將繼續執行後續的指令，可能在延遲還沒有完成的情況下執行其他操作。
            這樣可能會導致錯誤的結果，因為後續的指令可能在延遲時間還沒結束的情況下執行。
            如果依賴於延遲時間完成的操作在這之前執行，可能會導致不正確的行為或錯誤的結果。
            因此，在使用 Task.Delay 方法進行延遲時，通常建議在需要等待延遲結束的地方使用 await 關鍵字，以確保在延遲時間完成之前，等待該非同步操作完成。
            這樣可以確保在延遲時間結束後，再繼續執行後續的操作。
            */
            // 模擬等待時間
            await Task.Delay(3000);

            Console.WriteLine("其他工作完成");

            // 使用資料庫連線執行查詢等操作
            using (SqlCommand command = new SqlCommand("SELECT top 10 * FROM tb_memberdata", connection))
            {
                // 使用非同步方式執行查詢
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    // 處理查詢結果
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine(reader["memcname"]);
                    }
                }
            }
        }

        Console.WriteLine("程式執行結束");
        Console.ReadLine();
    }
}