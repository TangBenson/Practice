// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace Blazor_Lunch.Pages
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "c:\Users\benso\OneDrive\文件\dotnetProject\Practice\Blazor_Lunch\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "c:\Users\benso\OneDrive\文件\dotnetProject\Practice\Blazor_Lunch\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "c:\Users\benso\OneDrive\文件\dotnetProject\Practice\Blazor_Lunch\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "c:\Users\benso\OneDrive\文件\dotnetProject\Practice\Blazor_Lunch\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "c:\Users\benso\OneDrive\文件\dotnetProject\Practice\Blazor_Lunch\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "c:\Users\benso\OneDrive\文件\dotnetProject\Practice\Blazor_Lunch\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "c:\Users\benso\OneDrive\文件\dotnetProject\Practice\Blazor_Lunch\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "c:\Users\benso\OneDrive\文件\dotnetProject\Practice\Blazor_Lunch\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "c:\Users\benso\OneDrive\文件\dotnetProject\Practice\Blazor_Lunch\_Imports.razor"
using Blazor_Lunch;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "c:\Users\benso\OneDrive\文件\dotnetProject\Practice\Blazor_Lunch\_Imports.razor"
using Blazor_Lunch.Shared;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/todo")]
    public partial class Todo : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 20 "c:\Users\benso\OneDrive\文件\dotnetProject\Practice\Blazor_Lunch\Pages\Todo.razor"
       
    private List<TodoItem> todos = new();
    private string? newTodo; //宣告一個newTodo的變數來做為雙向綁定
    private void AddTodo()
    {
        if (!string.IsNullOrWhiteSpace(newTodo))
        {
            todos.Add(new TodoItem { Title = newTodo });
            newTodo = string.Empty;
        }
    }

    //這class應該抽出去在Models資料夾，但我懶
    public class TodoItem
    {
        public string? Title { get; set; }
        public bool IsDone { get; set; }
    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
