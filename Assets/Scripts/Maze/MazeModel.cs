using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

    public class MazeModel
    {
        public Coordinates startingPoint { get; }
        public Coordinates exitPoint { get; }
        public List<List<int>> matrix { get; }
        public List<Coordinates> matrixCoordinates { get; }

        public List<Coordinates> wallLocations { get; }
        public List<Coordinates> pathLocations { get; }
        public List<Coordinates> itemLocations { get; }


        public MazeModel(Coordinates startingPoint, Coordinates exitPoint, List<List<int>> matrix)
        {
            this.startingPoint = startingPoint;
            this.exitPoint = exitPoint;
            this.matrix = matrix;

            wallLocations = new List<Coordinates>();
            pathLocations  = new List<Coordinates>();
            itemLocations = new List<Coordinates>();
            matrixCoordinates = new List<Coordinates>();

            for (var i = 0; i < matrix.Count; i++)
            {
                for (var j = 0; j < matrix[i].Count; j++)
                {
                    var cords = new Coordinates(i, j);
                    matrixCoordinates.Add(cords);
                    if (matrix[i][j] == MazeTiles.PATH) pathLocations.Add(cords);
                    if (matrix[i][j] == MazeTiles.ITEM) itemLocations.Add(cords);
                    if (matrix[i][j] == MazeTiles.WALLS) wallLocations.Add(cords);

                }
            }
        }

        public int getCell(int x, int y)
        {
            return getCell(new Coordinates(x, y));
        }

        public int getCell(Coordinates c)
        {
            return matrix[c.x][c.y];
        }

        public bool isType(Coordinates c, int type)
        {
            return getCell(c) == type;
        }

        private void print()
        {            
            string temp = "";
            for (var i = 0; i < matrix.Count; i++)
            {
                for (var j = 0; j < matrix[i].Count; j++)
                {
                    temp += matrix[i][j] + " ";
                }
                Console.WriteLine(temp);                
                temp = "";
            }
            Console.WriteLine("");         
        }
        
    }

