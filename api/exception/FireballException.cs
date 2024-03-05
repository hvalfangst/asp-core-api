namespace Hvalfangst.api.exception;

public class FireballException : Exception
{
    public FireballException() { }

    public FireballException(string message) : base(message) { }

    public FireballException(string message, Exception innerException) : base(message, innerException) { }
}