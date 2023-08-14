// namespace CsharpStudy;
// // C# 有這樣的語法，?? 運算子，當左邊是null時，回傳右邊的值

// class EnumTest
// {
//     // 可以一次宣告好幾個屬性，不用一個一個宣告，而且還會自動賦值
//     enum Day { a, Mon, Tue, Wed, Thu, Fri, Sat }; //預設Int型別
//     enum Range : long { Max = 2000000000L, Min = 1000000000L }; //設long型別
//     static void Main(string[] args)
//     {
//         string a = "3";
//         string b = null;
//         string c = b ?? "tt";
//         string d = a ?? "ww";
//         Console.WriteLine("a:"+a);
//         Console.WriteLine("b:"+b);
//         Console.WriteLine("c:"+c);
//         Console.WriteLine("d:"+d);

//         Console.WriteLine("1:"+Day.Fri);
//         Console.WriteLine("2:"+(int)Day.Fri);
//         Console.WriteLine("3:"+(int)Day.a);
//         Console.WriteLine("4:"+Range.Max);
//         Console.WriteLine("5:"+(int)Range.Max);
//         Console.WriteLine("6:"+Day.Mon.GetType());
//         Console.WriteLine("7:"+Range.Min.GetType());
//     }
// }