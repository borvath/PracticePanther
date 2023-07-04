﻿using System;
namespace PracticePanther.Library.Models; 
public class Time {
	
	public int Id { get; set; }
	public int ClientId { get; set; }
	public int ProjectId { get; set; }
	public int EmployeeId { get; set; }
	public double Hours { get; set; }
	public DateTime? Date { get; set; }
	public string? Narrative { get; set; }
	public string AsClientShortString => ToClientShortString();
	public string AsEmployeeShortString => ToEmployeeShortString();
	public string AsString => ToString();

	public Time() {
		ProjectId = 0;
		EmployeeId = 0;
		Hours = 0;
		Date = DateTime.Today;
		Narrative = "Default Entry";
	}
	public Time(int empId, double hours, DateTime? date, string? narrative) {
		EmployeeId = empId;
		Hours = hours;
		Date = date;
		Narrative = narrative;
	}
	public string ToClientShortString() {
		return $"EmployeeID: {EmployeeId, -5}Hours: {Hours, -5}Date: {Date:MM/dd/yyyy}";
	}
	public string ToEmployeeShortString() {
		return $"ClientID: {ClientId, -5}ProjectID: {ProjectId, -5}Hours: {Hours, -5}Date: {Date:MM/dd/yyyy}";
	}
	public override string ToString() {
		return $"ClientID: {ClientId,-5}ProjectID: {ProjectId,-5}EmployeeID: {EmployeeId, -5}TimeID: {Id, -5}Hours: {Hours, -5}Date: {Date:MM/dd/yyyy}";
	}
}
