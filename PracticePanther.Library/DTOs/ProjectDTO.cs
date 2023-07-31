using System;
using PracticePanther.Library.Models;

namespace PracticePanther.Library.DTOs; 

public class ProjectDTO {
	public int Id { get; }
	public int ClientId { get; }
	public DateTime Open { get ; }
	public DateTime? Close { get; }
	public string Name { get; }
	public string? ShortName { get; }
	public bool Active { get; }

	public ProjectDTO(int id, int clientId, string name, string? shortName, DateTime open, DateTime? close, bool active) {
		Id = id;
		ClientId = clientId;
		Open = open;
		Close = close;
		Name = name;
		ShortName = shortName;
		Active = active;
	}
	public Project ConvertToProject() {
		return new Project(Id, ClientId, Name, ShortName, Open, Close, Active);
	}
}
