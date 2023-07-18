using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels.BillViewModels; 

public class BillBuilderViewModel : IQueryAttributable, INotifyPropertyChanged{
	
	public DateTime? DueDate { get; set; }
	private Project? p;
	private int projectId;
	
	public void AddOrUpdateBill() {
		p = ProjectService.Current.GetProject(projectId);
		if (p != null) {
			BillService.Current.CreateBill(p, DueDate ?? DateTime.Today);
		}
	}
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ProjectId"] as string), out projectId);
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
