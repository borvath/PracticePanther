using System;
using PracticePanther.Library.Services;

namespace PracticePanther.Library.Models; 

public class Time {
	
	public int Id { get; set; }
	public int ProjectId { get; set; }
	public int EmployeeId { get; set; }
	public int? BillId { get; set; }
	public decimal Hours { get; set; }
	public DateTime Date { get; set; }
	public string? Narrative { get; set; }
	public bool Billed => BillId          != null;
	public bool HasNarrative => Narrative != null;
	public string? ClientName => ClientService.GetClient(ProjectService.GetProject(ProjectId)?.ClientId ?? 0)?.Name;
	public string? ProjectName => ProjectService.GetProject(ProjectId)?.Name;
	public string? EmployeeName => EmployeeService.GetEmployee(EmployeeId)?.Name;
	
	public Time(int id, int pid, int empid, int? billid, decimal hours, DateTime date, string? narrative) {
		Id = id;
		ProjectId = pid;
		EmployeeId = empid;
		BillId = billid;
		Hours = hours;
		Date = date;
		Narrative = narrative;
	}
}
