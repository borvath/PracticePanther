using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticePanther.Library.Models;

public class Client {
	
	public int Id { get; set; }
	public string Name { get; set; }
	public DateTime? Open { get; set; }
	public DateTime? Close { get; set; }
	public bool IsActive { get; set; }
	public string Notes { get; set; }
	public List<Project> Projects { get; set; } = new List<Project>();
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
		return $"Client ID: {Id}\tClient Name: {Name}\n";
	}

	// Project Management - Client acts as the service for project (for now at least)
	public void AddProject(Project p) {
		if (p.Id == 0) {
			p.Id = Projects[^1].Id + 1;
		}
		p.ClientId = Id;
		Projects.Add(p);
	}
	public void RemoveProject(int id) {
		Project? p = GetProject(id);
		if (p != null)
			Projects.Remove(p); 
	}
	public Project? GetProject(int id) {
		return Projects.FirstOrDefault(p => p.Id == id);
	}
}
