using System;
using PracticePanther.Library.Models;

namespace PracticePanther.Library.DTOs; 

public class TimeDTO {
	public int Id { get; }
	public int ProjectId { get; }
	public int EmployeeId { get; }
	public decimal Hours { get; }
	public DateTime Date { get; }
	public string? Narrative { get; }
	public bool HasBeenBilled { get; }

	public TimeDTO(int id, int projectId, int employeeId, decimal hours, DateTime date, string? narrative, bool hasBeenBilled) {
		Id = id;
		ProjectId = projectId;
		EmployeeId = employeeId;
		Hours = hours;
		Date = date;
		Narrative = narrative;
		HasBeenBilled = hasBeenBilled;
	}
	public Time ConvertToTime() {
		return new Time(Id, ProjectId, EmployeeId, Hours, Date, Narrative, HasBeenBilled);
	}
}
