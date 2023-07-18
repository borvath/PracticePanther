using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels.ClientViewModels;
namespace PracticePanther.Maui.Views.ClientViews; 
public partial class ClientDisplayPage : ContentPage {
	public ClientDisplayPage(ClientDisplayViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void DisplayProjectClicked(object sender, EventArgs eventArgs) {
		((ClientDisplayViewModel)BindingContext).DisplayProject(Shell.Current);
	}
	private void CloseProjectClicked(object sender, EventArgs eventArgs) {
		((ClientDisplayViewModel)BindingContext).CloseProject();
	}
	private void CloseClientClicked(object sender, EventArgs eventArgs) {
		((ClientDisplayViewModel)BindingContext).CloseClient();
	}
	protected override void OnAppearing() {
		((ClientDisplayViewModel)BindingContext).RefreshView();
	}
}
