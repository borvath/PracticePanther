﻿using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels.EmployeeViewModels;
namespace PracticePanther.Maui.Views.EmployeeViews; 
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
		Shell.Current.GoToAsync(nameof(EmployeeBuilderPage), new Dictionary<string, object>{{"EmployeeId", "-1"}});
	}
	private void EditClicked(object sender, EventArgs e) {
		((EmployeeListViewModel)BindingContext).EditEmployee(Shell.Current);
	}
	private void DeleteClicked(object sender, EventArgs e) {
		((EmployeeListViewModel)BindingContext).DeleteEmployee();
	}
	private void DisplayClicked(object sender, EventArgs e) {
		((EmployeeListViewModel)BindingContext).DisplayEmployee(Shell.Current);
	}
	protected override void OnAppearing() {
		((EmployeeListViewModel)BindingContext).RefreshView();
	}
}
