using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using PracticePanther.Maui.Views.TimeViews;
namespace PracticePanther.Maui.ViewModels.TimeViewModels; 

public class TimeListViewModel : INotifyPropertyChanged {
	private string? m_query;
	public string Query {
		get => m_query ?? "";
		set {
			m_query = value;
			NotifyPropertyChanged(nameof(Times));
		}
	}
	public ObservableCollection<Time> Times { get; set; } = new ObservableCollection<Time>();
	public Time? SelectedTime { get; set; }
	
	public void EditTime(Shell s) {
		if (SelectedTime != null) {
			s.GoToAsync(nameof(TimeBuilderPage), new Dictionary<string, object> {
				{"TimeId", SelectedTime.Id.ToString()}
			});
			RefreshView();
		}
	}
	public void DeleteTime() {
		if (SelectedTime != null) {
			TimeService.Delete(SelectedTime.Id);
			RefreshView();
		}
	}
	public void DisplayTime(Shell s) {
		if (SelectedTime != null) {
			s.GoToAsync(nameof(TimeDisplayPage), new Dictionary<string, object> {
				{"TimeId", SelectedTime.Id.ToString()}
			});
			SelectedTime = null;
		}
	}
	public void GetSearchResults(string query) {
		Query = query;
		RefreshView();
	}
	public void RefreshView() {
		Times = new ObservableCollection<Time>(TimeService.GetTimes(Query));
		NotifyPropertyChanged(nameof(Times));
		SelectedTime = null;
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;
	private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "") {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
	}
}
