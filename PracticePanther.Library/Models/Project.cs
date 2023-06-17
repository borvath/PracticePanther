using System;
using System.Globalization;

namespace PracticePanther.Library.Models;

public class Project {
	
	public int Id { get; set; }
	public int ClientId { get; set; }
	public DateTime OpenDate { get; set; }
	public DateTime? CloseDate { get; set; }
	public bool IsActive { get; set; }
	public string LongName { get; set; }
	public string ShortName { get; set; }
	

	public Project(DateTime open, DateTime close, string lName, string sName) {
		OpenDate = open;
		CloseDate = close;
		IsActive = CheckActive();
		LongName = lName;
		ShortName = sName;
	}
	private bool CheckActive() {
		if (CloseDate == null)
			return true;
		return (CloseDate > DateTime.Today);
	}
	
	public static Project CreateProject() {
		DateTime start = DateTime.Parse(Console.ReadLine() ?? DateTime.MinValue.ToString(CultureInfo.InvariantCulture));
		DateTime end = DateTime.Parse(Console.ReadLine()   ?? DateTime.MaxValue.ToString(CultureInfo.InvariantCulture));
		string longName = Console.ReadLine() ?? "NULL";
		string shortName = Console.ReadLine() ?? longName.Trim();
		return new Project(start, end, longName, shortName);
	}
	
	public static Project UpdateProject(Project p) {
		var userInput = 0;
		while (userInput != 7) {
			userInput = UInt16.Parse(Console.ReadLine() ?? String.Empty);
			switch (userInput) {
				case 1:
					p.OpenDate = DateTime.Parse(Console.ReadLine() ?? DateTime.MinValue.ToString(CultureInfo.InvariantCulture));
					break;
				case 2:
					p.CloseDate = DateTime.Parse(Console.ReadLine() ?? DateTime.MaxValue.ToString(CultureInfo.InvariantCulture));
					break;
				case 3:
					Console.Write("New Project Full Name: ");
					p.LongName = Console.ReadLine() ?? "NULL";
					break;
				case 4:
					Console.Write("New Project Shortened Name: ");
					p.ShortName = Console.ReadLine() ?? p.LongName.Trim();
					break;
				default:
					continue;
			}
		}
		return p;
	}
	
	public override string ToString() {
		return $"Project ID: {Id}\tClient ID: {ClientId}\n" + $"Long Name: {LongName}\tShort Name: {ShortName}\n" +
		       $"Start: {OpenDate:MM/dd/yyyy}\tEnd: {CloseDate:MM/dd/yyyy}\tActive: {IsActive}\n";
	}
}
