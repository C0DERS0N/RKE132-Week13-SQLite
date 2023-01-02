using System.Data.SQLite;

ReadDatabase(CreateConnection());
//InsertCustomer(CreateConnection());
//RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection Connection = new SQLiteConnection("Data Source = mydb.db; Version = 3; New = True; Compress = True;");

    try
    {
        Connection.Open();
        //Console.WriteLine("Database found.");
    }
    catch
    {
        Console.WriteLine("Database not found.");
    }
    return Connection;
}

static void ReadDatabase(SQLiteConnection CreateConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = CreateConnection.CreateCommand();
    command.CommandText = ("SELECT rowid, * FROM Customer");

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string ReadStringRowId = reader["rowid"].ToString();
        string ReaderStringFirstName = reader.GetString(1);
        string ReaderStringLastName = reader.GetString(2);
        string ReaderStringDoB = reader.GetString(3);

        Console.WriteLine($"{ReadStringRowId}. Full name: {ReaderStringFirstName} {ReaderStringLastName}, born in: {ReaderStringDoB}.");
    }
    CreateConnection.Close();
}

static void InsertCustomer(SQLiteConnection CreateConnection)
{
    SQLiteCommand command;

    string FName, LName, DoB;

    Console.WriteLine("To add a customer enter the details below.");

    Console.WriteLine("Enter a first name:");
    FName = Console.ReadLine();

    Console.WriteLine("Enter a last name:");
    LName = Console.ReadLine();

    Console.WriteLine("Enter the date of birth (yyyy-mm-dd):");
    DoB = Console.ReadLine();

    command = CreateConnection.CreateCommand();
    command.CommandText = ($"INSERT INTO customer (firstname, lastname, dateofbirth)\r\n" +
        $"VALUES (\"{FName}\", \"{LName}\", \"{DoB}\");");

    int RowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"{RowInserted} row(s) have been inserted.");

    ReadDatabase(CreateConnection);
}

static void RemoveCustomer(SQLiteConnection CreateConnection)
{
    SQLiteCommand command;

    string DeleteCustomer;
    Console.WriteLine("Enter an Id to delete a customer:");

    DeleteCustomer = Console.ReadLine();

    command = CreateConnection.CreateCommand();
    command.CommandText = ($"DELETE FROM customer WHERE rowid = {DeleteCustomer}");

    int RowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{RowRemoved} was deleted from the table Customer.");

    ReadDatabase(CreateConnection);
}