using System;
using System.Collections.Generic;

namespace AdventOfCode_21_9 {
  public class CellNeighbours {
    public Cell Top { get; set; }
    public Cell Right { get; set; }
    public Cell Bottom { get; set; }
    public Cell Left { get; set; }

    public List<Cell> All {
      get { return new(new[] {Top, Left, Bottom, Right}); }
    }

    public CellNeighbours(Cell top, Cell right, Cell bottom, Cell left) {
      Top = top;
      Right = right;
      Bottom = bottom;
      Left = left;
    }
  }

  public class Cell {
    public int Value { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public bool LowPoint { get; set; }

    public Cell(int value, int x, int y) {
      Value = value;
      X = x;
      Y = y;
      LowPoint = false;
    }

    public bool IsSame(Cell other) {
      return other.X == X && other.Y == Y && other.Value == Value;
    }

    public bool IsNeighbour(Cell o) {
      return Math.Abs(o.X - X) == 1 || Math.Abs(o.Y - Y) == 1;
    }
  }
}
