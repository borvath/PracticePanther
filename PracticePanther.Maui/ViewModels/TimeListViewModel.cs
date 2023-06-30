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
public class TimeListViewModel : INotifyPropertyChanged {
	private string? m_query;
	public string Query {
		get => m_query ?? "";
		set {
			m_query = value;
			NotifyPropertyChanged(nameof(Times));
		}
	}
	public ObservableCollection<Time> Times =>
		String.IsNullOrWhiteSpace(Query) ? 
			new ObservableCollection<Time>(TimeService.Current.Times) : 
			new ObservableCollection<Time>(TimeService.Current.Search(Query));
	public Time? SelectedTime { get; set; }
	
	public void GetSearchResults(string query) {
		Query = query;
		RefreshView();
	}
	public void EditTime(Shell s) {
		if (SelectedTime != null)
			s.GoToAsync(nameof(TimeBuilderPage), new Dictionary<string, object>{{"ProjectId", SelectedTime.ProjectId}, {"EmployeeId", SelectedTime.EmployeeId}});
	}
	public void DeleteTime() {
		if (SelectedTime == null)
			return;
		TimeService.Current.Times.Remove(SelectedTime);
		RefreshView();
	}
	public void DisplayTime(Shell s) {
		throw new NotImplementedException();
	}
	public void RefreshView() {
		NotifyPropertyChanged(nameof(Times));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "") {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
	}
}
