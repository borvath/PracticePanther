using System;
using PracticePanther.Library.Services;

namespace PracticePanther.Library.Models; 

public class Time {
	
	public int Id { get; set; }
	public int ProjectId { get; set; }
	public int EmployeeId { get; set; }
	public decimal Hours { get; set; }
	public DateTime Date { get; set; }
	public string? Narrative { get; set; }
	public bool HasBeenBilled { get; set; }
	public string? ClientName => ClientService.GetClient(ProjectService.GetProject(ProjectId)?.ClientId ?? 0)?.Name;
	public string? ProjectName => ProjectService.GetProject(ProjectId)?.Name;
	public string? EmployeeName => EmployeeService.GetEmployee(EmployeeId)?.Name;
	
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
