using System;
using System.IO;
using System.Net;

namespace FtpDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            // 這方法一直權限錯誤，媽的
            // int a = Download(@"\\fiyhi02\HI-MS\HIMS企劃管理部\業績日報交接檔案\【2021年度日報】\1月\0101\iRent原始檔0101(0107).xls", @"C:\Users\benson922\Desktop\FtpDownload\");
            // Console.WriteLine(a);

            

            string folder = @"C:\TangTest\";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string[] monthList = Directory.GetFileSystemEntries(@"\\fiyhi02\HI-MS\HIMS企劃管理部\業績日報交接檔案\【2021年度日報】");
            try
            {
                foreach (string mfile in monthList)
                {
                    string[] fileList = Directory.GetFileSystemEntries(mfile);
                    foreach (string dfile in fileList)
                    {
                        // Console.WriteLine(dfile);
                        string[] fileList2 = Directory.GetFileSystemEntries($"{dfile}");
                        foreach (string file in fileList2)
                        {
                            if(file.Contains("日報作業檔")){
                                System.IO.File.Copy($@"{dfile}\{System.IO.Path.GetFileName(file)}", $@"{folder}\{System.IO.Path.GetFileName(file)}", true);
                            }                     
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                Console.WriteLine(s);
            }
        }

        //從共享資料夾下載  
        public static int Download(string server_path, string local_path)
        {
            try
            {
                // server_path = @"\\" + server_path;
                Console.WriteLine(local_path);
                string folder = local_path.Substring(0, local_path.LastIndexOf("\\"));
                Console.WriteLine(folder);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                if (!File.Exists(server_path))
                {
                    return 0;
                }
                if (!Directory.Exists(folder))
                {
                    return 1;
                }
                //伺服器檔案資訊 
                FileInfo serverInfo = new FileInfo(server_path);

                serverInfo.MoveTo(folder, true);
                return 2;

                // DateTime serverTime = serverInfo.LastWriteTime;
                // if (File.Exists(local_path))
                // {
                //     FileInfo localinfo = new FileInfo(local_path);
                //     DateTime localTime = localinfo.LastWriteTime;
                //     if (localTime >= serverTime)
                //         return 1;
                // }

            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return 3;
            }
        }

        //從ftp伺服器上下載檔案的功能 
        public void Download(string ftpServerIP, string ftpUserID, string ftpPassword, string fileName, string Destination)
        {
            FtpWebRequest reqFTP;
            try
            {
                FileStream outputStream = new FileStream(Destination + "\\" + fileName, FileMode.Create);
                // 根據uri建立FtpWebRequest物件  
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName));
                // 指定執行什麼命令 
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                // 預設為true，連線不會被關閉 
                reqFTP.UseBinary = true;
                // ftp使用者名稱和密碼 
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse ftpresponse = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = ftpresponse.GetResponseStream();
                long cl = ftpresponse.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                ftpresponse.Close();
            }
            catch (Exception ex)
            {

            }
        }

    }
}
