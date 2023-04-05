using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequest : MonoBehaviour
{
    [SerializeField] private string _url;
    [SerializeField] private TextMeshProUGUI _dynamicTimeText;
    private string serverTime;

    private void Awake()
    {
        StartCoroutine(SendRequest());
    }
    
    private IEnumerator SendRequest()
    {
        while (true)
        {
            UnityWebRequest request = UnityWebRequest.Get(_url);
            yield return request.SendWebRequest();
    
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log( request.error);
                yield return request.SendWebRequest();
            }

            else if (request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
                yield return request.SendWebRequest();
            }

            else
            {
                string json = request.downloadHandler.text;
                ServerTimeRespone respone = JsonUtility.FromJson<ServerTimeRespone>(json);
    
                serverTime = respone.datetime;
                _dynamicTimeText.text = serverTime.ToString();
            }
            
            yield return new WaitForSeconds(1);
        }
    }
}

