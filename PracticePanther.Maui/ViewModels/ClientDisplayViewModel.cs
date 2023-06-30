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
	public Project? SelectedProject { get; set; }

	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ClientId"] as string), out int clientId);
		DisplayedClient = ClientService.Current.Clients.FirstOrDefault(c => c.Id == clientId);
		NotifyPropertyChanged(nameof(DisplayedClient));
	}
	public void EditProject(Shell s) {
		if (SelectedProject != null) 
			s.GoToAsync(nameof(ProjectBuilderPage), new Dictionary<string, object>{{"ProjectId", SelectedProject.Id}});
	}
	public void DeleteProject() {
		if (DisplayedClient != null && SelectedProject != null)
			ClientService.Current.GetClient(DisplayedClient.Id)?.ProjectList.Projects.Remove(SelectedProject);
		RefreshView();
		SelectedProject = null;
	}
	public void DisplayProject(Shell s) {
		if (DisplayedClient != null && SelectedProject != null) {
			s.GoToAsync(nameof(ProjectDisplayPage), new Dictionary<string, object>{{"ProjectId", SelectedProject.Id}, {"ClientId", DisplayedClient.Id}});
		}
	}
	private void RefreshView() {
		NotifyPropertyChanged(nameof(DisplayedClient));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
