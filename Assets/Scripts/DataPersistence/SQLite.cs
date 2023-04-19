using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public class SQLite : MonoBehaviour
{
    public static SQLite Instance;
    // Start is called before the first frame update
    void Start()
    {
        if(!Instance){
            Destroy(this);
        }else{
            Instance = this;
        }
        // IDbConnection dbConnection = CreateAndOpenDatabase();
        // IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        // dbCommandReadValues.CommandText = "SELECT * FROM HitCountTableSimple";
        // IDataReader dataReader = dbCommandReadValues.ExecuteReader();
        // dbConnection.Close();
        UpdateLeaderboard(0,1);
    }

    
    void UpdateLeaderboard(int id, int deaths){
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandInsert = dbConnection.CreateCommand();
        dbCommandInsert.CommandText = "INSERT OR REPLACE INTO HitCountTableSimple (id, deaths) VALUES (" + id + ", " + deaths + ")";
        dbCommandInsert.ExecuteNonQuery();
        dbConnection.Close();
    }
    private IDbConnection CreateAndOpenDatabase()
    {
        string dbUri = "URI=file:MyDatabase.sqlite"; 
        IDbConnection dbConnect = new SqliteConnection(dbUri);
        dbConnect.Open();
        IDbCommand dbCommandCreateTable = dbConnect.CreateCommand(); // 6
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HitCountTableSimple (id INTEGER PRIMARY KEY, deaths INTEGER )"; // 7
        dbCommandCreateTable.ExecuteReader(); // 8

        return dbConnect;
    }
}