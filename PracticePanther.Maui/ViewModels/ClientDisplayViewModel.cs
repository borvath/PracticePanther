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
		var clientId = query["ClientId"] as int?;
		foreach (Client c in ClientService.Current.Clients.Where(c => c.Id == clientId)) {
			DisplayedClient = c;
			break;
		}
		NotifyPropertyChanged(nameof(DisplayedClient));
	}
	
	public void EditProject(Shell s) {
		int projectId = SelectedProject?.Id ?? -1;
		int? index = DisplayedClient?.GetProjectIndex(projectId);
		if (index >= 0) 
			s.GoToAsync(nameof(ProjectBuilderPage), new Dictionary<string, object>{{"ProjectId", projectId}});
	}
	public void DeleteProject() {
		if (SelectedProject == null)
			return;
		DisplayedClient?.RemoveProject(SelectedProject.Id);
		RefreshView();
	}
	public void DisplayProject(Shell s) {
		int projectId = SelectedProject?.Id ?? -1;
		int index = ClientService.Current.GetClientIndex(projectId);
		if (index != -1) {
			s.GoToAsync(nameof(ProjectDisplayPage), new Dictionary<string, object>{{"ProjectId", projectId}});
		}
	}
	public void RefreshView() {
		NotifyPropertyChanged(nameof(DisplayedClient));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
