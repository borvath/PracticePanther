﻿using System;

namespace PracticePanther.Library.Models;

public class Client {
	
	public int Id { get; set; }
	public string Name { get; set; }
	public DateTime Open { get; set; }
	public DateTime? Close { get; set; }
	public bool IsActive { get; set; } = true;
	public string? Notes { get; set; }
	public string AsString => ToString(); // IntelliSense says this is unused, that is untrue - used in TimeBuilder, do not delete
	
	public Client(string name, DateTime open, DateTime? close, string? notes) {
		Name = name;
		Open = open;
		Close = close;
		Notes = notes;
	}
	public override string ToString() {
		return $"{Id}.   {Name}";
	}
}
