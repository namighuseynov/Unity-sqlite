using System.Collections.Generic;

public interface IDatabaseManager
{
    public void ConnectToDatabase();

    public void DisconnectFromDatabase();

    public List<string> ExecuteCommand(string query);
}
