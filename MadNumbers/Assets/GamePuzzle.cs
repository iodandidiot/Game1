using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePuzzle : MonoBehaviour
{

    public GameObject cell;
    public int poleRazmer;
    GameObject[,] cells;
    public Text StepText;
    private int _turn = 1;
    int stepsPL = -1;
    public int maxDepth;
    public Text endText;
    public Text StepTextBig;
    public Button restartButton;
    public GameObject img;
    public int memScore1;
    public int memScore2;
    public Image EndImage;
    int line;
    int column;
    bool _addLine;
    // Use this for initialization
    void Start()
    {
        EndImage.gameObject.SetActive(false);
        StepText.text = stepsPL.ToString();
        cells = new GameObject[poleRazmer, poleRazmer];
        int str=Random.Range(0, poleRazmer);
        int col=Random.Range(0, poleRazmer);
        int color = Random.Range(0, 2);
        int number=Random.Range(0, 11);        
        Generate1();
        ChouseLine(Random.Range(0, poleRazmer), Random.Range(0, poleRazmer), 0, true);
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
                cells[i, j] = (GameObject)Instantiate(cell, new Vector2(-3.5f + j * 1.25f, 4 - i * 1.25f), Quaternion.identity);
                CellsPuzzle cellPozition = cells[i, j].GetComponent<CellsPuzzle>();
                cellPozition.x = j;
                cellPozition.y = i;

            }
        }
    }
    public void ChouseLine(int y, int x, int number, bool start = false)
    {
        bool end = true;
        stepsPL++;
        StepText.text = stepsPL.ToString();
        if (_turn == 0)
        {
            _turn=1;
            OffAllCollaider();
            for (int i = 0; i < poleRazmer; i++)
            {
                if (cells[y,i] != null && i!=x)
                {
                    CellsPuzzle Point = cells[y, i].GetComponent<CellsPuzzle>();
                    Point.Number += number;
                    SpriteRenderer _render = cells[y, i].GetComponent<SpriteRenderer>();
                    _render.color = Color.yellow;
                    Point.changeColor();
                }                
            }

            for (int j = 0; j < poleRazmer; j++)
            {
                if (cells[j, x] != null && j != y || cells[j, x] != null && start)
                {
                    end = false;
                    BoxCollider2D cellColl = cells[j, x].AddComponent<BoxCollider2D>();
                    SpriteRenderer _render = cells[j, x].GetComponent<SpriteRenderer>();
                    _render.color = Color.blue;
                }

            }
        }
        else
        {
            _turn = 0;
            OffAllCollaider();
            if (!start)
            {
                for (int i = 0; i < poleRazmer; i++)
                {
                    if (cells[i, x] != null && i != y)
                    {
                        CellsPuzzle Point = cells[i, x].GetComponent<CellsPuzzle>();
                        Point.Number += number;
                        SpriteRenderer _render = cells[i, x].GetComponent<SpriteRenderer>();
                        _render.color = Color.yellow;
                        Point.changeColor();
                    }

                }
            }
            
            for (int i = 0; i < poleRazmer; i++)
            {
                if (cells[y, i] != null && i != x || cells[y, i] != null && start)
                {
                    end = false;
                    BoxCollider2D cellColl = cells[y, i].AddComponent<BoxCollider2D>();
                    SpriteRenderer _render = cells[y, i].GetComponent<SpriteRenderer>();
                    _render.color = Color.blue;

                }
            }
        }
        if (end) EndGame();
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

    public void EndGame(int plusStep=0)
    {
        EndImage.gameObject.SetActive(true);
        stepsPL += plusStep;
        StepText.gameObject.SetActive(false);
        StepTextBig.text = stepsPL.ToString();
        StartCoroutine("lostCells");
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

    IEnumerator lostCells()
    {
        foreach (GameObject i in cells)
        {
            if (i != null)
            {
                Destroy(i.gameObject);
                stepsPL++;
                StepTextBig.text = stepsPL.ToString();
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                continue;
            }
        }
    }
}
