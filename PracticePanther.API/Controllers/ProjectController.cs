using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PracticePanther.API.Database;
using PracticePanther.Library.DTOs;

namespace PracticePanther.API.Controllers; 

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase {
	private readonly ILogger<ProjectController> _logger;

	public ProjectController(ILogger<ProjectController> logger) {
		_logger = logger;
	}
	[HttpGet("/Project/{name:alpha?}")]
	public List<ProjectDTO?> Get(string? name = null) {
		SqlCommand cmd;
		if (name == null) {
			const string query = "SELECT id, client_id, name, short_name, open_date, close_date, active " +
			                     "FROM practicepanther.project "                                          +
			                     "ORDER BY id";
			cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		}
		else {
			const string query = "SELECT id, client_id, name, short_name, open_date, close_date, active " +
			                     "FROM practicepanther.project "                                          +
			                     "WHERE name LIKE @p_name "                                           +
			                     "ORDER BY id";
			cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_name", $"%{name}%");
		}
		var projects = new List<ProjectDTO?>();
		SqlDataReader? reader = cmd.ExecuteReader();
		while (reader.Read()) {
			projects.Add(new ProjectDTO(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), 
				reader.IsDBNull(3) ? null : reader.GetString(3), reader.GetDateTime(4), 
				reader.IsDBNull(5) ? null : reader.GetDateTime(5),reader.GetBoolean(6))
				);
		}
		reader.Close();
		return projects;
	}
	[HttpGet("/Project/{id:int}")]
	public ProjectDTO? GetById(int id) {
		const string query = "SELECT id, client_id, name, short_name, open_date, close_date, active " +
		                     "FROM practicepanther.project "                                          +
		                     "WHERE id=@p_id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		SqlDataReader? reader = cmd.ExecuteReader();
		while (reader.Read()) {
			var p =  new ProjectDTO(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), 
				reader.IsDBNull(3) ? null : reader.GetString(3), reader.GetDateTime(4), 
				reader.IsDBNull(5) ? null : reader.GetDateTime(5),reader.GetBoolean(6));
			reader.Close();
			return p;
		}
		reader.Close();
		return null;
	}
	[HttpDelete("Delete/{id:int}")]
	public int Delete(int id) {
		const string query = "DELETE FROM practicepanther.project " +
		                     "WHERE id=@p_id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		return cmd.ExecuteNonQuery();
	}
	[HttpPost]
	public int AddOrUpdate([FromBody]ProjectDTO p) {
		if (p.Id == -1) {
			const string query = "INSERT INTO practicepanther.project "                          +
			                     "(client_id, name, short_name, open_date, close_date, active) " +
			                     "VALUES "                                                       +
			                     "(@p_cid, @p_name, @p_short_name, @p_open, @p_close, '1')";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_cid", p.ClientId);
			cmd.Parameters.AddWithValue("p_name", p.Name);
			cmd.Parameters.AddWithValue("p_short_name", p.ShortName != null ? p.ShortName : DBNull.Value);
			cmd.Parameters.AddWithValue("p_open", p.Open);
			cmd.Parameters.AddWithValue("p_close", p.Close != null ? p.Close : DBNull.Value);
			return cmd.ExecuteNonQuery();
		}
		else {
			const string query = "UPDATE practicepanther.project " +
			                     "SET client_id=@c_id, name=@p_name, short_name=@p_short_name, open_date=@p_open, close_date=@p_close, active=@p_active " +
			                     "WHERE id=@p_id";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_id", p.Id);
			cmd.Parameters.AddWithValue("c_id", p.ClientId);
			cmd.Parameters.AddWithValue("p_name", p.Name);
			cmd.Parameters.AddWithValue("p_short_name", p.ShortName != null ? p.ShortName : DBNull.Value);
			cmd.Parameters.AddWithValue("p_open", p.Open);
			cmd.Parameters.AddWithValue("p_close", p.Close != null ? p.Close : DBNull.Value);
			cmd.Parameters.AddWithValue("p_active", p.Active);
			return cmd.ExecuteNonQuery();
		}
	}
}
