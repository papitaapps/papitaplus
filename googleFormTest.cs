using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class googleFormTest : MonoBehaviour
{
    // Start is called before the first frame update
    string m_t = "sadegh";
    string url = "https://docs.google.com/forms/d/e/1FAIpQLSdHsFTI4JuJ-Esx6oROcZbBwnsNwsjE0_b7jh-s7BzMsrmJ9A/formResponse";
    void Start()
    {
        
    }
    public void Bt()
    {
        StartCoroutine(Posting());
    }

    IEnumerator Posting()
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1323480639", m_t);
        using (var www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            Debug.Log(www.downloadHandler.text);
        }
            
    }
}
