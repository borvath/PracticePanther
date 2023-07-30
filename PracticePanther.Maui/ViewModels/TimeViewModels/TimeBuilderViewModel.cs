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
	
	public List<Client> Clients { get; set; } = new List<Client>(ClientService.GetClients());
	public Client? SelectedClient { get; set; }
	public List<Project>? Projects { get; set; } = new List<Project>();
	public Project? SelectedProject { get; set; }
	public List<Employee> Employees { get; set; } = new List<Employee>(EmployeeService.Current.Employees);
	public Employee? SelectedEmployee { get; set; }
	
	public double Hours { get; set; }
	public DateTime Date { get; set; }
	public string? Narrative { get; set; }
	
	private int timeId;

	public void AddTime() {
		if (SelectedClient != null && SelectedProject != null && SelectedEmployee != null) {
			if (timeId == -1) {
				TimeService.Current.AddTime(new Time(SelectedEmployee.Id, Hours, Date, Narrative), SelectedProject);
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
		}
		else {
			foreach (Time t in TimeService.Current.Times.Where(t => t.Id == timeId)) {
				SelectedClient = Clients.Find(c => c.Id == t.ClientId);
				NotifyPropertyChanged(nameof(SelectedClient));
				Projects = ProjectService.Current.Projects.Where(p => p.ClientId == SelectedClient?.Id).ToList();
				NotifyPropertyChanged(nameof(Projects));
				SelectedProject = Projects?.Find(p => p.Id == t.ProjectId);
				NotifyPropertyChanged(nameof(SelectedProject));
				SelectedEmployee = Employees.Find(e => e.Id == t.EmployeeId);
				NotifyPropertyChanged(nameof(SelectedEmployee));
				Hours = t.Hours;
				Date = t.Date;
				Narrative = t.Narrative;
				NotifyPropertyChanged(nameof(Narrative));
			}
		}
		NotifyPropertyChanged(nameof(Hours));
		NotifyPropertyChanged(nameof(Date));
	}
	public void RefreshView() {
		if (SelectedClient != null) {
			SelectedProject = null;
			Projects = new List<Project>(ProjectService.Current.Projects.Where(p => p.ClientId == SelectedClient.Id));
		}
		NotifyPropertyChanged(nameof(SelectedProject));
		NotifyPropertyChanged(nameof(Projects));
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
