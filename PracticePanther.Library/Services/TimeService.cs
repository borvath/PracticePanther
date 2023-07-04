using System;
using System.Collections.Generic;
using System.Linq;
using PracticePanther.Library.Models;

namespace PracticePanther.Library.Services;

public class TimeService {
	
	private static object _lock = new object();
	private static TimeService? instance;
	public static TimeService Current {
		get {
			lock (_lock) {
				return instance ??= new TimeService();
			}
		}
	}
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
	public Time? GetTime(int timeId) {
		return Times.FirstOrDefault(t => t.Id == timeId);
	}
	public List<Time> Search(string query) {
		Int32.TryParse(query, out int id);
		return Times.Where(t => (t.Id.ToString().StartsWith(id.ToString()))).ToList();
	}
}
