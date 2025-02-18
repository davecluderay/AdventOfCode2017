using System.Diagnostics;

namespace Aoc2017_Day23;

internal class ProgramInstance
{
    private readonly Instruction[] _program;
    private readonly Registers _registers;
    private int _instructionPointer;

    public int MulCount { get; private set; }

    public ProgramInstance(int id, Instruction[] program)
    {
        _program = program;
        _registers = new();
        _registers.SetValue('p', id);
    }

    public void RunUntilComplete()
    {
        while (_instructionPointer >= 0 && _instructionPointer < _program.Length)
        {
            var instruction = _program[_instructionPointer++];
            switch (instruction)
            {
                case Instruction.Set set:
                    _registers.SetValue(set.X, _registers.GetValue(set.Y));
                    break;
                case Instruction.Sub sub:
                    _registers.SetValue(sub.X, _registers.GetValue(sub.X) - _registers.GetValue(sub.Y));
                    break;
                case Instruction.Mul mul:
                    _registers.SetValue(mul.X, _registers.GetValue(mul.X) * _registers.GetValue(mul.Y));
                    MulCount++;
                    break;
                case Instruction.Jnz jnz:
                    if (_registers.GetValue(jnz.X) != 0)
                        _instructionPointer += (int)(_registers.GetValue(jnz.Y) - 1);
                    break;
                default:
                    throw new UnreachableException($"Unknown instruction: {instruction}");
            }
        }
    }
}
