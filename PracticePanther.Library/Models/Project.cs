using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PracticePanther.Library.Services;
namespace PracticePanther.Library.Models;

public class Project : INotifyPropertyChanged {
	private DateTime _open;
	private DateTime? _close;
	private bool _isActive = true;
	private string _name;
	private string? _shortName;

	public int Id { get; set; }
	public int ClientId { get; set; }
	public string? ClientName => ClientService.Current.GetClient(ClientId)?.Name;
	public DateTime Open { get => _open; set { _open = value; NotifyPropertyChanged(); } }
	public DateTime? Close { get => _close; set { _close = value; NotifyPropertyChanged(); } }
	public bool IsActive { get => _isActive; set { _isActive = value; NotifyPropertyChanged(); } }
	public string Name { get => _name; set { _name = value; NotifyPropertyChanged(); } }
	public string? ShortName { get => _shortName; set { _shortName = value; NotifyPropertyChanged(); } }
	public string AsString => ToString(); // IntelliSense says this is unused, that is untrue - used in TimeBuilder, do not delete
	
	public Project(DateTime open, DateTime? close, string name, string? shortName) {
		_open = open;
		_close = close;
		_name = name;
		_shortName = shortName;
	}
	public override string ToString() {
		return $"Project: {ShortName ?? Name}   Active: {IsActive}";
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
