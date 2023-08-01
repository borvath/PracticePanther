using System;
using PracticePanther.Library.Models;

namespace PracticePanther.Library.DTOs; 

public class BillDTO {
	public int Id { get; }
	public int ProjectId { get; }
	public decimal TotalAmount { get; }
	public DateTime DueDate { get; }

	public BillDTO(int id, int projectId, decimal totalAmount, DateTime dueDate) {
		Id = id;
		ProjectId = projectId;
		TotalAmount = totalAmount;
		DueDate = dueDate;
	}
	public Bill ConvertToBill() {
		return new Bill(Id, ProjectId, TotalAmount, DueDate);
	}
}
