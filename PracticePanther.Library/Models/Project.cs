using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace PracticePanther.Library.Models;

public class Project : INotifyPropertyChanged {
	private DateTime _open;
	private DateTime? _close;
	private bool _isActive = true;
	private string _name;
	private string? _shortName;

	public int Id { get; set; }
	public int ClientId { get; set; }
	public DateTime Open { get => _open; set { _open = value; NotifyPropertyChanged(); } }
	public DateTime? Close { get => _close; set { _close = value; NotifyPropertyChanged(); } }
	public bool IsActive { get => _isActive; set { _isActive = value; NotifyPropertyChanged(); } }
	public string Name { get => _name; set { _name = value; NotifyPropertyChanged(); } }
	public string? ShortName { get => _shortName; set { _shortName = value; NotifyPropertyChanged(); } }
	public string AsString => ToString();
	
	public Project(DateTime open, DateTime? close, string name, string? shortName) {
		_open = open;
		_close = close;
		_name = name;
		_shortName = shortName;
	}
	public override string ToString() {
		string ret = "ID: {0, -5}" + "Name: {2, -" + (Name.Length + 5) + "}" + "Active: {3}";
		return String.Format(ret, Id, ClientId, Name, IsActive);
	}
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
