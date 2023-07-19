using System;
namespace PracticePanther.Library.Models;

public class Employee {

	public int Id { get; set; }
	public string Name { get; set; }
	public double Rate { get; set; } = 0;
	public string AsString => ToString();
	
	public Employee(string name, double rate) {
		Name = name;
		Rate = rate;
	}
	public override string ToString() {
		string ret = "ID: {0, -5}" + "Name: {1, -" + (Name.Length + 5) + "}" + "Rate: {2}";
		return String.Format(ret, Id, Name, Rate);
	}
}
