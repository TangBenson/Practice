// using System;

// namespace DelegateEvent;

// /*
// Q：delegateTestA看起來和 Delegate.cs的 readKeyEvent功能一樣，為何readKeyEvent就是個事件？

// A：readKeyEvent 是一个事件。虽然它的声明看起来与 Delegate2 类中的 delegateTestA 委托变量相似，但它们有着不同的语义和用途。
// 在 C# 中，事件是一种特殊的委托用法，用于实现观察者模式和发布-订阅模式。事件允许类在特定条件下触发事件，
// 并允许其他类注册事件处理程序来响应事件的发生。事件提供了封装和安全性，使得外部代码无法直接赋值或调用事件委托，
// 而只能通过 += 和 -= 运算符订阅和取消订阅事件处理程序。
// 在您的代码中，readKeyEvent 声明为一个事件，使用了委托类型 MyFunction 作为事件的类型。在 Main 方法中，
// 我们可以看到使用 += 运算符将 myFunction1 方法订阅到 readKeyEvent 事件。这表示当 readKeyEvent 事件触发时，将调用 myFunction1 方法。
// 尽管 readKeyEvent 看起来与 delegateTestA 相似，但它们的语义是不同的：
//     *delegateTestA 是一个普通的委托变量，可以像普通方法一样直接调用它，并且可以通过赋值进行更改。
//     *readKeyEvent 是一个事件，不能直接调用它，而是通过事件触发来调用事件处理程序，且只能使用 += 和 -= 运算符来订阅和取消订阅事件处理程序。
// 因此，虽然 readKeyEvent 的声明和 delegateTestA 的声明都是委托类型，但 readKeyEvent 是一个事件，而 delegateTestA 是一个委托变量。
// */

// class Delegate2
// {
//     public delegate void WriteSometing(int number);
    
    
//     public static void PrintNumber(int number)
//     {
//         Console.WriteLine($"PrintNumber:{number}");
//     }
//     public static void SquareFunction(int number)
//     {
//         Console.WriteLine($"SquareFunction:{Math.Pow(number, 2)}");
//     }
//     public static void RadicalFunction(int number)
//     {
//         Console.WriteLine($"RadicalFunction:{Math.Sqrt(number)}");
//     }

//     static void Main(string[] args)
//     {
//         WriteSometing delegateTestA = new WriteSometing(PrintNumber);// 等同 WriteSometing delegateTestA = PrintNumber;
//         delegateTestA.Invoke(25); // 等同 delegateTestA(25);

//         //委派事件也可以把多個方法繫結再一起
//         WriteSometing delegateTestB = SquareFunction;
//         delegateTestB += RadicalFunction;
//         delegateTestB += PrintNumber;
//         delegateTestB += SquareFunction;
//         delegateTestB.Invoke(25);
//     }
// }