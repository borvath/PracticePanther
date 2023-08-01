using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels.EmployeeViewModels; 

public class EmployeeDisplayViewModel : INotifyPropertyChanged, IQueryAttributable {
	public Employee? DisplayedEmployee { get; set; }
	public List<Time>? EmployeeTimes { get; set; } = new List<Time>();

	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["EmployeeId"] as string), out int employeeId);
		DisplayedEmployee = EmployeeService.GetEmployee(employeeId);
		foreach (Time t in TimeService.GetTimes().Where(t => t.EmployeeId == employeeId))
				EmployeeTimes?.Add(t);
		NotifyPropertyChanged(nameof(DisplayedEmployee));
		NotifyPropertyChanged(nameof(EmployeeTimes));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
