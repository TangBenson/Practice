/*
建立一個 Parameter 資料夾，用來放一些傳入的參數，並且新增一個 CardParameter 
來當作我們新增和修改卡片的參數類別
*/

/// <summary>
/// 卡片參數
/// </summary>
public class CardParameter
{
    /// <summary>
    /// 卡片名稱
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 卡片描述
    /// </summary>
    public string Description { get; set; }
}