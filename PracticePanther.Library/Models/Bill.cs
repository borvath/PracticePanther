using System;
using PracticePanther.Library.Services;

namespace PracticePanther.Library.Models; 

public class Bill {
	public int Id { get; set; }
	public int ProjectId { get; set; }
	public double TotalAmount { get; set; }
	public DateTime DueDate { get; set; }

	public Bill() {
		TotalAmount = 0.0;
		DueDate = DateTime.Today.AddMonths(1);
	}
	public Bill(Time t, DateTime dueDate) {
		ProjectId = t.ProjectId;
		Employee? emp = EmployeeService.Current.GetEmployee(t.EmployeeId);
		TotalAmount = (emp != null) ? t.Hours * emp.Rate : 0.0;
		DueDate = dueDate;
	}
	public Bill(int projectId, double projectSum, DateTime dueDate) {
		ProjectId = projectId;
		TotalAmount = projectSum;
		DueDate = dueDate;
	}
}
