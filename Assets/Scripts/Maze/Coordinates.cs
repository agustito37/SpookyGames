using System;
using System.Collections.Generic;
using System.Text;

    public class Coordinates
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj) => this.Equals(obj as Coordinates);

        public bool Equals(Coordinates b)
        {
            return this.x == b.x && this.y == b.y;
        }
 
        public override string ToString()
        {
            return "(" + this.x + "),(" + this.y + ")";
        }

        public bool isNextTo(Coordinates b)
        {
            return
                (this.x == b.x + 1 && this.y == b.y) ||
                (this.x == b.x - 1 && this.y == b.y) ||
                (this.x == b.x && this.y == b.y + 1) ||
                (this.x == b.x && this.y == b.y - 1);
        }

        public Coordinates move(int where)
        {
            if (where == Movements.LEFT) return new Coordinates(this.x, this.y - 1);
            if (where == Movements.RIGHT) return new Coordinates(this.x, this.y + 1);
            if (where == Movements.UP) return new Coordinates(this.x - 1, this.y );
            return new Coordinates(x + 1, this.y);

        }

        public int getMazeValue(List<List<int>> maze)
        {
            try
            {
                if (maze[this.x][this.y] == 0) return 1;
            } catch
            {
                return 0;
            }
            return 0;
            
        }

        public int bound0s(List<List<int>> maze)
        {
            return move(Movements.LEFT).getMazeValue(maze) + move(Movements.UP).getMazeValue(maze) + move(Movements.RIGHT).getMazeValue(maze) + move(Movements.DOWN).getMazeValue(maze);
        }
    }

