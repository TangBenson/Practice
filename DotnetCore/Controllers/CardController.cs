using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; //List<>
using System.Linq; //FirstOrDefault

[ApiController]//讓Controller知道他現在負責搞 API 了
[Route("[controller]")]
public class CardController : ControllerBase //繼承 ControllerBase 取得控制器該有的方法和成員
{
    // 測試用的資料集合
    private static List<Card> _cards = new List<Card>();

    // 查詢卡片列表
    [HttpGet]
    public List<Card> GetList()
    {
        return _cards;
    }

    // 查詢卡片
    /*
    [FromRoute] 的屬性來告訴 API 說 int id 這個參數是來自於 Route 上的。
    除了 FromRoute 以外，還有 GET 時很常用到的 [FromQuery] 或舊版本的
    [FromUri]（指這個參數從 QueryString 也就是 ?a=1&b=2 那串裡面接收）、
    POST 和其他狀況常用到的 [FromBody]（指這個參數要從 Body 接收）等等，可以讓我們標明參數的來源。
    */
    [HttpGet]
    [Route("{id}")]//Route會變成 GET /card/1（查詢 ID 為 1 的卡片）
    public Card Get([FromRoute] int id)
    {
        return _cards.FirstOrDefault(card => card.Id == id);
    }

    // 新增卡片
    /// <param name="parameter">卡片參數</param>
    [HttpPost]
    public IActionResult Insert([FromBody] CardParameter parameter)
    {
        _cards.Add(new Card
        {
            Id = _cards.Any()
              ? _cards.Max(card => card.Id) + 1
              : 0, // 臨時防呆，如果沒東西就從 0 開始
            Name = parameter.Name,
            Description = parameter.Description
        });

        return Ok();
    }

    // 更新卡片
    /// <param name="id">卡片編號</param>
    /// <param name="parameter">卡片參數</param>
    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(
        [FromRoute] int id,
        [FromBody] CardParameter parameter)
    {
        var targetCard = _cards.FirstOrDefault(card => card.Id == id);
        if (targetCard is null)
        {
            return NotFound(); //HTTP Status＝404
        }

        targetCard.Name = parameter.Name;
        targetCard.Description = parameter.Description;

        return Ok(); //HTTP Status＝200
    }

    // 刪除卡片
    /// <param name="id">卡片編號</param>
    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        _cards.RemoveAll(card => card.Id == id);
        return Ok();
    }
}