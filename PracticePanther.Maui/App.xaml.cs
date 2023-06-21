using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace PracticePanther.Maui;

public partial class App : Application {
	public App() {
		InitializeComponent();
		MainPage = new AppShell();
	}
	protected override Window CreateWindow(IActivationState? activationState)
	{
		Window window = base.CreateWindow(activationState);
		window.MinimumWidth = 800;
		window.MinimumHeight = 680;
		window.Width = 960;
		window.Height = 800;
		window.MaximumWidth = 1580;
		window.MaximumHeight = 12280;
		return window;
	}
}
