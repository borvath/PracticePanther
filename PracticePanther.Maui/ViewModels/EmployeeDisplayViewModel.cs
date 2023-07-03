using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels; 

public class EmployeeDisplayViewModel : INotifyPropertyChanged, IQueryAttributable {
	public Employee? DisplayedEmployee { get; set; }
	public List<Time>? Times { get; set; } = new List<Time>();
	public Time? SelectedTime { get; set; }

	private int employeeId;
	
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["EmployeeId"] as string), out employeeId);
		DisplayedEmployee = EmployeeService.Current.GetEmployee(employeeId);
		if (DisplayedEmployee != null) {
			foreach (Client c in ClientService.Current.Clients) {
				foreach (Project p in c.ProjectList.Projects) {
					foreach (Time t in p.TimeService.Times.Where(t => t.EmployeeId == employeeId)) {
						Times?.Add(t);
					}
				}
			}
		}
		NotifyPropertyChanged(nameof(DisplayedEmployee));
		NotifyPropertyChanged(nameof(Times));
	}
	public void RefreshView() {
		NotifyPropertyChanged(nameof(DisplayedEmployee));
		if (DisplayedEmployee != null) {
			Times = new List<Time>();
			foreach (Client c in ClientService.Current.Clients) {
				foreach (Project p in c.ProjectList.Projects) {
					foreach (Time t in p.TimeService.Times.Where(t => t.EmployeeId == employeeId)) {
						Times?.Add(t);
					}
				}
			}
		}
		NotifyPropertyChanged(nameof(Times));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
