using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class PlayfabAuthentication : MonoBehaviour
{
    // Start is called before the first frame update

    public string TitleId;
    public TextMeshProUGUI debugText;
    public GameObject loginUI, player1UI, player2UI;
    void Start()
    {
        if (FB.IsInitialized)
            return;
        //FB.Init(() =>FB.ActivateApp());
        FB.Init (CheckFbLogin);
      
    }

    void CheckFbLogin()
    {
        if (FB.IsLoggedIn)
        {
            debugText.text = "Already Logged In";
            loginUI.SetActive(false);
            player1UI.SetActive(true);
            FB.GetAppLink (PlayerInput.Instance.ApplinkCallback);
        }
        
        else 
        {
            debugText.text = "Not Logged In";
        }
    }

    public void LoginWithFB()
    {
        FB.LogInWithReadPermissions(new List<string>{"public_profile","email"}, Res =>
        {
            LoginWithPlayFab();
        });
        
    }

    public void LoginWithPlayFab()
    {
        PlayFabClientAPI.LoginWithFacebook(new PlayFab.ClientModels.LoginWithFacebookRequest
        {
            TitleId=TitleId,
            AccessToken = AccessToken.CurrentAccessToken.TokenString,
            CreateAccount = true
        }, PlayfabLoginSuccess,PlayfabLoginFailed);
    }

    public void PlayfabLoginSuccess(PlayFab.ClientModels.LoginResult result)
    {

        debugText.text = "Login Successful";
        loginUI.SetActive(false);
        player1UI.SetActive(true);

    }
    
    public void PlayfabLoginFailed(PlayFabError error)
    {
        debugText.text = "Login Failed";;
    }

}
