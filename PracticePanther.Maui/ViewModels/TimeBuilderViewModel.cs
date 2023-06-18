using System;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels;

public class TimeBuilderViewModel {
	public int ProjectId { get; set; }
	public int EmployeeId { get; set; }
	public double Hours { get; set; }
	public DateTime Date { get; set; }
	public string? Narrative { get; set; }
	
	public void AddTime() {
		Narrative ??= "Bad Entry";
		TimeService.Current.Add(new Time(ProjectId, EmployeeId, Hours, Date, Narrative));
	}
}
