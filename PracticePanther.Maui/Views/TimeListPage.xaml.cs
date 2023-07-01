using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels;

namespace PracticePanther.Maui.Views; 
public partial class TimeListPage : ContentPage {
	public TimeListPage(TimeListViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void SearchTextChanged(object sender, EventArgs e) {
		var searchBar = (SearchBar)sender;
		((TimeListViewModel)BindingContext).GetSearchResults(searchBar.Text);
	}
	private void AddClicked(object sender, EventArgs e) {
		Shell.Current.GoToAsync(nameof(TimeBuilderPage), new Dictionary<string, object>{{"EmployeeId", "-1"}, {"ProjectId", "-1"}});
	}
	private void EditClicked(object sender, EventArgs e) {
		((TimeListViewModel)BindingContext).EditTime(Shell.Current);
	}
	private void DeleteClicked(object sender, EventArgs e) {
		((TimeListViewModel)BindingContext).DeleteTime();
	}
	protected override void OnAppearing() {
		((TimeListViewModel)BindingContext).RefreshView();
	}
}
