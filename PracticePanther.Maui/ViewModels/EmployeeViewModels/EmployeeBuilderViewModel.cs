using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.DTOs;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels.EmployeeViewModels; 

public class EmployeeBuilderViewModel : INotifyPropertyChanged, IQueryAttributable {
	
	public string Name { get; set; } = "John Doe";
	public decimal Rate { get; set; }
	
	private int employeeId;

	public void AddOrUpdateEmployee() {
		if (employeeId == -1) {
			EmployeeService.AddOrUpdate(new EmployeeDTO(employeeId, Name, Rate));
		}
		else {
			Employee? e = EmployeeService.GetEmployee(employeeId);
			if (e != null)
				EmployeeService.AddOrUpdate(new EmployeeDTO(e.Id, Name, Rate));
		}
	}
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["EmployeeId"] as string), out employeeId);
		if (employeeId == -1) {
			Name = "John Doe";
			Rate = 0;
		}
		else {
			Employee? e = EmployeeService.GetEmployee(employeeId);
			if (e != null) {
				Name = e.Name;
				Rate = e.Rate;
			}
		}
		NotifyPropertyChanged(nameof(Name));
		NotifyPropertyChanged(nameof(Rate));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
