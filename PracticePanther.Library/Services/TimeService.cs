using System;
using System.Collections.Generic;
using System.Linq;
using PracticePanther.Library.Models;

namespace PracticePanther.Library.Services;

public class TimeService {
	
	public List<Time> Times { get; }

	public TimeService() {
		Times = new List<Time>();
	}

	public void AddTime(Time t, Project p) {
		t.Id = Times.Count == 0 ? 1 : Times[^1].Id + 1;
		t.ClientId = p.ClientId;
		t.ProjectId = p.Id;
		Times.Add(t);
	}
	public void Remove(Time t) {
		Times.Remove(t);
	}
	public Time? GetTime(int clientId, int projectId, int timeId) {
		return Times.FirstOrDefault(t => t.ClientId == clientId && t.ProjectId == projectId && t.Id == timeId);
	}
	public List<Time> Search(string query) {
		return Int32.TryParse(query, out int id) ? 
			       Times.Where(t => (t.ProjectId.ToString().StartsWith(id.ToString()))).ToList() : 
			       Times.Where(t => (t.EmployeeId.ToString().StartsWith(id.ToString()))).ToList();
	}
}
