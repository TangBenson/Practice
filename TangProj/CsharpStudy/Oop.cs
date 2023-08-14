// namespace CsharpStudy;
// internal class People
// {
//     //如果沒有加上Virtual，子類別又override這個方法，編譯器就會生氣喔!
//     public virtual void Eat() { Console.WriteLine("什麼都吃"); }
//     //故意沒加上Virtual
//     public void Test() { Console.WriteLine("People.Test"); }

//     //GoodPeople無PreTest，所以呼叫People的PreTest，而Test()這行因為GoodPeople沒有override Test(亦即存在兩個Test方法)，所以就會直接使用People的
//     public void PreTest()
//     {
//         Console.WriteLine("People.PreTest");
//         Test();
//     }

//     //子類別使用new，就可以不加上virtual
//     public void Test2() { Console.WriteLine("People.Test2"); }
// }

// internal class GoodPeople : People
// {
//     //這裡使用override來複寫
//     public override void Eat() { Console.WriteLine("蔬食健康又好吃"); }
//     //這裡也不使用override關鍵字來複寫。若加上new關鍵字，可讓編譯器開心一點
//     public void Test() { Console.WriteLine("GoodPeople.Test"); }
//     public new void Test2() { Console.WriteLine("GoodPeople.Test2"); }
// }

// // 抽象類別 ＆ 抽象方法
// abstract class Animal {
//     internal string Color { get; set; } = "黑色";
//     internal string Type { get; set; } = "柴柴";
//     public abstract void Eat();
// }
// class Dog : Animal {
//     internal new string Type { get; set; } = "柯爾鴨";

//     //必須覆寫
//     public override void Eat() { Console.WriteLine("嚼嚼嚼"); }
// }
// public class Dog2 { 
//     public void Eat() { Console.WriteLine("嚼嚼熱狗"); }
// }
// public class RobotDog {
//     public void Eat() { Console.WriteLine("嚼嚼汽油"); }
// }


// class Oops
// {
//     static void Main(string[] args)
//     {
//         GoodPeople Peggy = new GoodPeople();
//         Peggy.Eat(); //蔬食健康又好吃
//         Peggy.Test(); //GoodPeople.Test
//         Peggy.PreTest();//出現卻是People.PreTest
//         //我們用個多型吧!
//         People c = new People();
//         c.Test2();//People.Test2

//         Console.WriteLine("-------------------------------------------------------------");

//         //Animal a = new Animal(); //錯! 不可建立
//         Dog a = new Dog();
//         Console.WriteLine(a.Color);
//         Console.WriteLine(a.Type);
//         a.Eat();

//         Dog2 b = new Dog2();
//         b.Eat();
//         RobotDog d = new RobotDog();
//         d.Eat();

//         //讓畫面可以暫停。
//         Console.ReadLine();
//     }
// }

// /*
// 結論:
// 寫了那麼多東西，基本的原則就是，如果是override，就和一般的繼承沒甚麼兩樣，
// 但若是用到new，會先看變數型別來決定呼叫的是哪裡的方法，如果這個變數型別裡面
// 的方法是繼承而來的，就會回到父層去執行，而如果父層裡面還有呼叫其他方法，
// 只要子層沒有override，則都會執行父層的方法。
// */