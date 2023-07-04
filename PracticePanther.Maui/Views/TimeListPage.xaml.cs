﻿using System;
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
	private void AddClicked(object? sender, EventArgs eventArgs) {
		((TimeListViewModel)BindingContext).AddTime(Shell.Current);
	}
	private void EditClicked(object? sender, EventArgs eventArgs) {
		((TimeListViewModel)BindingContext).EditTime(Shell.Current);
	}
	private void DeleteClicked(object? sender, EventArgs eventArgs) {
		((TimeListViewModel)BindingContext).DeleteTime();
	}
	private void DisplayClicked(object? sender, EventArgs eventArgs) {
		((TimeListViewModel)BindingContext).DisplayTime(Shell.Current);
	}
	protected override void OnAppearing() {
		((TimeListViewModel)BindingContext).RefreshView();
	}
}
