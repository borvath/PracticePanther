using System;
using System.Collections.Generic;
using PracticePanther.Library.Services;

namespace PracticePanther.Library.Models; 

public class Bill {
	public int Id { get; set; }
	public int ProjectId { get; set; }
	public double TotalAmount { get; set; }
	public DateTime DueDate { get; set; }
	
	public Bill(List<Time> times, DateTime dueDate) {
		ProjectId = times[0].ProjectId;
		DueDate = dueDate;
		foreach (Time t in times)
			TotalAmount += TimeService.Current.BillTime(t.Id);
	}
}
