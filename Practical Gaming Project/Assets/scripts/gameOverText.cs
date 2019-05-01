using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameOverText : MonoBehaviour {

    public GameObject gameOverPanel;
    public Text text;
    IEnumerator fadePanel;
    IEnumerator fadeText;
    Color colourValue;

    // Use this for initialization
    void Start () {

        gameOverPanel.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void gameOver()
    {
        gameOverPanel.SetActive(true);
        fadePanel = fadePanelOverTime(new Color(0, 0, 0, 0), new Color(0, 0, 0, 200), 400);
        StartCoroutine(fadePanel);
        text.text = "Game Over";
        fadeText = fadeTextOverTime(new Color(214, 52, 52, 0), new Color(214, 52, 52, 200), 400);
        StartCoroutine(fadeText);
    }

    public void win()
    {
        gameOverPanel.SetActive(true);
        text.text = "You Escaped!";
    }

    IEnumerator fadePanelOverTime(Color start, Color end, float duration)
    {
        Debug.Log("In fadeOverTime coroutine");

        Image img = GameObject.FindGameObjectWithTag("gameOverPanel").GetComponent<Image>();

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            img.color = Color.Lerp(start, end, normalizedTime);

            yield return null;
        }
        colourValue = end; //without this, the value will end at something like 0.9992367
    }

    IEnumerator fadeTextOverTime(Color start, Color end, float duration)
    {
        Debug.Log("In fadeOverTime coroutine");

        Text txt = GameObject.FindGameObjectWithTag("panelText").GetComponent<Text>();

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            txt.color = Color.Lerp(start, end, normalizedTime);

            yield return null;
        }
        colourValue = end; //without this, the value will end at something like 0.9992367
    }
}
