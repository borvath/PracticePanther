using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels.TimeViewModels;
namespace PracticePanther.Maui.Views.TimeViews; 
public partial class TimeBuilderPage : ContentPage {
	public TimeBuilderPage(TimeBuilderViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void AddClicked(object sender, EventArgs eventArgs) {
		((TimeBuilderViewModel)BindingContext).AddTime();
		Shell.Current.GoToAsync("..");
	}
	private void ClientChanged(object sender, EventArgs eventArgs) {
		((TimeBuilderViewModel)BindingContext).RefreshView();
	}
}
