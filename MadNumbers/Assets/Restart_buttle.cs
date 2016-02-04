using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart_buttle : MonoBehaviour {
    public void OnRestart_battle()
    {
        SceneManager.LoadScene("PlayerVsPC_battle");
    }
}
