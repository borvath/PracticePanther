using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.DTOs;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels.BillViewModels; 

public class BillBuilderViewModel : IQueryAttributable, INotifyPropertyChanged{
	
	public DateTime MinDate { get; } = DateTime.Today;
	public DateTime DueDate { get; set; }
	public List<Time>? Times { get; set; }
	public IList<object>? SelectedTimes { get; set; }

	private int billId;
	private int projectId;
	
	public void AddBill() {
		
		if (SelectedTimes is { Count: > 0 }) {
			if (billId == -1) {
				decimal totalAmount = (from Time t in SelectedTimes 
				                       let e = EmployeeService.GetEmployee(t.EmployeeId) 
				                       where e != null 
				                       select t.Hours * e.Rate).Sum();
				BillService.AddOrUpdate(new BillDTO(-1, projectId, totalAmount, DueDate));
				foreach (Time t in SelectedTimes)
					TimeService.AddOrUpdate(new TimeDTO(t.Id, t.ProjectId, t.EmployeeId, -1, t.Hours, t.Date, t.Narrative));
			}
			else {
				decimal totalAmount = (from Time t in SelectedTimes 
				                       let e = EmployeeService.GetEmployee(t.EmployeeId) 
				                       where e != null 
				                       select t.Hours * e.Rate).Sum();
				BillService.AddOrUpdate(new BillDTO(billId, projectId, totalAmount, DueDate));
				foreach (Time t in SelectedTimes)
					TimeService.AddOrUpdate(new TimeDTO(t.Id, t.ProjectId, t.EmployeeId, billId, t.Hours, t.Date, t.Narrative));
			}
		}
	}
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ProjectId"] as string), out projectId);
		Int32.TryParse((query["BillId"] as string), out billId);
		Times = billId == -1 ? 
			        TimeService.GetTimes().Where(t => t.ProjectId == projectId && t.BillId == null).ToList() : 
			        TimeService.GetTimes().Where(t => t.ProjectId == projectId && (t.BillId == null || t.BillId == billId)).ToList();
		SelectedTimes = new List<object>(Times.Where(t => t.BillId == billId));
		DueDate = MinDate;
		NotifyPropertyChanged(nameof(Times));
		NotifyPropertyChanged(nameof(SelectedTimes));
		NotifyPropertyChanged(nameof(DueDate));
	}
	public void SelectionChanged(SelectionChangedEventArgs e) {
		if (!Equals(SelectedTimes, (IList<object>?)e.CurrentSelection)) {
			SelectedTimes = (IList<object>?)e.CurrentSelection;
		}
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
