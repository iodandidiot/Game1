using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	
    public void OnRestart()
    {
        Application.LoadLevel("PlayerVsPC");
    }
}
