using System.ComponentModel;
using System.Runtime.CompilerServices;
using PracticePanther.Library.Models;

namespace PracticePanther.Maui.ViewModels; 

public class TimeListViewModel : INotifyPropertyChanged {
	private string? m_query;
	public string Query {
		get => m_query ?? "";
		set {
			m_query = value;
	//		NotifyPropertyChanged(nameof(Times));
		}
	}
	//public ObservableCollection<Time> Times =>
	//	String.IsNullOrWhiteSpace(Query) ? 
	//		new ObservableCollection<Time>(TimeService.Current.Times) : 
	//		new ObservableCollection<Time>(TimeService.Current.Search(Query));
	public Time? SelectedTime { get; set; }
	
	public void GetSearchResults(string query) {
		Query = query;
		RefreshView();
	}
	public void RefreshView() {
	//	NotifyPropertyChanged(nameof(Times));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "") {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
	}
}
