using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels;

namespace PracticePanther.Maui.Views; 
public partial class EmployeeListPage : ContentPage {
	public EmployeeListPage(EmployeeListViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void SearchTextChanged(object sender, EventArgs e) {
		var searchBar = (SearchBar)sender;
		((EmployeeListViewModel)BindingContext).GetSearchResults(searchBar.Text);
	}
	private void AddClicked(object sender, EventArgs e) {
		Shell.Current.GoToAsync("EmployeeBuilderPage");
	}
	private void EditClicked(object sender, EventArgs e) {
		((EmployeeListViewModel)BindingContext).EditEmployee(Shell.Current);
	}
	private void DeleteClicked(object sender, EventArgs e) {
		((EmployeeListViewModel)BindingContext).DeleteEmployee();
	}
	private void EmployeeSelected(object sender, EventArgs e) {
		
	}
	private void DisplayEmployee(object sender, EventArgs e) {
		((EmployeeListViewModel)BindingContext).DisplayEmployee(Shell.Current);
	}
	protected override void OnAppearing() {
		((EmployeeListViewModel)BindingContext).RefreshView();
	}
}
