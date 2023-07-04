using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels; 

public class ProjectDisplayViewModel : INotifyPropertyChanged, IQueryAttributable{
	public Project? DisplayedProject { get; set; }
	public Client? ProjectClient { get; set; }
	public List<Time>? Times { get; set; }
	public Time? SelectedTime { get; set; }
	
	
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ClientId"] as string), out int clientId);
		Int32.TryParse((query["ProjectId"] as string), out int projectId);
		ProjectClient = ClientService.Current.GetClient(clientId);
		if (ProjectClient != null) 
			DisplayedProject = ProjectClient.ProjectList.GetProject(projectId);
		if (DisplayedProject != null) {
			Times = TimeService.Current.Times.Where(t => t.ClientId == clientId && t.ProjectId == projectId).ToList();
		}
		NotifyPropertyChanged(nameof(DisplayedProject));
	}
	public void RefreshView() {
		if (ProjectClient != null && DisplayedProject != null) 
			Times = new List<Time>(TimeService.Current.Times.Where(t => t.ClientId == ProjectClient.Id && t.ProjectId == DisplayedProject.Id).ToList());
		NotifyPropertyChanged(nameof(Times));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
