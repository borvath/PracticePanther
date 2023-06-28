using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels; 

public class EmployeeBuilderViewModel {
	public string? Name { get; set; }
	public double Rate { get; set; }

	public void AddEmployee() {
		Name ??= "No Name";
		EmployeeService.Current.Add(new Employee(Name, Rate));
	}
}
