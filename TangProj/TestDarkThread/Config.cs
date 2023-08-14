using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestDarkThread
{
    public class Config
    {
        IConfiguration config;
        LineNotifySettings Settings => config.Get<LineNotifySettings>(); // 委派結合 Lambda用法，再研究
        string settingFilePath;
        const string settingFileName = "line-notify-settings.json";
        public Config(string basePath = null!)
        {
            // basePath若是 null時給 AppContext.BaseDirectory的值
            // AppContext.BaseDirectory取得執行檔所在位置
            basePath = basePath ?? AppContext.BaseDirectory;
            settingFilePath = Path.Combine(basePath, settingFileName);
            if (!File.Exists(settingFilePath))
            {
                Save(new LineNotifySettings());
            }
            config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(settingFileName, optional: false, reloadOnChange: true)
                .Build();
        }

        public string ClientId => Settings.ClientId; // 宣告 Lambda函式 ClientId
        public string Secret => Settings.Secret; // 宣告 Lambda函式 Secret
        public bool IsSet => !string.IsNullOrEmpty(ClientId) && !string.IsNullOrEmpty(Secret); // 宣告 Lambda函式 IsSet，但為何不寫!string.IsNullOrEmpty(Settings.ClientId)就好?
        public void SetAuthIdentity(string clientId, string secret)
        {
            /*
            lock：參數 config是要鎖定的物件，代表這段程式碼區塊只會被單一執行緒訪問。當一個執行緒進入 lock 區塊時，其他執行緒將會等待，
            直到第一個執行緒離開 lock 區塊，其他執行緒才能進入。通常用於多執行緒環境下，需要對特定資源或物件做同步訪問，避免競爭條件或死結的情況。
            */
            lock (config)
            {
                var settings = Settings;
                settings.ClientId = clientId;
                settings.Secret = secret;
                Save(settings);
            }
        }

        public string[] ListUsers() => Settings.Tokens.Keys.ToArray(); // 這又是啥寫法

        public string GetAccessToken(string userName) =>
            Settings.Tokens.ContainsKey(userName) ?
            Settings.Tokens[userName] : null!;

        public void SaveAccessToken(string userName, string token)
        {
            lock (config)
            {
                var settings = Settings;
                if (settings.Tokens.ContainsKey(userName))
                    throw new ApplicationException($"Token [{userName}] already exists");
                settings.Tokens.Add(userName, token);
                Save(settings);
            }
        }
        public void Save(LineNotifySettings settings)
        {
            // WriteAllText建立新檔案，將內容寫入檔案，然後關閉檔案。 如果檔案已經存在，則會覆寫該檔案。
            File.WriteAllText(settingFilePath,
                JsonSerializer.Serialize(settings, new JsonSerializerOptions
                {
                    WriteIndented = true // 取得或設定值，這個值表示 JSON 是否應該使用美化列印。 根據預設，JSON 會序列化，而不會有任何額外的空白字元。
                }));
        }
    }
    public class LineNotifySettings
    {
        public string ClientId { get; set; } = null!; // null!，表示該變數不會為 null，也就是告訴編譯器該變數一定有值，不需要再進行額外的null判斷
        public string Secret { get; set; } = null!;
        public Dictionary<string, string> Tokens { get; set; } = new Dictionary<string, string>();
    }
}