using UnityEngine;
using Assets.BaseGameLogic;

using UniRx;

using UnityEngine.UI;

public enum Steps
{
    player,
    ai
}

public class GameConstructor : MonoBehaviour
{
    private BaseGameLogic logic;

    public Cell _cell;
    private BaseCellLogic _cellLogic;
    public GameObject _instanseGameObject;
    public GameObject _colorGameObject;
    public GameObject _numberGameObject;
    public Sprite[] _colorsSprite;
    public Sprite[] _numbersSprite;

    public int _size;
    public int _maxDepth;

    public Text _playerScore;
    public Text _aiScore;

    public int _playerScoreCount;
    public int _aiScoreCount;


    void Awake()
    {
        _cell.ColorsSprite = _colorsSprite;
        _cell.NumbersSprite = _numbersSprite;
        _cell.InstanseGameObject = _instanseGameObject;
        _cell.NumberGameObject = _numberGameObject;
        _cell.ColorGameObject = _colorGameObject;
        logic = new BaseGameLogic(_maxDepth, _size);
    }

    void Start()
    {
        var vector = _colorsSprite[0].border.w;
        _cellLogic = new BaseCellLogic(_cell, _size, new Vector2(-1, 1));
        this._cellLogic.CellTouch += Handle;
    }

    private void Handle(CellTouchEvent cellEvent)
    {
        if (cellEvent.Step == Steps.player)
        {
            _playerScoreCount += cellEvent.Number;
            _playerScore.text = _playerScoreCount.ToString();
            NextStep(cellEvent);
        }

        else
        {
            _aiScoreCount += cellEvent.Number;
            _aiScore.text = _aiScoreCount.ToString();
        }
    }

    private void NextStep(CellTouchEvent cellEvent)
    {
        var choisedCell = Observable.Start(
            () =>
            {
                var choise = logic.ChoiceCell(
                    cellEvent.Step,
                    cellEvent.X,
                    10,
                    cellEvent.cellParamList,
                    _playerScoreCount,
                    _aiScoreCount);
                return choise;
            });
        Observable.WhenAll(choisedCell).ObserveOnMainThread().Subscribe(result => _cellLogic.ChoiceCell(Steps.ai, result[0]));
    }

}
