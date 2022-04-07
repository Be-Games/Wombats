using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [Header("Variables for Full Game")] 
    
    public int lightingMode = 1;
    public bool canControlCar;


    [Header("Stadium Scene Stuff")] 
    public int charNumber = 1;                                                    //1= MM , 2 = DH , 3 = TO 
    public int podiumPos = 1;

    public void setCharacter(int charNo)
    {
        charNumber = charNo;
    }

}
