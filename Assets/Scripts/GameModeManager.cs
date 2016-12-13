using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class GameModeManager : MonoBehaviour {

    public GameMode currentGameMode;
    public WebApiRequest webApi;

    public void GetGameModes()
    {
        StartCoroutine(webApi.Get<GameMode[]>("http://localhost:53985/Api/GameModesApi", GetModecallback));
       
    }

    private void GetModecallback(object result, Exception error)
    {
        if (error == null)
        {
            if (result is GameMode[])
            {
                if (result != null)
                {
                    GameMode[] gameMode = result as GameMode[];
                    for (int i = 0; i < gameMode.Length; i++)
                    {
                        Debug.Log(gameMode[i].name);
                    }
                       
                    
                }
                else
                    Debug.LogError("Result is null");
            }
            else
                Debug.LogError("Bad format");
        }
        else
            Debug.LogError(error.Message);
    }

    public void GetGameMode()
    {
     int gameModeId=1;
        Debug.Log("cc");
        StartCoroutine( webApi.Get<GameMode>("http://localhost:53985/api/GameModesApi", gameModeId, GetGameModeCallback));
        
    }

    private void GetGameModeCallback(object result, Exception error)
    {
        if (error == null)
        {
            if (result is GameMode)
            {
                if (result != null)
                {
                    GameMode gameMode = result as GameMode;
                    currentGameMode = gameMode;
                    Debug.Log(gameMode.name);

                }
                else
                    Debug.LogError("Result is null");
            }
            else
                Debug.LogError("Bad format");
        }
        else
            Debug.LogError(error.Message);
    }
}
