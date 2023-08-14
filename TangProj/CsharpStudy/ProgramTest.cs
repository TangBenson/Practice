// using System;
// using System.Collections.Generic;       // 為了使用List
// using System.Text.RegularExpressions;   // 為了使用Regex
// using System.Linq;                      // 為了使用ToArray()

// // 定義命名空間 (namespace)，namespace是不能使用存取修飾詞
// namespace CsharpStudy;

// // 定義結構使用關鍵字struct, 是簡單的物件, 更複雜的由類別來定義
// public struct Point1
// {
//     //宣告兩個欄位 (field), 不同於屬性喔
//     public int x, y;
// }
// public struct Point2
// {
//     public int x, y;

//     // 定義建構子(建構函式),就是建立結構時所執行的特殊方法，可用作初始化動作。要跟結構名稱同名，不需要回傳值(c#無函式定義)。
//     // 建構子的input要在建立物件時給予。若沒寫則系統會產生預設的
//     public Point2(int p1, int p2)
//     {
//         x = p1;
//         y = p2;
//     }

//     // 定義方法，宣告為void沒有回傳值，參數列留空
//     public void PrintCoordinate()
//     {
//         Console.WriteLine("({0}, {1})", x, y);
//     }
// }

// /*
// 自我練習定義class
// 迷之音:需要和主程式main同一層？ Ans:class裡可以宣告其他class，method裡不行，不可在Main裡宣告 
// */
// class Ps5
// {
//     /*
//     建構函式:
//     名稱需與類別相同
//     不能有回傳值和使用void
//     不會被繼承
//     類別內可有0或n個(多載overloading)
//     除非類別是靜態，否則沒有建構函式的類別會從C#編譯器取得一個公開無參數建構函式，以啟用類別具現化
//     */
//     public Ps5()
//     {

//     }
//     public Ps5(int yyyymm)
//     {
//         int date = yyyymm;
//         Console.WriteLine($"測試建構函式{date}");
//     }

//     //欄位(Field)
//     //要給其他的 Method 方法調用。多用private型別。若用private則習慣首字母小寫並在前面加個"_"，目的是說這個欄位已被封裝。
//     public int price;
//     private int _newWeight;

//     //屬性(property)
//     //是對欄位的封裝。欄位純粹儲存資料, 而屬性多了檢查機制, 可限制 ( if ) 讀出/寫入的值
//     //希望可以像變數一樣被使用，又想要偷偷的對這個變數做一些處理，不讓使用的人知道
//     //使用get和set存取子設定屬性最大優點是可以隱藏我們真的要做的事情(資料封裝)
//     private string _uc5 { get; set; }
//     public int weight
//     {
//         //get存取子可以回傳屬性值
//         get
//         {
//             return _newWeight;
//         }
//         //set存取子可以設定屬性值，value是外部傳來的數字
//         set
//         {
//             if (value < 0) { value = 0; }
//             if (value > 60) { value = 60; }
//             _newWeight = value;
//         }
//     }

//     //方法，預設為傳值呼叫Call By Value，若要呼叫Call By Reference，加上"ref"在方法宣告及使用上
//     public int HowMuch()
//     {
//         Console.WriteLine(_newWeight); //設成private所以只能在class內用
//         return 1699;
//     }
//     private static int addMoney(int amount)
//     {
//         amount = amount * 2;
//         return amount;
//     }

//     //支援多載(overloading)，同一個方法名稱可以依據參數不同執行不同功能的方法
//     private static string addMoney(string amount)
//     {
//         amount = amount + "hi";
//         return amount;
//     }
//     //不加存取修飾詞, 預設為private
// }

// class ProgramTest
// {
//     // 列舉是一組用識別字定義的整數常數, 預設int
//     enum Day { Sun, Mon, Tue, Wed, Thu, Fri, Sat };
//     enum Range : long { Max = 2000000000L, Min = 1000000000L };

//     /*
//     定義static的Main()方法, 預設的參數為字串陣列的args
//     Main方法是c#應用程式的進入點。(程式庫和服務不需要Main方法作為進入點)。
//     當應用程式啟動時，Main方法是第一個叫用的方法。 c#程式中只能有一個進入點。
//     */
//     static void Main(string[] args)
//     {
//         Console.WriteLine(DateTime.Now.ToString("yyyyMMdd"));
//         Console.WriteLine("天空一聲巨響,老娘閃亮登場");
//         Console.WriteLine("老娘一出現,全球都停電");
//         Console.WriteLine(DateTime.Now);


