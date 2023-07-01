using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels;

public class TimeBuilderViewModel : INotifyPropertyChanged, IQueryAttributable {
	public IList<Client> Clients = new List<Client>(ClientService.Current.Clients);
	public Client? SelectedClient { get; set; }

	public int ProjectId { get; set; }
	public int EmployeeId { get; set; }
	public double Hours { get; set; }
	public DateTime Date { get; set; }
	public string? Narrative { get; set; }

	private int projectId;
	private int employeeId;
	
	public void AddTime() {
		Narrative ??= "Default Entry";
		TimeService.Current.Times.Add(new Time(ProjectId, EmployeeId, Hours, Date, Narrative));
	}
	
	public void ApplyQueryAttributes(IDictionary<string, object> query) {
		Int32.TryParse((query["ProjectId"] as string), out projectId);
		Int32.TryParse((query["EmployeeId"] as string), out employeeId);
		
		if (projectId == -1 || employeeId == -1) {
			ProjectId = 0;
			EmployeeId = 0;
			Hours = 0;
			Date = DateTime.Today;
			Narrative = "Default";
		}
		else {
			//TODO
		}
		
		NotifyPropertyChanged(nameof(ProjectId));
		NotifyPropertyChanged(nameof(EmployeeId));
		NotifyPropertyChanged(nameof(Hours));
		NotifyPropertyChanged(nameof(Date));
		NotifyPropertyChanged(nameof(Narrative));
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
