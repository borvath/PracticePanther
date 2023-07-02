using System;
namespace PracticePanther.Library.Models; 
public class Time {
	
	public int Id { get; set; }
	public int ClientId { get; set; }
	public int ProjectId { get; set; }
	public int EmployeeId { get; set; }
	public double Hours { get; set; }
	public DateTime Date { get; set; }
	public string? Narrative { get; set; }
	public string AsShortString => ToShortString();
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
	public string ToShortString() {
		string dateString = $"{Date:MM/dd/yyyy}";
		return $"Employee: {EmployeeId, -7}Date: {dateString,-15}Hours: {Hours}";
	}
	public override string ToString() {
		string dateString = $"{Date:MM/dd/yyyy}";
		return $"Project: {ProjectId,-13}Employee: {EmployeeId}\nDate: {dateString,-15}Hours: {Hours}\nEntry Notes: {Narrative}";
	}
}
