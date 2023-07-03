using System;
namespace PracticePanther.Library.Models; 
public class Time {
	
	public int Id { get; set; }
	public int ClientId { get; set; }
	public int ProjectId { get; set; }
	public int EmployeeId { get; set; }
	public double Hours { get; set; }
	public DateTime? Date { get; set; }
	public string? Narrative { get; set; }
	public string AsClientShortString => ToClientShortString();
	public string AsEmployeeShortString => ToEmployeeShirtString();
	public string AsString => ToString();

	public Time() {
		ProjectId = 0;
		EmployeeId = 0;
		Hours = 0;
		Date = DateTime.Today;
		Narrative = "Default Entry";
	}
	public Time(int empId, double hours, DateTime? date, string? narrative) {
		EmployeeId = empId;
		Hours = hours;
		Date = date;
		Narrative = narrative;
	}
	public string ToClientShortString() {
		return $"EmployeeID: {EmployeeId, -7}Hours: {Hours, -10}Date: {Date:MM/dd/yyyy}";
	}
	public string ToEmployeeShirtString() {
		return $"ClientID: {ClientId, -7}ProjectID: {ProjectId, -7}Hours: {Hours, -10}Date: {Date:MM/dd/yyyy}";
	}
	public override string ToString() {
		return $"ProjectID: {ProjectId,-7}EmployeeID: {EmployeeId}\nHours: {Hours, -7}Date: {Date:MM/dd/yyyy}\nEntry Notes: {Narrative}";
	}
}
