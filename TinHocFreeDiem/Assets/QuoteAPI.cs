using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class QuoteAPI : MonoBehaviour
{
    public TextMeshProUGUI quoteText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GetQuote());
        }

    }
    IEnumerator GetQuote()
    {

        // Inspirational quotes web request unity
        string QuoteUrl = "https://api.quotable.io/random";

        UnityWebRequest QuoteRequest = UnityWebRequest.Get(QuoteUrl);
        yield return QuoteRequest.SendWebRequest();

        if (QuoteRequest.result == UnityWebRequest.Result.ConnectionError || QuoteRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(QuoteRequest.error);
            yield break;
        }

        // Get the JSON data
        var QuoteData = QuoteRequest.downloadHandler.text;

        // Get the author from the JSON
        string Author = JsonUtility.FromJson<Quote>(QuoteData).author;

        // Get the Quote from the JSON
        string Quote = JsonUtility.FromJson<Quote>(QuoteData).content;

        // Set the text
        quoteText.text = Quote + "\n" + Author;

    }



    public class Quote
    {
        public string author;
        public string content;
    }
}
