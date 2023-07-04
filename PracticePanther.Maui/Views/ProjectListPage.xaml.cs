using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels;

namespace PracticePanther.Maui.Views; 
public partial class ProjectListPage : ContentPage {
	public ProjectListPage(ProjectListViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void SearchTextChanged(object sender, EventArgs e) {
		var searchBar = (SearchBar)sender;
		((ProjectListViewModel)BindingContext).GetSearchResults(searchBar.Text);
	}
	private void AddClicked(object? sender, EventArgs eventArgs) {
		((ProjectListViewModel)BindingContext).AddProject(Shell.Current);
	}
	private void EditClicked(object? sender, EventArgs eventArgs) {
		((ProjectListViewModel)BindingContext).EditProject(Shell.Current);
	}
	private void DeleteClicked(object? sender, EventArgs eventArgs) {
		((ProjectListViewModel)BindingContext).DeleteProject();
	}
	private void DisplayClicked(object? sender, EventArgs eventArgs) {
		((ProjectListViewModel)BindingContext).DisplayProject(Shell.Current);
	}
	protected override void OnAppearing() {
		((ProjectListViewModel)BindingContext).RefreshView();
	}
}

