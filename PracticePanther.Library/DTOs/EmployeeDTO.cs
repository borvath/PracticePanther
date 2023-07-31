namespace PracticePanther.Library.DTOs; 

public class EmployeeDTO {
	public int Id { get; }
	public string Name { get; }
	public double Rate { get; }

	public EmployeeDTO(int id, string name, double rate) {
		Id = id;
		Name = name;
		Rate = rate;
	}
}
