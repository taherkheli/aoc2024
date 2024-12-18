using System;

namespace aoc.D17
{
  public class Computer
  {
    private long _registerA;
    private long _registerB;
    private long _registerC;
    private int _iPointer;
    private readonly Instruction[]? _program;
    private string _output = "";
    private long[] _initialRegisters;
    private string _initialProgram = "";

    public Computer(string path)
    {
      _iPointer = 0;
      bool blankFound = false;
      var lines = File.ReadAllLines(path);

      for (int i = 0; i < lines.Length; i++)
      {
        if (!blankFound)
        {
          if (String.IsNullOrWhiteSpace(lines[i]))
          {
            blankFound = true;
            continue;
          }
          else
          {
            var temp = int.Parse(lines[i].Split(':', StringSplitOptions.TrimEntries)[1]);

            switch (i)
            {
              case 0:
                _registerA = temp;
                break;
              case 1:
                _registerB = temp;
                break;
              case 2:
                _registerC = temp;
                break;
            }
          }
        }
        else
        {
          _initialProgram = lines[i].Split(' ', StringSplitOptions.TrimEntries)[1];
          var nums_s = _initialProgram.Split(',');
          _program = new Instruction[nums_s.Length / 2];

          for (int j = 0; j < nums_s.Length; j = j + 2)
            _program[j / 2] = new Instruction((OpCodes)int.Parse(nums_s[j]), int.Parse(nums_s[j + 1]));
        }
      }

      _initialRegisters = new long[3] { _registerA, _registerB, _registerC };
    }

    public string PartI()
    {
      Execute();
      return _output;
    }

    public long PartII()
    {
      int index = _initialProgram.Length - 1;
      long i = 0;
      index = _initialProgram.LastIndexOf(',', index);
      var target = _initialProgram.Substring(index + 1).Trim();
      index--;

      for (i = 0; i < long.MaxValue; i++)
      {
        _output = "";
        _registerA = i;
        _registerB = _initialRegisters[1];
        _registerC = _initialRegisters[2];
        _iPointer = 0;

        Execute();

        if (_output == target)
        {
          if (target == _initialProgram)
            return i;
          else
          {
            //from LSB to MSB, each digit occurs after at least 8xA => 3 bits shifted left in Octal representation
            i = (i * 8) - 1;
            index = _initialProgram.LastIndexOf(',', index);
            target = _initialProgram.Substring(index + 1).Trim();
            index--;
          }
        }
      }

      return -1;
    }

    private void Execute()
    {
      while (true)
      {
        if (_iPointer == _program?.Length)
        {
          _output = _output.TrimEnd(new char[] { ' ', ',' });
          return;
        }

        var instruction = _program?[_iPointer] ?? new Instruction(OpCodes.OUT, 0);

        switch (instruction.Opcode)
        {
          case OpCodes.ADV:
            _registerA = (long)(_registerA / Math.Pow(2, GetComboOperandValue(instruction.Operand)));
            _iPointer++;
            break;

          case OpCodes.BXL:
            _registerB = _registerB ^ instruction.Operand;
            _iPointer++;
            break;

          case OpCodes.BST:
            _registerB = GetComboOperandValue(instruction.Operand) % 8;
            _iPointer++;
            break;

          case OpCodes.JNZ:
            if (_registerA != 0)
              _iPointer = instruction.Operand;
            else
              _iPointer++;
            break;

          case OpCodes.BXC:
            _registerB = _registerB ^ _registerC;
            _iPointer++;
            break;

          case OpCodes.OUT:
            _output = _output + ((GetComboOperandValue(instruction.Operand) % 8).ToString()) + ",";
            _iPointer++;
            break;

          case OpCodes.BDV:
            _registerB = (long)(_registerA / Math.Pow(2, GetComboOperandValue(instruction.Operand)));
            _iPointer++;
            break;

          case OpCodes.CDV:
            _registerC = (long)(_registerA / Math.Pow(2, GetComboOperandValue(instruction.Operand)));
            _iPointer++;
            break;

          default:
            throw new Exception("Should not happen");
        }
      }
    }

    private long GetComboOperandValue(long i)
    {
      switch (i)
      {
        case 0:
        case 1:
        case 2:
        case 3:
          return i;
        case 4:
          return _registerA;
        case 5:
          return _registerB;
        case 6:
          return _registerC;
        case 7:
        default:
          throw new Exception("Should not happen");
      }
    }
  }
}
