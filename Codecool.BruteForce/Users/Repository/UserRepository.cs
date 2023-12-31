﻿using Codecool.BruteForce.Users.Model;
using Microsoft.Data.Sqlite;

namespace Codecool.BruteForce.Users.Repository;

public class UserRepository : IUserRepository
{
    private readonly string _dbFilePath;

    public UserRepository(string dbFilePath)
    {
        _dbFilePath = dbFilePath;
    }

    private SqliteConnection GetPhysicalDbConnection()
    {
        var dbConnection = new SqliteConnection($"Data Source ={_dbFilePath};Mode=ReadWrite");
        dbConnection.Open();
        return dbConnection;
    }

    private void ExecuteNonQuery(string query)
    {
        using var connection = GetPhysicalDbConnection();
        using var command = GetCommand(query, connection);
        command.ExecuteNonQuery();
    }

    private static SqliteCommand GetCommand(string query, SqliteConnection connection)
    {
        return new SqliteCommand
        {
            CommandText = query,
            Connection = connection,
        };
    }

    public void Add(string userName, string password)
    {
        string addStatement = $"INSERT INTO users(user_name, password) VALUES ('{userName}', '{password}')";
        ExecuteNonQuery(addStatement);
    }

    public void Update(int id, string userName, string password)
    {
        string updateStatement = $"UPDATE users SET user_name={userName},password={password} WHERE id={id}";
        ExecuteNonQuery(updateStatement);
    }

    public void Delete(int id)
    {
        string deleteStatement = $"DELETE FROM users WHERE id={id}";
        ExecuteNonQuery(deleteStatement);
    }

    public void DeleteAll()
    {
        string deleteAllStatement = "DELETE FROM users";
        ExecuteNonQuery(deleteAllStatement);
    }

    public User Get(int id)
    {
        var query = @$"SELECT * FROM users WHERE id = {id}";
        using var connection = GetPhysicalDbConnection();
        using var command = GetCommand(query, connection);

        using var reader = command.ExecuteReader();
        return new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
    }

    public IEnumerable<User> GetAll()
    {
        List<User> users = new();
        string getAllStatement = "SELECT * FROM users";
        using var connection = GetPhysicalDbConnection();
        using var command = GetCommand(getAllStatement, connection);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            User user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
            users.Add(user);
        }

        return users;
    }
}
