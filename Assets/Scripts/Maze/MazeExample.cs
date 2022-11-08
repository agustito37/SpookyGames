using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MazeExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MazeModel m = MazeFactory.Instance.generateMaze();        
        string tmp = "";
        for (var i = 0; i <  m.matrix.Count; i++)
        {
            for (var j = 0; j < m.matrix.Count; j++)
            {
                tmp += m.matrix[i][j] + " ";
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
