// using System;
// /* 
// 定義命名空間 (namespace) Demo
// */
// namespace CsharpStudy;
// /*
// 介面宣告方法，不實作，像是一組規格(只出張嘴的老闆)。而class實作介面，就把介面裡的method實作出來。
// class可以實作多個方法，但只能繼承一個類別(單一繼承機制)。
// interface就有抽象的概念，假如有好幾個class有相同的method，這時可以定義interface讓class去繼承，
// 你問為何不定義父class讓子class繼承?我想是因為定義父class要實作出method，
// 但這些method在各子class都有自己的邏輯，所以一開始用interface就不用實作。
// 還有一個功能也是實現抽象概念:abstract class
// */
// interface IShape
// {
//     double GetPerimeter();
//     double GetArea();
// }
// // Circle實作Shape
// class Circle : IShape
// {
//     //欄位
//     public double radius;
//     //建構函式
//     public Circle(double r)
//     {
//         radius = r;
//     }
//     //method
//     public double GetPerimeter()
//     {
//         return 2 * radius * 3.14;
//     }
//     public double GetArea()
//     {
//         return 3.14 * radius * radius;
//     }
// }
// class InterfaceTang
// {
//     static void Main(string[] args)
//     {
//         Circle c = new Circle(10.0);
//         Console.WriteLine(c.GetPerimeter());
//         Console.WriteLine(c.GetArea());
//         IShape shapeobject;
//         shapeobject = new Circle(10.0);
//         Console.WriteLine(shapeobject.GetPerimeter());
//     }
// }