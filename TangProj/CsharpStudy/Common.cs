// using System;
// using System.Collections.Generic;

// namespace CsharpStudy;
// class Common
// {
//     static void Main(string[] args)
//     {
//         SportsCar CarA = new SportsCar();
//         CarA.model = "海神";
//         CarA.price = 5000000000;
//         CarA.TaxRate = 0.3M;
//         motorcycle CarB = new motorcycle() { model = "小50", price = 30000, TaxRate = 0.1M };
//         Shoes ShoesA = new Shoes() { model = "愛迪達", price = 3000, TaxRate = 0.07M };

//         List<IProduct> proList = new List<IProduct>();
//         proList.Add(CarA);
//         proList.Add(CarB);
//         proList.Add(ShoesA);

//         proList.ForEach(o =>
//         {
//             Console.WriteLine(pritTax(o));
//         });
//     }
//     //印出繳稅結果
//     static string pritTax(IProduct product)
//     {
//         return $"{product.model} 售價：{product.price} 需繳稅：{product.Tax().ToString("N0")}";
//     }
// }


// //不只有賣車要繳稅，只要是涉及買賣都須要繳稅，將繳稅這個動作放在Car這個類別顯然是不合適的。定義這種橫跨類別的動作及屬性便是介面(Interface)
// interface IProduct
// {
//     //string model; --錯誤的寫法。介面不能實作，只能宣告
//     //型號
//     string model { get; set; }//宣告 model這個屬性需有 get 與 set 兩個方法
//     decimal price { get; set; }
//     //要繳給國家的稅
//     decimal Tax();
// }

// //鞋子
// // 不論是甚麼商品只要實作了 IProduct的這個介面都會擁有繳稅這個行為
// // 但是好像有哪裡怪怪的。結果都是要各自實作
// // 介面最大的功用就是讓不同的類別之間產生關聯性。我們可以確定只要有實作了 IProduct這個介面的類別都擁有 model與 Tax()這兩個屬性與方法
// /*
// 補充：
// 1.介面只能宣告，不能實作，且只能為公開(public)。 *預設就是公開所以不用特別加上public
// 2.介面可以繼承介面
// 3.介面不可以繼承類別
// 4.可以同時繼承多個介面
// 5.繼承介面的類別必須實作所有的屬性與方法。
// */
// class Shoes : IProduct
// {
//     //string model; --錯誤的寫法。必須明確實作介面屬性的方法
//     /*屬性宣告的完整寫法
//     private string _model = "";
//     string IProduct.model {
//         get {
//             return _model;
//         }
//         set {
//             _model = value;
//         }
//     }
//     */
//     public string model { get; set; } //語法糖。與介面的 string model { get; set; } 是不一樣的意思 
//     public decimal price { get; set; } //售價
//     public decimal TaxRate; //稅率
//     public int Size;
//     public decimal Tax()
//     {
//         return price * TaxRate;
//     }

// }

// // 我們可以將共同項目訂為抽象(abstract)類別，由子類別繼承(extends)並將須自行定義的動作設為抽象(abstract)方法，由子類別自行覆寫(override)
// /*
// 補充：
// 1.抽象類別不能實作。因為設計邏輯上屬於一個未完整的類別。
// 2.抽象類別中可以定義抽象方法但不能實作且必須為公開(public)。因為這部份是要開放給子類別複寫的。
// 3.抽象類別可以繼承抽象類別，但是一樣不能實作抽象方法
// 4.繼承抽象類別的子類必須複寫父類的的抽象方法
// */
// abstract class Car : IProduct
// {
//     public string model { get; set; } //型號
//     public int CC;  //CC數
//     public decimal price { get; set; } //售價
//     public decimal TaxRate; //稅率


//     public string Start()
//     {
//         return $"引擎啟動";
//     }

//     abstract public string run();

//     public decimal Tax()
//     {
//         return price * TaxRate;
//     }

// }

// //跑車
// // 跑車與機車都繼承了Car這個抽象類別並各自覆寫了run()這個方法之後，我們就可以更明確的區分跑車與機車之間的差異。
// class SportsCar : Car
// {
//     //乘客數
//     public int passengers = 4;
//     //幾門
//     public int Door = 4;

//     //跑車的類別上增加了openDoor()這個方法，並回傳為側開。但是少數車種開門方法是上掀式的，難道又要將跑車類別改成抽象類別然後再各自實作嗎？這顯然有點因噎廢食，這時候可以使用虛擬(virtual)方法。
//     //虛擬(virtual)方法可以實作並且允許繼承的子類別複寫
//     /*
//     補充：
//     1.虛擬方法必須實作。
//     2.虛擬方法中必須為公開(public)。因為允許子類別複寫。
//     3.子類別可以直接引用或選擇複寫(override)虛擬方法
//     */
//     public virtual string openDoor()
//     {
//         return "側開";
//     }

//     public override string run()
//     {
//         return $"{model} 跑超快";
//     }
// }

// //上開車款
// class SpecialSportsCar : SportsCar
// {
//     public override string openDoor()
//     {
//         return "上開";
//     }

// }

// //機車
// // 跑車與機車都繼承了Car這個抽象類別並各自覆寫了run()這個方法之後，我們就可以更明確的區分跑車與機車之間的差異。
// class motorcycle : Car
// {
//     //乘客數
//     public int passengers = 2;
//     public override string run()
//     {
//         return $"{model} 跑不快";
//     }
// }