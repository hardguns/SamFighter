using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    public Text text;
    public bool shouldFade;
    public float fadeSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFade)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.MoveTowards(text.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (text.color.a == 0)
            {
                text.gameObject.SetActive(false);
                shouldFade = false;
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
            }
        }
    }

    public void TextFade()
    {
        shouldFade = true;
    }
}
