using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.Collections.Generic;
public class PlayerCreator : MonoBehaviour
{
    public new InputField name;
    public InputField motdepass;
    public InputField confirmPassword;
    public WebApiRequest webRequest;
    public Text messageField;
    public Popup pp = new Popup();
    public GameManager1 gm = new GameManager1();
    public PlayerManager pm = new PlayerManager();

    public void Create()
    {
        if ((!string.IsNullOrEmpty(name.text)) && (!string.IsNullOrEmpty(motdepass.text)) && (!string.IsNullOrEmpty(confirmPassword.text)))
        {
            StartCoroutine(webRequest.Post<Player>("http://localhost:53985/api/Account/Register", new Player()
            { Email = name.text, Password = motdepass.text, ConfirmPassword = confirmPassword.text, name = name.text }, Callback));
        }
        else if ((string.IsNullOrEmpty(name.text)) && (!string.IsNullOrEmpty(motdepass.text)) && (!string.IsNullOrEmpty(confirmPassword.text)))
        {
            pp.Show("Vueillez bien remplir votre login");
        }
        else if (!(string.IsNullOrEmpty(name.text)) && (string.IsNullOrEmpty(motdepass.text)) && (!string.IsNullOrEmpty(confirmPassword.text)))
        {
            pp.Show("Vueillez bien remplir votre mot de passe");
        }
        else if (!(string.IsNullOrEmpty(name.text)) && (!string.IsNullOrEmpty(motdepass.text)) && (string.IsNullOrEmpty(confirmPassword.text)))
            pp.Show("Vueillez bien confirmer votre mot de passe");
        else
            pp.Show("Tous les champs sont obligatoires, veuillez bien les remplir");
        
    }

    // Player result à remettre object result
    private void Callback(object result, Exception error)
    {
        // provisoire : erreur nullreferenceException
        result = new Player();
        if (error == null)
        {
            if (result is Player)
            {
                Player player = result as Player;
                if (result != null)
                {
                    Debug.Log("Player created Id : " + player.id + " name : " + player.name);
                    pp.Show("Félicitation " + name.text + " !!! Vous avez créé votre compte avec succée...");
                }
                else
                {
                    Debug.Log("Player is null");
                    pp.Show("Erreur de saisi");
                }
            }
            else
            {
                Debug.Log("Bad format");
                pp.Show("Erreur de saisi");
                Debug.Log(error.Message);
            }
        }
        else
        {
            Debug.Log(error.Message);
            if (error.Message.Contains("400"))
                pp.Show("Mot de passe incorrecte ou username exsite déjà");
            if (error.Message.Contains("Connection refused"))
                pp.Show("Connexion refusée : serveur introuvable");
        }
    }


    public void Login()
    {
        if ((!string.IsNullOrEmpty(name.text)) && (!string.IsNullOrEmpty(motdepass.text)))
        {
            StartCoroutine(webRequest.GetToken<Player>  ("http://localhost:53985/token", "grant_type=password&username=" + name.text + "&password=" + motdepass.text, LoginCallback));
            //gm.PlayOn();

        }
        else if ((string.IsNullOrEmpty(name.text)) && (!string.IsNullOrEmpty(motdepass.text)))
        {
            pp.Show("Vueillez bien remplir votre login");           
        }
        else if (!(string.IsNullOrEmpty(name.text)) && (string.IsNullOrEmpty(motdepass.text)))
        {
            pp.Show("Vueillez bien remplir votre mot de passe");
        }
        else if ((string.IsNullOrEmpty(name.text)) && (string.IsNullOrEmpty(motdepass.text)))
            pp.Show("Vueillez bien remplir votre login et votre mot de passe");

    }
   
    private void LoginCallback(object result, Exception error)
    {

        if (error == null)
        {
            Debug.Log("error est null" + error);
            
            if (result != null)
            {
                Debug.Log("result n'est pas null " + result);
                gm.PlayOn();
                
            }
            else
            {
                pp.Show("Erreur de saisi");
                Debug.Log("result est null" + result);
            }
        }
        else
        {
        Debug.Log(error.Message);
        if (error.Message.Contains("400"))
            pp.Show("login ou mot de passe incorrecte");
        if (error.Message.Contains("Connection refused"))
            pp.Show("Connexion refusée : serveur introuvable");
        if(error.Message.Contains("502"))
            pp.Show("Connexion refusée : serveur introuvable");
        }
    }

}


