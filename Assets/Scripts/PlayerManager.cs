using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{

    public Player currentPlayer;
    public WebApiRequest webRequest;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    
    public void Save(Player player)
    {
        currentPlayer = player;
        PlayerPrefs.SetInt("playerId", player.id);
        PlayerPrefs.Save();
    }

    public void GetPlayer()
    {
        int playerId = 0;
       
        if (PlayerPrefs.HasKey("playerId"))
        {
            playerId = PlayerPrefs.GetInt("playerId");
            StartCoroutine(
                webRequest.Get<Player>("http://localhost:53985/token",
                playerId, GetPlayerCallback));
        }
    }
    
    public void GetPlayers()
    {
        StartCoroutine(webRequest.Get<Player[]>("http://localhost:53985/api/players", GetPlayersCallback));
    }
  
    private void GetPlayersCallback(object result, Exception error)
    {
        if (error == null)
        {
            if (result is Player[])
            {
                if (result != null)
                {
                    Player[] player = result as Player[];
                    for (int i = 0; i < player.Length; i++)
                    {
                        Debug.Log(player[i].name);
                        Debug.Log(error.ToString());
                    }
                }
                else

                    Debug.LogError("Result is null");
                 }
                else if (result is Player)
                {
                    Player player = result as Player;
                    Debug.Log(player.name);
                Debug.Log(error.ToString());

            }

            else
                Debug.LogError("Bad format");
        }
        else
            Debug.LogError(error.Message);
    }


    private void GetPlayerCallback(object result, Exception error)
    {
        if (error == null)
        {
            if (result is Player)
            {
                if (result != null)
                {
                    Player player = result as Player;
                    currentPlayer = player;
                    Debug.Log(player.name);
                    
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


