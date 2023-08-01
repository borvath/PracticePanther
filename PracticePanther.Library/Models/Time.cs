using System;
using PracticePanther.Library.Services;

namespace PracticePanther.Library.Models; 

public class Time {
	
	public int Id { get; set; }
	public int ClientId { get; set; }
	public string? ClientName => ClientService.GetClient(ClientId)?.Name;
	public int ProjectId { get; set; }
	public string? ProjectName => ProjectService.GetProject(ProjectId)?.Name;
	public int EmployeeId { get; set; }
	public string? EmployeeName => EmployeeService.GetEmployee(EmployeeId)?.Name;
	public decimal Hours { get; set; }
	public DateTime Date { get; set; }
	public string? Narrative { get; set; }
	public bool HasBeenBilled { get; set; }

	public Time(int empId, decimal hours, DateTime date, string? narrative) {
		EmployeeId = empId;
		Hours = hours;
		Date = date;
		Narrative = narrative;
	}
	public Time(int id, int pid, int empid, decimal hours, DateTime date, string? narrative, bool billed) {
		Id = id;
		ProjectId = pid;
		EmployeeId = empid;
		Hours = hours;
		Date = date;
		Narrative = narrative;
		HasBeenBilled = billed;
	}
}
