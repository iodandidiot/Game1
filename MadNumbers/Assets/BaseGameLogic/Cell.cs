using UnityEngine;
using System;

public class Cell : MonoBehaviour
{
    public event Action<int> mouseDown;

    public GameObject InstanseGameObject { get; set; }
    public GameObject ColorGameObject { get; set; }
    public GameObject NumberGameObject { get; set; }
    public Sprite[] ColorsSprite { get; set; }
    public Sprite[] NumbersSprite { get; set; }

    public Cell Load(Vector2 position, int number, int id)
    {
        var cell = CellInstanse(position, id);
        cell.ColorsSprite = ColorsSprite;
        cell.NumbersSprite = NumbersSprite;
        return cell;
    }

    public void Initialize(SpriteRenderer numberSprite, SpriteRenderer colorSprite, int number)
    {
        if (number > 0)
            colorSprite.sprite = ColorsSprite[0];
        else
            colorSprite.sprite = ColorsSprite[1];
        numberSprite.sprite = NumbersSprite[Mathf.Abs(number) - 1];
    }

    private Cell CellInstanse(Vector2 position, int id)
    {
        var cellInstanse = (GameObject)Instantiate(InstanseGameObject, new Vector2(position.x, position.y), Quaternion.identity);
        var cell = cellInstanse.AddComponent<Cell>();
        //cellInstanse.AddComponent<BoxCollider2D>();
        cellInstanse.gameObject.name = String.Format("cell_{0}", id);

        return cell;
    }

    public GameObject CreateGameObject(GameObject instanse, GameObject sprite, Vector2 position, int order, string name)
    {
        var gObject = (GameObject)Instantiate(sprite, new Vector2(position.x, position.y), Quaternion.identity);
        gObject.gameObject.name = name;
        gObject.transform.parent = instanse.transform;
        gObject.GetComponent<SpriteRenderer>().sortingLayerName = "cell";
        gObject.GetComponent<SpriteRenderer>().sortingOrder = order;
        return gObject.GetComponent<SpriteRenderer>().gameObject;

    }

    public void OnMouseDown()
    {
        if (mouseDown != null)
            mouseDown(this.GetInstanceID());
        DeInitialize();
    }

    public void DeInitialize()
    {
        Destroy(InstanseGameObject);
        Destroy(gameObject);
    }
}
