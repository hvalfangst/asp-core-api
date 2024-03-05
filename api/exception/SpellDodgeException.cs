namespace Hvalfangst.api.exception;

public class SpellDodgeException : Exception
{
    public SpellDodgeException() { }

    public SpellDodgeException(string message) : base(message) { }

    public SpellDodgeException(string message, Exception innerException) : base(message, innerException) { }
}