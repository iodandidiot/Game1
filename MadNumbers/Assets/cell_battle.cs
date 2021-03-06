﻿using UnityEngine;
using System.Collections;

public class cell_battle : MonoBehaviour {

    public Sprite[] numbers;
    public Sprite[] colors;
    public GameObject Numbers;
    public GameObject emptyCells;
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
    void Update()
    {

    }
    public void OnMouseDown()
    {
        Game_PlayerVSPc_buttle gPole = pole.GetComponent<Game_PlayerVSPc_buttle>();
        gPole.ChangePoints(this.Number);
        gPole.ChouseLine(x, y);
        Destroy(ColorThis);
        Destroy(NumbersThis);
        GameObject emptyCell = (GameObject)Instantiate(emptyCells, this.transform.position, Quaternion.identity);
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
        }
    }
}
