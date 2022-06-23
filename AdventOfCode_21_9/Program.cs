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

      // part I.
      var lowPoints = GetLowPoints(cells);
      var risk = lowPoints.Sum(cell => cell.Value + 1);

      Console.WriteLine("risk = " + risk);

      // part II.

      var basins = lowPoints.Select(lp => new Basin(lp)).ToList();

      foreach (var b in basins)
        b.BuildBasin(cells);

      basins = basins.OrderByDescending(b => b.Count()).ToList();


      var product = 1;
      for (var i = 0; i < Math.Min(basins.Count, 3); i++) {
        Console.WriteLine("Top Basin weight - " + i + " = " + basins[i].Count());
        product *= basins[i].Count();
      }

      Console.WriteLine("Basins product = " + product);
    }

    public static CellNeighbours GetNeighbours(Cell cell, Cell[,] map) {
      var y = cell.Y;
      var x = cell.X;

      var top = y - 1 >= 0 ? map[x, y - 1] : null;
      var right = x + 1 < map.GetLength(0) ? map[x + 1, y] : null;
      var bottom = x - 1 >= 0 ? map[x - 1, y] : null;
      var left = y + 1 < map.GetLength(1) ? map[x, y + 1] : null;

      return new CellNeighbours(top, right, bottom, left);
    }

    private static List<Cell> GetLowPoints(Cell[,] cells) {
      var lowPoints = new List<Cell>();

      for (var x = 0; x < cells.GetLength(0); x++) {
        for (var y = 0; y < cells.GetLength(1); y++) {
          var cell = cells[x, y];

          var n = GetNeighbours(cell, cells);

          var lowPoint = n.Top == null || n.Top.Value > cell.Value;
          lowPoint &= n.Right == null || n.Right.Value > cell.Value;
          lowPoint &= n.Left == null || n.Left.Value > cell.Value;
          lowPoint &= n.Bottom == null || n.Bottom.Value > cell.Value;

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
