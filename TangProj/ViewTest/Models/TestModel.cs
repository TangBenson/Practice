using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewTest.Models;

public class TestModel
{
    public string? TangText { get; set; }
    public People? p { get; set; }
    public IEnumerable<SelectListItem> lst { get; set; }
}