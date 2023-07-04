using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using PracticePanther.Maui.Views;

namespace PracticePanther.Maui.ViewModels; 

public class ProjectListViewModel : INotifyPropertyChanged {
	private string? m_query;
	public string Query {
		get => m_query ?? "";
		set {
			m_query = value;
			NotifyPropertyChanged(nameof(Projects));
		}
	}
	public ObservableCollection<Project> Projects =>
		String.IsNullOrWhiteSpace(Query) ? 
			new ObservableCollection<Project>(ProjectService.Current.Projects) : 
			new ObservableCollection<Project>(ProjectService.Current.Search(Query));
	public Project? SelectedProject { get; set; }
	
	public void GetSearchResults(string query) {
		Query = query;
		RefreshView();
	}
	public void AddProject(Shell s) {
		s.GoToAsync(nameof(ProjectBuilderPage), new Dictionary<string, object> {
			{"ProjectId", "-1"}
		});
	}
	public void EditProject(Shell s) {
		if (SelectedProject != null) 
			s.GoToAsync(nameof(ProjectBuilderPage), new Dictionary<string, object> {
				{"ProjectId", SelectedProject.Id.ToString()}
			});
	}
	public void DeleteProject() {
		if (SelectedProject != null) {
			TimeService.Current.Times.RemoveAll(t => t.ProjectId == SelectedProject.Id);
			ProjectService.Current.Projects.Remove(SelectedProject);
			RefreshView();
		}
		SelectedProject = null;
	}
	public void DisplayProject(Shell s) {
		if (SelectedProject != null) {
			s.GoToAsync(nameof(ProjectDisplayPage), new Dictionary<string, object> {
				{"ProjectId", SelectedProject.Id.ToString()}
			});
		}
	}
	public void RefreshView() {
		NotifyPropertyChanged(nameof(Projects));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "") {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
	}
}
