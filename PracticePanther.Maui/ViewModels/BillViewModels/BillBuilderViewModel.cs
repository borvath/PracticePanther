using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
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
		if (SelectedTimes?.Count > 0)
			BillService.Current.AddBill(SelectedTimes.Cast<Time>().ToList(), DueDate);
	}
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ProjectId"] as string), out projectId);
		Times = TimeService.Current.Times.Where(t => t.ProjectId == projectId && !t.HasBeenBilled).ToList();
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
