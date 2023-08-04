using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PracticePanther.Library.DTOs;
using PracticePanther.Library.Models;
using PracticePanther.Library.Utilities;

namespace PracticePanther.Library.Services; 

public static class BillService {
	
	public static void AddOrUpdate(BillDTO b) {
		new WebRequestHandler().Post("/Bill", b).Wait();
	}
	public static void Delete(int id) {
		new WebRequestHandler().Delete($"/Bill/Delete/{id}").Wait();
	}
	public static Bill? GetBill(int id) {
		string? response = new WebRequestHandler().Get($"/Bill/{id}").Result;
		return (response != null) ? JsonConvert.DeserializeObject<BillDTO>(response)?.ConvertToBill() : null;
	}
	public static List<Bill> GetBills(int projectId) {
		string? response = new WebRequestHandler().Get($"/Bill/Project/{projectId}").Result;
		List<BillDTO> dtoList = response != null ? JsonConvert.DeserializeObject<List<BillDTO>>(response) ?? new List<BillDTO>() : new List<BillDTO>();
		return dtoList.Select(b => b.ConvertToBill()).ToList();
	}
}
