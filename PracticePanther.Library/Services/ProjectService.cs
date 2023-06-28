using System;
using System.Collections.Generic;
using System.Linq;
using PracticePanther.Library.Models;

namespace PracticePanther.Library.Services;

public class ProjectService {
	
	public List<Project> Projects { get; }
	
	public ProjectService() {
		Projects = new List<Project>();
	}
	
	public void AddProject(Project p, int clientId) {
		if (p.Id == 0) {
			p.Id = Projects[^1].Id + 1;
		}
		p.ClientId = clientId;
		Projects.Add(p);
	}
	public Project? GetProject(int id) {
		return Projects.FirstOrDefault(p => p.Id == id);
	}
	public List<Project> Search(string query) {
		return Int32.TryParse(query, out int projectId) ? 
			       Projects.Where(p => (p.Id.ToString().StartsWith(projectId.ToString()))).ToList() : 
			       Projects.Where(p => (p.LongName.Contains(query, StringComparison.OrdinalIgnoreCase))).ToList();
	}
}
