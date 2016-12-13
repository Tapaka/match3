using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;
using UnityEngine.Networking;
using System.Linq;

public class WebApiRequest : MonoBehaviour
{
    private static string token;
    private static Token _token=new Token();

    public object userState { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
/*
    private static WebApiRequest _instance = null;

    private WebApiRequest() { }

    public static WebApiRequest GetInstance()
    {
        if (_instance == null) _instance = new WebApiRequest();
        return _instance;
    }
*/
    public IEnumerator Get<T>(string url, Action<object, Exception> callback)
    {
        //WWWForm form = new WWWForm();
        Dictionary<string, string> headers = new Dictionary<string, string>();
        //headers.Add("authorization", "bearer " + token);
        Debug.Log("token " + token);
        headers.Add("authorization", "bearer " + token);
        //form.AddField("headers", headers);
        Debug.Log(url);
        WWW www = new WWW(url,new byte[] {0 }, headers);
        yield return www;
        Debug.Log(www.text);

        if (string.IsNullOrEmpty(www.error))
        {
            T result = JsonConvert.DeserializeObject<T>(www.text);
            callback(result, null);
        }
        else callback(null, new Exception(www.error));
    }

    public IEnumerator GetPlayersApi<T>(string v,Action<object, Exception> callback)
    {

        //byte[] body = Encoding.UTF8.GetBytes("");
        WWWForm form = new WWWForm();
        // Create a download object
      //  download = new WWW(requestString, form);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        WWW download = null;

        form.headers.Add("authorization", "bearer " + token);
        form.headers.Add("X-HTTP-Method-Override", "GET");
        headers.Add("Content-Type", "application/json");
        //headers.Add("Connection", "close");
        Debug.Log("token pour getplayers "+_token.access_token);
        headers.Add("authorization", "bearer " + token);
        Debug.Log(form.headers.ToString());
        download = new WWW(v, form);

        yield return download;
        /*/////////////////////////////////////////////////////////////

        Dictionary<string, string> headers = new Dictionary<string, string>();
        WWW download = null;
        form.headers.Add("authorization", "bearer " + token);
        headers.Add("Content-Type", "application/json");
        headers.Add("Connection", "close");
        headers.Add("", "Get");
        Debug.Log("token pour getplayers " + _token.access_token);
        headers.Add("authorization", "bearer " + token);
        download = new WWW(v, new byte[] { 0}, headers);
        ///////////////////////////////////////////////////////////////*/

       // yield return download;
        Debug.Log(download.text);
        if (download.responseHeaders.Count > 0)
        {

            if (download.responseHeaders["STATUS"].Contains("200"))
            {

                try
                {
                    Debug.Log(download.text);
                    object deserializedObject = JsonConvert.DeserializeObject<T>(download.text);
                            callback(deserializedObject, null);
                    

                }
                catch (Exception ex)
                {
                   
                            callback(null, ex);
                    
                }

            }
            Debug.LogError(new Exception(download.error));

        }
        else if ((!string.IsNullOrEmpty(download.error)))
        {
            Debug.Log("je suis là");

            callback(null, new Exception(download.error));
            

        }
        else
        {
            Debug.Log("je suis là");
            try
            {

                object deserializedObject = JsonConvert.DeserializeObject<T>(download.text);
                
                        callback(deserializedObject, null);
                

            }
            catch (Exception ex)
            {
                
                        callback(null, ex);
                
            }


        }

    }

    public IEnumerator Get<T>(string url, int id, Action<object, Exception> callback)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("authorization", (" bearer " + token));
        //byte[] data = Encoding.UTF8.GetBytes("a");
        Debug.Log("token de getplayers " + token);

        StartCoroutine(Get<T>(url + "?id=12", callback));
        yield return null;
    }
  
