using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using PracticePanther.Maui.ViewModels.BillViewModels;
using PracticePanther.Maui.ViewModels.ClientViewModels;
using PracticePanther.Maui.ViewModels.EmployeeViewModels;
using PracticePanther.Maui.ViewModels.ProjectViewModels;
using PracticePanther.Maui.ViewModels.TimeViewModels;
using PracticePanther.Maui.Views;
using PracticePanther.Maui.Views.BillViews;
using PracticePanther.Maui.Views.ClientViews;
using PracticePanther.Maui.Views.EmployeeViews;
using PracticePanther.Maui.Views.ProjectViews;
using PracticePanther.Maui.Views.TimeViews;

namespace PracticePanther.Maui;
public static class MauiProgram {
	public static MauiApp CreateMauiApp() {
		MauiAppBuilder builder = MauiApp.CreateBuilder();
		builder.UseMauiApp<App>().ConfigureFonts(fonts => {
			fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
		});
		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddSingleton<ClientListPage>();
		builder.Services.AddSingleton<ClientListViewModel>();
		builder.Services.AddTransient<ClientBuilderPage>();
		builder.Services.AddTransient<ClientBuilderViewModel>();
		builder.Services.AddTransient<ClientDisplayPage>();
		builder.Services.AddTransient<ClientDisplayViewModel>();

		builder.Services.AddSingleton<ProjectListPage>();
		builder.Services.AddSingleton<ProjectListViewModel>();
		builder.Services.AddTransient<ProjectBuilderPage>();
		builder.Services.AddTransient<ProjectBuilderViewModel>();
		builder.Services.AddTransient<ProjectDisplayPage>();
		builder.Services.AddTransient<ProjectDisplayViewModel>();

		builder.Services.AddSingleton<EmployeeListPage>();
		builder.Services.AddSingleton<EmployeeListViewModel>();
		builder.Services.AddTransient<EmployeeBuilderPage>();
		builder.Services.AddTransient<EmployeeBuilderViewModel>();
		builder.Services.AddTransient<EmployeeDisplayPage>();
		builder.Services.AddTransient<EmployeeDisplayViewModel>();

		builder.Services.AddSingleton<TimeListPage>();
		builder.Services.AddSingleton<TimeListViewModel>();
		builder.Services.AddTransient<TimeBuilderPage>();
		builder.Services.AddTransient<TimeBuilderViewModel>();
		builder.Services.AddTransient<TimeDisplayPage>();
		builder.Services.AddTransient<TimeDisplayViewModel>();
		
		builder.Services.AddTransient<BillBuilderPage>();
		builder.Services.AddTransient<BillBuilderViewModel>();
#if DEBUG
		builder.Logging.AddDebug();
#endif
		return builder.Build();
	}
}
