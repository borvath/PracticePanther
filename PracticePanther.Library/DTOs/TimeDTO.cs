using System;
using PracticePanther.Library.Models;

namespace PracticePanther.Library.DTOs; 

public class TimeDTO {
	public int Id { get; }
	public int ProjectId { get; }
	public int EmployeeId { get; }
	public decimal Hours { get; }
	public DateTime Date { get; }
	public string? Summary { get; }
	public bool Billed { get; }

	public TimeDTO(int id, int p_id, int e_id, decimal hours, DateTime date, string? summary, bool billed) {
		Id = id;
		ProjectId = p_id;
		EmployeeId = e_id;
		Hours = hours;
		Date = date;
		Summary = summary;
		Billed = billed;
	}
	public Time ConvertToTime() {
		return new Time(Id, ProjectId, EmployeeId, Hours, Date, Summary, Billed);
	}
}
