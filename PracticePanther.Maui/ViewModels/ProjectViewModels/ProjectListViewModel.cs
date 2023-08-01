using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using PracticePanther.Maui.Views.ProjectViews;
namespace PracticePanther.Maui.ViewModels.ProjectViewModels; 

public class ProjectListViewModel : INotifyPropertyChanged {
	private string? m_query;
	public string Query {
		get => m_query ?? "";
		set {
			m_query = value;
			NotifyPropertyChanged(nameof(Projects));
		}
	}
	public ObservableCollection<Project> Projects { get; set; } = new ObservableCollection<Project>();
	public Project? SelectedProject { get; set; }
	
	public void GetSearchResults(string query) {
		Query = query;
		RefreshView();
	}
	public void EditProject(Shell s) {
		if (SelectedProject != null) 
			s.GoToAsync(nameof(ProjectBuilderPage), new Dictionary<string, object> {
				{"ProjectId", SelectedProject.Id.ToString()}
			});
	}
	public void DeleteProject() {
		if (SelectedProject != null) {
			ProjectService.Delete(SelectedProject.Id);
			RefreshView();
		}
		SelectedProject = null;
	}
	public void DisplayProject(Shell s) {
		if (SelectedProject != null)
			s.GoToAsync(nameof(ProjectDisplayPage), new Dictionary<string, object> {
				{"ProjectId", SelectedProject.Id.ToString()}
			});
	}
	public void RefreshView() {
		Projects = new ObservableCollection<Project>(ProjectService.GetProjects(Query));
		NotifyPropertyChanged(nameof(Projects));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "") {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
	}
}
