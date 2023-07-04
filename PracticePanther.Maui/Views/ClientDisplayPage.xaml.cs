using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels;

namespace PracticePanther.Maui.Views; 
public partial class ClientDisplayPage : ContentPage {
	public ClientDisplayPage(ClientDisplayViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
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
