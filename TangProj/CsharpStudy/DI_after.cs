// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace CsharpStudy
// {
//     public class DI_after
//     {
//         private ICoffeeMachine coffeeMachine;
//         private IToaster toaster;
//         private CoffeeBean coffeeBean; // 太簡易的 class就不做抽象化(interface)處理
//         private Milk milk;
//         private Bread bread;

//         public DependencyEX_after(ICoffeeMachine coffeeMachine, IToaster toaster, CoffeeBean coffeeBean, Milk milk, Bread bread)
//         {
//             this.coffeeMachine = coffeeMachine;
//             this.toaster = toaster;
//             this.coffeeBean = coffeeBean;
//             this.milk = milk;
//             this.bread = bread;
//         }

//         public async Task<Breakfast> MakeBreakfast()
//         {
//             Console.WriteLine("開始做早餐");

//             Task<Coffee> coffeeTask = coffeeMachine.MakeCoffee(coffeeBean, milk);
//             Task<Bread> breadTask = toaster.ToastBread(bread);

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
//             // 在 api專案中，這裡就是 start up中做的 DI容器，用 DI容器管理這裡宣告的 class
//             ICoffeeMachine a = new CoffeeMachine();
//             IToaster b = new Toaster();
//             CoffeeBean c = new CoffeeBean();
//             Milk d = new Milk();
//             Bread e = new Bread();

//             DependencyEX_after breakfastMaker = new DependencyEX_after(a, b, c, d, e);
//             Breakfast breakfast = await breakfastMaker.MakeBreakfast();
//         }
//     }








//     public interface ICoffeeMachine
//     {
//         Task<Coffee> MakeCoffee(CoffeeBean bean, Milk milk);
//     }
//     public class CoffeeMachine : ICoffeeMachine
//     {
//         public Task<Coffee> MakeCoffee(CoffeeBean bean, Milk milk)
//         {
//             Console.WriteLine("先做咖啡");
//             var coffee = new Coffee();
//             Console.WriteLine("煮好了");
//             return Task.FromResult<Coffee>(coffee);
//         }
//     }
//     public interface IToaster
//     {
//         Task<Bread> ToastBread(Bread bread);
//     }
//     public class Toaster : IToaster
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