using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels;

namespace PracticePanther.Maui.Views; 
public partial class ProjectDisplayPage : ContentPage {
	public ProjectDisplayPage(ProjectDisplayViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	protected override void OnAppearing() {
		((ProjectDisplayViewModel)BindingContext).RefreshView();
	}
}