using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PracticePanther.API.Database;
using PracticePanther.Library.DTOs;
namespace PracticePanther.API.Controllers; 

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase {
	private readonly ILogger<ClientController> _logger;

	public ClientController(ILogger<ClientController> logger) {
		_logger = logger;
	}
	[HttpGet("/Client/{name:alpha?}")]
	public List<ClientDTO?> Get(string? name = null) {
		SqlCommand cmd;
		if (name == null) {
			const string query = "SELECT * FROM client ORDER BY id ASC";
			cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		}
		else {
			const string query = "SELECT * FROM client WHERE name LIKE ?param_name ORDER BY id ASC";
			cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("param_name", $"%{name}%");
		}
		var clients = new List<ClientDTO?>();
		SqlDataReader? reader = cmd.ExecuteReader();
		while (reader.Read()) {
			clients.Add(new ClientDTO(reader.GetInt32(0), reader.GetString(1), 
					reader.GetDateTime(2), reader.IsDBNull(3) ? null : reader.GetDateTime(3), 
					reader.IsDBNull(5) ? null : reader.GetString(5), reader.GetInt16(4) != 0)
				);
		}
		reader.Close();
		return clients;
	}
	[HttpGet("/Client/{id:int}")]
	public ClientDTO? GetById(int id) {
		const string query = "SELECT name, open, close, notes, active FROM client WHERE id=?param_id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("param_id", id);
		SqlDataReader? reader = cmd.ExecuteReader();
		while (reader.Read()) {
			var c =  new ClientDTO(
				id, reader.GetString(0), reader.GetDateTime(1), reader.IsDBNull(2) ? null : reader.GetDateTime(2),
				reader.IsDBNull(3) ? null : reader.GetString(3), reader.GetInt16(4) != 0);
			reader.Close();
			return c;
		}
		reader.Close();
		return null;
	}
	[HttpDelete("Delete/{id:int}")]
	public int Delete(int id) {
		const string query = "DELETE FROM client WHERE id=?param_id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("param_id", id);
		return cmd.ExecuteNonQuery();
	}
	[HttpPost]
	public int AddOrUpdate([FromBody]ClientDTO c) {
		string open = $"'{c.Open:yyyy-MM-dd hh:mm:ss}'";
		string close = c.Close != null ? $"{c.Close:yyyy-MM-dd hh:mm:ss}" : "null";
		string notes = c.Notes != null ? $"{c.Notes}" : "null";
		if (c.Id == -1) {
			const string query = "INSERT INTO client (name, open, close, active, notes) VALUES (?p_name, ?p_open, ?p_close, '1', ?p_notes)";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_name", c.Name);
			cmd.Parameters.AddWithValue("p_open", open);
			cmd.Parameters.AddWithValue("p_close", close);
			cmd.Parameters.AddWithValue("p_notes", notes);
			return cmd.ExecuteNonQuery();
		}
		else {
			const string query = "REPLACE INTO client (id, name, open, close, active, notes) VALUES (?p_id, ?p_name, ?p_open, ?p_close, ?p_active, ?p_notes)";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_id", c.Id);
			cmd.Parameters.AddWithValue("p_name", c.Name);
			cmd.Parameters.AddWithValue("p_open", open);
			cmd.Parameters.AddWithValue("p_close", close);
			cmd.Parameters.AddWithValue("p_active", c.Active);
			cmd.Parameters.AddWithValue("p_notes", notes);
			return cmd.ExecuteNonQuery();
		}
	}
}