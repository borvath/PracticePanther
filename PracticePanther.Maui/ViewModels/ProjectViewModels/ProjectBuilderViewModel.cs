using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels.ProjectViewModels;

public class ProjectBuilderViewModel : INotifyPropertyChanged, IQueryAttributable {
	
	public List<Client> Clients { get; set; } = new List<Client>(ClientService.GetClients());
	public Client? SelectedClient { get; set; }
	public DateTime Open { get; set; }
	public DateTime? Close { get; set; }
	public string Name { get; set; } = "Default Project";
	public string? ShortName { get; set; }
	
	private int projectId;

	public void AddOrUpdateProject() {
		if (SelectedClient != null) {
			if (projectId == -1) {
				ProjectService.Current.AddProject(new Project(Open, Close, Name, ShortName), SelectedClient.Id);
			}
			else {
				foreach (Project p in ProjectService.Current.Projects.Where(p => p.ClientId == SelectedClient.Id && p.Id == projectId)) {
					p.ClientId = SelectedClient.Id;
					p.Open = Open;
					p.Close = Close;
					p.Name = Name;
					p.ShortName = ShortName;
					break;
				}
			}
		}
	}
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ProjectId"] as string), out projectId);
		if (projectId == -1) {
			Open = DateTime.Today;
			Name = "Default Project";
		}
		else { 
			Project? p = ProjectService.Current.GetProject(projectId);
			if (p != null) {
				SelectedClient = ClientService.GetClient(p.ClientId);
				Open = p.Open; 
				Close = p.Close; 
				Name = p.Name; 
				ShortName = p.ShortName;
			}
		}
		NotifyPropertyChanged(nameof(SelectedClient));
		NotifyPropertyChanged(nameof(Open));
		NotifyPropertyChanged(nameof(Close));
		NotifyPropertyChanged(nameof(Name));
		NotifyPropertyChanged(nameof(ShortName));
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
