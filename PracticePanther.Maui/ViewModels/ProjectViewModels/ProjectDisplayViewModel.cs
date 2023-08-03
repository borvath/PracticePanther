using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using PracticePanther.Maui.Views.BillViews;

namespace PracticePanther.Maui.ViewModels.ProjectViewModels; 

public class ProjectDisplayViewModel : INotifyPropertyChanged, IQueryAttributable{
	public Project? DisplayedProject { get; set; }
	public List<Time>? Times { get; set; }
	public List<Bill>? Bills { get; set; }
	public Bill? SelectedBill { get; set; }

	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ProjectId"] as string), out int projectId);
		DisplayedProject = ProjectService.GetProject(projectId);
		if (DisplayedProject != null) {
			Times = new List<Time>(TimeService.GetTimes(projectId));
			Bills = new List<Bill>(BillService.GetBills(projectId));
		}
		NotifyPropertyChanged(nameof(DisplayedProject));
		NotifyPropertyChanged(nameof(Times));
		NotifyPropertyChanged(nameof(Bills));
	}
	public void CreateBill(Shell s) {
		if (DisplayedProject != null)
			s.GoToAsync(nameof(BillBuilderPage), new Dictionary<string, object> {
				{"ProjectId", DisplayedProject.Id.ToString()},
				{"BillId", "-1"}
			});
	}
	public void EditBill(Shell s) {
		if (DisplayedProject != null && SelectedBill != null)
			s.GoToAsync(nameof(BillBuilderPage), new Dictionary<string, object> {
				{"ProjectId", DisplayedProject.Id.ToString()},
				{"BillId", SelectedBill.Id.ToString()}
			});
		SelectedBill = null;
	}
	public void DeleteBill() {
		if (DisplayedProject != null && SelectedBill != null)
			BillService.Delete(SelectedBill.Id);
		SelectedBill = null;
	}
	public void RefreshView() {
		NotifyPropertyChanged(nameof(DisplayedProject));
		NotifyPropertyChanged(nameof(Times));
		NotifyPropertyChanged(nameof(Bills));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
