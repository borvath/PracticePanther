using System;
using System.Collections.Generic;
using PracticePanther.Library.Services;

namespace PracticePanther.Library.Models; 

public class Bill {
	public int Id { get; set; }
	public int ProjectId { get; set; }
	public decimal TotalAmount { get; set; }
	public DateTime DueDate { get; set; }
	
	public Bill(List<Time> times, DateTime dueDate) {
		Id = BillService.Current.Bills.Count == 0 ? 1 : BillService.Current.Bills[^1].Id + 1;
		ProjectId = times[0].ProjectId;
		DueDate = dueDate;
		foreach (Time t in times)
			TotalAmount += TimeService.Current.BillTime(t.Id);
	}
	public Bill(int id, int pid, decimal totalAmount, DateTime dueDate) {
		Id = id;
		ProjectId = pid;
		TotalAmount = totalAmount;
		DueDate = dueDate;
	}
}
