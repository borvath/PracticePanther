using System;
using System.Collections.Generic;
using System.Linq;
using PracticePanther.Library.Models;

namespace PracticePanther.Library.Services; 

public class BillService {
	private static object _lock = new object();
	private static BillService? instance;
	public static BillService Current {
		get {
			lock (_lock) {
				return instance ??= new BillService();
			}
		}
	}
	public List<Bill> Bills { get; }
	
	public BillService() {
		Bills = new List<Bill>();
	}
	public void AddBill(Bill b) {
		b.Id = Bills.Count == 0 ? 1 : Bills[^1].Id + 1;
		Bills.Add(b);
	}
	public void Remove(Bill b) {
		Bills.Remove(b);
	}
	public Bill? GetBill(int billId) {
		return Bills.FirstOrDefault(b => b.Id == billId);
	}
	public void CreateBill(Time t, DateTime dueDate) {
		Bills.Add(new Bill(t, dueDate));
	}
	public void CreateBill(Project p, DateTime dueDate) {
		List<Time> times = TimeService.Current.Times.Where(t => t.ProjectId == p.Id).ToList();
		double total = (from t in times let emp = EmployeeService.Current.GetEmployee(t.EmployeeId) 
		                select (emp != null) ? t.Hours * emp.Rate : 0.0).Sum();
		Bills.Add(new Bill(p.Id, total, dueDate));
	}
	public double ClientTotal(int clientId) {
		return (from b in Bills where ProjectService.Current.GetProject(b.ProjectId)?.ClientId == clientId 
		        select TimeService.Current.Times.Where(t => t.ProjectId == b.ProjectId).ToList() into times 
		        select (from t in times let emp = EmployeeService.Current.GetEmployee(t.EmployeeId) 
		                select (emp != null) ? t.Hours * emp.Rate : 0.0).Sum()).Sum();
	}
}
