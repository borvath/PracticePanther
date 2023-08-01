using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.DTOs;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
namespace PracticePanther.Maui.ViewModels.TimeViewModels;

public class TimeBuilderViewModel : INotifyPropertyChanged, IQueryAttributable {
	
	public List<Client> Clients { get; set; } = new List<Client>(ClientService.GetClients());
	public Client? SelectedClient { get; set; }
	public List<Project> Projects { get; set; } = new List<Project>();
	public Project? SelectedProject { get; set; }
	public List<Employee> Employees { get; set; } = new List<Employee>(EmployeeService.GetEmployees());
	public Employee? SelectedEmployee { get; set; }
	
	public decimal Hours { get; set; }
	public DateTime Date { get; set; }
	public string? Narrative { get; set; }
	
	private int timeId;

	public void AddTime() {
		if (SelectedClient != null && SelectedProject != null && SelectedEmployee != null) {
			if (timeId == -1) {
				TimeService.AddOrUpdate(new TimeDTO(timeId, SelectedProject.Id, SelectedEmployee.Id, Hours, Date, Narrative, false));
			}
			else {
				Time? t = TimeService.GetTime(timeId);
				if (t != null)
					TimeService.AddOrUpdate(new TimeDTO(t.Id, SelectedProject.Id, SelectedEmployee.Id, Hours, Date, Narrative, t.HasBeenBilled));
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
			Time? t = TimeService.GetTime(timeId);
			if (t != null) {
				Projects = new List<Project>(ProjectService.GetProjects().Where(p => p.ClientId == SelectedClient?.Id));
				NotifyPropertyChanged(nameof(Projects));
				SelectedProject = Projects.Find(p => p.Id == t.ProjectId);
				NotifyPropertyChanged(nameof(SelectedProject));
				SelectedClient = Clients.Find(c => c.Id == SelectedProject?.ClientId);
				NotifyPropertyChanged(nameof(SelectedClient));
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
			Projects = new List<Project>(ProjectService.GetProjects().Where(p => p.ClientId == SelectedClient.Id));
		}
		NotifyPropertyChanged(nameof(SelectedProject));
		NotifyPropertyChanged(nameof(Projects));
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
