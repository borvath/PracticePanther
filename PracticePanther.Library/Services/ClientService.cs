using System;
using System.Collections.Generic;
using System.Linq;
using PracticePanther.Library.Models;

namespace PracticePanther.Library.Services;

public class ClientService {
	private static object _lock = new object();
	private static ClientService? instance;
	public static ClientService Current {
		get {
			lock (_lock) {
				return instance ??= new ClientService();
			}
		}
	}
	public List<Client> Clients { get; }

	private ClientService() {
		Clients = new List<Client>();
	}
	
	public void Add(Client c) {
		if (c.Id == 0) {
			c.Id = Clients[^1].Id + 1;
		}
		Clients.Add(c);
	}
	public Client? GetClient(int id) {
		return Clients.FirstOrDefault(c => c.Id == id);
	}
	public List<Client> Search(string query) {
		return Int32.TryParse(query, out int clientId) ? 
			       Clients.Where(c => (c.Id.ToString().StartsWith(clientId.ToString()))).ToList() : 
			       Clients.Where(c => (c.Name.Contains(query, StringComparison.OrdinalIgnoreCase))).ToList();
	}
}
