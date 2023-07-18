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
	public Time? SelectedTime { get; set; }
	
	
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ProjectId"] as string), out int projectId);
		DisplayedProject = ProjectService.Current.GetProject(projectId);
		if (DisplayedProject != null) {
			Times = TimeService.Current.Times.Where(t => t.ProjectId == projectId).ToList();
		}
		NotifyPropertyChanged(nameof(DisplayedProject));
	}
	public void CreateBill(Shell s) {
		if (DisplayedProject != null) {
			s.GoToAsync(nameof(BillBuilderPage), new Dictionary<string, object>{{"ProjectId", DisplayedProject.Id.ToString()}});
		}
	}
	public void RefreshView() {
		if ( DisplayedProject != null) 
			Times = new List<Time>(TimeService.Current.Times.Where(t => t.ProjectId == DisplayedProject.Id).ToList());
		NotifyPropertyChanged(nameof(Times));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
