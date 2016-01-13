using UnityEngine;
using System.Collections;

public class CellGenerate : MonoBehaviour {
    public Sprite[] numbers;
    public Sprite[] colors;
    public GameObject Numbers;
    public int x;
    public int y;
    GameObject pole;
    GameObject NumbersThis;
    GameObject ColorThis;
    int cellNumber;
    int color;

	// Use this for initialization
	void Start () 
    {
        pole = GameObject.FindWithTag("Pole");
        cellNumber = Random.Range(0, 10);
        NumbersThis = (GameObject)Instantiate(Numbers, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        SpriteRenderer chouseSprite = NumbersThis.AddComponent<SpriteRenderer>();
        chouseSprite.sprite = numbers[cellNumber];
        chouseSprite.sortingLayerName = "cell";
        chouseSprite.sortingOrder = 2;
        ColorThis = (GameObject)Instantiate(Numbers, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        SpriteRenderer chouseColor = ColorThis.AddComponent<SpriteRenderer>();
        color = Random.Range(0, 2);
        chouseColor.sprite = colors[color];
        chouseColor.sortingLayerName = "cell";
        chouseColor.sortingOrder = 1;
	}    
	// Update is called once per frame
	void Update () 
    {
	    
	}
    void OnMouseDown() 
    {
        Generate_pole gPole = pole.GetComponent<Generate_pole>();
        gPole.ChangePoints(color, cellNumber+1);
        gPole.ChouseLine(x,y);
        Destroy(ColorThis);
        Destroy(NumbersThis);
        Destroy(gameObject);
    }
    public int Number
    {
        get
        {
            return cellNumber;
        }
    }
}
