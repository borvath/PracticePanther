using System;
using System.Collections.Generic;

namespace PracticePanther.Library.Models;

public class Client {
	
	public int Id { get; set; }
	public string Name { get; set; }
	public DateTime OpenDate { get; set; }
	public DateTime? CloseDate { get; set; }
	public bool IsActive { get; set; }
	public string Notes { get; set; }
	public List<Project> Projects { get; set; } = new List<Project>();
	public string AsString => ToString();

	public Client() {
		Id = -1;
		OpenDate = DateTime.MinValue;
		CloseDate = DateTime.MaxValue;
		IsActive = false;
		Name = "John Doe";
		Notes = String.Empty;
	}
	public Client(List<Client> clients,  string name, DateTime open, DateTime? close, string? notes) {
		Id = (clients.Count == 0) ? 1 : clients[^1].Id + 1;
		Name = name;
		OpenDate = open;
		CloseDate = close;
		IsActive = (CloseDate == null) || (CloseDate > DateTime.Today);
		Notes = notes ?? "No notes";
	}
	public override string ToString() {
		string clientString = 
			$"Client ID: {Id}\tClient Name: {Name}\n"                                                     +
			$"Dates: {OpenDate:MM/dd/yyyy} - {CloseDate:MM/dd/yyyy}\tActive: {IsActive}\n" +
			$"Notes: {Notes}\n";
		if (Projects.Count == 0) {
			clientString += "Client has no projects\n";
		}
		else {
			clientString += "Projects: ";
			for (var i = 0; i < Projects.Count - 1; i++) {
				clientString += $"{Projects[i].LongName}, ";
			}
			clientString += $"{Projects[^1].LongName}\n";
		}
		return clientString;
	}
	
	// Project Management - Client acts as the service for project (for now at least)
	public void AddProjectToClient() {
		var p = Project.CreateProject();
		if (Projects.Count > 0)
			p.Id = Projects[^1].Id + 1;
		else {
			p.Id = 1;
		}
		p.ClientId = Id;
		Projects.Add(p);
	}
	
	public void UpdateClientProject(int index) {
		Projects[index] = Project.UpdateProject(GetProjectAtIndex(index));
	}
	public void RemoveProject(int id) {
		int projectIndex = GetProjectIndex(id);
		if (projectIndex != -1)
			Projects.RemoveAt(projectIndex); 
	}
	public void DisplayProject(int index) {
		Console.Write(GetProjectAtIndex(index));
	}
	public void DisplayAllProjects() {
		foreach (Project project in Projects)
			Console.WriteLine(project);
	}
	private int GetProjectIndex(int id) {
		if (id < 0) return -1;
		var i = 0;
		foreach (Project p in Projects) {
			if (p.Id == id) return i;
			i++;
		}
		return -1;
	}
	private Project GetProjectAtIndex(int index) {
		return Projects[index];
	}
}
