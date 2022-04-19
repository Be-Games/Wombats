using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IniLoad : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().isLoaded)
        {
            SceneManager.LoadScene("HomeScreen");
        }
    }
}
