using System;
using PracticePanther.Library.Models;

namespace PracticePanther.Library.DTOs; 

public class ClientDTO {
	public int Id { get; }
	public string Name { get; }
	public DateTime Open { get; }
	public DateTime? Close { get; }
	public bool Active { get; }
	public string? Notes { get; }
	
	public ClientDTO(int id, string name, DateTime open, DateTime? close, string? notes, bool active) {
		Id = id;
		Name = name;
		Open = open;
		Close = close;
		Notes = notes;
		Active = active;
	}
	public Client ConvertToClient() {
		return new Client(Id, Name, Open, Close, Notes, Active);
	}
}
