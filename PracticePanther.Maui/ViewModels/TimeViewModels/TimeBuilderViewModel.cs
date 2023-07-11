using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
namespace PracticePanther.Maui.ViewModels.TimeViewModels;

public class TimeBuilderViewModel : INotifyPropertyChanged, IQueryAttributable {
	public List<Client> Clients { get; set; } = new List<Client>(ClientService.Current.Clients);
	public Client? SelectedClient { get; set; }
	public List<Project>? Projects { get; set; } = new List<Project>();
	public Project? SelectedProject { get; set; }
	public List<Employee> Employees { get; set; } = new List<Employee>(EmployeeService.Current.Employees);
	public Employee? SelectedEmployee { get; set; }
	
	public double Hours { get; set; }
	public DateTime? Date { get; set; }
	public string? Narrative { get; set; }
	
	private int timeId;

	public void AddTime() {
		if (SelectedClient != null && SelectedProject != null && SelectedEmployee != null) {
			if (timeId == -1) {
				TimeService.Current.AddTime(new Time(SelectedEmployee.Id, Hours, Date ?? DateTime.Today, Narrative ?? "Default Entry Notes"), SelectedProject);
			}
			else {
				foreach (Time t in TimeService.Current.Times.Where(t => t.Id == timeId)) {
					t.ClientId = SelectedClient.Id;
					t.ProjectId = SelectedProject.Id;
					t.EmployeeId = SelectedEmployee.Id;
					t.Hours = Hours;
					t.Date = Date;
					t.Narrative = Narrative;
				}
			}
		}
	}
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["TimeId"] as string), out timeId);
		
		if (timeId == -1) {
			Hours = 0;
			Date = DateTime.Today;
			Narrative = "Default Entry Notes";
		}
		else {
			foreach (Time t in TimeService.Current.Times.Where(t => t.Id == timeId)) {
				SelectedClient = Clients.Find(c => c.Id == t.ClientId);
				Projects = ProjectService.Current.Projects.Where(p => p.ClientId == SelectedClient?.Id).ToList();
				SelectedProject = Projects?.Find(p => p.Id == t.ProjectId);
				SelectedEmployee = Employees.Find(e => e.Id == t.EmployeeId);
				Hours = t.Hours;
				Date = t.Date ?? DateTime.Today;
				Narrative = t.Narrative ?? "Default Entry Notes";
			}
		}
		NotifyPropertyChanged(nameof(SelectedClient));
		NotifyPropertyChanged(nameof(Projects));
		NotifyPropertyChanged(nameof(SelectedProject));
		NotifyPropertyChanged(nameof(SelectedEmployee));
		NotifyPropertyChanged(nameof(Hours));
		NotifyPropertyChanged(nameof(Date));
		NotifyPropertyChanged(nameof(Narrative));
	}
	public void RefreshView() {
		if (SelectedClient != null)
			Projects = new List<Project>(ProjectService.Current.Projects.Where(p => p.ClientId == SelectedClient.Id));
		NotifyPropertyChanged(nameof(Projects));
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
