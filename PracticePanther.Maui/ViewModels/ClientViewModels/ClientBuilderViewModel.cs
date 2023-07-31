using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.DTOs;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels.ClientViewModels; 

public class ClientBuilderViewModel : IQueryAttributable, INotifyPropertyChanged {
	
	public string Name { get; set; } = "John Doe";
	public DateTime Open { get; set; }
	public DateTime? Close { get; set; }
	public string? Notes { get; set; }

	private int clientId;
	
	public void AddOrUpdateClient() {
		if (clientId == -1) {
			ClientService.AddOrUpdate(new ClientDTO(clientId, Name, Open, Close, Notes, true));
		}
		else {
			Client? c = ClientService.GetClient(clientId);
			if (c != null)
				ClientService.AddOrUpdate(new ClientDTO(c.Id, Name, Open, Close, Notes, c.IsActive));
		}
	}
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ClientId"] as string), out clientId);
		if (clientId == -1) {
			Name = "John Doe";
			Open = DateTime.Now;
		}
		else {
			Client? c = ClientService.GetClient(clientId);
			if (c != null) {
				Name = c.Name;
				Open = c.Open;
				Close = c.Close;
				Notes = c.Notes;
			}
		}
		NotifyPropertyChanged(nameof(Name));
		NotifyPropertyChanged(nameof(Open));
		NotifyPropertyChanged(nameof(Close));
		NotifyPropertyChanged(nameof(Notes));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
