using UnityEngine;
using Assets.BaseGameLogic;

public class GameConstructor : MonoBehaviour 
{
    public Cell _cell;
    private BaseCellLogic _cellLogic;
    public GameObject _instanseGameObject;
    public GameObject _colorGameObject;
    public GameObject _numberGameObject;
    public Sprite[] _colorsSprite;
    public Sprite[] _numbersSprite;
    public int _size;


    void Awake()
    {
        _cell.ColorsSprite = _colorsSprite;
        _cell.NumbersSprite = _numbersSprite;
        _cell.InstanseGameObject = _instanseGameObject;
        _cell.NumberGameObject = _numberGameObject;
        _cell.ColorGameObject = _colorGameObject;
    }

	// Use this for initialization
	void Start ()
	{
	    var vector = _colorsSprite[0].border.w;
        _cellLogic = new BaseCellLogic(_cell, _size, new Vector2(-1, -1));
	    this._cellLogic.CellTouch += Handle;
	}

    private void Handle(CellTouchEvent cellEvent)
    {
        
    }
	
}
