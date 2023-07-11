using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels.EmployeeViewModels;
namespace PracticePanther.Maui.Views.EmployeeViews; 
public partial class EmployeeBuilderPage : ContentPage {
	public EmployeeBuilderPage(EmployeeBuilderViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void AddClicked(object sender, EventArgs eventArgs) {
		((EmployeeBuilderViewModel)BindingContext).AddOrUpdateEmployee();
		Shell.Current.GoToAsync("..");
	}
}

