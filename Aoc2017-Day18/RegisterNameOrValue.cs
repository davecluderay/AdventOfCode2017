namespace Aoc2017_Day18;

internal sealed class RegisterNameOrValue
{
    private readonly char _register;
    private readonly int _value;

    private RegisterNameOrValue(char register) => _register = register;
    private RegisterNameOrValue(int value) => _value = value;

    public bool IsRegisterName => _register != '\0';
    public char AsRegisterName
    {
        get
        {
            if (!IsRegisterName) throw new InvalidOperationException("Attempted to treat value as a register name.");
            return _register;
        }
    }

    public int AsValue
    {
        get
        {
            if (IsRegisterName) throw new InvalidOperationException("Attempted to treat register name as a value.");
            return _value;
        }
    }

    public override string ToString()
        => IsRegisterName
               ? $"Register: {AsRegisterName}"
               : $"Value: {AsValue}";

    public static RegisterNameOrValue Parse(string text)
        => text.Length == 1 && char.IsLetter(text[0])
               ? new RegisterNameOrValue(text[0])
               : new RegisterNameOrValue(int.Parse(text));
}
