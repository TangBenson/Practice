// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace DelegateEvent
// {
//     public class Delegate5
//     {
//         static void Main(string[] args)
//         {
//             Test ob = new Test();
//             ob.a = ob.Fuck;
//             Console.WriteLine(ob.MakeLove("幹死我寶貝"));
//         }
//     }
//     class Test
//     {
//         Func<string, string>? _a;
//         public Func<string, string>? a
//         {
//             set => _a = value;
//         }

//         public string Fuck(string memo)
//         {
//             return memo;
//         }
//         public string MakeLove(string memo)
//         {
//             return _a(memo);
//         }
//     }
// }