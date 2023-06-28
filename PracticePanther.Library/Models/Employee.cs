namespace PracticePanther.Library.Models;

public class Employee {

	public int Id { get; set; }
	public string Name { get; set; }
	public double Rate { get; set; }
	public string AsString => ToString();

	public Employee() {
		Id = 0;
		Name = "John Doe";
		Rate = 0;
	}
	public Employee( string name, double rate) {
		Name = name;
		Rate = rate;
	}
	public override string ToString() {
		return $"EmpID: {Id}\tName: {Name}\tRate: {Rate}";
	}
}
