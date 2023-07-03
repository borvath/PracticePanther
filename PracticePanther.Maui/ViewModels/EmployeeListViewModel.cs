using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using PracticePanther.Maui.Views;

namespace PracticePanther.Maui.ViewModels; 

public class EmployeeListViewModel : INotifyPropertyChanged {
	private string? m_query;
	public string Query {
		get => m_query ?? "";
		set {
			m_query = value;
			NotifyPropertyChanged(nameof(Employees));
		}
	}
	public ObservableCollection<Employee> Employees =>
		String.IsNullOrWhiteSpace(Query) ? 
			new ObservableCollection<Employee>(EmployeeService.Current.Employees) : 
			new ObservableCollection<Employee>(EmployeeService.Current.Search(Query));
	public Employee? SelectedEmployee { get; set; }
	
	public void GetSearchResults(string query) {
		Query = query;
		RefreshView();
	}
	public void EditEmployee(Shell s) {
		if (SelectedEmployee != null) {
			s.GoToAsync(nameof(EmployeeBuilderPage), new Dictionary<string, object>{{"EmployeeId", SelectedEmployee.Id.ToString()}});
		}
	}
	public void DeleteEmployee() {
		if (SelectedEmployee != null) {
			EmployeeService.Current.Employees.Remove(SelectedEmployee);
			RefreshView();
		}
		SelectedEmployee = null;
	}
	public void DisplayEmployee(Shell s) {
		if (SelectedEmployee != null) {
			s.GoToAsync(nameof(EmployeeDisplayPage), new Dictionary<string, object>{{"EmployeeId", SelectedEmployee.Id.ToString()}});
		}
	}
	public void RefreshView() {
		NotifyPropertyChanged(nameof(Employees));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "") {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
	}
}
