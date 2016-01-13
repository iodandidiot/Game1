using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Generate_pole : MonoBehaviour {
    public GameObject cell;
    public int poleRazmer;
    GameObject[,] cells;
    public int points;
    public Text pointsText;
    private int _turn = 1;
	// Use this for initialization
	void Start () 
    {
        cells = new GameObject[poleRazmer, poleRazmer];
        Generate();
        pointsText.text=string.Format("{0}",points);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void Generate()
    {
        for (int i = 0; i < poleRazmer; i++)
        {
            for (int j = 0; j < poleRazmer; j++)
            {                
                cells[i,j]=(GameObject)Instantiate(cell, new Vector2(-3.5f + j * 1.4f, 4 - i * 1.4f), Quaternion.identity);
                CellGenerate cellPozition = cells[i, j].GetComponent<CellGenerate>();
                cellPozition.x = j;
                cellPozition.y = i;

            }
        }
    }

    public void ChangePoints(int color,int number)
    {
        if (color == 0)
        {
            points += number;
        }
        else
        {
            points -= number;
        }
        pointsText.text = string.Format("{0}", points);
    
    }
    public void ChouseLine(int x,int y)
    {
        OffAllCollaider();
        if (_turn == 0)
        {
            for (int j = 0; j < poleRazmer; j++)
            {
                if (cells[j, x] != null)
                {
                    PolygonCollider2D cellColl = cells[j, x].AddComponent<PolygonCollider2D>();
                    SpriteRenderer _render = cells[j, x].GetComponent<SpriteRenderer>();
                    _render.color = Color.blue;

                }
            }
            _turn = 1;
            return;
        }
        else
        {
            for (int i = 0; i < poleRazmer; i++)
            {
                if (cells[y, i] != null)
                {
                    PolygonCollider2D cellColl = cells[y, i].AddComponent<PolygonCollider2D>();
                    SpriteRenderer _render = cells[y, i].GetComponent<SpriteRenderer>();
                    _render.color = Color.blue;
                }
            }
            _turn = 0;
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
    
}
