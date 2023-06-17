using System.Collections.Generic;

namespace PracticePanther.Library.Models; 
public class Employee {

	public int Id { get; set; }
	public string Name { get; set; }
	public double Rate { get; set; }
	public string AsString => ToString();

	public Employee() {
		Id = -1;
		Name = "No Name";
		Rate = -1.0;
	}
	public Employee(List<Employee> empList, string name, double rate) {
		Id = empList.Count == 0 ? 1 : empList[^1].Id + 1;
		Name = name;
		Rate = rate;
	}
	public override string ToString() {
		return $"EmpID: {Id}\t\tName: {Name}\t\tRate: {Rate}";
	}
}
