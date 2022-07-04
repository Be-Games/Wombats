using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public CrossWordManager CrossWordManager;
    
    public TextMeshProUGUI player1Name, player2Name;

    [Header("PlayerAInput")] 
    public TMP_InputField[] hintsFields;
    public TMP_InputField answerField;

    [Space(8)] 
    public string[] acceptedWords;

    [Space] public TextMeshProUGUI debugText;

    public GameObject player2Turn, player1Turn;
    private static PlayerInput instance;
    public static PlayerInput Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    public void EnterBtn()
    {
        for (int i = 0; i < 3; i++)
        {
            if (hintsFields[i].text == String.Empty || answerField.text == String.Empty)
            {
                debugText.text = "Make Sure all Field are Filled";
                return;
            }
            else
            {
                debugText.text = "";

                for (int j = 0; j < 4; j++)
                {
                    acceptedWords[i] = hintsFields[i].text;
                    
                    if (j == 3)
                        acceptedWords[j] = answerField.text;
                }

                SaveData();

                //CrossWordManager.Challenge();
                /*player2Turn.SetActive(true);
                player1Turn.SetActive(false);*/
            }
                
        }
    }

    void SaveData()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Hint1", acceptedWords[0].ToUpper()},
                {"Hint2", acceptedWords[1].ToUpper()},
                {"Hint3", acceptedWords[2].ToUpper()},
                {"Answer", acceptedWords[3].ToUpper()}
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    void OnDataSend(UpdateUserDataResult userDataResult)
    {
        Challenge();
        debugText.text = "SuccessFull User Data Send";
    }
    
    void OnError(PlayFabError error)
    {
        debugText.text = "Error";
    }
    
    
    
    public void Challenge(){
        FB.AppRequest ("Custom message", null, new List<object>{ "app_users" }, null, null, "Whats Up", "Challenge your friends!", ChallengeCallback);
    }

    void ChallengeCallback(IAppRequestResult result){
        if (result.Cancelled) {
            debugText.text =   "Challenge cancelled.";		
        } else if (!string.IsNullOrEmpty (result.Error)) {
            debugText.text ="Error in challenge:" + result.Error;
        } else {
            debugText.text ="Challenge was successful:" + result.RawResult;
        }
    }
	

    public void ApplinkCallback(IAppLinkResult result){
        if (string.IsNullOrEmpty (result.Error)) {
			
            debugText.text ="Applink done:" + result.RawResult; 
			
            IDictionary<string, object> dictio = result.ResultDictionary;
            Debug.Log(dictio);
			
            if (dictio.ContainsKey ("target_url"))
            {
                debugText.text = "Welcome";
				
                string url = dictio ["target_url"].ToString ();
                string keyword = "request_ids=";
                int k = 0;
                while (k < url.Length - keyword.Length && !url.Substring (k, keyword.Length).Equals (keyword))
                    k++;
                k += keyword.Length;
                int l = k;
                while (url [l] != '&' && url [l] != '%')
                    l++;
                string id = url.Substring (k, l - k);
                FB.API ("/" + id + "_" + AccessToken.CurrentAccessToken.UserId, HttpMethod.GET, RequestCallback);
            }
        } else {
            debugText.text =  "Applink error:" + result.Error;
        }
    }
    
    void RequestCallback(IGraphResult result){
        debugText.text =  "Request callback";
        if (string.IsNullOrEmpty (result.Error)) {
            IDictionary<string, object> dictio = result.ResultDictionary;
            if (dictio.ContainsKey ("data"))
                debugText.text = "" + dictio ["data"];
            if (dictio.ContainsKey ("id"))
                FB.API ("/" + dictio ["id"], HttpMethod.DELETE, null);
        } else {
            Debug.Log ("Error in request:" + result.Error);
        }
    }

}
