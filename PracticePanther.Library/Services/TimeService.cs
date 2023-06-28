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

	private TimeService() {
		Times = new List<Time>();
	}

	public List<Time> Search(string query) {
		return Int32.TryParse(query, out int id) ? 
			       Times.Where(t => (t.ProjectId.ToString().StartsWith(id.ToString()))).ToList() : 
			       Times.Where(t => (t.EmployeeId.ToString().StartsWith(id.ToString()))).ToList();
	}
}
