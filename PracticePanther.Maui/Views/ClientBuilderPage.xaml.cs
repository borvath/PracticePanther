using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels;

namespace PracticePanther.Maui.Views; 
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
