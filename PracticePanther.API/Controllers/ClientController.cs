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
			const string query = "SELECT id, name, open_date, close_date, notes, active " +
			                     "FROM practicepanther.client "                           +
			                     "ORDER BY id";
			cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		}
		else {
			const string query = "SELECT id, name, open_date, close_date, notes, active " +
			                     "FROM practicepanther.client "                           +
			                     "WHERE name LIKE @p_name "                           +
			                     "ORDER BY id";
			cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_name", $"%{name}%");
		}
		var clients = new List<ClientDTO?>();
		SqlDataReader? reader = cmd.ExecuteReader();
		while (reader.Read()) {
			clients.Add(new ClientDTO(reader.GetInt32(0), reader.GetString(1), 
					reader.GetDateTime(2), reader.IsDBNull(3) ? null : reader.GetDateTime(3), 
					reader.IsDBNull(4) ? null : reader.GetString(4), reader.GetBoolean(5))
				);
		}
		reader.Close();
		return clients;
	}
	[HttpGet("/Client/{id:int}")]
	public ClientDTO? GetById(int id) {
		const string query = "SELECT name, open_date, close_date, notes, active " +
		                     "FROM practicepanther.client "                       +
		                     "WHERE id=@p_id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		SqlDataReader? reader = cmd.ExecuteReader();
		while (reader.Read()) {
			var c =  new ClientDTO(
				id, reader.GetString(0), reader.GetDateTime(1), reader.IsDBNull(2) ? null : reader.GetDateTime(2),
				reader.IsDBNull(3) ? null : reader.GetString(3), reader.GetBoolean(4));
			reader.Close();
			return c;
		}
		reader.Close();
		return null;
	}
	[HttpDelete("Delete/{id:int}")]
	public int Delete(int id) {
		const string query = "DELETE FROM practicepanther.client " +
		                     "WHERE id=@p_id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		return cmd.ExecuteNonQuery();
	}
	[HttpPost]
	public int AddOrUpdate([FromBody]ClientDTO c) {
		if (c.Id == -1) {
			const string query = "INSERT INTO practicepanther.client "           +
			                     "(name, open_date, close_date, notes, active) " +
			                     "VALUES "                                       +
			                     "(@p_name, @p_open, @p_close, @p_notes, '1')";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_name", c.Name);
			cmd.Parameters.AddWithValue("p_open", c.Open);
			cmd.Parameters.AddWithValue("p_close", c.Close != null ? c.Close : DBNull.Value);
			cmd.Parameters.AddWithValue("p_notes", c.Notes != null ? $"{c.Notes}" : DBNull.Value);
			return cmd.ExecuteNonQuery();
		}
		else {
			const string query = "UPDATE practicepanther.client " +
			                     "SET name=@p_name, open_date=@p_open, close_date=@p_close, notes=@p_notes, active=@p_active " +
			                     "WHERE id=@p_id";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_id", c.Id);
			cmd.Parameters.AddWithValue("p_name", c.Name);
			cmd.Parameters.AddWithValue("p_open", c.Open);
			cmd.Parameters.AddWithValue("p_close", c.Close != null ? c.Close : DBNull.Value);
			cmd.Parameters.AddWithValue("p_notes", c.Notes != null ? c.Notes : DBNull.Value);
			cmd.Parameters.AddWithValue("p_active", c.Active);
			return cmd.ExecuteNonQuery();
		}
	}
}