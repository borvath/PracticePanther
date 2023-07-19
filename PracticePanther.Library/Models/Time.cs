using System;
using PracticePanther.Library.Services;
namespace PracticePanther.Library.Models; 
public class Time {
	
	public int Id { get; set; }
	public int ClientId { get; set; } = 0;
	public string? ClientName => ClientService.Current.GetClient(ClientId)?.Name;
	public int ProjectId { get; set; } = 0;
	public string? ProjectName => ProjectService.Current.GetProject(ProjectId)?.Name;
	public int EmployeeId { get; set; } = 0;
	public string? EmpName => EmployeeService.Current.GetEmployee(EmployeeId)?.Name;
	public double Hours { get; set; } = 0;
	public DateTime Date { get; set; }
	public string? Narrative { get; set; }
	public bool HasBeenBilled { get; set; } = false;
	public string AsString => ToString();
	
	public Time(int empId, double hours, DateTime date, string? narrative) {
		EmployeeId = empId;
		Hours = hours;
		Date = date;
		Narrative = narrative;
	}
	public override string ToString() {
		return $"TimeID: {Id, -5}ClientID: {ClientId,-5}ProjectID: {ProjectId,-5}EmployeeID: {EmployeeId, -5}Hours: {Hours, -5}Date: {Date:MM/dd/yyyy}";
	}
}
