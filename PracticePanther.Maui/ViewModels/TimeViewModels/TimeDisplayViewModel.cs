using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels.TimeViewModels; 

public class TimeDisplayViewModel : INotifyPropertyChanged, IQueryAttributable {
	public Time? DisplayedTime { get; set; }
	
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["TimeId"] as string), out int timeId);
		DisplayedTime = TimeService.GetTime(timeId);
		NotifyPropertyChanged(nameof(DisplayedTime));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
