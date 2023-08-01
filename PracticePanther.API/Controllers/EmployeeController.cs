using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PracticePanther.API.Database;
using PracticePanther.Library.DTOs;
namespace PracticePanther.API.Controllers; 

[ApiController]
[Route("[controller]")]
public class EmployeeController {
	private readonly ILogger<EmployeeController> _logger;

	public EmployeeController(ILogger<EmployeeController> logger) {
		_logger = logger;
	}
	[HttpGet("/Employee/{name:alpha?}")]
	public List<EmployeeDTO?> Get(string? name = null) {
		SqlCommand cmd;
		if (name == null) {
			const string query = "SELECT id, name, rate " +
			                     "FROM practicepanther.employee "                           +
			                     "ORDER BY id";
			cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		}
		else {
			const string query = "SELECT id, name, rate " +
			                     "FROM practicepanther.employee "                           +
			                     "WHERE name LIKE @p_name "                           +
			                     "ORDER BY id";
			cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_name", $"%{name}%");
		}
		var employees = new List<EmployeeDTO?>();
		SqlDataReader? reader = cmd.ExecuteReader();
		while (reader.Read()) {
			employees.Add(new EmployeeDTO(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2)));
		}
		reader.Close();
		return employees;
	}
	[HttpGet("/Employee/{id:int}")]
	public EmployeeDTO? GetById(int id) {
		const string query = "SELECT name, rate " +
		                     "FROM practicepanther.employee "                       +
		                     "WHERE id=@p_id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		SqlDataReader? reader = cmd.ExecuteReader();
		while (reader.Read()) {
			var e =  new EmployeeDTO(id, reader.GetString(0), reader.GetDecimal(1));
			reader.Close();
			return e;
		}
		reader.Close();
		return null;
	}
	[HttpDelete("Delete/{id:int}")]
	public int Delete(int id) {
		const string query = "DELETE FROM practicepanther.employee " +
		                     "WHERE id=@p_id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		return cmd.ExecuteNonQuery();
	}
	[HttpPost]
	public int AddOrUpdate([FromBody]EmployeeDTO e) {
		if (e.Id == -1) {
			const string query = "INSERT INTO practicepanther.employee "           +
			                     "(name, rate) " +
			                     "VALUES "                                       +
			                     "(@p_name, @p_rate)";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_name", e.Name);
			cmd.Parameters.AddWithValue("p_rate", e.Rate);
			return cmd.ExecuteNonQuery();
		}
		else {
			const string query = "UPDATE practicepanther.employee " +
			                     "SET name=@p_name, rate=@p_rate " +
			                     "WHERE id=@p_id";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_id", e.Id);
			cmd.Parameters.AddWithValue("p_name", e.Name);
			cmd.Parameters.AddWithValue("p_rate", e.Rate);
			return cmd.ExecuteNonQuery();
		}
	}
}
