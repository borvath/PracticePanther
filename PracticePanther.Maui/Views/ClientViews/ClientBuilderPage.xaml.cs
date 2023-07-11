using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels.ClientViewModels;
namespace PracticePanther.Maui.Views.ClientViews; 
public partial class ClientBuilderPage : ContentPage {
	public ClientBuilderPage(ClientBuilderViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void AddClicked(object sender, EventArgs eventArgs) {
		((ClientBuilderViewModel)BindingContext).AddOrUpdateClient();
		Shell.Current.GoToAsync("..");
	}
}
