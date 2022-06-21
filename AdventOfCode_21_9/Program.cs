using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode_21_9 {
  public static class Program {
    private static void Main() {
      var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? @"C:\input.txt", @"..\..\..\assets\input.txt");
      var files = new List<string>(File.ReadAllLines(path));

      files.RemoveAll(r => r == "");

      if (files.Count <= 0) {
        Console.WriteLine("Input is empty.");
        return;
      }

      // [x,y]
      var cells = ParseCells(files);
      var lowPoints = GetLowPoints(cells);
      var risk = lowPoints.Sum(cell => cell.Value + 1);

      Console.WriteLine("risk = " + risk);
    }

    private static List<Cell> GetLowPoints(Cell[,] cells) {
      var lowPoints = new List<Cell>();

      for (var x = 0; x < cells.GetLength(0); x++) {
        for (var y = 0; y < cells.GetLength(1); y++) {
          var cell = cells[x, y];

          var top = y - 1 >= 0 ? cells[x, y - 1] : null;
          var right = x + 1 < cells.GetLength(0) ? cells[x + 1, y] : null;
          var bottom = x - 1 >= 0 ? cells[x - 1, y] : null;
          var left = y + 1 < cells.GetLength(1) ? cells[x, y + 1] : null;

          var lowPoint = top == null || top.Value > cell.Value;
          lowPoint &= right == null || right.Value > cell.Value;
          lowPoint &= left == null || left.Value > cell.Value;
          lowPoint &= bottom == null || bottom.Value > cell.Value;

          cell.LowPoint = lowPoint;
          if (lowPoint)
            lowPoints.Add(cell);
        }
      }

      return lowPoints;
    }

    private static Cell[,] ParseCells(List<string> files) {
      // [x,y]
      var cells = new Cell[files[0].Length, files.Count];

      for (var y = 0; y < files.Count; y++) {
        var row = files[y];
        for (var x = 0; x < row.ToCharArray().Length; x++) {
          var c = row.ToCharArray()[x];

          if (!int.TryParse(c.ToString(), out var value))
            value = 9;

          cells[x, y] = new Cell(value, x, y);
        }
      }

      return cells;
    }
  }
}
