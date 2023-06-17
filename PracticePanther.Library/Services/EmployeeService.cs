using System;
using System.Collections.Generic;
using System.Linq;
using PracticePanther.Library.Models;
namespace PracticePanther.Library.Services; 

public class EmployeeService {
	
	private static object _lock = new object();
	private static EmployeeService? instance;
	public static EmployeeService Current {
		get {
			lock (_lock) {
				return instance ??= new EmployeeService();
			}
		}
	}
	public List<Employee> Employees { get; }

	private EmployeeService() {
		Employees = new List<Employee>();
	}
	public void Add(Employee newEmployee) {
		Employees.Add(newEmployee);
	}
	public List<Employee> Search(string query) {
		return Int32.TryParse(query, out int clientId) ? 
			       Employees.Where(c => (c.Id.ToString().Contains(clientId.ToString()))).ToList() : 
			       Employees.Where(c => (c.Name.Contains(query, StringComparison.OrdinalIgnoreCase))).ToList();
	}
	public void RemoveEmployee(int index) {
		if (index > 0 && index < Employees.Count) 
			Employees.RemoveAt(index);
	}
	private int GetEmployeeIndex(int id) {
		if (id < 0) 
			return -1;
		var i = 0;
		foreach (Employee e in Employees) {
			if (e.Id == id) return i;
			i++;
		}
		return -1;
	}
	private Employee GetEmployeetAtIndex(int index) {
		return Employees[index];
	}
}
