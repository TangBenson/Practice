
// String filename = @"C:\Users\benson922\Documents\Github_Mine\TangProj\CsharpStudy\content\Filex.txt";

// #region 讀檔
// // using (StreamReader sr = new StreamReader(filename))
// // {
// // 	string? line;
// // 	while ((line = sr.ReadLine()) != null)
// // 	{
// // 		Console.WriteLine(line);
// // 	}
// // }
// #endregion

// #region 讀檔 async
// // Char[] buffer;
// // using (var sr = new StreamReader(filename))
// // {
// // 	Console.WriteLine((int)sr.BaseStream.Length);
// // 	buffer = new Char[(int)sr.BaseStream.Length];
// // 	await sr.ReadAsync(buffer, 0, (int)sr.BaseStream.Length);
// // }

// // Console.WriteLine(new String(buffer));
// #endregion


// #region  StringReader(讀取字串)
// string message=@"Characters in 1st line to read
// and 2nd line
// and the end";

// using (StringReader reader = new StringReader(message))
// {
// 	string readText = await reader.ReadToEndAsync();
// 	Console.WriteLine(readText);
// }
// #endregion


// #region MemoryStream
// /*
// 有時候我們在處理資料的時候，就會產生所謂的Stream，但我們不想保存資料在實體位置的時候，就需要把資料暫存在memory裡面，等用戶端保存後此memory就要清空，相關範例在web api下載檔案的部份使用到很多
// */


// #endregion