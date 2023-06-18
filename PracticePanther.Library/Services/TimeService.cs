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
	public void Add(Time newTime) {
		Times.Add(newTime);
	}
	public List<Time> Search(string query) {
		List<Time>? ret = null;
		if (Int32.TryParse(query, out int id)) {
			ret = Times.Where(t => (t.ProjectId.ToString().Contains(id.ToString()))).ToList();
			if (ret.Count == 0) {
				ret = Times.Where(t => (t.EmployeeId.ToString().Contains(id.ToString()))).ToList();
			}
		}
		if (ret == null) {
			ret = new List<Time>();
			// if program is upset about empty list add a default time
		}
		return ret;
	}
}
