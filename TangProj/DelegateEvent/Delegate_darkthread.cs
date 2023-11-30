// using System;
// using System.Text;

// class Program
// {
//     /*
//     delegate宣告主要是要明確定義呼叫時傳入的參數及傳回值型別，當作方法變數的型別(即MyFunc[] funcs = new MyFunc[4];)，
//     常常只用一次就丟，每次要為這些"抛棄式"delegate宣告取名稱都害我要想半天，因此我始終覺得它是件該被省略的無聊事。
//     Action Delegate的出現解決了以上困擾! .NET 內建了Action, Action<T>, Action<T, T>, Action<T, T, T>, Action<T, T, T, T>，
//     可以省下0到4個參數的delegate宣告。
//     引用Action<T>，我們可省去MyFunc delegate的宣告，然後把funcs變數宣告那一列改成Action<string>[] funcs = new Action<string>[4];，
//     其餘的程式碼完全不變，這樣就不用再花時間去想MyFunc這種鳥名字了
//     */

//     delegate void MyFunc(string paramStr); //為了要呼叫執行期間才產生的方法，宣告了delegate void MyFunc(string paramStr)
//     static void Main(string[] args)
//     {
//         MyFunc[] funcs = new MyFunc[4];
//         // Action<string>[] funcs = new Action<string>[4];

//         int outVar = 5;
//         for (int i = 0; i < 4; i++)
//         {
//             int intVar = i;
//             funcs[i] = (s) =>
//             {
//                 Console.WriteLine(
//                     "outVar = {0} intVar = {1} paramStr = {2}",
//                     outVar, intVar, s);
//                 intVar++;
//             };
//         }
//         funcs[0]("First Call"); // 等同：funcs[0].Invoke("First Call");
//         funcs[0]("Second Call");
//         funcs[2]("Test1");
//         funcs[3]("Test2");
//         outVar = 6;
//         funcs[2]("Test3");
//         funcs[3]("Test4");
//         Console.Read();
//     }
// }




// // class Program
// // {
// //     /*
// //     Action適用於不傳回值的方法，若方法有傳回值，則.NET 2.0另外備有Func<TResult>, Func<T, TResult>,
// //     Func<T, T, TResult>, Func<T, T, T, TResult>, Func<T, T, T, T, TResult>可資利用
// //     */
// //     static void Main(string[] args)
// //     {
// //         Action<int> f1 = i => { Console.WriteLine("i={0}", i); };
// //         f1(10);
// //         Action<int, string> f2 =
// //             (i, s) => { Console.WriteLine("i={0} s={1}", i, s); };
// //         f2(99, "Hello");
// //         Action<byte[], Encoding> f3 =
// //             (b, e) => { Console.WriteLine(e.GetString(b)); };
// //         f3(new byte[] { 65, 66, 67 }, Encoding.ASCII);
// //         Func<int, string> f4 =
// //             i => string.Format("i={0} i+1={1}", i, i+1);
// //         Console.WriteLine(f4(1));
// //         Console.Read();
// //     }
// // }