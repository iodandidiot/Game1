using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game_PlayerVsPc_endless : MonoBehaviour
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
    // Use this for initialization
    void Start()
    {
        cells = new GameObject[poleRazmer, poleRazmer];
        Generate();
        pointsTextPlayer.text = string.Format("{0}", PlayerPoints);
        pointsTextComp.text = string.Format("{0}", CompPoints);
        ChouseLine(Random.Range(0, poleRazmer), Random.Range(0, poleRazmer), true);
        img.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < poleRazmer; i++)
        {
            if (isEmptyLine(i))
            {
                addLine(i);
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

    public void addLine(int y)// Смещение и довабление новой строки
    {
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
                    _cells.y = i+1;
                }
                
            }
        }
        for (int j = 0; j < poleRazmer; j++)
        {
            cells[0, j] = (GameObject)Instantiate(cell, new Vector2(-3.5f + j * 1.4f, 4 - 0 * 1.4f), Quaternion.identity);
            CellEndlessGenerate cellPozition = cells[0, j].GetComponent<CellEndlessGenerate>();
            cellPozition.x = j;
            cellPozition.y = 0;
        }
    }

    private void Generate()
    {
        for (int i = 0; i < poleRazmer; i++)
        {
            for (int j = 0; j < poleRazmer; j++)
            {
                cells[i, j] = (GameObject)Instantiate(cell, new Vector2(-3.5f + j * 1.4f, 4 - i * 1.4f), Quaternion.identity);
                CellEndlessGenerate cellPozition = cells[i, j].GetComponent<CellEndlessGenerate>();
                cellPozition.x = j;
                cellPozition.y = i;

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
        if (_turn == 0)
        {
            StartCoroutine(CompStep(x));
            for (int j = 0; j < poleRazmer; j++)
            {
                if (cells[j, x] != null/*&& j != y*/ || cells[j, x] != null && start)
                {
                    isEnd = false;
                    //PolygonCollider2D cellColl = cells[j, x].AddComponent<PolygonCollider2D>();
                    SpriteRenderer _render = cells[j, x].GetComponent<SpriteRenderer>();
                    _render.color = Color.blue;
                }

            }
            _turn = 1;
            //if (isEnd) EndGame(0, x, y);
            //StartCoroutine(CompStep(x));
            return;
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
            return;
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

    public int AiChoice(int _turn, int _line, int dept)
    {
        dept++;
        int bestScore1 = -9999;
        int bestScore2 = -9999;
        int bestChoice = -1;
        int currScore1 = memScore1;
        int currScore2 = memScore2;
        int choice;
        int temp;

        for (int i = 0; i < poleRazmer; i++)
        {
            if (_turn == 0)//игрок
            {
                if (cells[_line, i] != null)
                {
                    CellEndlessGenerate checkNumber = cells[_line, i].GetComponent<CellEndlessGenerate>();
                    if (Mathf.Abs(checkNumber.Number) == 99) continue;

                    temp = checkNumber.Number;
                    checkNumber.Number = 98;
                    memScore1 = currScore1;
                    memScore2 = currScore2 + temp;
                    if (dept < maxDepth)
                    {
                        choice = AiChoice(1, i, dept);
                        //if ((memScore1 - memScore2 < bestScore1 - bestScore2 && (choice != -1 || (choice == -1 && memScore1 + CompPoints < memScore2 + PlayerPoints)) || bestScore1 == -9999))
                        if ((memScore1 - memScore2 < bestScore1 - bestScore2 && (choice != -1 || (choice == -1 && memScore1 + PlayerPoints < memScore2 + CompPoints)) || bestScore1 == -9999))
                        {
                            bestScore1 = memScore1;
                            bestScore2 = memScore2;
                            bestChoice = i;
                            if (choice == -1)
                                bestScore2 += poleRazmer * 5;
                        }

                    }
                    else
                    {
                        if ((memScore1 - memScore2 < bestScore1 - bestScore2))
                        {
                            bestScore1 = memScore1;
                            bestScore2 = memScore2;
                            bestChoice = i;
                        }
                    }
                    checkNumber.Number = Mathf.Abs(temp) - 1;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                if (cells[i, _line] != null)
                {
                    CellEndlessGenerate checkNumber = cells[i, _line].GetComponent<CellEndlessGenerate>();
                    if (Mathf.Abs(checkNumber.Number) == 99) continue;

                    temp = checkNumber.Number;
                    checkNumber.Number = 98;
                    memScore1 = currScore1 + temp;
                    memScore2 = currScore2;
                    if (dept < maxDepth)
                    {
                        choice = AiChoice(0, i, dept);
                        //if ((memScore1 - memScore2 > bestScore1 - bestScore2 && (choice != -1 || (choice == -1 && memScore1 + CompPoints > memScore2 + PlayerPoints)) || bestScore1 == -9999))
                        if ((memScore1 - memScore2 > bestScore1 - bestScore2 && (choice != -1 || (choice == -1 && memScore1 + PlayerPoints > memScore2 + CompPoints)) || bestScore1 == -9999))
                        {
                            bestScore1 = memScore1;
                            bestScore2 = memScore2;
                            bestChoice = i;
                            if (choice == -1)
                                bestScore1 += poleRazmer * 5;
                        }

                    }
                    else
                    {
                        if ((memScore1 - memScore2 > bestScore1 - bestScore2))
                        {
                            bestScore1 = memScore1;
                            bestScore2 = memScore2;
                            bestChoice = i;
                        }
                    }
                    checkNumber.Number = Mathf.Abs(temp) - 1;
                }
            }
        }
        if (bestScore1 != -9999)
        {
            memScore1 = bestScore1;
            memScore2 = bestScore2;
        }

        return bestChoice;
    }
    IEnumerator CompStep(int x)
    {
        yield return new WaitForSeconds(1f);
        //GameObject compChoice = cells[AIchoice(_turn, x, 1), x];

        memScore2 = CompPoints;
        memScore1 = PlayerPoints;
        GameObject compChoice = cells[AiChoice(_turn, x, 0), x];
        for (int i = 0; i < 10; i++)
        {
            SpriteRenderer _render = compChoice.GetComponent<SpriteRenderer>();
            Color color = new Color(1, 0.92f, 0.016f, 1);
            color.a -= i / 10;
            _render.color = color;
            yield return new WaitForSeconds(0.1f);
        }
        CellEndlessGenerate stepComp = compChoice.GetComponent<CellEndlessGenerate>();
        stepComp.OnMouseDown();
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

