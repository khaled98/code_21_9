using System.Collections.Generic;

namespace AdventOfCode_21_9 {
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
  }
}
