// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace CsharpStudy
// {
//     public class DI_before
//     {
//         private CoffeeMachine coffeeMachine;
//         private Toaster toaster;


//         /*
//         用 new Class時就太依賴了，要做依賴反轉，就是要在外部來完成(外部做實例化傳進來)，例如 new CoffeeMachine就是自己做咖啡機，應該改成從外部買來
//         但也不是所有 new就不好，例如 Breakfast可以 new，因為這個環境下 Breakfast是自己做的沒錯
//         依賴反轉也要求高等模組不能依賴具體實現，也就是不能傳入具體實例，應傳入 interface
//         */
//         public DependencyEX_before()
//         {
//             this.coffeeMachine = new CoffeeMachine();
//             this.toaster = new Toaster();
//         }

//         public async Task<Breakfast> MakeBreakfast()
//         {
//             Console.WriteLine("開始做早餐");

//             Task<Coffee> coffeeTask = coffeeMachine.MakeCoffee(new CoffeeBean(), new Milk());
//             Task<Bread> breadTask = toaster.ToastBread(new Bread());

//             Coffee coffee = await coffeeTask;
//             Bread newBread = await breadTask;

//             Console.WriteLine("可以開動了");

//             return new Breakfast()
//             {
//                 Coffee = coffee,
//                 Bread = newBread
//             };
//         }


//         public static async Task Main(string[] args)
//         {
//             DependencyEX_before breakfastMaker = new DependencyEX_before();
//             Breakfast breakfast = await breakfastMaker.MakeBreakfast();
//         }
//     }






//     public class CoffeeMachine
//     {
//         public Task<Coffee> MakeCoffee(CoffeeBean bean, Milk milk)
//         {
//             Console.WriteLine("先做咖啡");
//             var coffee = new Coffee();
//             Console.WriteLine("煮好了");
//             return Task.FromResult<Coffee>(coffee);
//         }
//     }
//     public class Toaster
//     {
//         public async Task<Bread> ToastBread(Bread bread)
//         {
//             Console.WriteLine("再做麵包");
//             await Task.Delay(1000);
//             Console.WriteLine("烤好了");
//             return bread;
//         }

//     }
//     public class Breakfast
//     {
//         public Coffee Coffee { get; set; }
//         public Bread Bread { get; set; }

//     }
//     public class Coffee
//     {

//     }
//     public class Bread
//     {

//     }
//     public class Milk
//     {

//     }
//     public class CoffeeBean
//     {

//     }
// }