// using System;
// using System.Net.Mail;

// namespace CsharpStudy;
// class SendEmail
// {
//     static void Main(string[] args)
//     {
//         try
//         {
//             System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
//             msg.To.Add("benson922@hotaimotor.com.tw");
//             //msg.To.Add("b@b.com");可以發送給多人
//             //msg.CC.Add("c@c.com");
//             //msg.CC.Add("c@c.com");可以抄送副本給多人 
//             //這裡可以隨便填，不是很重要
//             msg.From = new MailAddress("nerodmd@gmail.com", "小魚", System.Text.Encoding.UTF8);
//             /* 上面3個參數分別是發件人地址（可以隨便寫），發件人姓名，編碼*/
//             msg.Subject = "測試標題";//郵件標題
//             msg.SubjectEncoding = System.Text.Encoding.UTF8;//郵件標題編碼
//             msg.Body = "測試一下"; //郵件內容
//             msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
//                                                          // msg.Attachments.Add(new Attachment(@"D:\test2.docx"));  //附件
//             msg.IsBodyHtml = true;//是否是HTML郵件 
//                                   //msg.Priority = MailPriority.High;//郵件優先級 

//             SmtpClient client = new SmtpClient();
//             client.Credentials = new System.Net.NetworkCredential("nerodmd@gmail.com", "我的gmail密碼"); //這裡要填正確的帳號跟密碼
//             client.Host = "smtp.gmail.com"; //設定smtp Server
//             client.Port = 25; //設定Port
//             client.EnableSsl = true; //gmail預設開啟驗證
//             client.Send(msg); //寄出信件
//             client.Dispose();
//             msg.Dispose();
//             // MessageBox.Show(this, "郵件寄送成功！");
//         }
//         catch (Exception ex)
//         {
//             // MessageBox.Show(this, ex.Message);
//         }
//     }
// }