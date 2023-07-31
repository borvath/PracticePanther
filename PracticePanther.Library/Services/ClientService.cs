using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PracticePanther.Library.DTOs;
using PracticePanther.Library.Models;
using PracticePanther.Library.Utilities;

namespace PracticePanther.Library.Services;

public static class ClientService {
	public static void AddOrUpdate(ClientDTO c) {
		new WebRequestHandler().Post("/Client", c).Wait();
	}
	public static void Delete(int id) {
		new WebRequestHandler().Delete($"/Client/Delete/{id}").Wait();
	}
	public static Client? GetClient(int id) {
		string? response = new WebRequestHandler().Get($"/Client/{id}").Result;
		return (response != null) ? JsonConvert.DeserializeObject<ClientDTO>(response)?.ConvertToClient() : null;
	}
	public static List<Client> GetClients(string? query = null) {
		string? response = query == null ? new WebRequestHandler().Get("/Client").Result : new WebRequestHandler().Get($"/Client/{query}").Result;
		List<ClientDTO> dtoList = response != null ? JsonConvert.DeserializeObject<List<ClientDTO>>(response) ?? new List<ClientDTO>() : new List<ClientDTO>();
		return dtoList.Select(c => c.ConvertToClient()).ToList();
	}
}
