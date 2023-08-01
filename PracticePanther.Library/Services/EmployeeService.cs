using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PracticePanther.Library.DTOs;
using PracticePanther.Library.Models;
using PracticePanther.Library.Utilities;

namespace PracticePanther.Library.Services; 

public static class EmployeeService {
	public static void AddOrUpdate(EmployeeDTO e) {
		new WebRequestHandler().Post("/Employee", e).Wait();
	}
	public static void Delete(int id) {
		new WebRequestHandler().Delete($"/Employee/Delete/{id}").Wait();
	}
	public static Employee? GetEmployee(int id) {
		string? response = new WebRequestHandler().Get($"/Employee/{id}").Result;
		return (response != null) ? JsonConvert.DeserializeObject<EmployeeDTO>(response)?.ConvertToEmployee() : null;
	}
	public static List<Employee> GetEmployees(string? query = null) {
		string? response = query == null ? new WebRequestHandler().Get("/Employee").Result : new WebRequestHandler().Get($"/Employee/{query}").Result;
		List<EmployeeDTO> dtoList = response != null ? JsonConvert.DeserializeObject<List<EmployeeDTO>>(response) ?? new List<EmployeeDTO>() : new List<EmployeeDTO>();
		return dtoList.Select(e => e.ConvertToEmployee()).ToList();
	}
}
