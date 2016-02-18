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
    private int _turn = 1;
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
        Generate();
        //pointsTextPlayer.text = string.Format("{0}", PlayerPoints);
        //pointsTextComp.text = string.Format("{0}", CompPoints);
        //ChouseLine(Random.Range(0, poleRazmer), Random.Range(0, poleRazmer), true);
        //img.gameObject.SetActive(false);
        //restartButton.gameObject.SetActive(false);
    }

    public void OnCheck()
    {
        StartCoroutine("Check");
    }
    IEnumerator Check()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < poleRazmer; i++)
        {
            if (isEmptyLine(i) && !_addLine)
            {
                StartCoroutine(addLine(i));
                break;
            }
        }
    }

    public bool isEmptyLine(int y)// Проверка строки на наличие ячейки
    {
        for (int j = 0; j < poleRazmer; j++)
        {
            if (cells[y, j] != null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator addLine(int y)// Смещение и довабление новой строки
    {
        yield return new WaitForEndOfFrame();
        _addLine = true;
        for (int i = y - 1; i >= 0; i--)//Смещение
        {
            for (int j = 0; j < poleRazmer; j++)
            {
                if (cells[i, j] != null)
                {
                    cells[i + 1, j] = cells[i, j];
                    cells[i + 1, j].gameObject.transform.position = new Vector2(cells[i + 1, j].gameObject.transform.position.x, cells[i + 1, j].gameObject.transform.position.y - 1.4f);
                    CellEndlessGenerate _cells = cells[i + 1, j].GetComponent<CellEndlessGenerate>();
                    _cells.checkPosition(-1.4f);
                    _cells.x = j;
                    _cells.y = i + 1;
                    cells[i, j] = null;
                }

            }
        }
        for (int j = 0; j < poleRazmer; j++)
        {
            cells[0, j] = (GameObject)Instantiate(cell, new Vector2(-3.5f + j * 1.4f, 4 - 0 * 1.4f), Quaternion.identity);
            CellEndlessGenerate cellPozition = cells[0, j].GetComponent<CellEndlessGenerate>();
            cellPozition.x = j;
            cellPozition.y = 0;
            if (_turn == 1 && j == column)
            {
                PolygonCollider2D cellColl = cells[0, j].AddComponent<PolygonCollider2D>();
                SpriteRenderer _render = cells[0, j].GetComponent<SpriteRenderer>();
                _render.color = Color.blue;
            }
        }
        _addLine = false;
    }

    private void Generate(int step=0,int col=0,int str=0,int color=0,int number=0)
    {
        //if (step == 0)
        //{
        //    int _col = Random.Range(0, poleRazmer);
        //    int _str = Random.Range(0, poleRazmer);
        //    int _number = Random.Range(1, 11);
        //    int _color = Random.Range(0, 2);
        //    int color;
        //    cells[_str, _col] = (GameObject)Instantiate(cell, new Vector2(-3.5f + _col * 1.4f, 4 - _str * 1.4f), Quaternion.identity);
        //    CellsPuzzle lastPoint = cells[_str, _col].GetComponent<CellsPuzzle>();
        //    lastPoint.CreateCellsPuzzle(Mathf.Abs(_number), _color);
        //    for (int i = 0; i < poleRazmer; i++)
        //    {
        //        if (cells[i, _col]==null)
        //        {
        //            cells[i, _col] = (GameObject)Instantiate(cell, new Vector2(-3.5f + _col * 1.4f, 4 - i * 1.4f), Quaternion.identity);
        //            CellsPuzzle Point = cells[i, _col].GetComponent<CellsPuzzle>();
        //            Point.CreateCellsPuzzle(Mathf.Abs(_number), color = _color == 1 ? 0 : 1);
        //        }                
        //    }
        //    Generate(1, 0, _str);
        //}
        //if (step != 0 && step % 2 != 0)
        //{
        //    int _col = Random.Range(0, poleRazmer);
        //    int _str = str;
        //    int _number = Random.Range(1, 11);
        //    int _color = Random.Range(0, 2);
        //    int color;
        //    if (cells[_str, _col] == null)
        //    {
        //        cells[_str, _col] = (GameObject)Instantiate(cell, new Vector2(-3.5f + _col * 1.4f, 4 - _str * 1.4f), Quaternion.identity);
        //        CellsPuzzle lastPoint = cells[_str, _col].GetComponent<CellsPuzzle>();
        //        lastPoint.CreateCellsPuzzle(Mathf.Abs(_number), _color);
        //    }
        //    else
        //    {
        //        CellsPuzzle lastPoint = cells[_str, _col].GetComponent<CellsPuzzle>();
        //        lastPoint.Number = lastPoint.Number + _number;
        //    }
        //    for (int i = 0; i < poleRazmer; i++)
        //    {
        //        if (cells[_str, i] == null)
        //        {
        //            cells[i, _col] = (GameObject)Instantiate(cell, new Vector2(-3.5f + _col * 1.4f, 4 - i * 1.4f), Quaternion.identity);
        //            CellsPuzzle Point = cells[_str, i].GetComponent<CellsPuzzle>();
        //            Point.CreateCellsPuzzle(Mathf.Abs(_number), color = _color == 1 ? 0 : 1);
        //        }
        //    }
        //}

        if (step == 0)
        {
            int _number = Random.Range(1, 11);
            int _color = Random.Range(0, 2);
            for (int i = 0; i < poleRazmer; i++)
            {
                if (cells[i, col] == null)
                {
                    cells[i, col] = (GameObject)Instantiate(cell, new Vector2(-3.5f + col * 1.4f, 4 - i * 1.4f), Quaternion.identity);
                    CellsPuzzle Point = cells[i, col].GetComponent<CellsPuzzle>();
                    Point.CreateCellsPuzzle(Mathf.Abs(_number), color = _color == 1 ? 0 : 1);
                }
            }

        }
    }

    public void ChangePoints(int number)
    {
        if (_turn == 0)
        {
            PlayerPoints += number;
            pointsTextPlayer.text = string.Format("{0}", PlayerPoints);
        }
        else
        {
            CompPoints += number;
            pointsTextComp.text = string.Format("{0}", CompPoints);
        }

    }
    public void ChouseLine(int x, int y, bool start = false)
    {
        OffAllCollaider();
        bool isEnd = true;
        line = y;
        column = x;
        if (_turn == 0)
        {
            for (int j = 0; j < poleRazmer; j++)
            {
                if (cells[j, x] != null/*&& j != y*/ || cells[j, x] != null && start)
                {
                    isEnd = false;
                    PolygonCollider2D cellColl = cells[j, x].AddComponent<PolygonCollider2D>();
                    SpriteRenderer _render = cells[j, x].GetComponent<SpriteRenderer>();
                    _render.color = Color.blue;
                }

            }
            _turn = 1;
            //if (isEnd) EndGame(0, x, y);
            //StartCoroutine(CompStep(x));
            //return;
        }
        else
        {
            for (int i = 0; i < poleRazmer; i++)
            {
                if (cells[y, i] != null/* && i != x*/ || cells[y, i] != null && start)
                {
                    isEnd = false;
                    PolygonCollider2D cellColl = cells[y, i].AddComponent<PolygonCollider2D>();
                    SpriteRenderer _render = cells[y, i].GetComponent<SpriteRenderer>();
                    _render.color = Color.blue;
                }
            }
            _turn = 0;
            //if (isEnd) EndGame(1, x, y);
            //return;
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
