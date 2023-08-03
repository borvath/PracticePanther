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
			const string query = "SELECT id, project_id, employee_id, bill_id, hours, due_date, summary " +
			                     "FROM practicepanther.time "                                          +
			                     "ORDER BY id";
			cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		}
		else {
			var query = String.Empty;
			if (name.ToLower().StartsWith("proj:")) {
				name = name.Split(':')[1];
				query = "SELECT time.id, project_id, employee_id, bill_id, hours, due_date, summary " +
				        "FROM practicepanther.time JOIN practicepanther.project "               +
				        "ON time.project_id = project.id "                                           +
				        "WHERE project.name LIKE @p_name "                                      +
				        "ORDER BY id";
			}
			if (name.ToLower().StartsWith("emp:")) {
				name = name.Split(':')[1];
				query = "SELECT time.id, project_id, employee_id, bill_id, hours, due_date, summary " +
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
				reader.IsDBNull(3) ? null : reader.GetInt32(3), reader.GetDecimal(4), reader.GetDateTime(5), 
				reader.IsDBNull(6) ? null : reader.GetString(6))
			);
		}
		reader.Close();
		return times;
	}
	[HttpGet("/Time/{id:int}")]
	public TimeDTO? GetById(int id) {
		const string query = "SELECT id, project_id, employee_id, bill_id, hours, due_date, summary " +
		                     "FROM practicepanther.time "                                          +
		                     "WHERE id = @p_id";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		SqlDataReader? reader = cmd.ExecuteReader();
		while (reader.Read()) {
			var t =  new TimeDTO(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), 
				reader.IsDBNull(3) ? null : reader.GetInt32(3), reader.GetDecimal(4), reader.GetDateTime(5), 
				reader.IsDBNull(6) ? null : reader.GetString(6));
			reader.Close();
			return t;
		}
		reader.Close();
		return null;
	}
	[HttpGet("/Time/proj/{id:int}")]
	public List<TimeDTO> GetByProject(int id) {
		const string query = "SELECT id, project_id, employee_id, bill_id, hours, due_date, summary " +
		                     "FROM practicepanther.time "                                            +
		                     "WHERE project_id = @p_id ";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		SqlDataReader? reader = cmd.ExecuteReader();
		var times = new List<TimeDTO>();
		while (reader.Read()) {
			times.Add(new TimeDTO(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), 
				reader.IsDBNull(3) ? null : reader.GetInt32(3), reader.GetDecimal(4), reader.GetDateTime(5), 
				reader.IsDBNull(6) ? null : reader.GetString(6))
			);
		}
		reader.Close();
		return times;
	}
	[HttpDelete("Delete/{id:int}")]
	public int Delete(int id) {
		var query = "SELECT bill_id FROM practicepanther.time WHERE id=@p_id; ";
		var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		var bill_id = (int)cmd.ExecuteScalar();
		query = $"UPDATE practicepanther.time "      +
		        $"SET bill_id = NULL "               +
		        $"WHERE bill_id = {bill_id} "        +
		        $"DELETE FROM practicepanther.bill " +
		        $"WHERE id = {bill_id}; "            +
		        $"DELETE FROM practicepanther.time " +
		        $"WHERE id = @p_id ";
		cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
		cmd.Parameters.AddWithValue("p_id", id);
		return cmd.ExecuteNonQuery();
	}
	[HttpPost]
	public int AddOrUpdate([FromBody]TimeDTO t) {
		if (t.Id == -1) {
			const string query = "INSERT INTO practicepanther.time "                          +
			                     "(project_id, employee_id, hours, due_date, summary) " +
			                     "VALUES "                                                       +
			                     "(@p_pid, @p_empid, @p_hours, @p_due_date, @p_summary)";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_pid", t.ProjectId);
			cmd.Parameters.AddWithValue("p_empid", t.EmployeeId);
			cmd.Parameters.AddWithValue("p_hours", t.Hours);
			cmd.Parameters.AddWithValue("p_due_date", t.Date);
			cmd.Parameters.AddWithValue("p_summary", t.Narrative == null ? DBNull.Value : t.Narrative);
			return cmd.ExecuteNonQuery();
		}
		if (t.BillId == -1) {
			const string query = "UPDATE practicepanther.time " +
			                     "SET bill_id = (SELECT MAX(id) FROM practicepanther.bill) " +
			                     "WHERE id = @p_id";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_id", t.Id);
			return cmd.ExecuteNonQuery();
		}
		else {
			const string query = "UPDATE practicepanther.time " +
			                     "SET project_id=@p_pid, employee_id=@p_empid, bill_id=@p_billid, hours=@p_hours, due_date=@p_due_date, summary=@p_summary " +
			                     "WHERE id=@p_id";
			var cmd = new SqlCommand(query, MSSQLContext.Current().Connection);
			cmd.Parameters.AddWithValue("p_id", t.Id);
			cmd.Parameters.AddWithValue("p_pid", t.ProjectId);
			cmd.Parameters.AddWithValue("p_empid", t.EmployeeId);
			cmd.Parameters.AddWithValue("p_billid", t.BillId == null ? DBNull.Value : t.BillId);
			cmd.Parameters.AddWithValue("p_hours", t.Hours);
			cmd.Parameters.AddWithValue("p_due_date", t.Date);
			cmd.Parameters.AddWithValue("p_summary", t.Narrative == null ? DBNull.Value : t.Narrative);
			return cmd.ExecuteNonQuery();
		}
	}
}
