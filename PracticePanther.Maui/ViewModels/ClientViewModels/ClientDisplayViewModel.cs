using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
namespace PracticePanther.Maui.ViewModels.ClientViewModels; 

public class ClientDisplayViewModel : IQueryAttributable, INotifyPropertyChanged {
	
	public Client? DisplayedClient { get; private set; }
	public ObservableCollection<Project>? Projects { get; set; }
	public Project? SelectedProject { get; set; }
	public bool Closable => AllProjectsClosed();

	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ClientId"] as string), out int clientId);
		DisplayedClient = ClientService.Current.Clients.FirstOrDefault(c => c.Id == clientId);
		if (DisplayedClient != null) {
			Projects = new ObservableCollection<Project>(ProjectService.Current.Projects.Where(p => p.ClientId == DisplayedClient.Id));
		}
		NotifyPropertyChanged(nameof(DisplayedClient));
		NotifyPropertyChanged(nameof(Projects));
	}
	public void CloseProject() {
		if (SelectedProject != null) {
			SelectedProject.Close = DateTime.Now;
			SelectedProject.IsActive = false;
		}
		NotifyPropertyChanged(nameof(Projects));
		NotifyPropertyChanged(nameof(Closable));
	}
	public void CloseClient() {
		if (DisplayedClient != null) {
			DisplayedClient.IsActive = false;
			DisplayedClient.Close = DateTime.Now;
		}
		NotifyPropertyChanged(nameof(DisplayedClient));
	}
	private bool AllProjectsClosed() {
		return ProjectService.Current.Projects.Where(p => p.ClientId == DisplayedClient?.Id && p.IsActive).ToList().Count == 0;
	}
	public void RefreshView() {
		NotifyPropertyChanged(nameof(DisplayedClient));
		NotifyPropertyChanged(nameof(SelectedProject));
		NotifyPropertyChanged(nameof(Projects));
		NotifyPropertyChanged(nameof(Closable));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
