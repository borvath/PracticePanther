using System;

namespace PracticePanther.Library.DTOs; 

public class BillDTO {
	public int Id { get; }
	public int ProjectId { get; }
	public double TotalAmount { get; }
	public DateTime DueDate { get; }

	public BillDTO(int id, int projectId, double total, DateTime dueDate) {
		Id = id;
		ProjectId = projectId;
		TotalAmount = total;
		DueDate = dueDate;
	}
}
