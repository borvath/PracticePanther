using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels;

namespace PracticePanther.Maui.Views; 
public partial class ProjectDisplayPage : ContentPage {
	public ProjectDisplayPage(ProjectDisplayViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void AddClicked(object? sender, EventArgs eventArgs) {
		((ProjectDisplayViewModel)BindingContext).AddTime(Shell.Current);
	}
	private void EditClicked(object? sender, EventArgs eventArgs) {
		((ProjectDisplayViewModel)BindingContext).EditTime(Shell.Current);
	}
	private void DeleteClicked(object? sender, EventArgs eventArgs) {
		((ProjectDisplayViewModel)BindingContext).DeleteTime();
	}
	private void DisplayClicked(object? sender, EventArgs eventArgs) {
		((ProjectDisplayViewModel)BindingContext).DisplayTime(Shell.Current);
	}
	protected override void OnAppearing() {
		((ProjectDisplayViewModel)BindingContext).RefreshView();
	}
}