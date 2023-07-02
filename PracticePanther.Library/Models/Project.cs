using System;
using PracticePanther.Library.Services;

namespace PracticePanther.Library.Models;

public class Project {
	
	public int Id { get; set; }
	public int ClientId { get; set; }
	public DateTime? Open { get; set; }
	public DateTime? Close { get; set; }
	public bool IsActive { get; set; }
	public string LongName { get; set; }
	public string? ShortName { get; set; }
	public TimeService TimeService { get; set; } = new TimeService();
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
		string projIdString = "Project ID: {0, -7}";
		string longNameString = "Project Name: {1, -" + (LongName.Length + 7) + "}";
		string isActiveString = "Active: {2}";
		string ret = projIdString + longNameString + isActiveString;
		return String.Format(ret, Id, LongName, IsActive);
	}
	public override string ToString() {
		string projIdString = "Project ID: {0, -7}";
		string clientIdString = "Client ID: {1, -7}";
		string longNameString = "Project Name: {2, -" + (LongName.Length + 7) + "}";
		string isActiveString = "Active: {3}";
		string ret = projIdString + clientIdString + longNameString + isActiveString;
		return String.Format(ret, Id, ClientId, LongName, IsActive);
	}
}
