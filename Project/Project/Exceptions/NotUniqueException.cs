namespace Project.Exceptions;

public class NotUniqueException(string field) 
    : Exception($"The {field} must be unique. There is already someone with such {field}");

