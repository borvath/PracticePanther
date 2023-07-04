using System;
using Microsoft.Maui.Controls;
namespace PracticePanther.Maui.Views;

public partial class MainPage {

	public MainPage() {
		InitializeComponent();
	}
	private void GoToClientList(object sender, EventArgs eventArgs) {
		Shell.Current.GoToAsync(nameof(ClientListPage));
	}
	private void GoToProjectList(object sender, EventArgs eventArgs) {
		Shell.Current.GoToAsync(nameof(ProjectListPage));
	}
	private void GoToEmployeeList(object sender, EventArgs eventArgs) {
		Shell.Current.GoToAsync(nameof(EmployeeListPage));
	}
	private void GoToTimeList(object sender, EventArgs eventArgs) {
		Shell.Current.GoToAsync(nameof(TimeListPage));
	}
}
