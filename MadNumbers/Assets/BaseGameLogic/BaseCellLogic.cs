using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.BaseGameLogic
{
    public class CellParam
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool Touch { get; set; }
        public GameObject BaseCellObject { get; set; }
        public GameObject NumberGameObject { get; set; }
        public GameObject ColorGameObject { get; set; }
    }


    public class BaseCellLogic
    {
        private List<List<CellParam>> _cellParamList;
        private readonly Cell _cell;
        private readonly int _size;
        private readonly Vector2 _offset;

        public event Action<CellTouchEvent> CellTouch;

        public BaseCellLogic(Cell cell, int size, Vector2 offset)
        {
            _cell = cell;
            _size = size;
            _offset = offset;
            _cellParamList = new List<List<CellParam>>(size);
            Create();
        }

        private void Create()
        {
            var id = 0;
            for (int i = 0; i < _size; i++)
            {
                _cellParamList.Add(new List<CellParam>(_size));
                for (int j = 0; j < _size; j++)
                {
                    CellInstance(i, j, id++);
                }
            }
            ChoiceCell(Steps.player, _cellParamList[Random.Range(0, _cellParamList.Capacity)][Random.Range(0, _cellParamList.Capacity)]);
        }

        private void CellInstance(int i, int j, int id)
        {
            var position = new Vector2(0 + _offset.x * i, 0 + _offset.y * j);
            var number = RandomNumber();
            var cell = _cell.Load(position, number, id);
            var colorGameObject = cell.CreateGameObject(cell.gameObject, _cell.ColorGameObject, position, 1, "color");
            var numberGameObject = cell.CreateGameObject(cell.gameObject, _cell.NumberGameObject, position, 2, "number");

            cell.Initialize(numberGameObject.GetComponent<SpriteRenderer>(), colorGameObject.GetComponent<SpriteRenderer>(), number);
            cell.mouseDown += OnCellClick;

            _cellParamList[i].Add(new CellParam()
            {
                Id = cell.GetInstanceID(),
                Number = number,
                BaseCellObject = cell.gameObject,
                NumberGameObject = numberGameObject,
                ColorGameObject = colorGameObject
            });

        }

        private int RandomNumber()
        {
            var count = _cell.NumbersSprite.Length - 1;
            var number = Random.Range(-count, count);
            return number == 0 ? Random.Range(1, count) : number;
        }

        private void OnCellClick(int id)
        {
            var cellEvent = CreateEvent(id, Steps.player);
            if (CellTouch != null)
            {
                CellTouch(cellEvent);
            }

        }

        private CellTouchEvent CreateEvent(int id, Steps step, bool start = false)
        {

            for (var i = 0; i < _cellParamList.Count; i++)
            {
                for (int j = 0; j < _cellParamList[i].Capacity; j++)
                {
                    if (_cellParamList[i][j].Id == id)
                    {
                        if (!start)
                        {
                            _cellParamList[i][j].Touch = true;
                        }

                        return new CellTouchEvent()
                        {
                            cellParamList = _cellParamList,
                            X = i,
                            Y = j,
                            Number = _cellParamList[i][j].Number,
                            Step = step
                        };
                    }
                }
            }
            return new CellTouchEvent();
        }

        public void ChoiceCell(Steps step, CellParam cell)
        {

            if (step == Steps.player)
            {
                var cellEvent = CreateEvent(cell.Id, step, true);
                DeleteColider();
                foreach (var cellChoise in _cellParamList[cellEvent.X])
                {
                    if (cellChoise.Touch)
                        continue;
                    cellChoise.BaseCellObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    if (cellChoise.BaseCellObject.GetComponent<BoxCollider2D>() == null)
                        cellChoise.BaseCellObject.AddComponent<BoxCollider2D>();
                }

            }
            else
            {
                var cellEvent = CreateEvent(cell.Id, step, true);
                DeleteColider();
                foreach (var cellChoise in _cellParamList)
                {
                    if (cellChoise[cellEvent.Y].Touch)
                        continue;
                    cellChoise[cellEvent.Y].BaseCellObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    if (cellChoise[cellEvent.Y].BaseCellObject.GetComponent<BoxCollider2D>() == null)
                        cellChoise[cellEvent.Y].BaseCellObject.AddComponent<BoxCollider2D>();
                }
            }
        }

        private void DeleteColider()
        {
            _cellParamList.ForEach(cells =>
                cells.ForEach(
                    cell =>
                        {
                            if (cell != null && cell.BaseCellObject.GetComponent<BoxCollider2D>() != null) 
                                cell.BaseCellObject.GetComponent<BoxCollider2D>();
                        }));
        }
    }
}