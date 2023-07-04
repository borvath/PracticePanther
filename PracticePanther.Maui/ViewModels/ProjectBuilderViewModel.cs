using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels;

public class ProjectBuilderViewModel : INotifyPropertyChanged, IQueryAttributable {
	public List<Client> Clients { get; set; } = new List<Client>(ClientService.Current.Clients);
	public Client? SelectedClient { get; set; }
	public DateTime? Open { get; set; }
	public DateTime? Close { get; set; }
	public string? LongName { get; set; }
	public string? ShortName { get; set; }
	
	private int projectId;

	public void AddOrUpdateProject() {
		if (SelectedClient != null) {
			if (projectId == -1) {
				ProjectService.Current.AddProject(
					new Project(Open ?? DateTime.Today, Close ?? DateTime.Today.AddYears(1), LongName ?? "Default Project", ShortName ?? "N/A"),
					SelectedClient.Id
					);
			}
			else {
				foreach (Project p in ProjectService.Current.Projects.Where(p => p.ClientId == SelectedClient.Id && p.Id == projectId)) {
					p.ClientId = SelectedClient.Id;
					p.Open = Open;
					p.Close = Close;
					p.LongName = LongName ?? "Default Project";
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
			Close = DateTime.Today.AddYears(1);
			LongName = "Default Project";
			ShortName = "N/A";
		}
		else { 
			Project? p = ProjectService.Current.GetProject(projectId);
			if (p != null) {
				SelectedClient = ClientService.Current.GetClient(p.ClientId);
				Open = p.Open; 
				Close = p.Close; 
				LongName = p.LongName; 
				ShortName = p.ShortName;
			}
		}
		NotifyPropertyChanged(nameof(SelectedClient));
		NotifyPropertyChanged(nameof(Open));
		NotifyPropertyChanged(nameof(Close));
		NotifyPropertyChanged(nameof(LongName));
		NotifyPropertyChanged(nameof(ShortName));
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
