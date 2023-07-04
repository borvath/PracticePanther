using System;
using System.Collections.Generic;
using System.Linq;
using PracticePanther.Library.Models;

namespace PracticePanther.Library.Services;

public class ProjectService {
	
	private static object _lock = new object();
	private static ProjectService? instance;
	public static ProjectService Current {
		get {
			lock (_lock) {
				return instance ??= new ProjectService();
			}
		} 
	}
	public List<Project> Projects { get; }
	
	public ProjectService() {
		Projects = new List<Project>();
	}
	
	public void AddProject(Project p, int clientId) {
		p.Id = Projects.Count == 0 ? 1 : Projects[^1].Id + 1;
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
