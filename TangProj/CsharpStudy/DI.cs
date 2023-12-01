// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace CsharpStudy
// {
//     public class DI
//     {
//         //最初的高耦合狀態，若低階模組 Tang 修改 mathod 則高階模組也要改，或是 Tang 要改成 Tang 也是一樣要改高階模組
//         public Product Work()
//         {
//             Tang programmer = new Tang();
//             var product = programmer.Programming();
//             return product;
//         }


//         /*
//         使用依賴反轉，原本是 高階模組 → 低階模組 的關係，變成了 高階模組 → 介面 ← 低階模組
//         但這邊遇到的問題：使用功能之前，必須先建立該類別的實例，也就是 new Chi()，那麼，我們不就還是直接依賴了實作嗎？
        
//         另外一提，IProgrammer programmer和 Chi programmer不完全相同，前者可以使代码更具灵活性和可扩展性。
//         如果将来需要使用不同的 class实现 IProgrammer 接口，只需修改变量的赋值部分而无需修改其它代码。这种方式符合面向接口(interface)编程的原则，
//         提高了代码的可维护性和可测试性。而直接使用具体类类型的变量 Chi programmer = new Chi(); 更适用于确切知道使用的是具体类
//         ，并且不需要多态性或接口的情况。这种方式更直接，并且可以直接访问具体类的特定成员和方法。
//         */
//         public Product Work2()
//         {
//             IProgrammer programmer = new Chi();
//             var product = programmer.Programming();
//             return product;
//         }


//         /*
//         控制反轉：
//         把實例的建立和實例的使用切分開來就好了，不再是由高階模組去建立並控制低階模組，而是我們讓一個控制反轉中心去建立低階模組，
//         然後高階模組要使用的時候再把這個低階模組交給高階模組使用。
//         從原先的 高階模組 —(建立)→ 低階模組，變成了 高階模組 ←(傳遞低階模組)— 控制反轉中心
        
//         依賴注入：把控制中心建立的低階模組，交給(注入、丟進去)高階模組做使用。
//         建構子，就是其中一種解決方法 => 在建構子內注入，把new Chi();替換掉
//         */
//         private IProgrammer _programmer;
//         public DI(IProgrammer programmer)
//         {
//             this._programmer = programmer;
//         }
//         public Product Work3()
//         {
//             var product = this._programmer.Programming();
//             return product;
//         }


//     }

//     public enum Product { }

//     //小唐
//     public class Tang
//     {
//         public Product Programming()
//         {
//             Product a = new Product();
//             return a;
//         }
//     }

//     //搞個介面做依賴反轉
//     public interface IProgrammer
//     {
//         Product Programming();
//     }

//     //小綺
//     public class Chi : IProgrammer
//     {
//         public Product Programming()
//         {
//             Product a = new Product();
//             return a;
//         }
//     }
// }