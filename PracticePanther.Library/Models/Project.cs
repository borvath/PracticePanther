using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PracticePanther.Library.Services;
namespace PracticePanther.Library.Models;

public class Project : INotifyPropertyChanged {
	private DateTime _open;
	private DateTime? _close;
	private bool _isActive;
	private string _name;
	private string? _shortName;

	public int Id { get; set; }
	public int ClientId { get; set; }
	public string? ClientName => ClientService.GetClient(ClientId)?.Name;
	public DateTime Open { 
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
	public string Name {
		get => _name;
		set { _name = value; NotifyPropertyChanged(); }
	}
	public string? ShortName {
		get => _shortName;
		set { _shortName = value; NotifyPropertyChanged(); }
	}
	public bool HasShortName => _shortName != null;
	public string AsString => $"Project: {ShortName ?? Name}   Active: {IsActive}";
	
	public Project(int id, int c_id, string name, string? shortName, DateTime open, DateTime? close, bool active) {
		Id = id;
		ClientId = c_id;
		_open = open;
		_close = close;
		_name = name;
		_shortName = shortName;
		_isActive = active;
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
