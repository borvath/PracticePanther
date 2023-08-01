using System;

namespace PracticePanther.Library.Models; 

public class Bill {
	public int Id { get; set; }
	public int ProjectId { get; set; }
	public decimal TotalAmount { get; set; }
	public DateTime DueDate { get; set; }
	
	public Bill(int id, int projectId, decimal totalAmount, DateTime dueDate) {
		Id = id;
		ProjectId = projectId;
		TotalAmount = totalAmount;
		DueDate = dueDate;
	}
}
