using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PracticePanther.Library.DTOs;
using PracticePanther.Library.Models;
using PracticePanther.Library.Utilities;

namespace PracticePanther.Library.Services;

public static class TimeService {
	public static void AddOrUpdate(TimeDTO t) {
		new WebRequestHandler().Post("/Time", t).Wait();
	}
	public static void Delete(int id) {
		new WebRequestHandler().Delete($"/Time/Delete/{id}").Wait();
	}
	public static Time? GetTime(int id) {
		string? response = new WebRequestHandler().Get($"/Time/{id}").Result;
		return (response != null) ? JsonConvert.DeserializeObject<TimeDTO>(response)?.ConvertToTime() : null;
	}
	public static List<Time> GetTimes(string? query = null) {
		string? response = query == null ? new WebRequestHandler().Get("/Time").Result : new WebRequestHandler().Get($"/Time/{query}").Result;
		List<TimeDTO> dtoList = response != null ? JsonConvert.DeserializeObject<List<TimeDTO>>(response) ?? new List<TimeDTO>() : new List<TimeDTO>();
		return dtoList.Select(t => t.ConvertToTime()).ToList();
	}
	public static decimal BillTime(int id) {
		Time? t = GetTime(id);
		if (t != null) {
			t.HasBeenBilled = true;
			return t.Hours * EmployeeService.GetEmployee(t.EmployeeId)?.Rate ?? 0;
		}
		return 0;
	}
}
