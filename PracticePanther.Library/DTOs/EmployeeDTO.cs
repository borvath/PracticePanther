using PracticePanther.Library.Models;

namespace PracticePanther.Library.DTOs; 

public class EmployeeDTO {
	public int Id { get; }
	public string Name { get; }
	public decimal Rate { get; }

	public EmployeeDTO(int id, string name, decimal rate) {
		Id = id;
		Name = name;
		Rate = rate;
	}
	public Employee ConvertToEmployee() {
		return new Employee(Id, Name, Rate);
	}
}
