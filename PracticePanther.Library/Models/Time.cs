using System;
namespace PracticePanther.Library.Models; 
public class Time {
	
	public int ProjectId { get; set; }
	public int EmployeeId { get; set; }
	public double Hours { get; set; }
	public DateTime Date { get; set; }
	public string? Narrative { get; set; }

	public Time() {
		ProjectId = -1;
		EmployeeId = -1;
		Hours = -1.0;
		Date = DateTime.MaxValue;
		Narrative = "Bad Entry";
	}
	public Time(Project proj, Employee emp, int hours, DateTime date, string narrative) {
		ProjectId = proj.Id;
		EmployeeId = emp.Id;
		Hours = hours;
		Date = date;
		Narrative = narrative;
	}
	public override string ToString() {
		return $"Project: {ProjectId}\tEmployee: {EmployeeId}\nDate: {Date}\tHours: {Hours}\nEntry Notes: {Narrative}";
	}
}
