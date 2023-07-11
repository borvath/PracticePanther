using Microsoft.Maui.Controls;
using PracticePanther.Maui.ViewModels.ProjectViewModels;
namespace PracticePanther.Maui.Views.ProjectViews; 
public partial class ProjectDisplayPage : ContentPage {
	public ProjectDisplayPage(ProjectDisplayViewModel vm) {
		InitializeComponent();
		BindingContext = vm;
	}
	protected override void OnAppearing() {
		((ProjectDisplayViewModel)BindingContext).RefreshView();
	}
}