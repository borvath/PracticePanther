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

public class ClientDisplayViewModel : IQueryAttributable, INotifyPropertyChanged {
	
	public Client? DisplayedClient { get; private set; }
	public List<Project>? Projects { get; set; }
	public Project? SelectedProject { get; set; }

	public bool Closable => AllProjectsClosed();

	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ClientId"] as string), out int clientId);
		DisplayedClient = ClientService.Current.Clients.FirstOrDefault(c => c.Id == clientId);
		NotifyPropertyChanged(nameof(DisplayedClient));
	}
	public void AddProject(Shell s) {
		if (DisplayedClient != null)
			s.GoToAsync(nameof(ProjectBuilderPage), new Dictionary<string, object> {
				{"ClientId", DisplayedClient.Id.ToString()}, {"ProjectId", "-1"}
			});
	}
	public void EditProject(Shell s) {
		if (DisplayedClient != null && SelectedProject != null) 
			s.GoToAsync(nameof(ProjectBuilderPage), new Dictionary<string, object> {
				{"ClientId", DisplayedClient.Id.ToString()}, {"ProjectId", SelectedProject.Id.ToString()}
			});
	}
	public void DeleteProject() {
		if (DisplayedClient != null && SelectedProject != null)
			ClientService.Current.GetClient(DisplayedClient.Id)?.ProjectList.Projects.Remove(SelectedProject);
		RefreshView();
		SelectedProject = null;
	}
	public void DisplayProject(Shell s) {
		if (DisplayedClient != null && SelectedProject != null) {
			s.GoToAsync(nameof(ProjectDisplayPage), new Dictionary<string, object> {
				{"ClientId", DisplayedClient.Id.ToString()}, {"ProjectId", SelectedProject.Id.ToString()}
			});
		}
	}
	public void CloseProject() {
		if (SelectedProject != null) {
			SelectedProject.IsActive = false;
			SelectedProject.Close = DateTime.Now;
			RefreshView();
		}
	}
	public void CloseClient() {
		if (DisplayedClient != null) {
			DisplayedClient.IsActive = false;
			DisplayedClient.Close = DateTime.Now;
			RefreshView();
		}
	}
	private bool AllProjectsClosed() {
		return DisplayedClient != null && DisplayedClient.ProjectList.Projects.All(p => !p.IsActive);
	}
	public void RefreshView() {
		NotifyPropertyChanged(nameof(DisplayedClient));
		if (DisplayedClient != null) {
			Projects = new List<Project>(DisplayedClient.ProjectList.Projects);
		}
		NotifyPropertyChanged(nameof(Projects));
		NotifyPropertyChanged(nameof(Closable));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
