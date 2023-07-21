using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels.BillViewModels;

namespace PracticePanther.Maui.Views.BillViews; 
public partial class BillBuilderPage : ContentPage {
	public BillBuilderPage(BillBuilderViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void AddClicked(object sender, EventArgs eventArgs) {
		((BillBuilderViewModel)BindingContext).AddBill();
		Shell.Current.GoToAsync("..");
	}
	private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e) {
		((BillBuilderViewModel)BindingContext).SelectionChanged(e);
	}
}

