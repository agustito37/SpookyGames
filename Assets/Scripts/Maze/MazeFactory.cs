using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


public class MazeFactory
{
    private static MazeFactory instance = null;

    private MazeFactory() { }
    private Random rng = new Random();

    private int TRY_THRESHOLD = 10;
    private List<Coordinates> bannedPositions = new List<Coordinates>();


    public static MazeFactory Instance
    {
        get
        {
            if (instance == null) instance = new MazeFactory();
            return instance;
        }

    }

    private int generateNumber(int min = 0, int max = 10)
    {
        return rng.Next(min, max + 1);
    }

    public MazeModel generateMaze(int width = 15, int height = 15, int difficulty = 3, int maxItems = 3)
    {
        List<List<int>> maze = new List<List<int>>();
        List<int> row = new List<int>();

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (i == 0 || j == 0 || i == height -1 || j == width - 1)
                {    
                    bannedPositions.Add(new Coordinates(i, j));
                }
                row.Add(MazeTiles.WALLS);
            }
            maze.Add(row);
            row = new List<int>();
        }
        bannedPositions.Remove(new Coordinates(1, 0));
        bannedPositions.Remove(new Coordinates(0, 1));

        bannedPositions.Remove(new Coordinates(0, width-2));
        bannedPositions.Remove(new Coordinates(1, width - 1));

        bannedPositions.Remove(new Coordinates(height-2, 0));
        bannedPositions.Remove(new Coordinates(height - 1, 1));

        bannedPositions.Remove(new Coordinates(height - 2, width - 1));
        bannedPositions.Remove(new Coordinates(height - 1, width - 2));

        // Whether starting point is First column or firstRow
        int RoC = generateNumber(0, 1);
        Coordinates startingPoint = null;
        Coordinates endingPoint = null;

        if (RoC == 0)
        {
            var startRng = generateNumber(1, width - 1);
            var endRng = generateNumber(1, width - 1);
            maze[0][startRng] = MazeTiles.START;
            maze[height - 1][endRng] = MazeTiles.END;
            startingPoint = new Coordinates(0, startRng);
            endingPoint = new Coordinates(height - 1, endRng);
        }
        else
        {
            var startRng = generateNumber(0, height - 1);
            var endRng = generateNumber(0, height - 1);
              
            maze[startRng][0] = MazeTiles.START;
            maze[endRng][width-1] = MazeTiles.END;

            startingPoint = new Coordinates(startRng, 0);
            endingPoint = new Coordinates(endRng, width - 1);

        }
        bannedPositions.Remove(startingPoint);
        generatePaths(maze, startingPoint, endingPoint, startingPoint, new List<Coordinates>());                
        generateItems(maze, maxItems);
        printMaze(maze);

        MazeModel f = new MazeModel(startingPoint, endingPoint, maze);
        return f;
    }

    private bool isValid(Coordinates c, List<List<int>> maze)
    {
        if (bannedPositions.Contains(c)) return false;
        try
        {
            var x = maze[c.x][c.y];
            return true;
        } catch
        {
            return false;
        }
    }

    private List<List<int>> generateItems(List<List<int>> maze, int maxItems)
    {
        List<Coordinates> candidates = new List<Coordinates>();
        for (var i = 0; i < maze.Count; i++)
        {
            for (var j = 0; j < maze[i].Count; j++)
            {
                if (maze[i][j] == MazeTiles.PATH)
                {
                    candidates.Add(new Coordinates(i, j));
                }
            }
        }

        candidates = candidates.Where(c => c.bound0s(maze) == 1 || c.bound0s(maze) == 2).ToList();
        for (var i = 0; i < maxItems; i++)
        {
            if (candidates.Count > 0)
            {
                Coordinates c =  candidates[generateNumber(0, candidates.Count - 1)];
                maze[c.x][c.y] = MazeTiles.ITEM;
                candidates.Remove(c);
            }
        }

        return maze;
    }

    private List<List<int>> generatePaths(List<List<int>> maze,
        Coordinates startingPoint, Coordinates endingPoint,
        Coordinates currentPosition, List<Coordinates> visitedStack)
    {
        if (currentPosition.Equals(endingPoint) || currentPosition.isNextTo(endingPoint)) return maze;
        var passedVerification = false;
        var tryCounter = 0;
        while (!passedVerification)
        {            
            int goTo = 0;
            if (currentPosition.Equals(startingPoint))
            {
                if (startingPoint.x == 0) goTo = Movements.DOWN;                
                if (startingPoint.y == 0) goTo = Movements.RIGHT;                
            }
            else
            {
                goTo = generateNumber(0, 3);                
            }
            var newPosition = currentPosition.move(goTo);
 
            if (newPosition.isNextTo(endingPoint) || (  isValid(newPosition, maze) && !visitedStack.Contains(newPosition) &&  newPosition.bound0s(maze) < 3))
            {
                currentPosition = new Coordinates(newPosition.x, newPosition.y);
                passedVerification = true;
                if (!visitedStack.Contains(currentPosition))
                {
                    visitedStack.Add(currentPosition);
                }


            } else
            {
                tryCounter += 1;
                if (tryCounter > TRY_THRESHOLD)
                {
                    passedVerification = true;
                    if (visitedStack.Count > 0)
                    {
                        currentPosition = visitedStack[generateNumber(0, visitedStack.Count - 1)];                        
                    }                                        
                }
            }

            

        }
        if (!currentPosition.Equals(startingPoint))
        {
            maze[currentPosition.x][currentPosition.y] = MazeTiles.PATH;
        }

        return generatePaths(maze, startingPoint, endingPoint, currentPosition, visitedStack);
    }

    private void printMaze(List<List<int>> maze)
    {
        string temp = "";
        for (var i = 0; i < maze.Count; i++)
        {
            for (var j = 0; j < maze[i].Count; j++)
            {
                temp += maze[i][j] + " ";
            }            
            temp = "";
        }        
    }

}
