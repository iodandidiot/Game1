using System;

using UnityEngine;
using System.Collections;
using System.Threading;

using Assets;

using UniRx;

using UnityEngine.UI;

using Random = UnityEngine.Random;

public class Game_PlayerVsPc : MonoBehaviour
{

    private AiChoice _aichoice;

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
        ChouseLine(Random.Range(0, poleRazmer), Random.Range(0, poleRazmer),true);
        img.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        _aichoice = new AiChoice(poleRazmer, cells, maxDepth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Generate()
    {
        for (int i = 0; i < poleRazmer; i++)
        {
            for (int j = 0; j < poleRazmer; j++)
            {
                cells[i, j] = (GameObject)Instantiate(cell, new Vector2(-3.5f + j * 1.25f, 4 - i * 1.25f), Quaternion.identity);
                cell_Pl_vs_Pc cellPozition = cells[i, j].GetComponent<cell_Pl_vs_Pc>();
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
    public void ChouseLine(int x, int y,bool start=false)
    {
        OffAllCollaider();
        bool isEnd=true;
        if (_turn == 0)
        {
            StartCoroutine(CompStep(x));
            for (int j = 0; j < poleRazmer; j++)
            {
                if (cells[j, x] != null && j != y || cells[j, x] != null && start)
                {
                    isEnd = false;
                    //PolygonCollider2D cellColl = cells[j, x].AddComponent<PolygonCollider2D>();
                    SpriteRenderer _render = cells[j, x].GetComponent<SpriteRenderer>();
                    _render.color = Color.blue;
                }
                
            }            
            _turn = 1;
            if (isEnd) EndGame(0,x,y);
            //StartCoroutine(CompStep(x));
            return;
        }
        else
        {
            for (int i = 0; i < poleRazmer; i++)
            {
                if (cells[y, i] != null && i != x || cells[y, i] != null && start)
                {
                    isEnd = false;
                    BoxCollider2D cellColl = cells[y, i].AddComponent<BoxCollider2D>();
                    SpriteRenderer _render = cells[y, i].GetComponent<SpriteRenderer>();
                    _render.color = Color.blue;
                }
            }
            _turn = 0;
            if (isEnd) EndGame(1,x,y);
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
                    cell_Pl_vs_Pc checkNumber = cells[_line, i].GetComponent<cell_Pl_vs_Pc>();
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
                    cell_Pl_vs_Pc checkNumber = cells[i, _line].GetComponent<cell_Pl_vs_Pc>();
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
        yield return new WaitForSeconds(0.1f);

        memScore2 = CompPoints;
        memScore1 = PlayerPoints;
        this._aichoice.CompPoints = CompPoints;
        this._aichoice.PlayerPoints = PlayerPoints;


        //GameObject compChoice = cells[AiChoice(_turn, x, 0), x];
        var cellNumber = 0;
        var heavyMethod = Observable.Start(() => _aichoice.Choice(_turn, x, 0));
        //Observable.Return(heavyMethod).Subscribe();
        Observable.WhenAll(heavyMethod).Subscribe(result => cellNumber = result[0]);
        var compChoice = cells[cellNumber, x];

        for (int i = 0; i < 10; i++)
        {
            SpriteRenderer _render = compChoice.GetComponent<SpriteRenderer>();
            Color color = new Color(1, 0.92f, 0.016f, 1);
            color.a -= i / 10;
            _render.color = color;
            yield return new WaitForSeconds(0.1f);
        }
        cell_Pl_vs_Pc stepComp = compChoice.GetComponent<cell_Pl_vs_Pc>();
        stepComp.OnMouseDown();
    }


    private IEnumerator ChangeColor(int i, GameObject compChoice)
    {
        var render = compChoice.GetComponent<SpriteRenderer>();
        var color = new Color(1, 0.92f, 0.016f, 1);
        color.a -= i / 10;
        render.color = color;
        yield return new WaitForSeconds(0.1f);
    }

    private void EndGame(int _turn,int x, int y)
    {
        if (IsCells(x,y))
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

    private bool IsCells(int x,int y)
    {        
        foreach (GameObject i in cells)
        {
            if (i != null && i != cells[y, x])
            {                
                return true;
            }
        }
        return false;
    }
}
