using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SudokuGenerator
{
    [DebuggerDisplay("{Worth}")]
    public class Cell
    {
        public byte X;
        public byte Y;
        public byte Worth;
        public byte BlockIndex;
        public List<byte> PossibleValues = new List<byte>();

        public Cell(byte x, byte y, byte blockIndex, byte tableLength)
        {
            X = x;
            Y = y;
            BlockIndex = blockIndex;
            for (byte i = 1; i <= tableLength; i++)
            {
                PossibleValues.Add(i);
            }
        }
    }
}
