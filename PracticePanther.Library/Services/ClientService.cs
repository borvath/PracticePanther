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
	public void Add(Client newClient) {
		Clients.Add(newClient);
	}
	public List<Client> Search(string query) {
		return Int32.TryParse(query, out int clientId) ? 
			       Clients.Where(c => (c.Id.ToString().Contains(clientId.ToString()))).ToList() : 
			       Clients.Where(c => (c.Name.Contains(query, StringComparison.OrdinalIgnoreCase))).ToList();
	}
	public void RemoveClient(int index) {
		if (index > 0 && index < Clients.Count) 
			Clients.RemoveAt(index);
	}
	public int GetClientIndex(int id) {
		if (id < 0) 
			return -1;
		var i = 0;
		foreach (Client c in Clients) {
			if (c.Id == id) return i;
			i++;
		}
		return -1;
	}
	public Client GetClientAtIndex(int index) {
		return Clients[index];
	}
}
