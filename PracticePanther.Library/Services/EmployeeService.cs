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
	public void Add(Employee e) {
		e.Id = Employees.Count == 0 ? 1 : Employees[^1].Id + 1;
		Employees.Add(e);
	}
	public Employee? GetEmployee(int id) {
		return Employees.FirstOrDefault(e => e.Id == id);
	}
	public List<Employee> Search(string query) {
		return Int32.TryParse(query, out int employeeId) ? 
			       Employees.Where(e => (e.Id.ToString().StartsWith(employeeId.ToString()))).ToList() : 
			       Employees.Where(e => (e.Name.Contains(query, StringComparison.OrdinalIgnoreCase))).ToList();
	}
}