//         Ps5 Tang = new Ps5();
//         Ps5 Wei = new Ps5(500);
//         Ps5 Chi = new Ps5() { price = 999 };
//         Tang.weight = 200;
//         Wei.price = 100;
//         // Wei._newWeight = 100;  //設成private所以不能用
//         // Wei._uc5 = 100;
//         Console.WriteLine($"重量是{Tang.weight}");
//         Console.WriteLine($"價格是{Wei.price}");
//         Console.WriteLine($"測試一下{Chi.price}");
//         Tang.HowMuch();
//         int a_3 = HowMuch2();

//         // 型別分實質和參考, 前者如int後者如class、interface、object
//         int a;
//         a = 1;
//         int b = 2;
//         // 預設＝0
//         int c = new int();
//         int d = a + b + c;
//         Console.WriteLine("int:" + c);

//         double e = 3.4;
//         double f = 5.1;
//         double g = e + f;
//         Console.WriteLine("double:" + g);

//         char h = 'F';
//         int i = h;
//         Console.WriteLine("char:" + h);
//         Console.WriteLine("char轉int:" + i);

//         bool j = true;
//         Console.WriteLine("bool:" + j);

//         // 列舉是一組用識別字定義的整數常數
//         int a_1 = (int)Day.Sun;
//         int b_1 = (int)Day.Thu;
//         Console.WriteLine("Sunday: {0}", a_1);
//         Console.WriteLine("Thursday: {0}", b_1);
//         long a_2 = (long)Range.Max;
//         long b_2 = (long)Range.Min;
//         Console.WriteLine("Max: {0}", a_2);
//         Console.WriteLine("Min: {0}", b_2);

//         // 宣告了 Point1 的變數 p1
//         Point1 p1;
//         p1.x = 10;
//         p1.y = 2;
//         Console.WriteLine("p1: ({0}, {1})", p1.x, p1.y);
//         Point2 p2 = new Point2(11, 22);
//         p2.PrintCoordinate();
//         Console.WriteLine("p2: ({0}, {1})", p2.x, p2.y);

//         int k = 4;
//         if (k == 1)
//         {
//             Console.WriteLine("姆姆");
//         }
//         else if (k == 2)
//         {
//             Console.WriteLine("布布");
//         }
//         else
//         {
//             Console.WriteLine("米古");
//         }
//         switch (k)
//         {
//             case 1:
//                 Console.WriteLine("蛋捲");
//                 break;
//             case 2:
//                 Console.WriteLine("本丸");
//                 break;
//             case 3:
//                 Console.WriteLine("米香");
//                 break;
//             default:
//                 Console.WriteLine("巴努");
//                 break;
//         }

//         int l = 3;
//         while (l > 0)
//         {
//             Console.Write("學習 ");
//             l--;
//             if (l == 1)
//             {
//                 break;
//             }
//         }

//         Console.WriteLine();
//         for (int m = 2; m > 0; m--)
//         {
//             Console.Write("開心 ");
//         }

//         //至少做一次
//         Console.WriteLine();
//         int w = 4;
//         do
//         {
//             // Console.Write(w+' ');
//             Console.Write(w + " ");
//             w--;
//         } while (w > 0);

//         //陣列
//         Console.WriteLine();
//         int[] n = new int[5] { 50, 60, 70, 80, 90 };
//         Console.WriteLine(n);
//         int[] o = { 51, 61, 71, 81, 91 };
//         Console.WriteLine(o);

//         int[] q = new int[5];
//         int value = 1;
//         for (int r = 0; r < 5; r++)
//         {
//             q[r] = value++;
//         }
//         //變數 r 會逐一取得整數陣列變數 q 當中的元素，所以變數 r 必須宣告為整數
//         foreach (int r in q)
//         {
//             Console.WriteLine("都你害的：" + r);
//         }

//         int[,] s = new int[4, 2];
//         Console.WriteLine(s);
//         int[,,] p = { { { 1, 2, 3 }, { 4, 5, 6 } }, { { 7, 8, 9 }, { 10, 11, 12 } } };
//         Console.WriteLine(p[1, 1, 1]);

//         string t = "溯溪回憶滿滿";
//         Console.WriteLine(t);
//         string u = "我是誰";
//         foreach (char v in u)
//         {
//             Console.WriteLine(v);
//         }
//         Console.WriteLine(u);

//         //關鍵字 var 用來設定匿名型態，這讓變數 banana 具有屬性 kg 與 price ，屬性值就是冒號後的整數
//         var banana = (kg: 10, price: 1);
//         Console.WriteLine($"香蕉{banana.kg}斤{banana.price}元");
//         Console.WriteLine("香蕉{0}斤{1}元", 10, 1);
//     }
//     //方法
//     public static int HowMuch2()
//     {
//         return 1699;
//     }
// }