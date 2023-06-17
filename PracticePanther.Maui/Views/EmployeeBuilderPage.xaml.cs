using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels;

namespace PracticePanther.Maui.Views; 
public partial class EmployeeBuilderPage : ContentPage {
	public EmployeeBuilderPage(EmployeeBuilderViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void AddClicked(object sender, EventArgs eventArgs) {
		((EmployeeBuilderViewModel)BindingContext).AddEmployee();
		Shell.Current.GoToAsync("..");
	}
}

