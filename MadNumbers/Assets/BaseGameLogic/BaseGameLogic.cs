using System.Collections.Generic;
using Assets.BaseGameLogic;
using UnityEngine;

public class BaseGameLogic
{
    private int _maxDepth;
    private int _poleRazmer;

    public int MemScore1 { get; set; }
    public int MemScore2 { get; set; }

    public BaseGameLogic(int maxDepth, int poleRazmer)
    {
        _maxDepth = maxDepth;
        _poleRazmer = poleRazmer;
    }

    public virtual CellParam ChoiceCell(Steps step, int line, int dept, List<List<CellParam>> cellArray, int playerPoints, int compPoints)
    {
        dept++;
        var bestScore1 = -9999;
        var bestScore2 = -9999;
        var bestChoice = new CellParam() { Id = -1 };
        var currScore1 = MemScore1;
        var currScore2 = MemScore2;
        int temp;

        for (int i = 0; i < _poleRazmer; i++)
        {
            if (step == Steps.player)
            {
                if (!cellArray[line][i].Touch)
                {
                    var checkNumber = cellArray[line][i].Number;
                    if (Mathf.Abs(checkNumber) == 99) continue;

                    temp = checkNumber;
                    checkNumber = 99;
                    MemScore1 = currScore1;
                    MemScore2 = currScore2 + temp;
                    if (dept < _maxDepth)
                    {
                        var choice = ChoiceCell(Steps.ai, i, dept, cellArray, playerPoints, compPoints);
                        if ((MemScore1 - MemScore2 < bestScore1 - bestScore2 && (choice.Id != -1 || (choice.Id == -1 && MemScore1 + playerPoints < MemScore2 + compPoints)) || bestScore1 == -9999))
                        {
                            bestScore1 = MemScore1;
                            bestScore2 = MemScore2;
                            bestChoice = cellArray[line][i];
                            if (choice.Id == -1)
                                bestScore2 += _poleRazmer * 5;
                        }
                    }
                    else
                    {
                        if ((MemScore1 - MemScore2 < bestScore1 - bestScore2))
                        {
                            bestScore1 = MemScore1;
                            bestScore2 = MemScore2;
                            bestChoice = cellArray[line][i];
                        }
                    }
                    checkNumber = temp;
                }
            }
            else
            {
                if (!cellArray[i][line].Touch)
                {
                    var checkNumber = cellArray[i][line].Number;
                    if (Mathf.Abs(checkNumber) == 99) continue;

                    temp = checkNumber;
                    checkNumber = 99;
                    MemScore1 = currScore1 + temp;
                    MemScore2 = currScore2;
                    if (dept < _maxDepth)
                    {
                        var choice = ChoiceCell(Steps.player, i, dept, cellArray, playerPoints, compPoints);
                        if ((MemScore1 - MemScore2 > bestScore1 - bestScore2 && (choice.Id != -1 || (choice.Id == -1 && MemScore1 + playerPoints > MemScore2 + compPoints)) || bestScore1 == -9999))
                        {
                            bestScore1 = MemScore1;
                            bestScore2 = MemScore2;
                            bestChoice = cellArray[i][line];
                            if (choice.Id == -1)
                                bestScore1 += _poleRazmer * 5;
                        }
                    }
                    else
                    {
                        if ((MemScore1 - MemScore2 > bestScore1 - bestScore2))
                        {
                            bestScore1 = MemScore1;
                            bestScore2 = MemScore2;
                            bestChoice = cellArray[i][line];
                        }
                    }
                    checkNumber = temp;
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

