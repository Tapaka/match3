using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridBuilder : MonoBehaviour {

    private enum Direction
    { 
        Up,
        Down,
        Left,
        Right
    }

    private static GridBuilder instance;
    public int rows, columns;
    public float cellSize;
    public GameObject cellPref;
    public Vector3 startPosition;
    public Cell[,] cells;
    public Sprite[] jewels;
    private Cell currentCell;

	// Use this for initialization
	void Start () {

        cells = new Cell[columns, rows];

        instance = this;

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject go = Instantiate(cellPref, 
                    new Vector3(startPosition.x + j + cellSize,
                        startPosition.y + i + cellSize, 0), 
                        Quaternion.identity) as GameObject;

                go.transform.SetParent(gameObject.transform);
                cells[i,j] = go.GetComponent<Cell>();
                cells[i, j].AddJewel(cells[i, j].jewel.Init());
                cells[i, j].position = new Cell.Position() { x = j, y = i };
            }
        }

        ParseGrid();
	}

    public static Sprite GetSprite(int index)
    {
        return instance.jewels[index];
    }

    public void ParseGrid()
    {
        List<Cell> ballsList = new List<Cell>();
        List<Cell> detected = new List<Cell>();

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Cell currentBall = cells[i,j];
                if (ballsList.Count > 0 && 
                    ballsList[ballsList.Count - 1].jewel.type == currentBall.jewel.type)
                {
                    ballsList.Add(currentBall);
                    if (ballsList.Count >= 3)
                    {
                        for (int k = 0; k < ballsList.Count; k++)
                        {
                            if(!detected.Contains(ballsList[k]))
                                detected.Add(ballsList[k]);
                        }
                    }
                }
                else
                {
                    ballsList.Clear();
                    ballsList.Add(currentBall);
                }
            }
            
            
            ballsList.Clear();

        }

        ballsList.Clear();
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Cell currentBall = cells[j,i];
                if (ballsList.Count > 0 && ballsList[ballsList.Count - 1].jewel.type == currentBall.jewel.type)
                {
                    ballsList.Add(currentBall);
                    if (ballsList.Count >= 3)
                    {
                        for (int k = 0; k < ballsList.Count; k++)
                        {
                            if (!detected.Contains(ballsList[k]))
                                detected.Add(ballsList[k]);
                        }
                    }
                }
                else
                {
                    ballsList.Clear();
                    ballsList.Add(currentBall);
                }
            }
            ballsList.Clear();
        }

        for (int i = 0; i < detected.Count; i++)
        {
            detected[i].jewel.Explode();
            detected[i].GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void Clear()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                cells[i,j].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    public void DetectHorizontal(int[] position, int start, int end)
    {
        int points = 1;
        for (int i = start; i < end; i++)
        {
            Debug.Log("start : " + start + " end " + end);
            if (position[1] + i < end)
            {

                if (cells[position[0], position[1]].jewel.type == cells[position[0], position[1] + i].jewel.type)
                {                
                    cells[position[0], position[1]].GetComponent<SpriteRenderer>().color = Color.red;
                    cells[position[0], position[1] + i].GetComponent<SpriteRenderer>().color = Color.red;

                    points++;
                }
                else
                    break;
            }

            
        }

        if (points > 2)
        {
            for (int i = start; i < points; i++)
            {
                cells[position[0], position[1] +i].GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

       
    }

    private void DetectVertical(int[] position, int start, int end)
    {
        int points = 1;
        for (int i = start; i < end -1; i++)
        {
            if (position[0] + i < end)
            {
                if (cells[position[0], position[1]].jewel.type == cells[position[0] + i, position[1]].jewel.type)
                    points++;
                else
                    break;
            }
        }
        if (points > 2)
        {
            for (int i = start; i < points; i++)
            {
                cells[position[0] + i, position[1]].GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    public static void SelectCell(Cell cell)
    {
        if (instance.currentCell != null)
        {
            instance.currentCell.Idle();
            if (IsNear(cell))
            {
                cell.Switch(instance.currentCell);
                Debug.Log("ok");
            }
        }
        instance.currentCell = cell;
    }

    private static bool IsNear(Cell cell)
    {
        if (instance.currentCell.position.y == 0)
        {
            if (instance.currentCell.position.x == 0)
            {
                if ((cell.position.y == 1 && 
                    instance.currentCell.position.x == cell.position.x) || 
                    (cell.position.x == 1 && 
                    instance.currentCell.position.y == cell.position.y))
                {
                    
                    return true;
                }
            }
            else if(instance.currentCell.position.x == instance.rows -1)
            {
                if ((cell.position.y == 1 && 
                    cell.position.x == instance.currentCell.position.x) ||
                    (cell.position.x == instance.currentCell.position.x - 1 &&
                    cell.position.y == instance.currentCell.position.y))
                {
                    Debug.Log("2");
                    Debug.Log("cell x " + cell.position.x + "  cell y " + cell.position.y);
                    Debug.Log("current cell x " + instance.currentCell.position.x + "  current cell y " + instance.currentCell.position.y);
                    
                    return true;
                }
            }

        }
        else if (instance.currentCell.position.y == instance.columns - 1)
        {
            if (instance.currentCell.position.x == 0)
            {
                if ((cell.position.y == instance.currentCell.position.y - 1 &&
                    cell.position.x == instance.currentCell.position.x) || 
                    (cell.position.x == 1 && 
                    cell.position.y == instance.currentCell.position.y))
                    return true;
            }
            else if (instance.currentCell.position.x == instance.rows - 1)
            {
                if ((cell.position.y == instance.currentCell.position.y - 1 &&
                    cell.position.x == instance.currentCell.position.x ) || 
                    (cell.position.x == instance.currentCell.position.x - 1 && 
                    cell.position.y == instance.currentCell.position.y))
                    return true;
            }

        }
        else
        {
            if (cell.position.x == 0)
            {
                if ((cell.position.x == instance.currentCell.position.x - 1 &&
                    cell.position.y == instance.currentCell.position.y) ||  
                    (cell.position.y == instance.currentCell.position.y - 1 && 
                    cell.position.x == instance.currentCell.position.x) || 
                    (cell.position.y == instance.currentCell.position.y + 1 &&
                    cell.position.x == instance.currentCell.position.x))
                    return true;
            }
            else if (cell.position.x == instance.rows - 1)
            {
                if ((cell.position.x == instance.currentCell.position.x + 1 &&
                    cell.position.y == instance.currentCell.position.y) ||
                    (cell.position.y == instance.currentCell.position.y - 1 &&
                    cell.position.x == instance.currentCell.position.x) ||
                    (cell.position.y == instance.currentCell.position.y + 1 &&
                    cell.position.x == instance.currentCell.position.x))
                    return true;
            }
            else
            {
                if ((cell.position.x == instance.currentCell.position.x + 1 &&
                    cell.position.y == instance.currentCell.position.y)||
                    (cell.position.x == instance.currentCell.position.x - 1 &&
                    cell.position.y == instance.currentCell.position.y) ||
                    (cell.position.y == instance.currentCell.position.y - 1 &&
                    cell.position.x == instance.currentCell.position.x) ||
                    (cell.position.y == instance.currentCell.position.y + 1 &&
                    cell.position.x == instance.currentCell.position.x))
                    return true;
            }

        }


        return false;
    }

}
