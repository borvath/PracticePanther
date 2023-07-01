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
	public DateTime? Open { get; set; }
	public DateTime? Close { get; set; }
	public string? LongName { get; set; }
	public string? ShortName { get; set; }
	
	private int clientId;
	private int projectId;

	public void AddOrUpdateProject() {
		if (clientId > 0) {
			if (projectId == -1) {
				foreach (Client c in ClientService.Current.Clients.Where(c => c.Id == clientId)) {
					c.ProjectList.AddProject(new Project(
						Open ?? DateTime.Today, Close ?? DateTime.Today.AddYears(1), LongName ?? "Default Project", ShortName ?? "N/A"), c.Id);
					break;
				}
			}
			else {
				foreach (Client c in ClientService.Current.Clients.Where(c => c.Id == clientId)) {
					foreach (Project p in c.ProjectList.Projects.Where(p => p.Id == projectId)) {
						p.Open = Open;
						p.Close = Close;
						p.LongName = LongName ?? "Default Project";
						p.ShortName = ShortName;
						break;
					}
					break;
				}
			}
		}
	}
	
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ClientId"] as string), out clientId);
		Int32.TryParse((query["ProjectId"] as string), out projectId);
		if (clientId > 0) {
			if (projectId == -1) {
				Open = DateTime.Today;
				Close = DateTime.Today.AddYears(1);
				LongName = "Default Project";
				ShortName = "N/A";
			}
			else {
				foreach (Client c in ClientService.Current.Clients.Where(c => c.Id == clientId)) {
					Project? p = c.ProjectList.GetProject(projectId);
					if (p != null) {
						Open = p.Open;
						Close = p.Close;
						LongName = p.LongName;
						ShortName = p.ShortName;
					}
					break;
				}
			}
		}
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
