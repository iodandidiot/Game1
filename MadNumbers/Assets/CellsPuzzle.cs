using UnityEngine;
using System.Collections;

public class CellsPuzzle : MonoBehaviour
{

    public Sprite[] numbers;
    public Sprite[] colors;
    public GameObject Numbers;
    SpriteRenderer chouseSprite;
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
        pole = GameObject.FindWithTag("Pole");
        cellNumber = Random.Range(0, 5);
        NumbersThis = (GameObject)Instantiate(Numbers, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        SpriteRenderer chouseSprite = NumbersThis.AddComponent<SpriteRenderer>();
        chouseSprite.sprite = numbers[cellNumber];
        chouseSprite.sortingLayerName = "cell";
        chouseSprite.sortingOrder = 2;
        NumbersThis.transform.parent = transform;
        ColorThis = (GameObject)Instantiate(Numbers, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        SpriteRenderer chouseColor = ColorThis.AddComponent<SpriteRenderer>();
        color = Random.Range(0, 2);
        chouseColor.sprite = colors[color];
        chouseColor.sortingLayerName = "cell";
        chouseColor.sortingOrder = 1;
        ColorThis.transform.parent = transform;
    }
    //public void CreateCellsPuzzle(int number,int color)
    //{
    //    pole = GameObject.FindWithTag("Pole");
    //    cellNumber = number;
    //    NumbersThis = (GameObject)Instantiate(Numbers, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    //    chouseSprite = NumbersThis.AddComponent<SpriteRenderer>();
    //    chouseSprite.sprite = numbers[cellNumber];
    //    chouseSprite.sortingLayerName = "cell";
    //    chouseSprite.sortingOrder = 2;
    //    NumbersThis.transform.parent = transform;
    //    this.color = color;
    //    ColorThis = (GameObject)Instantiate(Numbers, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
    //    SpriteRenderer chouseColor = ColorThis.AddComponent<SpriteRenderer>();
    //    chouseColor.sprite = colors[color];
    //    chouseColor.sortingLayerName = "cell";
    //    chouseColor.sortingOrder = 1;
    //    ColorThis.transform.parent = transform;
    //}
    // Update is called once per frame
    void Update()
    {

    }
    public void OnMouseDown()
    {
        GamePuzzle gPole = pole.GetComponent<GamePuzzle>();       
        gPole.ChouseLine(y, x, this.Number);
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
            if (value == 0)
            {
                Destroy(gameObject);
                return;
            }
            cellNumber = Mathf.Abs(value) - 1;
            if (cellNumber > 10)
            {
                GamePuzzle gPole = pole.GetComponent<GamePuzzle>();
                gPole.EndGame(36);
                return;
            }
            if (value > 0)
            {
                SpriteRenderer chouseColor = ColorThis.GetComponent<SpriteRenderer>();
                chouseColor.sprite = colors[0];
            }
            else
            {
                SpriteRenderer chouseColor = ColorThis.GetComponent<SpriteRenderer>();
                chouseColor.sprite = colors[1];
            }
            chouseSprite = NumbersThis.GetComponent<SpriteRenderer>();
            print(cellNumber);
            chouseSprite.sprite = numbers[cellNumber];

        }
    }
    
}
