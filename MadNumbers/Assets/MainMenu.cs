using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void OnClassic()
    {
        SceneManager.LoadScene("PlayerVsPC");
    }

    public void OnBattle()
    {
        SceneManager.LoadScene("PlayerVsPC_battle");
    }

    public void OnEndless()
    {
        SceneManager.LoadScene("PlayerVsPC_endless");
    }

    public void OnPuzzle()
    {
        SceneManager.LoadScene("Puzzle");
    }

    public void OnMM()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
