using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using PracticePanther.Maui.Views.ClientViews;
namespace PracticePanther.Maui.ViewModels.ClientViewModels; 

public class ClientListViewModel : INotifyPropertyChanged {
	private string? m_query;
	public string? Query {
		get => m_query ?? null;
		set {
			m_query = value;
			NotifyPropertyChanged(nameof(Clients));
		}
	}
	public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();
	public Client? SelectedClient { get; set; }
	
	public void GetSearchResults(string query) {
		Query = query;
		RefreshView();
	}
	public void EditClient(Shell s) {
		if (SelectedClient != null)
			s.GoToAsync(nameof(ClientBuilderPage), new Dictionary<string, object> {
				{"ClientId", SelectedClient.Id.ToString()}
			});
	}
	public void DeleteClient() {
		if (SelectedClient != null) {
			ClientService.Delete(SelectedClient.Id);
			RefreshView();
		}
		SelectedClient = null;
	}
	public void DisplayClient(Shell s) {
		if (SelectedClient != null)
			s.GoToAsync(nameof(ClientDisplayPage), new Dictionary<string, object> {
				{"ClientId", SelectedClient.Id.ToString()}
			});
	}
	public void RefreshView() {
		Clients = new ObservableCollection<Client>(ClientService.GetClients(Query));
		NotifyPropertyChanged(nameof(Clients));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "") {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
	}
}
