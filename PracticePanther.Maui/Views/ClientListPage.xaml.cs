using System;
using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels;

namespace PracticePanther.Maui.Views; 
public partial class ClientListPage : ContentPage {
	public ClientListPage(ClientListViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	private void SearchTextChanged(object sender, EventArgs e) {
		var searchBar = (SearchBar)sender;
		((ClientListViewModel)BindingContext).GetSearchResults(searchBar.Text);
	}
	private void AddClicked(object sender, EventArgs e) {
		Shell.Current.GoToAsync(nameof(ClientBuilderPage));
	}
	private void EditClicked(object sender, EventArgs e) {
		((ClientListViewModel)BindingContext).EditClient(Shell.Current);
	}
	private void DeleteClicked(object sender, EventArgs e) {
		((ClientListViewModel)BindingContext).DeleteClient();
	}
	private void ClientSelected(object sender, EventArgs e) {
		
	}
	private void DisplayClient(object sender, EventArgs e) {
		((ClientListViewModel)BindingContext).DisplayClient(Shell.Current);
	}
	protected override void OnAppearing() {
		((ClientListViewModel)BindingContext).RefreshView();
	}
}
