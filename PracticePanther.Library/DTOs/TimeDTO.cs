using System;
using PracticePanther.Library.Models;

namespace PracticePanther.Library.DTOs; 

public class TimeDTO {
	public int Id { get; }
	public int ProjectId { get; }
	public int EmployeeId { get; }
	public int? BillId { get; }
	public decimal Hours { get; }
	public DateTime Date { get; }
	public string? Narrative { get; }

	public TimeDTO(int id, int projectId, int employeeId, int? billid, decimal hours, DateTime date, string? narrative) {
		Id = id;
		ProjectId = projectId;
		EmployeeId = employeeId;
		BillId = billid;
		Hours = hours;
		Date = date;
		Narrative = narrative;
	}
	public Time ConvertToTime() {
		return new Time(Id, ProjectId, EmployeeId, BillId, Hours, Date, Narrative);
	}
}
