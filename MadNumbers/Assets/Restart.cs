using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {


    public void OnRestart_PlayerVsPC()
    {
        SceneManager.LoadScene("PlayerVsPC");
    }

}
