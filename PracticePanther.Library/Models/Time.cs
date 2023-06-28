using System;
namespace PracticePanther.Library.Models; 
public class Time {
	
	public int ProjectId { get; set; }
	public int EmployeeId { get; set; }
	public double Hours { get; set; }
	public DateTime Date { get; set; }
	public string? Narrative { get; set; }
	public string AsString => ToString();

	public Time() {
		ProjectId = 0;
		EmployeeId = 0;
		Hours = 0;
		Date = DateTime.Today;
		Narrative = "Default Entry";
	}
	public Time(int projectId, int empId, double hours, DateTime date, string narrative) {
		ProjectId = projectId;
		EmployeeId = empId;
		Hours = hours;
		Date = date;
		Narrative = narrative;
	}
	public override string ToString() {
		return $"Project: {ProjectId}\tEmployee: {EmployeeId}\nDate: {Date}\tHours: {Hours}\nEntry Notes: {Narrative}";
	}
}
