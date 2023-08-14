namespace Maui_AsyncAwait;

//應用程式的進入點
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>() //註冊此應用程式，而應用程式的類別名稱是 "App"。這個 App 類別就是專案根目錄下的 App.xaml 以及與之關聯的 C# 檔案 App.xaml.cs
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}
