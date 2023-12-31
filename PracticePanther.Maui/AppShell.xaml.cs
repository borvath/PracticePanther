﻿using Microsoft.Maui.Controls;
using PracticePanther.Maui.Views;
using PracticePanther.Maui.Views.BillViews;
using PracticePanther.Maui.Views.ClientViews;
using PracticePanther.Maui.Views.EmployeeViews;
using PracticePanther.Maui.Views.ProjectViews;
using PracticePanther.Maui.Views.TimeViews;
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
		Routing.RegisterRoute(nameof(BillBuilderPage), typeof(BillBuilderPage));

		Routing.RegisterRoute(nameof(ClientDisplayPage), typeof(ClientDisplayPage));
		Routing.RegisterRoute(nameof(ProjectDisplayPage), typeof(ProjectDisplayPage));
		Routing.RegisterRoute(nameof(EmployeeDisplayPage), typeof(EmployeeDisplayPage));
		Routing.RegisterRoute(nameof(TimeDisplayPage), typeof(TimeDisplayPage));
	}
}
