using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using PracticePanther.Maui.Views;

namespace PracticePanther.Maui.ViewModels; 

public class ProjectDisplayViewModel : INotifyPropertyChanged, IQueryAttributable{
	public Project? DisplayedProject { get; set; }
	public Client? ProjectClient { get; set; }
	public List<Time>? Times { get; set; }
	public Time? SelectedTime { get; set; }
	
	
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ClientId"] as string), out int clientId);
		Int32.TryParse((query["ProjectId"] as string), out int projectId);
		ProjectClient = ClientService.Current.Clients.FirstOrDefault(c => c.Id == clientId);
		if (ProjectClient != null) 
			DisplayedProject = ProjectClient.ProjectList.GetProject(projectId);
		if (DisplayedProject != null) {
			Times = DisplayedProject.TimeService.Times;
		}
		NotifyPropertyChanged(nameof(DisplayedProject));
	}
	public void AddTime(Shell s) {
		if (DisplayedProject != null && ProjectClient != null)
			s.GoToAsync(nameof(TimeBuilderPage),
				new Dictionary<string, object> {
					{ "ProjectId", DisplayedProject.Id.ToString() },
					{ "ClientId", ProjectClient.Id.ToString() },
					{ "TimeId", "-1" }
				});
	}
	public void EditTime(Shell s) {
		if (SelectedTime != null) {
			s.GoToAsync(nameof(TimeBuilderPage),
				new Dictionary<string, object> {
					{ "ProjectId", SelectedTime.ProjectId.ToString() },
					{ "ClientId", SelectedTime.ClientId.ToString() },
					{ "TimeId", SelectedTime.Id.ToString() }
				});
		}
	}
	public void DeleteTime() {
		if (SelectedTime != null) {
			ClientService.Current.GetClient(SelectedTime.ClientId)?
				.ProjectList.GetProject(SelectedTime.ProjectId)?
				.TimeService.Remove(SelectedTime);
			RefreshView();
		}
		SelectedTime = null;
	}
	public void DisplayTime(Shell s) {
		if (SelectedTime != null) {
			s.GoToAsync(nameof(TimeDisplayPage),
				new Dictionary<string, object> {
					{ "ProjectId", SelectedTime.ProjectId.ToString() },
					{ "ClientId", SelectedTime.ClientId.ToString() },
					{ "TimeId", SelectedTime.Id.ToString() }
				});
		}
	}
	public void RefreshView() {
		if (DisplayedProject != null) 
			Times = new List<Time>(DisplayedProject.TimeService.Times);
		NotifyPropertyChanged(nameof(Times));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
