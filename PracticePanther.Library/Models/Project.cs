using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace PracticePanther.Library.Models;

public class Project : INotifyPropertyChanged {
	private DateTime? _open;
	private DateTime? _close;
	private bool _isActive;
	private string _longName;
	private string? _shortName;
	
	public int Id { get; set; }
	public int ClientId { get; set; }
	public DateTime? Open {
		get => _open;
		set { _open = value; NotifyPropertyChanged(); }
	}
	public DateTime? Close {
		get => _close;
		set { _close = value; NotifyPropertyChanged(); }
	}
	public bool IsActive {
		get => _isActive;
		set { _isActive = value; NotifyPropertyChanged(); }
	}
	public string LongName {
		get => _longName;
		set { _longName = value; NotifyPropertyChanged(); }
	}
	public string? ShortName { 
		get => _shortName;
		set { _shortName = value; NotifyPropertyChanged(); }
	}
	public string AsString => ToString();
	public string AsShortString => ToShortString();

	public Project() {
		Id = 0;
		Open = DateTime.Today;
		Close = DateTime.Today.AddYears(1);
		IsActive = true;
		_longName = "Default Project";
		ShortName = "Project";
	}
	public Project(DateTime? open, DateTime? close, string longName, string? shortName) {
		Open = open;
		Close = close;
		IsActive = true;
		_longName = longName;
		ShortName = shortName;
	}
	public string ToShortString() {
		string projIdString = "ID: {0, -5}";
		string longNameString = "Name: {1, -" + (LongName.Length + 5) + "}";
		string isActiveString = "Active: {2}";
		string ret = projIdString + longNameString + isActiveString;
		return String.Format(ret, Id, LongName, IsActive);
	}
	public override string ToString() {
		string projIdString = "Project ID: {0, -5}";
		string clientIdString = "Client ID: {1, -5}";
		string longNameString = "Project Name: {2, -" + (LongName.Length + 5) + "}";
		string isActiveString = "Active: {3}";
		string ret = projIdString + clientIdString + longNameString + isActiveString;
		return String.Format(ret, Id, ClientId, LongName, IsActive);
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
