using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels.ProjectViewModels;

namespace PracticePanther.Maui.Views.ProjectViews; 

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
		Shell.Current.GoToAsync(nameof(ProjectBuilderPage), new Dictionary<string, object> {
			{"ProjectId", "-1"}
		});
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

