using UnityEngine;
using System;

namespace Assets
{

    public class AiChoice
    {

        private Action<int> _callBack;
        private Func<int, int> _getComponent;
        private int _poleRazmer;
        private GameObject[,] _cells;
        private int _maxDepth;

        private int MemScore1 { get; set; }
        private int MemScore2 { get; set; }
        public int PlayerPoints { get; set; }
        public int CompPoints { get; set; }


        public AiChoice(int poleRazmer, GameObject[,] cells, int maxDepth)
        {

            _poleRazmer = poleRazmer;
            _cells = cells;
            this._maxDepth = maxDepth;

        }

        public int Choice(int turn, int line, int dept)
        {
            dept++;
            var bestScore1 = -9999;
            var bestScore2 = -9999;
            var bestChoice = -1;
            var currScore1 = MemScore1;
            var currScore2 = MemScore2;
            int choice;
            int temp;

            for (int i = 0; i < _poleRazmer; i++)
            {
                if (turn == 0)//игрок
                {
                    if (_cells[line, i] != null)
                    {
                        var checkNumber = _cells[line, i].GetComponent<cell_Pl_vs_Pc>();
                        if (Mathf.Abs(checkNumber.Number) == 99) continue;

                        temp = checkNumber.Number;
                        checkNumber.Number = 98;
                        MemScore1 = currScore1;
                        MemScore2 = currScore2 + temp;
                        if (dept < _maxDepth)
                        {
                            choice = Choice(1, i, dept);
                            //if ((memScore1 - memScore2 < bestScore1 - bestScore2 && (choice != -1 || (choice == -1 && memScore1 + CompPoints < memScore2 + PlayerPoints)) || bestScore1 == -9999))
                            if ((MemScore1 - MemScore2 < bestScore1 - bestScore2 && (choice != -1 || (choice == -1 && MemScore1 + PlayerPoints < MemScore2 + CompPoints)) || bestScore1 == -9999))
                            {
                                bestScore1 = MemScore1;
                                bestScore2 = MemScore2;
                                bestChoice = i;
                                if (choice == -1)
                                    bestScore2 += _poleRazmer * 5;
                            }

                        }
                        else
                        {
                            if ((MemScore1 - MemScore2 < bestScore1 - bestScore2))
                            {
                                bestScore1 = MemScore1;
                                bestScore2 = MemScore2;
                                bestChoice = i;
                            }
                        }
                        checkNumber.Number = Mathf.Abs(temp) - 1;
                    }
                    
                }
                else
                {
                    if (_cells[i, line] != null)
                    {
                        var checkNumber = _cells[i, line].GetComponent<cell_Pl_vs_Pc>();
                        if (Mathf.Abs(checkNumber.Number) == 99) continue;

                        temp = checkNumber.Number;
                        checkNumber.Number = 98;
                        MemScore1 = currScore1 + temp;
                        MemScore2 = currScore2;
                        if (dept < _maxDepth)
                        {
                            choice = Choice(0, i, dept);
                            //if ((memScore1 - memScore2 > bestScore1 - bestScore2 && (choice != -1 || (choice == -1 && memScore1 + CompPoints > memScore2 + PlayerPoints)) || bestScore1 == -9999))
                            if ((MemScore1 - MemScore2 > bestScore1 - bestScore2 && (choice != -1 || (choice == -1 && MemScore1 + PlayerPoints > MemScore2 + CompPoints)) || bestScore1 == -9999))
                            {
                                bestScore1 = MemScore1;
                                bestScore2 = MemScore2;
                                bestChoice = i;
                                if (choice == -1)
                                    bestScore1 += _poleRazmer * 5;
                            }

                        }
                        else
                        {
                            if ((MemScore1 - MemScore2 > bestScore1 - bestScore2))
                            {
                                bestScore1 = MemScore1;
                                bestScore2 = MemScore2;
                                bestChoice = i;
                            }
                        }
                        checkNumber.Number = Mathf.Abs(temp) - 1;
                    }
                }
            }
            if (bestScore1 != -9999)
            {
                MemScore1 = bestScore1;
                MemScore2 = bestScore2;
            }

            return bestChoice;
        }
    }
}