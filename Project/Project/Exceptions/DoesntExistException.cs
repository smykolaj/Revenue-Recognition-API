namespace Project.Exceptions;

public class DoesntExistException(string table, string field) 
    : Exception($"The {table} with such {field} does not exist");
