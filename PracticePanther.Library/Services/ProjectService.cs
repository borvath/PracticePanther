using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PracticePanther.Library.DTOs;
using PracticePanther.Library.Models;
using PracticePanther.Library.Utilities;

namespace PracticePanther.Library.Services;

public static class ProjectService {
	public static void AddOrUpdate(ProjectDTO p) {
		new WebRequestHandler().Post("/Project", p).Wait();
	}
	public static void Delete(int id) {
		new WebRequestHandler().Delete($"/Project/Delete/{id}").Wait();
	}
	public static Project? GetProject(int id) {
		string? response = new WebRequestHandler().Get($"/Project/{id}").Result;
		return (response != null) ? JsonConvert.DeserializeObject<ProjectDTO>(response)?.ConvertToProject() : null;
	}
	public static List<Project> GetProjects(string? query = null) {
		string? response = (query == null) ? new WebRequestHandler().Get("/Project").Result : new WebRequestHandler().Get($"/Project/{query}").Result;
		List<ProjectDTO> dtoList = (response != null) ? JsonConvert.DeserializeObject<List<ProjectDTO>>(response) ?? new List<ProjectDTO>() : new List<ProjectDTO>();
		return dtoList.Select(p => p.ConvertToProject()).ToList();
	}
}
