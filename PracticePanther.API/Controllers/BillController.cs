using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PracticePanther.API.Database;
using PracticePanther.Library.DTOs;

namespace PracticePanther.API.Controllers; 

[ApiController]
[Route("[controller]")]
public class BillController {
	private readonly ILogger<BillController> _logger;

	public BillController(ILogger<BillController> logger) {
		_logger = logger;
	}
	[HttpGet("/Bill/{id:int}")]
	public BillDTO? GetById(int id) {
		const string query = "SELECT id, project_id, total_amount, due_date " +
		                     "FROM practicepanther.bill "                                          +
		                     "WHERE id = @p_id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		SqlDataReader? reader = cmd.ExecuteReader();
		while (reader.Read()) {
			var b = new BillDTO(reader.GetInt32(0), reader.GetInt32(1), reader.GetDecimal(2), reader.GetDateTime(4));
			reader.Close();
			return b;
		}
		reader.Close();
		return null;
	}
	[HttpGet("/Bill/Project/{id:int?}")]
	public List<BillDTO?> Get(int? id = null) {
		const string query = "SELECT id, project_id, total_amount, due_date " + 
		                     "FROM practicepanther.bill "                     +
		                     "WHERE project_id = @p_id "                      +
		                     "ORDER BY id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		var bills = new List<BillDTO?>();
		SqlDataReader? reader = cmd.ExecuteReader();
		while (reader.Read()) {
			bills.Add(new BillDTO(reader.GetInt32(0), reader.GetInt32(1), reader.GetDecimal(2), reader.GetDateTime(3)));
		}
		reader.Close();
		return bills;
	}
	[HttpDelete("Delete/{id:int}")]
	public int Delete(int id) {
		const string query = "DELETE FROM practicepanther.bill " +
		                     "WHERE id = @p_id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		return cmd.ExecuteNonQuery();
	}
	[HttpPost]
	public int AddOrUpdate([FromBody]BillDTO b) {
		if (b.Id == -1) {
			const string query = "INSERT INTO practicepanther.bill "                          +
			                     "(project_id, total_amount, due_date) " +
			                     "VALUES "                                                       +
			                     "(@p_pid, @p_total, @p_due_date)";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_pid", b.ProjectId);
			cmd.Parameters.AddWithValue("p_total", b.TotalAmount);
			cmd.Parameters.AddWithValue("p_due_date", b.DueDate);
			return cmd.ExecuteNonQuery();
		}
		else {
			const string query = "UPDATE practicepanther.bill " +
			                     "SET project_id=@p_pid, total_amount=@p_total, due_date=@p_due_date " +
			                     "WHERE id=@p_id";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_id", b.Id);
			cmd.Parameters.AddWithValue("p_pid", b.ProjectId);
			cmd.Parameters.AddWithValue("p_total", b.TotalAmount);
			cmd.Parameters.AddWithValue("p_due_date", b.DueDate);
			return cmd.ExecuteNonQuery();
		}
	}
}
