// namespace CsharpStudy;

// public delegate void Bb(int q);
// class LambdaTest
// {
//     // 使用lambda時，如果不使用到傳入參數，就要打一個底線。
//     static void Main(string[] args)
//     {
//         // 基本 Lambda用法
//         // int Trd() => 5 + 5;
//         // Console.WriteLine(Trd());
//         // void Trd2() => Console.WriteLine("d");
//         // Trd2();
        


//         int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
//         var firstNumbersLessThanSix = numbers.TakeWhile(n => n < 6);
//         Console.WriteLine(string.Join(" ~ ", firstNumbersLessThanSix));

//         // c# 的Lambda大多搭配委派使用
//         Aa a = new Aa(69);

//         Bb b = new Bb((q) =>
//         {
//             if (q < 10) { Console.WriteLine("數字小於10"); }
//             else { Console.WriteLine("數字大於10"); }
//         });
//         b.Invoke(5);

//         Action<int> ActionDelegate = (x) => Console.WriteLine("一、" + x);
//         ActionDelegate(5);

//         Action action = () => Console.WriteLine("二、" + "77777");
//         Action<int, int> action2 = (a, b) => Console.WriteLine("三、" + (a + b).ToString());
//         action();
//         action2(1, 3);

//         Func<int, int> square = x => x * x;
//         Console.WriteLine("四、" + square(8));

//         Func<int, int, bool> testForEquality = (x, y) => x == y;
//         Console.WriteLine("五、" + testForEquality(5, 4));

//         Action<string> greet = name =>
//         {
//             string greeting = $"Hello {name}!";
//             Console.WriteLine("六、" + greeting);
//         };
//         greet("World");
//     }
// }

// class Aa
// {
//     public Aa(int a) { Console.WriteLine(a); }
// }