using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class MazeExample : MonoBehaviour
{

    public bool FullRandom = false;
    public int RandomSeed = 12345;
    public GameObject Floor = null;
    public GameObject Wall = null;
    public GameObject Pillar = null;

    public int Rows = 5;
    public int Columns = 5;
    public float CellWidth = 5;
    public float CellHeight = 5;
    public bool AddGaps = true;
    public GameObject GoalPrefab = null;
    public GameObject EndPrefab = null;
    public GameObject Character = null;

    public GameObject collectible = null;
    // Start is called before the first frame update
    void Start()
    {
        MazeModel m = MazeFactory.Instance.generateMaze(Rows, Columns, 10, 10);
        string tmp = "";

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            player = Character;
        }

        for (var column = 0; column < m.matrix.Count; column++)
        {
            for (var row = 0; row < m.matrix.Count; row++)
            {
                float x = column * (CellWidth + (AddGaps ? .2f : 0));
                float z = row * (CellHeight + (AddGaps ? .2f : 0));
                GameObject gameObject;

                gameObject = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0));
                gameObject.name = "Floor - " + (new Coordinates(column, row)).ToString();
                gameObject.transform.parent = transform;



                if (m.matrix[column][row] == MazeTiles.ITEM)
                {

                    gameObject = Instantiate(collectible, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0));
                    gameObject.name = "Item - " + (new Coordinates(column, row)).ToString();
                    gameObject.transform.parent = transform;
                }

                if (m.matrix[column][row] == MazeTiles.START)
                {
                    player.transform.position = new Vector3(x, 0, z);
                }

                if (m.matrix[column][row] == MazeTiles.END)
                {
                    gameObject = Instantiate(EndPrefab, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0));
                    gameObject.name = "End - " + (new Coordinates(column, row)).ToString();
                    gameObject.transform.parent = transform;
                }


                if (m.matrix[column][row] == MazeTiles.WALLS)
                {

                    GameObject parentObject = new GameObject();
                    parentObject.name = "Set of Walls 1- " + (new Coordinates(column, row)).ToString();
                    parentObject.transform.parent = transform;

                    for (int k = 0; k < 4; k++)
                    {
                        gameObject = Instantiate(Wall, new Vector3(x - k + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0));
                        gameObject.name = "Wall " + (k + 1) + " " + (new Coordinates(column, row)).ToString();
                        gameObject.transform.parent = parentObject.transform;
                    }

                }


            }
            Debug.Log(tmp);
            tmp = "";
        }
        Debug.Log("");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
