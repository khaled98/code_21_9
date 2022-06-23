using System;
using System.Collections.Generic;

namespace AdventOfCode_21_9 {
  public class CellNeighbours {
    public Cell Top { get; }
    public Cell Right { get; }
    public Cell Bottom { get; }
    public Cell Left { get; }

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
    public int Value { get; }
    public int X { get; }
    public int Y { get; }

    public Cell(int value, int x, int y) {
      Value = value;
      X = x;
      Y = y;
    }

    public bool IsSame(Cell other) {
      return other.X == X && other.Y == Y && other.Value == Value;
    }

    public bool IsNeighbour(Cell o) {
      return Math.Abs(o.X - X) == 1 || Math.Abs(o.Y - Y) == 1;
    }
  }
}
