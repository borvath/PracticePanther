using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.DTOs;
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
				ProjectService.AddOrUpdate(new ProjectDTO(projectId, SelectedClient.Id, Name, ShortName, Open, Close, true));
			}
			else {
				Project? p = ProjectService.GetProject(projectId);
				if (p != null)
					ProjectService.AddOrUpdate(new ProjectDTO(p.Id, SelectedClient.Id, Name, ShortName, Open, Close, p.IsActive));
			}
		}
	}
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ProjectId"] as string), out projectId);
		if (projectId == -1) {
			Name = "Default Project";
			Open = DateTime.Today;
		}
		else { 
			Project? p = ProjectService.GetProject(projectId);
			if (p != null) {
				SelectedClient = Clients.FirstOrDefault(c => c.Id == p.ClientId);
				NotifyPropertyChanged(nameof(Clients));
				NotifyPropertyChanged(nameof(SelectedClient));
				Name = p.Name; 
				ShortName = p.ShortName;
				NotifyPropertyChanged(nameof(ShortName));
				Open = p.Open; 
				Close = p.Close;
				NotifyPropertyChanged(nameof(Close));
			}
		}
		NotifyPropertyChanged(nameof(Open));
		NotifyPropertyChanged(nameof(Name));
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
