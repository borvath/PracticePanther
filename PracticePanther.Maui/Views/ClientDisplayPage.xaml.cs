using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels;

namespace PracticePanther.Maui.Views; 
public partial class ClientDisplayPage : ContentPage {
	public ClientDisplayPage(ClientDisplayViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void AddClicked(object? sender, EventArgs eventArgs) {
		Shell.Current.GoToAsync(nameof(ProjectBuilderPage), new Dictionary<string, object>{{"ProjectId", "-1"}});
	}
	private void EditClicked(object? sender, EventArgs eventArgs) {
		((ClientDisplayViewModel)BindingContext).EditProject(Shell.Current);
	}
	private void DeleteClicked(object? sender, EventArgs eventArgs) {
		((ClientDisplayViewModel)BindingContext).DeleteProject();
	}
	private void DisplayClicked(object? sender, EventArgs eventArgs) {
		((ClientDisplayViewModel)BindingContext).DisplayProject(Shell.Current);
	}
}