    // Creation compte
    public IEnumerator Post<T>(string url, object obj, Action<object, Exception> callback)
    {
        if (url == "http://localhost:53985/api/Account/Register")
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/json");
            headers.Add("X-HTTP-Method-Override", "POST");
            string json = JsonConvert.SerializeObject(obj);
            byte[] data = Encoding.UTF8.GetBytes(json);
            WWW www = new WWW(url, data, headers);
            yield return www;

            

            if (string.IsNullOrEmpty(www.error))
            {
                //Created
                if (www.responseHeaders["STATUS"].Contains("201"))
                {
                    callback(true, null);
                }

                //Bad request
                else if (www.responseHeaders["STATUS"].Contains("400"))
                {
                    callback(false, new Exception("Bad Request"));
                }

                else
                {
                    T result = JsonConvert.DeserializeObject<T>(www.text);
                    callback(result, null);
                }
            }
            else
            {
                callback(null, new Exception(www.error));
            }
        }
       
    }

    // Authentification et récupération token
    public IEnumerator GetToken<T>(string url, string chaine, Action<object, Exception> callback)
    {
        
        //string json = JsonConvert.SerializeObject(obj);
        byte[] data = Encoding.UTF8.GetBytes(chaine);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        WWW download = null;

        headers.Add("Content-Type", "x-www-form-urlencoded");
        headers.Add("X-HTTP-Method-Override", "POST");
        // Dictionary<string, string> headers = new Dictionary<string, string>();

        //headers.Add("Connection", "close");
       // headers.Add("X-HTTP-Method-Override", RESTMethod.POST.ToString());
        download = new WWW((url), data, headers);

        yield return download;

        //WWW www = new WWW(url, data, headers);
        
        //yield return www;

        //string resultat = "";

        if (string.IsNullOrEmpty(download.error))
        {
            //resultat = JsonConvert.SerializeObject(download.text);
            //token = resultat.Substring(21, (resultat.IndexOf(",") - 23));
            //Debug.Log(resultat);

            if (download.responseHeaders["STATUS"].Contains("200"))
            {
                try
                {

                    _token = JsonConvert.DeserializeObject<Token>(download.text);
                    Debug.Log("token " + _token.access_token);
                    token = _token.access_token;

                    callback(true, null);
                }
                catch (Exception ex)
                {
                    Debug.Log("error token " + ex.Message);
                }
            }

            //Bad request
            else if (download.responseHeaders["STATUS"].Contains("400"))
                callback(false, new Exception("Bad Request"));
            else
            {
                T result = JsonConvert.DeserializeObject<T>(download.text);
                callback(result, null);
            }
        }
        else
        {
            callback(null, new Exception(download.error));
        }
        
    }
   

    public IEnumerator PostMethod<T>(string controler, string method, object obj, System.Action<object, System.Exception, object>[] callback)
    {
        string json = JsonConvert.SerializeObject(obj);

        byte[] body = Encoding.UTF8.GetBytes(json);

        Dictionary<string, string> headers = new Dictionary<string, string>();
        WWW download = null;

        headers.Add("Content-Type", "application/json");
        headers.Add("Connection", "close");
        headers.Add("X-HTTP-Method-Override", "POST");
        headers.Add("authorization", "bearer " + _token.access_token);
        download = new WWW(("http://localhost:53985" + "/" + controler + "/" + method), body, headers);

        yield return download;

        if (download.responseHeaders.Count > 0)
        {

            if (download.responseHeaders["STATUS"].Contains("200"))
            {
                try
                {

                    object deserializedObject = JsonConvert.DeserializeObject<T>(download.text);
                    for (int i = 0; i < callback.Length; i++)
                    {
                        if (callback[i] != null)
                            callback[i](deserializedObject, null, userState);
                    }

                }
                catch (Exception ex)
                {
                    for (int i = 0; i < callback.Length; i++)
                    {
                        if (callback[i] != null)
                            callback[i](null, ex, userState);
                    }
                }

            }
        }
        else if ((!string.IsNullOrEmpty(download.error)))
        {
            for (int i = 0; i < callback.Length; i++)
            {
                if (callback[i] != null)
                    callback[i](null, new Exception(download.error), userState);
            }

        }
        else
        {
            try
            {

                object deserializedObject = JsonConvert.DeserializeObject<T>(download.text);
                for (int i = 0; i < callback.Length; i++)
                {
                    if (callback[i] != null)
                        callback[i](deserializedObject, null, userState);
                }

            }
            catch (Exception ex)
            {
                for (int i = 0; i < callback.Length; i++)
                {
                    if (callback[i] != null)
                        callback[i](null, ex, userState);
                }
            }


        }

    }




}







