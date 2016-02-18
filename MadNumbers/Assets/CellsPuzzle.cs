using UnityEngine;
using System.Collections;

public class CellsPuzzle : MonoBehaviour
{

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
    void Start()
    {           

    }
    public void CreateCellsPuzzle(int number,int color)
    {
        pole = GameObject.FindWithTag("Pole");
        cellNumber = number;
        NumbersThis = (GameObject)Instantiate(Numbers, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        SpriteRenderer chouseSprite = NumbersThis.AddComponent<SpriteRenderer>();
        chouseSprite.sprite = numbers[cellNumber];
        chouseSprite.sortingLayerName = "cell";
        chouseSprite.sortingOrder = 2;
        NumbersThis.transform.parent = transform;
        this.color = color;
        ColorThis = (GameObject)Instantiate(Numbers, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        SpriteRenderer chouseColor = ColorThis.AddComponent<SpriteRenderer>();
        chouseColor.sprite = colors[color];
        chouseColor.sortingLayerName = "cell";
        chouseColor.sortingOrder = 1;
        ColorThis.transform.parent = transform;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnMouseDown()
    {
        Game_PlayerVsPc_endless gPole = pole.GetComponent<Game_PlayerVsPc_endless>();
        gPole.ChangePoints(this.Number);
        gPole.ChouseLine(x, y);
        gPole.OnCheck();
        Destroy(ColorThis);
        Destroy(NumbersThis);
        Destroy(gameObject);
    }
    public int Number
    {
        get
        {
            if (color == 0)
            {
                return cellNumber + 1;
            }
            else
            {
                return -cellNumber - 1;
            }
        }
        set
        {
            cellNumber = value;
            SpriteRenderer chouseSprite = NumbersThis.AddComponent<SpriteRenderer>();
            chouseSprite.sprite = numbers[value-1];

        }
    }
    public void checkPosition(float pos)
    {

        //NumbersThis.transform.position = new Vector2(NumbersThis.transform.position.x, NumbersThis.transform.position.y + pos);
        //ColorThis.transform.position = new Vector2(ColorThis.transform.position.x, ColorThis.transform.position.y + pos);
    }
}
