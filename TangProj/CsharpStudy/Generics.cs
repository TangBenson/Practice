// using System;

// namespace CsharpStudy;

// /*
// 泛型 : 讓你在定義Class、Method、Interface時先不用決定型別，到了要實體化的時候再決定其型別。
// public T Generic<T>(T b) where T : new()
// {
//     return new T();
// }
// public後面的T是回傳值，表示我還不確定型別，而<T>表示我這個方法要使用泛型，(T b)表示傳入參數b不確定
// 型別。
// where定義型別參數的條件，以此例來說，where T : new()定義T有建構子，如此一來我們才可以在new T()
// */

// # region 範例
// class Program<T>
// {
//     T[] array = new T[0];

//     //一個一個加入
//     public void add(T item)
//     {
//         // 將一維陣列中的項目數目變更為指定的新大小。
//         Array.Resize(ref array, array.Length + 1);
//         array[array.Length - 1] = item;
//     }

//     public T[] all()
//     {
//         return array;
//     }
// }

// class Test
// {
//     public T Generic<T>(T b) where T : new()
//     {
//         return new T();
//     }
// }

// class Generics
// {
//     // 若宣告 class時沒有加上<T>，則 Method用泛型要自己加在 Method名稱後面
//     public void MyMethod<T>(T a)
//     {
//         Console.WriteLine("參數類型" + a.GetType() + "，值" + a);
//     }

//     static void Main(string[] args)
//     {
//         Generics p0 = new Generics();
//         p0.MyMethod<int>(2); //p.s(2);這樣也行，編譯時自動判斷參數類型
//         p0.MyMethod<string>("dd");


//         Program<int> p = new Program<int>();
//         p.add(3);
//         p.add(5);
//         p.add(6);
//         foreach (int row in p.all())
//         {
//             Console.WriteLine(row);
//         }
//     }
// }
// #endregion