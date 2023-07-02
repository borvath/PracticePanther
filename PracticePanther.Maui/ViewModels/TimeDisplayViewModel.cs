
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;
namespace PracticePanther.Maui.ViewModels; 

public class TimeDisplayViewModel : INotifyPropertyChanged, IQueryAttributable {
	public Time? DisplayedTime { get; set; }
	
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ClientId"] as string), out int clientId);
		Int32.TryParse((query["ProjectId"] as string), out int projectId);
		Int32.TryParse((query["TimeId"] as string), out int timeId);
		DisplayedTime = ClientService.Current.GetClient(clientId)?
									 .ProjectList.GetProject(projectId)?
									 .TimeService.GetTime(clientId, projectId, timeId);
		
		NotifyPropertyChanged(nameof(DisplayedTime));
	}
	public void RefreshView() {
		NotifyPropertyChanged(nameof(DisplayedTime));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
