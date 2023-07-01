using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels;

namespace PracticePanther.Maui.Views; 
public partial class ProjectBuilderPage : ContentPage {
	public ProjectBuilderPage(ProjectBuilderViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void AddClicked(object sender, EventArgs eventArgs) {
		((ProjectBuilderViewModel)BindingContext).AddOrUpdateProject();
		Shell.Current.GoToAsync("..");
	}
}
