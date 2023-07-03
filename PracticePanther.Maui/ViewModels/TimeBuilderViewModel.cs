using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels;

public class TimeBuilderViewModel : INotifyPropertyChanged, IQueryAttributable {
	public double Hours { get; set; }
	public DateTime? Date { get; set; }
	public string? Narrative { get; set; }
	public List<Employee> Employees { get; set; }= new List<Employee>(EmployeeService.Current.Employees);
	public Employee? SelectedEmployee { get; set; }

	private int clientId;
	private int projectId;
	private int timeId;
	
	public void AddTime() {
		if (clientId > 0 && projectId > 0 && SelectedEmployee != null) {
			if (timeId == -1) {
				foreach (Client c in ClientService.Current.Clients.Where(c => c.Id == clientId)) {
					foreach (Project p in c.ProjectList.Projects.Where(p => p.Id == projectId)) {
						p.TimeService.AddTime(new Time(SelectedEmployee.Id, Hours, Date ?? DateTime.Today, Narrative ?? "Default Entry Notes"), p);
					}
				}
			}
			else {
				foreach (Client c in ClientService.Current.Clients.Where(c => c.Id == clientId)) {
					foreach (Project p in c.ProjectList.Projects.Where(p => p.Id == projectId)) {
						foreach (Time t in p.TimeService.Times.Where(t => t.Id == timeId)) {
							t.EmployeeId = SelectedEmployee.Id;
							t.Hours = Hours;
							t.Date = Date;
							t.Narrative = Narrative;
						}
					}
				}
			}
		}
	}
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ClientId"] as string), out clientId);
		Int32.TryParse((query["ProjectId"] as string), out projectId);
		Int32.TryParse((query["TimeId"] as string), out timeId);
		
		if (timeId == -1) {
			Hours = 0;
			Date = DateTime.Today;
			Narrative = "Default Entry Notes";
		}
		else {
			foreach (Client c in ClientService.Current.Clients.Where(c => c.Id == clientId)) {
				foreach (Project p in c.ProjectList.Projects.Where(p => p.Id == projectId)) {
					foreach (Time t in p.TimeService.Times.Where(t => t.Id == timeId)) {
						SelectedEmployee = Employees.Find(e => e.Id == t.EmployeeId);
						Hours = t.Hours;
						Date = t.Date ?? DateTime.Today;
						Narrative = t.Narrative ?? "Default Entry Notes";
					}
				}
			}
		}
		NotifyPropertyChanged(nameof(SelectedEmployee));
		NotifyPropertyChanged(nameof(Hours));
		NotifyPropertyChanged(nameof(Date));
		NotifyPropertyChanged(nameof(Narrative));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
