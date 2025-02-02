using System.Diagnostics;

namespace Aoc2017_Day18;

internal class ProgramInstance
{
    private readonly Instruction[] _program;
    private readonly Registers _registers;
    private int _instructionPointer;

    public int SendCount { get; private set; }
    public Queue<long> OutputQueue { get; } = new();

    public ProgramInstance(int id, Instruction[] program)
    {
        _program = program;
        _registers = new();
        _registers.SetValue('p', id);
    }

    public void RunUntilBlockedOrComplete(Queue<long> inputQueue)
    {
        while (_instructionPointer >= 0 && _instructionPointer < _program.Length)
        {
            var instruction = _program[_instructionPointer++];
            switch (instruction)
            {
                case Instruction.Snd snd:
                    OutputQueue.Enqueue(_registers.GetValue(snd.X));
                    SendCount++;
                    break;
                case Instruction.Set set:
                    _registers.SetValue(set.X, _registers.GetValue(set.Y));
                    break;
                case Instruction.Add add:
                    _registers.SetValue(add.X, _registers.GetValue(add.X) + _registers.GetValue(add.Y));
                    break;
                case Instruction.Mul mul:
                    _registers.SetValue(mul.X, _registers.GetValue(mul.X) * _registers.GetValue(mul.Y));
                    break;
                case Instruction.Mod mod:
                    _registers.SetValue(mod.X, _registers.GetValue(mod.X) % _registers.GetValue(mod.Y));
                    break;
                case Instruction.Rcv rcv:
                    if (inputQueue.Count == 0)
                    {
                        _instructionPointer--;
                        return;
                    }
                    _registers.SetValue(rcv.X, inputQueue.Dequeue());
                    break;
                case Instruction.Jgz jgz:
                    if (_registers.GetValue(jgz.X) > 0)
                        _instructionPointer += (int)(_registers.GetValue(jgz.Y) - 1);
                    break;
                default:
                    throw new UnreachableException($"Unknown instruction: {instruction}");
            }
        }
    }
}
