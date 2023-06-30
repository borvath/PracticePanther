using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels; 

public class EmployeeBuilderViewModel : INotifyPropertyChanged, IQueryAttributable {
	public string? Name { get; set; }
	public double Rate { get; set; }
	
	private int employeeId;

	public void AddOrUpdateEmployee() {
		if (employeeId == -1) {
			EmployeeService.Current.Add(new Employee(Name ?? "John Doe", Rate));
		}
		else {
			foreach (Employee e in EmployeeService.Current.Employees.Where(e => e.Id == employeeId)) {
				e.Name = Name ?? "John Doe";
				e.Rate = Rate;
				break;
			}
		}
	}
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["EmployeeId"] as string), out employeeId);
		if (employeeId > 0) {
			Employee? e = EmployeeService.Current.GetEmployee(employeeId);
			if (e != null) {
				Name = e.Name;
				Rate = e.Rate;
			}
		}
		else {
			Name = "John Doe";
			Rate = 0.0;
		}
		NotifyPropertyChanged(nameof(Name));
		NotifyPropertyChanged(nameof(Rate));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
