namespace Aoc2017_Day18;

internal class Registers
{
    private readonly Dictionary<char, long> _registerValues = new();

    public long GetValue(char register)
    {
        return _registerValues.GetValueOrDefault(register);
    }

    public long GetValue(RegisterNameOrValue input)
    {
        return input.IsRegisterName
                   ? GetValue(input.AsRegisterName)
                   : input.AsValue;
    }

    public void SetValue(char register, long value)
    {
        _registerValues[register] = value;
    }
}
