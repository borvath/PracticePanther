using Microsoft.Maui.Controls;
using PracticePanther.Maui.Views;
namespace PracticePanther.Maui;
public partial class AppShell : Shell {
	public AppShell() {
		InitializeComponent();
		Routing.RegisterRoute(nameof(ClientListPage), typeof(ClientListPage));
		Routing.RegisterRoute(nameof(ProjectListPage), typeof(ProjectListPage));
		Routing.RegisterRoute(nameof(EmployeeListPage), typeof(EmployeeListPage));
		Routing.RegisterRoute(nameof(TimeListPage), typeof(TimeListPage));
		
		Routing.RegisterRoute(nameof(ClientBuilderPage), typeof(ClientBuilderPage));
		Routing.RegisterRoute(nameof(ProjectBuilderPage), typeof(ProjectBuilderPage));
		Routing.RegisterRoute(nameof(EmployeeBuilderPage), typeof(EmployeeBuilderPage));
		Routing.RegisterRoute(nameof(TimeBuilderPage), typeof(TimeBuilderPage));

		Routing.RegisterRoute(nameof(ClientDisplayPage), typeof(ClientDisplayPage));
		Routing.RegisterRoute(nameof(ProjectDisplayPage), typeof(ProjectDisplayPage));
		Routing.RegisterRoute(nameof(EmployeeDisplayPage), typeof(EmployeeDisplayPage));
		Routing.RegisterRoute(nameof(TimeDisplayPage), typeof(TimeDisplayPage));
	}
}
