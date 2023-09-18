using UnityEngine;

public class SqliteService : MonoBehaviour
{
    public void CreateTable()
    {
        string sqlQuery = "CREATE TABLE IF NOT EXISTS USERS ('id' INTEGER NOT NULL, 'name'  TEXT NOT NULL, 'sourname'  TEXT NOT NULL, 'email' TEXT NOT NULL, 'age'   INTEGER NOT NULL, 'gender'    TEXT NOT NULL, 'password'  TEXT NOT NULL, 'level' INTEGER, PRIMARY KEY('id' AUTOINCREMENT));";
        DatabaseManager.Instance.ExecuteCommand(sqlQuery);
    }

    public void InsertInto(string table, string[] fields, string[] values)
    {
        if (fields.Length != values.Length)
        {
            Debug.Log("the size of the fields must be equal to the size of the values");
            return;
        }
        string sqlQuery = "INSERT INTO " + table.ToUpper() + " (" + FormalizeFields(fields) + ") " + "VALUES " + "( " + FormalizeValues(values) + " );";
        DatabaseManager.Instance.ExecuteCommand(sqlQuery);
    }
    public void InsertInto(string table, string[] values)
    {
        string sqlQuery = "INSERT INTO " + table.ToUpper() + " VALUES " + "( " + FormalizeValues(values) + " );";
        Debug.Log(sqlQuery);
        DatabaseManager.Instance.ExecuteCommand(sqlQuery);
    }
    public string Select(string[] columns, string table, string condition)
    {
        return string.Empty;
    }


    public string Select(string[] columns, string table)
    {
        return string.Empty;
    }

    public string Select(string table)
    {
        string sqlQuery = $"SELECT * FROM {table.ToUpper()} ";
        return string.Empty;
    }

    private string FormalizeFields(string[] fields)
    {
        string temp = "";
        foreach (var item in fields)
        {
            temp = temp + item + ", ";
        }
        temp = temp.Remove(temp.Length - 2, 1);
        return temp;
    }

    private string FormalizeValues(string[] values)
    {
        string temp = "";
        foreach (var item in values)
        {
            temp = temp + "'" + item + "'" + ", ";
        }
        temp = temp.Remove(temp.Length - 2, 1);
        return temp;
    }
}
