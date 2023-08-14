// using System;
// using System.Collections.Generic;

// namespace CsharpStudy;
// class OutOfMemory
// {
//     static void Main(string[] args)
//     {
//         try
//         {
//             if (args.Length < 1)
//                 throw new ApplicationException("missing objType argument");
//             var objType = (ObjectTypes)Enum.Parse(typeof(ObjectTypes), args[0]);
//             Test_Use_All_Mem(objType);
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("ERROR-" + ex.Message);
//         }
//         Console.ReadLine();
//     }

//     static void DumpMemSize(string msg)
//     {
//         var memSz = GC.GetTotalMemory(false) / 1024 / 1024;
//         Console.WriteLine($"Managed Heap = {memSz}MB {msg}");
//     }
//     enum ObjectTypes { String, ByteArray }
//     static void Test_Use_All_Mem(ObjectTypes objType)
//     {
//         Func<int, byte[]> createBigArray = (mb) =>
//         {
//             return new byte[mb * 1024 * 1024];
//         };
//         var list = new List<object>();
//         System.Threading.Thread.Sleep(2000);
//         int i = 1;
//         try
//         {
//             while (true)
//             {
//                 if (objType == ObjectTypes.ByteArray)
//                     list.Add(createBigArray(2));
//                 else if (objType == ObjectTypes.String)
//                     //String stored as UCS-2 encoding, 2 bytes/char
//                     list.Add(new String('A', 1 * 1024 * 1024));
//                 else
//                     throw new NotImplementedException();
//                 i++;
//             }
//         }
//         catch (Exception ex)
//         {
//             DumpMemSize($"{i}*2MB {objType}");
//             Console.WriteLine("ERROR-" + ex.Message);
//         }
//     }
// }