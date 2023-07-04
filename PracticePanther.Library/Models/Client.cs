using System;
using PracticePanther.Library.Services;

namespace PracticePanther.Library.Models;

public class Client {
	
	public int Id { get; set; }
	public string Name { get; set; }
	public DateTime? Open { get; set; }
	public DateTime? Close { get; set; }
	public bool IsActive { get; set; }
	public string Notes { get; set; }
	public ProjectService ProjectList { get; set; } = new ProjectService();
	public string AsString => ToString();

	public Client() {
		Id = 0;
		Name = "John Doe";
		Open = DateTime.Today;
		Close = DateTime.Today.AddYears(1);
		IsActive = true;
		Notes = String.Empty;
	}
	public Client(string name, DateTime? open, DateTime? close, string? notes) {
		Name = name;
		Open = open;
		Close = close;
		IsActive = true;
		Notes = notes ?? "No notes";
	}
	public override string ToString() {
		return $"ID: {Id, -5}Name: {Name}";
	}
}
