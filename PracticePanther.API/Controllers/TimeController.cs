using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PracticePanther.API.Database;
using PracticePanther.Library.DTOs;

namespace PracticePanther.API.Controllers; 

[ApiController]
[Route("[controller]")]
public class TimeController : ControllerBase{
	private readonly ILogger<TimeController> _logger;

	public TimeController(ILogger<TimeController> logger) {
		_logger = logger;
	}
	[HttpGet("/Time/{name?}")]
	public List<TimeDTO?> Get(string? name = null) {
		SqlCommand cmd;
		if (name == null || (!name.ToLower().StartsWith("proj:") && (!name.ToLower().StartsWith("emp:")))) {
			const string query = "SELECT id, project_id, employee_id, hours, due_date, summary, billed " +
			                     "FROM practicepanther.time "                                          +
			                     "ORDER BY id";
			cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		}
		else {
			var query = String.Empty;
			if (name.ToLower().StartsWith("proj:")) {
				name = name.Split(':')[1];
				query = "SELECT time.id, project_id, employee_id, hours, due_date, summary, billed " +
				        "FROM practicepanther.time JOIN practicepanther.project "               +
				        "ON time.project_id = project.id "                                           +
				        "WHERE project.name LIKE @p_name "                                      +
				        "ORDER BY id";
			}
			if (name.ToLower().StartsWith("emp:")) {
				name = name.Split(':')[1];
				query = "SELECT time.id, project_id, employee_id, hours, due_date, summary, billed " +
				        "FROM practicepanther.time JOIN practicepanther.employee "               +
				        "ON time.employee_id = employee.id "                                                                     +
				        "WHERE name LIKE @p_name "                                              +
				        "ORDER BY id";
			}
			cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_name", $"%{name}%");
		}
		var times = new List<TimeDTO?>();
		SqlDataReader? reader = cmd.ExecuteReader();
		while (reader.Read()) {
			times.Add(new TimeDTO(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), 
				reader.GetDecimal(3), reader.GetDateTime(4), reader.IsDBNull(5) ? null : reader.GetString(5),
				reader.GetBoolean(6))
			);
		}
		reader.Close();
		return times;
	}
	[HttpGet("/Time/{id:int}")]
	public TimeDTO? GetById(int id) {
		const string query = "SELECT id, project_id, employee_id, hours, due_date, summary, billed " +
		                     "FROM practicepanther.time "                                          +
		                     "WHERE id=@p_id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		SqlDataReader? reader = cmd.ExecuteReader();
		while (reader.Read()) {
			var t =  new TimeDTO(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), 
				reader.GetDecimal(3), reader.GetDateTime(4), reader.IsDBNull(5) ? null : reader.GetString(5),
				reader.GetBoolean(6));
			reader.Close();
			return t;
		}
		reader.Close();
		return null;
	}
	[HttpDelete("Delete/{id:int}")]
	public int Delete(int id) {
		const string query = "DELETE FROM practicepanther.time " +
		                     "WHERE id=@p_id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		return cmd.ExecuteNonQuery();
	}
	[HttpPost]
	public int AddOrUpdate([FromBody]TimeDTO t) {
		if (t.Id == -1) {
			const string query = "INSERT INTO practicepanther.time "                          +
			                     "(project_id, employee_id, hours, due_date, summary, billed) " +
			                     "VALUES "                                                       +
			                     "(@p_pid, @p_empid, @p_hours, @p_due_date, @p_summary, '0')";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_pid", t.ProjectId);
			cmd.Parameters.AddWithValue("p_empid", t.EmployeeId);
			cmd.Parameters.AddWithValue("p_hours", t.Hours);
			cmd.Parameters.AddWithValue("p_due_date", t.Date);
			cmd.Parameters.AddWithValue("p_summary", t.Narrative == null ? DBNull.Value : t.Narrative);
			return cmd.ExecuteNonQuery();
		}
		else {
			const string query = "UPDATE practicepanther.time " +
			                     "SET project_id=@p_pid, employee_id=@p_empid, hours=@p_hours, due_date=@p_due_date, summary=@p_summary, billed=@p_billed " +
			                     "WHERE id=@p_id";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_id", t.Id);
			cmd.Parameters.AddWithValue("p_pid", t.ProjectId);
			cmd.Parameters.AddWithValue("p_empid", t.EmployeeId);
			cmd.Parameters.AddWithValue("p_hours", t.Hours);
			cmd.Parameters.AddWithValue("p_due_date", t.Date);
			cmd.Parameters.AddWithValue("p_summary", t.Narrative == null ? DBNull.Value : t.Narrative);
			cmd.Parameters.AddWithValue("p_billed", t.HasBeenBilled);
			return cmd.ExecuteNonQuery();
		}
	}
}
