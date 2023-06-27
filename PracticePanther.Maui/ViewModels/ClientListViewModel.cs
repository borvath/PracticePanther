﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
using PracticePanther.Maui.Views;
namespace PracticePanther.Maui.ViewModels; 

public class ClientListViewModel : INotifyPropertyChanged {
	private string? m_query;
	public string Query {
		get => m_query ?? "";
		set {
			m_query = value;
			NotifyPropertyChanged(nameof(Clients));
		}
	}
	public ObservableCollection<Client> Clients =>
		String.IsNullOrWhiteSpace(Query) ? 
			new ObservableCollection<Client>(ClientService.Current.Clients) : 
			new ObservableCollection<Client>(ClientService.Current.Search(Query));
	public Client? SelectedClient { get; set; }
	
	public void GetSearchResults(string query) {
		Query = query;
		RefreshView();
	}
	public void EditClient(Shell s) {
		int clientId = SelectedClient?.Id ?? -1;
		int index = ClientService.Current.GetClientIndex(clientId);
		if (index >= 0) 
			s.GoToAsync(nameof(ClientBuilderPage), new Dictionary<string, object>{{"ClientId", clientId}});
	}
	public void DisplayClient(Shell s) {
		int clientId = SelectedClient?.Id ?? -1;
		int index = ClientService.Current.GetClientIndex(clientId);
		if (index != -1) {
			s.GoToAsync(nameof(ClientDisplayPage), new Dictionary<string, object>{{"ClientId", clientId}});
		}
	}
	public void DeleteClient() {
		if (SelectedClient == null)
			return;
		ClientService.Current.Clients.Remove(SelectedClient);
		RefreshView();
	}
	public void RefreshView() {
		NotifyPropertyChanged(nameof(Clients));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "") {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
	}
}
