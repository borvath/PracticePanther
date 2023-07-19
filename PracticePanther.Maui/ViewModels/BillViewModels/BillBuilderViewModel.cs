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
	public List<Time>? SelectedTimes { get; set; }
	
	private int projectId;
	
	public void AddBill() {
		if (SelectedTimes?.Count > 0)
			BillService.Current.AddBill(SelectedTimes, DueDate);
	}
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ProjectId"] as string), out projectId);
		Times = TimeService.Current.Times.Where(t => t.ProjectId == projectId && !t.HasBeenBilled).ToList();
		NotifyPropertyChanged(nameof(Times));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
