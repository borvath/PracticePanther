using System;

namespace PracticePanther.Library.Models;

public class Project {
	
	public int Id { get; set; }
	public int ClientId { get; set; }
	public DateTime? Open { get; set; }
	public DateTime? Close { get; set; }
	public bool IsActive { get; set; }
	public string LongName { get; set; }
	public string? ShortName { get; set; }
	public string AsString => ToString();
	public string AsShortString => ShortToString();

	public Project() {
		Id = 0;
		Open = DateTime.Today;
		Close = DateTime.Today.AddYears(1);
		IsActive = true;
		LongName = "Default Project";
		ShortName = "Project";
	}
	public Project(DateTime? open, DateTime? close, string longName, string? shortName) {
		Open = open;
		Close = close;
		IsActive = true;
		LongName = longName;
		ShortName = shortName;
	}
	public string ShortToString() {
		return $"Project ID: {Id, -7}Project Name: {LongName}";
	}
	public override string ToString() {
		return $"Project ID: {Id}\tClient ID: {ClientId}\tProject Name: {LongName}";
	}
}
