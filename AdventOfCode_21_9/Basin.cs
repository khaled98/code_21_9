using System.Collections.Generic;

namespace AdventOfCode_21_9 {
  public class Basin {
    public List<Cell> Cells { get; set; }
    public Cell LowPoint { get; set; }

    public Basin(Cell lowPoint) {
      Cells = new List<Cell>(new[] {lowPoint});
      LowPoint = lowPoint;
    }

    // map: [x,y]
    public void BuildBasin(Cell[,] map) { //Flood fill, non-recursive
      var leaves = new Queue<Cell>(new[] {LowPoint});

      while (leaves.Count > 0) {
        var workingNode = leaves.Dequeue();

        var n = Program.GetNeighbours(workingNode, map);

        var allNeighbours = n.All;
        allNeighbours.RemoveAll(neighbourCell => Cells.Contains(neighbourCell));

        foreach (var cell in allNeighbours) {
          if (AddCell(cell))
            leaves.Enqueue(cell);
        }
      }
    }

    public int Count() {
      return Cells.Count;
    }

    public bool AddCell(Cell cell) {
      if (cell == null || cell.Value >= 9 || cell.IsSame(LowPoint))
        return false;

      var possibleNeighbours = Cells.FindAll(c => c.IsNeighbour(cell) && cell.Value > c.Value);

      if (possibleNeighbours.Count <= 0)
        return false;

      Cells.Add(cell);
      return true;
    }
  }
}
