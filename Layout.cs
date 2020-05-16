using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuGenerator
{
    public class Layout
    {
        private Random r = new Random();
        private static byte _x;
        private static byte _y;
        private static byte _blockIndex;
        private static byte _temp;
        private static int _tempIndex;
        private static int _colorFactor;
        private byte blockWidth;
        private byte tableLength;
        public byte BlockWidth
        {
            get
            {
                return blockWidth;
            }
            set
            {
                if (TableLength == 0)
                {
                    throw new MissingMemberException("Blok genişliği istenmeden önce tablo genişliği ayarlanmalıdır");
                }
                else if (TableLength % value == 0)
                {
                    blockWidth = value;
                    BlockHeight = (byte)(TableLength / value);
                }
                else
                {
                    throw new InvalidOperationException("Blok genişliği tablo genişliğinin tam böleni olmak zorundadır");
                }
            }
        }
        public byte BlockHeight { get; private set; }
        public byte TableLength
        {
            get
            {
                return tableLength;
            }
            set
            {
                bool isValid = false;
                for (int i = 2; i < value / 2; i++)
                {
                    if ((double)value / i % 1 == 0)
                    {
                        isValid = true;
                        break;
                    }
                }
                if (isValid)
                {
                    tableLength = value;
                }
                else if (value == 4)
                {
                    tableLength = value;
                }
                else
                {
                    throw new InvalidOperationException("Tablonun kenar uzunluğunun değeri asal sayı olamaz");
                }
            }
        }
        public Stack<Cell> Cells { get; set; }
        public Layout(byte tableLength, byte blockWidth)
        {
            TableLength = tableLength;
            BlockWidth = blockWidth;
            Cells = new Stack<Cell>((int)Math.Pow(TableLength, 2));

            int totalCount = (int)Math.Pow(TableLength, 2);
            byte blockHorizontalCount = (byte)(TableLength / BlockWidth);
            _colorFactor = blockHorizontalCount + 1;

            while (Cells.Count < totalCount)
            {
                if (_x % BlockWidth == 0 && _x != 0)
                {
                    _blockIndex++;
                }
                if (_x == TableLength)
                {
                    _x = 0;
                    _y++;
                    _blockIndex -= blockHorizontalCount;
                    if (_y % BlockHeight == 0)
                    {
                        _blockIndex += blockHorizontalCount;
                    }
                }

                Cells.Push(new Cell(_x, _y, _blockIndex, TableLength));
                AssignWorth();
                _x++;
            }
        }

        private void AssignWorth()
        {
            _tempIndex = r.Next(0, Cells.Peek().PossibleValues.Count);
            _temp = Cells.Peek().PossibleValues[_tempIndex];
            if (IsCellUnique())
            {
                Cells.Peek().Worth = _temp;
                //WriteCellColored();
            }
            else
            {
                PopImpossibleCells();
                AssignWorth();
            }
        }
        private void PopImpossibleCells()
        {
            if (Cells.Peek().PossibleValues.Count == 0)
                PopLastCell();
            if (Cells.Peek().PossibleValues.Count == 0)
                PopImpossibleCells();
        }
        public bool IsCellUnique()
        {
            bool isUnique = Compare();
            return isUnique;
        }
        public bool Compare()
        {
            foreach (var cell in Cells)
            {
                if ((cell.Y == Cells.Peek().Y && cell.X < Cells.Peek().X))//aynı satırdaysa
                {
                    if (cell.Worth == _temp)//deneme başarısızsa
                    {
                        Cells.Peek().PossibleValues.RemoveAt(_tempIndex);//denemeyi ele
                        if (Cells.Peek().PossibleValues.Count == 0)
                            PopLastCell();
                        return false;
                    }
                }
                if (cell.X == Cells.Peek().X && cell.Y < Cells.Peek().Y)//aynı sütundaysa
                {
                    if (cell.Worth == _temp)//deneme başarısızsa
                    {
                        Cells.Peek().PossibleValues.RemoveAt(_tempIndex);//denemeyi ele
                        if (Cells.Peek().PossibleValues.Count == 0)
                            PopLastCell();
                        return false;
                    }
                }
                if (cell.BlockIndex == Cells.Peek().BlockIndex)//aynı bloktaysa
                {
                    if (cell.Worth == _temp)//deneme başarısızsa
                    {
                        Cells.Peek().PossibleValues.RemoveAt(_tempIndex);//denemeyi ele
                        if (Cells.Peek().PossibleValues.Count == 0)
                            PopSameBlock();
                        return false;
                    }
                }
            }
            return true;
        }
        private void PopLastCell()
        {
            //WriteCellToBePopped();
            Cells.Pop();
            _x = Cells.Peek().X;
            _y = Cells.Peek().Y;
            _blockIndex = Cells.Peek().BlockIndex;
        }
        private void PopSameBlock()
        {
            //WriteCellToBePopped();
            Cells.Pop();
            if (Cells.Peek().BlockIndex == _blockIndex)
            {
                PopSameBlock();
            }
            else
            {
                _x = Cells.Peek().X;
                _y = Cells.Peek().Y;
                _blockIndex = Cells.Peek().BlockIndex;
            }
        }
        public void WriteCell()
        {
            Console.CursorLeft = (Cells.Peek().X + 1) * 4;
            Console.CursorTop = (Cells.Peek().Y + 1) * 2;
            Console.Write(" {0,2} ", Cells.Peek().Worth);
        }
        public void WriteCellToBePopped()
        {
            Console.CursorLeft = (Cells.Peek().X + 1) * 4;
            Console.CursorTop = (Cells.Peek().Y + 1) * 2;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("  X  ", Cells.Peek().Worth);
            Console.CursorTop++;
            Console.CursorLeft -= 5;
            Console.Write("     ");
        }
        public void WriteCellColored()
        {
            Console.BackgroundColor = (ConsoleColor)(Cells.Peek().BlockIndex % _colorFactor + 1);
            Console.CursorLeft = (Cells.Peek().X + 1) * 4;
            Console.CursorTop = (Cells.Peek().Y + 1) * 2;
            Console.Write(" {0,2}  ", Cells.Peek().Worth);
            Console.CursorTop++;
            Console.CursorLeft -= 5;
            Console.Write("     ");
        }
    }
}
