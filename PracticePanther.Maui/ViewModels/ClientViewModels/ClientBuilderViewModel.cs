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
			if (c != null) {
				c.Name = Name;
				c.Open = Open;
				c.Close = Close;
				c.Notes = Notes;
				ClientService.AddOrUpdate(new ClientDTO(c.Id, c.Name, c.Open, c.Close, c.Notes, c.IsActive));
			}
		}
	}
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ClientId"] as string), out clientId);
		if (clientId > 0) {
			Client? c = ClientService.GetClient(clientId);
			if (c != null) {
				Name = c.Name;
				Open = c.Open;
				Close = c.Close;
				Notes = c.Notes;
			}
		}
		else {
			Name = "John Doe";
			Open = DateTime.Now;
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
