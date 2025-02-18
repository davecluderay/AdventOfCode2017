namespace Aoc2017_Day23;

internal abstract record Instruction
{
    public static Instruction Parse(string text)
    {
        var components = text.Split(' ');
        return components[0] switch
               {
                   "set" => new Set(components[1].Single(), RegisterNameOrValue.Parse(components[2])),
                   "sub" => new Sub(components[1].Single(), RegisterNameOrValue.Parse(components[2])),
                   "mul" => new Mul(components[1].Single(), RegisterNameOrValue.Parse(components[2])),
                   "jnz" => new Jnz(RegisterNameOrValue.Parse(components[1]), RegisterNameOrValue.Parse(components[2])),
                   _     => throw new FormatException($"Unknown instruction: {text}")
               };
    }

    public sealed record Set(char X, RegisterNameOrValue Y) : Instruction;
    public sealed record Sub(char X, RegisterNameOrValue Y) : Instruction;
    public sealed record Mul(char X, RegisterNameOrValue Y) : Instruction;
    public sealed record Jnz(RegisterNameOrValue X, RegisterNameOrValue Y) : Instruction;
}
