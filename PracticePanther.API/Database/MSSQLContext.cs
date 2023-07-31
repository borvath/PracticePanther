using Microsoft.Data.SqlClient;
namespace PracticePanther.API.Database; 

public class MSSQLContext {
	private const string Server = "DESKTOP-1583VLQ";
	private const string DatabaseName = "practicepanther";

	private static MSSQLContext? _instance;
	public static MSSQLContext Current() {
		return _instance ??= new MSSQLContext();
	}
	public SqlConnection? Connection { get; }

	private MSSQLContext() {
		const string conn = $"server='{Server}';database='{DatabaseName}';Trusted_Connection=True;TrustServerCertificate=true;";
		Connection = new SqlConnection(conn);
		Connection.Open();
	}
	public void Close() {
		Connection?.Close();
	}
}
