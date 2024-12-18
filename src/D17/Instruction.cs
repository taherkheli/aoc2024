namespace aoc.D17
{
  public struct Instruction
  {
    public OpCodes Opcode;
    public int Operand;

    public Instruction(OpCodes opcode, int operand)
    {
      this.Opcode = opcode;
      this.Operand = operand;      
    }
  }
}