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
	
	private int projectId;
	
	public void AddBill() {
		if (SelectedTimes is { Count: > 0 }) {
			decimal totalAmount = 0;
			foreach (Time t in SelectedTimes) {
				Employee? e = EmployeeService.GetEmployee(t.EmployeeId);
				if (e != null) 
					totalAmount += t.Hours * e.Rate;
				TimeService.AddOrUpdate(new TimeDTO(t.Id, t.ProjectId, t.EmployeeId, t.Hours, t.Date, t.Narrative, true));
			}
			BillService.AddOrUpdate(new BillDTO(-1, projectId, totalAmount, DueDate));
		}
	}
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ProjectId"] as string), out projectId);
		Times = TimeService.GetTimes().Where(t => t.ProjectId == projectId && !t.HasBeenBilled).ToList();
		SelectedTimes = new List<object>();
		DueDate = MinDate;
		NotifyPropertyChanged(nameof(Times));
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
