using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class GameManager1 : MonoBehaviour {

    //pour changer le menu
    public Canvas CreationComptePanel;
    public Canvas LoginPanel;
    public Canvas GamePanel;
    public Canvas GameModes;
    public GameModeManager gmd = new GameModeManager();
    public Canvas CanvasPopup;
    public Anim anim= new Anim();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
       // GamePanel.enabled = false;
        //GameModes.enabled = false;

    }

    public void PlayOn()
    {
        // to do : grant type, token , authorisation...
        //
        //LoginPanel.enabled = false;
        // Application.LoadLevel("Game");
        // GamePanel.enabled = true;
        //LoginPanel.enabled = false;
        //CreationComptePanel.enabled = false;
        Application.LoadLevel("Menu");
    }



    public void NewGameOn()
    {
        //gmd.GetGameModes();
        // GamePanel.enabled = false;
        // CreationComptePanel.enabled = false;
        // GameModes.enabled = true;
        Application.LoadLevel("Game");
    }

    public void BtnCreationCompteOn()
    {
        
        anim.loginToCreationCompte();
        

    }
    public void BtnMode1ClickOn()
    {
        gmd.GetGameMode();
    }
    public void BtnRetourPlay()
    {
        anim.UnpauseGame();
        
    }

}
