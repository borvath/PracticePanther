using System;
using System.Globalization;
using System.Linq;
using System.Xml;
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
		string idString = "EmpID: {0, -7}";
		string nameString = "Name: {1, -" + (Name.Length + 7) + "}";
		string rateString = "Rate: {2}";
		string ret = idString + nameString + rateString;
		return String.Format(ret, Id, Name, Rate);
	}
}
