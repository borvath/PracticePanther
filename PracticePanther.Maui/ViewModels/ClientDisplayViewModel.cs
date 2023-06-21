using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;

namespace PracticePanther.Maui.ViewModels; 

public class ClientDisplayViewModel : IQueryAttributable, INotifyPropertyChanged {
	
	public Client? SelectedClient { get; private set; }

	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		SelectedClient = query["SelectedClient"] as Client;
		SelectedClient ??= new Client();
		NotifyPropertyChanged(nameof(SelectedClient));
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
