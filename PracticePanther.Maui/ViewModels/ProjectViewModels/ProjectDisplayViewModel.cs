using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
	
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ProjectId"] as string), out int projectId);
		DisplayedProject = ProjectService.GetProject(projectId);
		if (DisplayedProject != null) {
			Times = new List<Time>(TimeService.Current.Times.Where(t => t.ProjectId == projectId));
			Bills = new List<Bill>(BillService.Current.Bills.Where(b => b.ProjectId == projectId));
		}
		NotifyPropertyChanged(nameof(DisplayedProject));
		NotifyPropertyChanged(nameof(Times));
		NotifyPropertyChanged(nameof(Bills));
	}
	public void CreateBill(Shell s) {
		if (DisplayedProject != null)
			s.GoToAsync(nameof(BillBuilderPage), new Dictionary<string, object> {
				{"ProjectId", DisplayedProject.Id.ToString()}
			});
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
