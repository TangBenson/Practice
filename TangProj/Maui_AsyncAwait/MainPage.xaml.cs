namespace Maui_AsyncAwait;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		// SemanticScreenReader.Announce(CounterBtn.Text); //讓螢幕報讀軟體讀出傳入該函式的文字，以便視障者透過聽覺來了解目前的狀況，所以對我沒用
	}
}

