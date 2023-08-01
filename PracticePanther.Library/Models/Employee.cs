namespace PracticePanther.Library.Models;

public class Employee {

	public int Id { get; set; }
	public string Name { get; set; }
	public decimal Rate { get; set; }
	public string AsString => $"{Id}.   {Name}   {Rate:C}/hour"; // IntelliSense says this is unused, that is untrue - used in TimeBuilder, do not delete
	
	public Employee(int id, string name, decimal rate) {
		Id = id;
		Name = name;
		Rate = rate;
	}
}
