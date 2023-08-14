namespace Maui_AsyncAwait;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell(); //指定了此應用程式的主頁是 AppShell，AppShell 就是應用程式的「外殼」，它定義了應用程式的起始畫面
	}
}
