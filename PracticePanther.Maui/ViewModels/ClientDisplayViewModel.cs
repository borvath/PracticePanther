using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels; 

public class ClientDisplayViewModel : IQueryAttributable, INotifyPropertyChanged {
	
	public Client? SelectedClient { get; private set; }

	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		var clientId = query["ClientId"] as int?;
		foreach (Client c in ClientService.Current.Clients.Where(c => c.Id == clientId)) {
			SelectedClient = c;
			break;
		}
		NotifyPropertyChanged(nameof(SelectedClient));
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
