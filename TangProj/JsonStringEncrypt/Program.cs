//這樣寫會錯：最上層陳述式必須在命名空間和型別宣告之前，要改用自己寫傳統的 main method
// public class Person
// {
//     public string Name { get; set; }
//     public int Age { get; set; }
// }

// Person jsonObject = new Person
// {
//     Name = "John Doe",
//     Age = 30
// };

using System;
using System.Dynamic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace Program
{
    class Program
    {
        /// <summary>
        /// ↓自己決定，可以的話，最好使用  RNGCryptoServiceProvider 來產生key 和 IV
        /// </summary>
        private static string myKey = "1234567812345678";//必須至少16個字元，最大32個字元，因為使用AES演算法，可以的話，最好給32個字比較安全
        private static string myIV = "1234567812345678";//必須至少16個字元，最大32個字元，因為使用AES演算法，可以的話，最好給32個字比較安全

        static void Main(string[] args)
        {
            //強行別宣告
            Person jsonObject = new Person
            {
                Name = "Benson Tang",
                Age = 33
            };
            Console.WriteLine($"Name: {jsonObject.Name}, Age: {jsonObject.Age}");
            string a = System.Text.Json.JsonSerializer.Serialize(jsonObject); //物件轉string
            Console.WriteLine(a);
            Person? jsonObject2 = System.Text.Json.JsonSerializer.Deserialize<Person>(a); //string轉物件
            Console.WriteLine(jsonObject2.Name);
            Console.WriteLine();

            //動態型別宣告
            dynamic djsonObject = new ExpandoObject();
            djsonObject.Name = "John Wick";
            djsonObject.Age = 45;
            Console.WriteLine($"Name: {djsonObject.Name}, Age: {djsonObject.Age}");
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(djsonObject));
            Console.WriteLine();

            //加密
            string base64String = Encrypt(myKey, myIV, a);
            Console.WriteLine($@"加密後:{base64String}");
            //解密
            string decrypt_text = Decrypt(myKey, myIV, base64String);
            Console.WriteLine($@"解密後:{decrypt_text}");
        }


        /// <summary>
        /// 驗證key和iv的長度(AES只有三種長度適用)
        /// </summary>
        private static void Validate_KeyIV_Length(string key, string iv)
        {
            //驗證key和iv都必須為128bits或192bits或256bits
            List<int> LegalSizes = new List<int>() { 128, 192, 256 };
            int keyBitSize = Encoding.UTF8.GetBytes(key).Length * 8;
            int ivBitSize = Encoding.UTF8.GetBytes(iv).Length * 8;
            if (!LegalSizes.Contains(keyBitSize) || !LegalSizes.Contains(ivBitSize))
            {
                throw new Exception($@"key或iv的長度不在128bits、192bits、256bits其中一個，輸入的key bits:{keyBitSize},iv bits:{ivBitSize}");
            }
        }
        /// <summary>
        /// 加密後回傳base64String，相同明碼文字編碼後的base64String結果會相同(類似雜湊)，除非變更key或iv
        /// 如果key和iv忘記遺失的話，資料就解密不回來
        /// base64String若使用在Url的話，Web端記得做UrlEncode
        /// </summary>
        public static string Encrypt(string key, string iv, string plain_text)
        {
            Validate_KeyIV_Length(key, iv);
            Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;//非必須，但加了較安全
            aes.Padding = PaddingMode.PKCS7;//非必須，但加了較安全

            ICryptoTransform transform = aes.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            byte[] bPlainText = Encoding.UTF8.GetBytes(plain_text);//明碼文字轉byte[]
            byte[] outputData = transform.TransformFinalBlock(bPlainText, 0, bPlainText.Length);//加密
            return Convert.ToBase64String(outputData);
        }
        /// <summary>
        /// 解密後，回傳明碼文字
        /// </summary>
        public static string Decrypt(string key, string iv, string base64String)
        {
            Validate_KeyIV_Length(key, iv);
            Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;//非必須，但加了較安全
            aes.Padding = PaddingMode.PKCS7;//非必須，但加了較安全

            ICryptoTransform transform = aes.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
            byte[] bEnBase64String = null;
            byte[] outputData = null;
            try
            {
                bEnBase64String = Convert.FromBase64String(base64String);//有可能base64String格式錯誤
                outputData = transform.TransformFinalBlock(bEnBase64String, 0, bEnBase64String.Length);//有可能解密出錯
            }
            catch (Exception ex)
            {
                //todo 寫Log
                throw new Exception($@"解密出錯:{ex.Message}");
            }

            //解密成功
            return Encoding.UTF8.GetString(outputData);

        }
    }

    public class Person
    {
        [JsonPropertyName("Name2")] //轉 json時改命名為 Name2
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}