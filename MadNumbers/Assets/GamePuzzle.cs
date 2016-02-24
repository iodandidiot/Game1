using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePuzzle : MonoBehaviour
{

    public GameObject cell;
    public int poleRazmer;
    GameObject[,] cells;
    public int PlayerPoints;
    public int CompPoints;
    public Text pointsTextPlayer;
    public Text pointsTextComp;
    private int _turn = 0;
    public int maxDepth;
    public Text endText;
    public Button restartButton;
    public GameObject img;
    public int memScore1;
    public int memScore2;
    int line;
    int column;
    bool _addLine;
    // Use this for initialization
    void Start()
    {
        cells = new GameObject[poleRazmer, poleRazmer];
        int str=Random.Range(0, poleRazmer);
        int col=Random.Range(0, poleRazmer);
        int color = Random.Range(0, 2);
        int number=Random.Range(0, 11);
        //cells[str, col] = (GameObject)Instantiate(cell, new Vector2(-3.5f + col * 1.4f, 4 - str * 1.4f), Quaternion.identity);
        //CellsPuzzle lastPoint = cells[str, col].GetComponent<CellsPuzzle>();
        //lastPoint.x = str;
        //lastPoint.y = col;
        //lastPoint.CreateCellsPuzzle(Mathf.Abs(number), color);
        //Generate(0, col, str, color, number);
        Generate1();
    }

    
    //private void Generate(int step=0,int col=0,int str=0,int color=0,int number=0)
    //{        
    //    if (step == 0)
    //    {
    //        int _color;
    //        for (int i = 0; i < poleRazmer; i++)
    //        {
    //            if (i != str && step == 0)
    //            {
    //                if (cells[i, col] == null)
    //                {
    //                    cells[i, col] = (GameObject)Instantiate(cell, new Vector2(-3.5f + col * 1.4f, 4 - i * 1.4f), Quaternion.identity);
    //                    CellsPuzzle Point = cells[i, col].GetComponent<CellsPuzzle>();
    //                    Point.CreateCellsPuzzle(Mathf.Abs(number), _color = color == 1 ? 0 : 1);
    //                    Point.x = i;
    //                    Point.y = col;
    //                }
    //                else
    //                {
    //                    CellsPuzzle Point = cells[i, col].GetComponent<CellsPuzzle>();
    //                    Point.Number = Point.Number + number;
    //                }
    //            }
    //            else
    //            {
    //                continue;
    //            }                   
    //        }
    //        //int nextCol=Random.Range(0, poleRazmer);
    //        int nextStr=Random.Range(0, poleRazmer);
    //        CellsPuzzle NextPoint = cells[nextStr, col].GetComponent<CellsPuzzle>();
    //        Generate(1, col, Random.Range(0, poleRazmer), Random.Range(0, 2), Random.Range(0, 11 - Mathf.Abs(NextPoint.Number)));
    //    }
    //    else
    //    {
    //        int _color;
    //        for (int i = 0; i < poleRazmer; i++)
    //        {
    //            //if (i != col)
    //            {
    //                if (cells[str, i] == null)
    //                {
    //                    cells[str, i] = (GameObject)Instantiate(cell, new Vector2(-3.5f + i * 1.4f, 4 - str * 1.4f), Quaternion.identity);
    //                    CellsPuzzle Point = cells[str, i].GetComponent<CellsPuzzle>();
    //                    Point.CreateCellsPuzzle(Mathf.Abs(number), _color = color == 1 ? 0 : 1);
    //                    Point.x = str;
    //                    Point.y = i;
    //                }
    //                else
    //                {
    //                    CellsPuzzle Point = cells[str, i].GetComponent<CellsPuzzle>();
    //                    Point.Number = Point.Number + number;
    //                }
    //            }
    //            //else
    //            //{
    //            //    continue;
    //            //}

    //        }
    //    }
    //}

    private void Generate1()
    {
        for (int i = 0; i < poleRazmer; i++)
        {
            for (int j = 0; j < poleRazmer; j++)
            {
                cells[i, j] = (GameObject)Instantiate(cell, new Vector2(-3.5f + j * 1.4f, 4 - i * 1.4f), Quaternion.identity);
                CellsPuzzle cellPozition = cells[i, j].GetComponent<CellsPuzzle>();
                cellPozition.x = j;
                cellPozition.y = i;

            }
        }
    }
    public void ChouseLine(int y, int x, int number, bool start = false)
    {
        if (_turn == 0)
        {
            _turn=1;
            for (int i = 0; i < poleRazmer; i++)
            {
                if (cells[y,i] != null && i!=x)
                {
                    CellsPuzzle Point = cells[y, i].GetComponent<CellsPuzzle>();
                    Point.Number += number;                    
                }                
            }
            OffAllCollaider();
            for (int j = 0; j < poleRazmer; j++)
            {
                if (cells[j, x] != null && j != y || cells[j, x] != null && start)
                {
                    PolygonCollider2D cellColl = cells[j, x].AddComponent<PolygonCollider2D>();
                    SpriteRenderer _render = cells[j, x].GetComponent<SpriteRenderer>();
                    _render.color = Color.blue;
                }

            }
        }
        else
        {
            _turn = 0;
            for (int i = 0; i < poleRazmer; i++)
            {
                if (cells[i, x] != null && i != y)
                {
                    CellsPuzzle Point = cells[i, x].GetComponent<CellsPuzzle>();
                    Point.Number += number;
                }

            }
            OffAllCollaider();
            for (int i = 0; i < poleRazmer; i++)
            {
                if (cells[y, i] != null && i != x || cells[y, i] != null && start)
                {
                    PolygonCollider2D cellColl = cells[y, i].AddComponent<PolygonCollider2D>();
                    SpriteRenderer _render = cells[y, i].GetComponent<SpriteRenderer>();
                    _render.color = Color.blue;
                }
            }
        }
    }

    private void OffAllCollaider()
    {

        for (int i = 0; i < poleRazmer; i++)
        {
            for (int j = 0; j < poleRazmer; j++)
            {
                if (cells[i, j] != null)
                {
                    Collider2D cellColl = cells[i, j].GetComponent<Collider2D>();
                    Destroy(cellColl);
                    SpriteRenderer _render = cells[i, j].GetComponent<SpriteRenderer>();
                    _render.color = Color.white;
                }
            }
        }

    }

    private void EndGame(int _turn, int x, int y)
    {
        if (IsCells(x, y))
        {
            if (_turn == 0)
            {
                endText.text = "You Win";
            }
            else
            {
                endText.text = "You Loose";
            }
            img.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
        else
        {
            if (PlayerPoints > CompPoints)
            {
                endText.text = "You Win";
            }
            else
            {
                endText.text = "You Loose";
            }
            img.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }

    private bool IsCells(int x, int y)
    {
        foreach (GameObject i in cells)
        {
            if (i != null && i != cells[y, x])
            {
                return true;
            }
            else continue;
        }
        return false;
    }
}
