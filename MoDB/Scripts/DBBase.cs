using System.Data.SQLite;

using Mophi;

namespace Mophi.DB
{
	public class DBBase
	{
		private SQLiteConnection sqlConn = null;

		private DBBase(string sqlConnStr)
		{
			if (sqlConn == null)
				sqlConn = new SQLiteConnection(sqlConnStr);

			sqlConn.Open();
		}

		public SQLiteDataReader DoQuery(string sqlStr)
		{
			try
			{
				SQLiteCommand sqlCmd = sqlConn.CreateCommand();
				sqlCmd.CommandText = sqlStr;
				SQLiteDataReader reader = sqlCmd.ExecuteReader();

				return reader;
			}
			catch (SQLiteException e)
			{
				Logger.Error(string.Format("ErrorCode: {0} \n{1} \n{2} \n{3}", e.ErrorCode, e.Message, e.StackTrace, e.HelpLink));
				return null;
			}
		}

		public int DoNoneQuery(string sqlStr)
		{
			try
			{
				SQLiteCommand sqlCmd = sqlConn.CreateCommand();
				sqlCmd.CommandText = sqlStr;
				return sqlCmd.ExecuteNonQuery();
			}
			catch (SQLiteException e)
			{
				Logger.Error(string.Format("ErrorCode: {0} \n{1} \n{2} \n{3}", e.ErrorCode, e.Message, e.StackTrace, e.HelpLink));
				return 0;
			}
		}
	}
}
