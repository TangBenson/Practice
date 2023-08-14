// namespace CsharpStudy;

// public delegate int MyDelegete(int x);
// public delegate int MyDelegate2(int x, int y);
// public delegate int MyDelegate3();
// public delegate void MyDelegate4();

// class Delegate3
// {
//     // *Action 不會傳回值，<T1,T2>代表傳入類型，若不傳入直接宣告Action即可
//     static Action? Action1; // 等同 public delegate void Action1();
//     static Action<int, int>? Action2; // 等同 public delegate void Action2(int x, int y);

//     // *Func 會傳回值，我們會將回傳型態放在最後一項,<T1,T2,TResult> T1,T2代表傳入類型 TResult為回傳類型
//     private static Func<string>? Func1; // 等同 public delegate string Func1();
//     private static Func<int, int, string>? Func2; // 等同 public delegate string Func2(int x, int y);

//     static void Main(string[] args)
//     {
//         var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
//         Console.WriteLine("一: " + string.Join(",", AddOne(array)));
//         Console.WriteLine("二: " + string.Join(",", MultipleTwo(array)));
//         Console.WriteLine("三: " + string.Join(",", Square(array)));
//         Console.WriteLine("-----------------------------------");

//         //用委派改寫
//         var array2 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
//         MyDelegete myDelegete1 = new MyDelegete(AddOne);
//         MyDelegete myDelegete2 = new MyDelegete(MultipleTwo);
//         MyDelegete myDelegete3 = Square; //語法糖(寫起來比較簡潔)
//         MyDelegete myDelegete4 = (x) => x + 500;
//         Console.WriteLine(string.Join(",", Change(array2, myDelegete1)));
//         Console.WriteLine(string.Join(",", Change(array2, myDelegete2)));
//         Console.WriteLine(string.Join(",", Change(array2, myDelegete3)));
//         Console.WriteLine(string.Join(",", myDelegete4(7)));
//         Console.WriteLine("-----------------------------------");

//         //Anonymous Function 匿名方法，省去寫具名函式 (被淘汰的寫法，看懂就好)
//         // MyDelegete mydelegate5 = delegate (int number) { return number + 1; };
//         // Console.WriteLine(mydelegate5(5));
//         // Console.WriteLine("---------------------------------");

//         //Lambda 功能更強大的匿名方法，完全取代Anonymous Function
//         // 1.運算式Lambda(Expression Lambdas)
//         MyDelegete mydelegate6 = x => x + 1;
//         MyDelegate2 mydelegate7 = (x, y) => x + y;
//         Console.WriteLine(mydelegate6(20));
//         Console.WriteLine(mydelegate7(20, 23));
//         // 2.陳述式Lambda(Statement Lambdas)
//         MyDelegate3 mydelegate8 = () =>
//         {
//             int q = 50;
//             return q + 50;
//         };
//         Console.WriteLine("mydelegate8:  " + mydelegate8());
//         Console.WriteLine("-----------------------------------");

//         //Action
//         // 1.一般寫法
//         MyDelegate4 aDe = NoResult;
//         aDe.Invoke();
//         // 2.Action寫法
//         Action1 = NoResult;
//         Action1();
//         Action2 = TestMethod;
//         Action2(5, 8);
//         Console.WriteLine("-----------------------------------");

//         //Func
//         Func1 = TestMethod2;
//         Console.WriteLine(Func1());
//         Func2 = Add;
//         Console.WriteLine(Func2(6, 5));
//         Console.WriteLine("-----------------------------------");
//     }

//     public static int AddOne(int number) { return ++number; }
//     public static int MultipleTwo(int number) { return number * 2; }
//     public static int Square(int number) { return number * number; }
//     public static int[] Change(int[] _array, MyDelegete myDelegete)
//     {
//         var array = new int[_array.Length];
//         for (int i = 0, c = _array.Length; i < c; i++) { array[i] = myDelegete(_array[i]); }
//         return array;
//     }

//     public static void NoResult() { Console.WriteLine('Q'); }
//     public static void TestMethod(int a, int b) { Console.WriteLine(a + b); }
//     public static string TestMethod2() { return "Me"; }
//     public static string Add(int num1, int num2) { return $"num1 + num2 = {num1 + num2}"; }

//     public static int[] AddOne(int[] _array)
//     {
//         var array = new int[_array.Length];
//         for (int i = 0, c = _array.Length; i < c; i++) { array[i] = _array[i] + 1; }
//         return array;
//     }
//     public static int[] MultipleTwo(int[] _array)
//     {
//         var array = new int[_array.Length];
//         for (int i = 0, c = _array.Length; i < c; i++) { array[i] = _array[i] * 2; }
//         return array;
//     }
//     public static int[] Square(int[] _array)
//     {
//         var array = new int[_array.Length];
//         for (int i = 0, c = _array.Length; i < c; i++) { array[i] = _array[i] * _array[i]; }
//         return array;
//     }
// }