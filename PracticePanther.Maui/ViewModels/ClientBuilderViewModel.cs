using System;
using PracticePanther.Library.Models;
using PracticePanther.Library.Services;

namespace PracticePanther.Maui.ViewModels; 

public class ClientBuilderViewModel {
	public string? Name { get; set; }
	public DateTime Open { get; set; }
	public DateTime? Close { get; set; }
	public string? Notes { get; set; }

	public void AddClient() {
		Name ??= "No Name";
		Close ??= DateTime.MaxValue;
		Notes ??= "No Notes";
		ClientService.Current.Add(new Client(ClientService.Current.Clients, Name, Open, Close, Notes));
	}
}
