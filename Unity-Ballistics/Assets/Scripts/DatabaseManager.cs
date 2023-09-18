using System.IO;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Collections.Generic;

public class DatabaseManager : MonoBehaviour, IDatabaseManager
{
    private static DatabaseManager instance;
    public static DatabaseManager Instance { get { return instance; } }
    public string DataBasePath;
    private bool _connectedToDatabase = false;
    private IDbConnection _iDatabaseConnection;
    private IDbCommand _iDatabaseCommand;

    public bool ConnectedToDatabase { get { return _connectedToDatabase; } }

    private void Start()
    {
        instance = this;
    }

    public void ConnectToDatabase()
    {
        if (_connectedToDatabase) return;
        string path = Application.persistentDataPath + '/' + DataBasePath;
        if (!File.Exists(path))
        {
            Debug.Log("Data Base file does not exists in " + path);
        } 
        else
        {
            string pathToDataBase = "Data Source=" + path;
            _iDatabaseConnection = new SqliteConnection(pathToDataBase);
            _iDatabaseConnection.Open();
            _connectedToDatabase = true;
            Debug.Log("Connected to database");
        }
    }

    public void DisconnectFromDatabase()
    {
        if (_connectedToDatabase)
        {
            _iDatabaseConnection.Close();
            _iDatabaseConnection = null;
            _connectedToDatabase = false;
            Debug.Log("Disconnected from database");
        }
    }

    public List<string> ExecuteCommand(string query)
    {
        CreateNewCommand();
        List<string> dataFromDatabase = new List<string>();
        _iDatabaseCommand.CommandText = query;
        using (IDataReader reader = _iDatabaseCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dataFromDatabase.Add(reader[i].ToString());
                }
            }
            reader.Close();
        }
        CompleteTheCommand();
        return dataFromDatabase;
    }

    private void CreateNewCommand()
    {
        ConnectToDatabase();
        _iDatabaseCommand = _iDatabaseConnection.CreateCommand();
    }
    private void CompleteTheCommand()
    {
        _iDatabaseCommand.Dispose();
        _iDatabaseCommand = null;
        DisconnectFromDatabase();
    }
}
