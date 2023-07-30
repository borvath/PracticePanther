using System;

namespace PracticePanther.Library.Models;

public class Client {
	
	public int Id { get; set; }
	public string Name { get; set; }
	public DateTime Open { get; set; }
	public DateTime? Close { get; set; }
	public bool IsActive { get; set; }
	public string? Notes { get; set; }
	public string AsString => $"{Id}.   {Name}"; // IntelliSense says this is unused, that is untrue - used in TimeBuilder, do not delete
	
	public Client(int id, string name, DateTime open, DateTime? close, string? notes, bool isActive = true) {
		Id = id;
		Name = name;
		Open = open;
		Close = close;
		IsActive = isActive;
		Notes = notes;
	}
}
