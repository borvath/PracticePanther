namespace PracticePanther.Library.Models;

public class Employee {

	public int Id { get; set; }
	public string Name { get; set; }
	public double Rate { get; set; } = 0;
	public string AsString => $"{Id}.   {Name}   {Rate:C}/hour"; // IntelliSense says this is unused, that is untrue - used in TimeBuilder, do not delete
	
	public Employee(string name, double rate) {
		Name = name;
		Rate = rate;
	}
}
