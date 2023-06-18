using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels;

namespace PracticePanther.Maui.Views; 
public partial class TimeBuilderPage : ContentPage {
	public TimeBuilderPage(TimeBuilderViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void AddClicked(object sender, EventArgs eventArgs) {
		((TimeBuilderViewModel)BindingContext).AddTime();
		Shell.Current.GoToAsync("..");
	}
}

