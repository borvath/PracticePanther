using System;
using PracticePanther.Library.Services;
namespace PracticePanther.Library.Models; 
public class Time {
	
	public int Id { get; set; }
	public int ClientId { get; set; }
	public string? ClientName => ClientService.Current.GetClient(ClientId)?.Name;
	public int ProjectId { get; set; }
	public string? ProjectName => ProjectService.Current.GetProject(ProjectId)?.Name;
	public int EmployeeId { get; set; }
	public string? EmployeeName => EmployeeService.Current.GetEmployee(EmployeeId)?.Name;
	public double Hours { get; set; }
	public DateTime Date { get; set; }
	public string? Narrative { get; set; }
	public bool HasBeenBilled { get; set; }

	public Time(int empId, double hours, DateTime date, string? narrative) {
		EmployeeId = empId;
		Hours = hours;
		Date = date;
		Narrative = narrative;
	}
}
