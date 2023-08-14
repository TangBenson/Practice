// using System;
// using System.Drawing;
// using System.IO;

// /* 
// 定義命名空間 (namespace) Demo
// */
// namespace CsharpStudy;
// class Base64Toimg
// {
//     static void Main(string[] args)
//     {
//         Base64StringToImage a = new Base64StringToImage("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");

//         Image img = Image.FromHbitmap(a.GetHbitmap());
//         pictureBox1.Image = img;
//         pictureBox1.Show();
//         pictureBox1.Refresh();

//     }

//     public static Image Base64StringToImage(string base64String)
//     {
//         // Convert base 64 string to byte[]
//         byte[] Buffer = Convert.FromBase64String(base64String);

//         byte[] data = null;
//         Image oImage = null;
//         MemoryStream oMemoryStream = null;
//         Bitmap oBitmap = null;
//         //建立副本
//         data = (byte[])Buffer.Clone();
//         try
//         {
//             oMemoryStream = new MemoryStream(data);
//             //設定資料流位置
//             oMemoryStream.Position = 0;
//             oImage = System.Drawing.Image.FromStream(oMemoryStream);
//             //建立副本
//             oBitmap = new Bitmap(oImage);
//         }
//         catch
//         {
//             throw;
//         }
//         finally
//         {
//             oMemoryStream.Close();
//             oMemoryStream.Dispose();
//             oMemoryStream = null;
//         }
//         //return oImage;
//         return oBitmap;
//     }
// }