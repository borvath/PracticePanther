﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using PracticePanther.Maui.ViewModels;
using PracticePanther.Maui.Views;

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

		builder.Services.AddTransient<ProjectBuilderPage>();
		builder.Services.AddTransient<ProjectBuilderViewModel>();

		builder.Services.AddSingleton<EmployeeListPage>();
		builder.Services.AddSingleton<EmployeeListViewModel>();
		builder.Services.AddTransient<EmployeeBuilderPage>();
		builder.Services.AddTransient<EmployeeBuilderViewModel>();

		builder.Services.AddSingleton<TimeListPage>();
		builder.Services.AddSingleton<TimeListViewModel>();
		builder.Services.AddTransient<TimeBuilderPage>();
		builder.Services.AddTransient<TimeBuilderViewModel>();
#if DEBUG
		builder.Logging.AddDebug();
#endif
		return builder.Build();
	}
}