// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Threading;

// namespace CsharpStudy
// {
//     public class AsyncAwait12
//     {
//         static async Task Main(string[] args)
//         {
//             Console.WriteLine("1");
//             // CookMeal();
//             await CookMeal();
//             Console.WriteLine("2");
//         }


//         public static async Task<int> CookMeal()
//         {
//             Console.WriteLine("3");
//             var grillTask = GrillSteakAsync(); // 開始煎牛排的工作，grillTask 就是回傳代表這個工作的物件
//             var boilTask = BoilSauceAsync(); // 開始煮醬汁的工作，並取得代表這個工作的物件
//             var fryTask = FryGarlicAsync(); // 開始煎蒜頭
//             Console.WriteLine("4");
            
//             // Thread.Sleep(3000);
//             // await Task.WhenAll(grillTask, boilTask, fryTask); 

//             var steak = await grillTask; // 等待煎牛排的工作結束，並且從這個工作物件中取得工作結果的牛排
//             Console.WriteLine("4-1");
//             var sauce = await boilTask; // 等待煮醬汁工作結束，取得醬汁
//             Console.WriteLine("4-2");
//             var garlic = await fryTask; // 等待煎好的蒜頭
//             Console.WriteLine("5");
//             //將做好的成品傳入擺盤的工作函式當中，並且等待擺盤的工作完成，回傳擺好的盤
//             return await DecoratePlateAsync(steak, sauce, garlic);
//             // 我們其實可以直接用下面這行，不過為了好解釋我們用上面的行數
//             // return await DecoratePlateA(await grillTask, await boilTask, await fryTask);
//         }
//         public static async Task<string> GrillSteakAsync()
//         {
//             Console.WriteLine("6");
//             await Task.Delay(3000);
//             Console.WriteLine("Steak grilled!");
//             return "Steak";
//         }

//         public static async Task<string> BoilSauceAsync()
//         {
//             Console.WriteLine("7");
//             await Task.Delay(3000);
//             Console.WriteLine("Sauce boiled!");
//             return "Sauce";
//         }

//         public static async Task<string> FryGarlicAsync()
//         {
//             Console.WriteLine("8");
//             await Task.Delay(3000);
//             Console.WriteLine("Garlic fried!");
//             return "Garlic";
//         }

//         public static async Task<int> DecoratePlateAsync(string steak, string sauce, string garlic)
//         {
//             Console.WriteLine("9");
//             await Task.Delay(3000);
//             Console.WriteLine("Plate decorated!");
//             return 42;
//         }
//     }
// }