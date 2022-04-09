using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConcertManagement : MonoBehaviour
{
    public void backtoHome()
    {
        SceneManager.LoadScene("StartUpScreen");
    }
}
