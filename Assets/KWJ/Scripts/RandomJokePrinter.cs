using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomJokePrinter : MonoBehaviour
{
    public TextMeshProUGUI setup;
    public TextMeshProUGUI punchline;
    public TextMeshProUGUI effect;

    private float currentTime = 0;
    private bool isShown = false;

    public static RandomJokePrinter rjp;

    void Awake()
    {
        if (rjp != null && rjp != this)
        {
            Destroy(this);
        }
        else
        {
            rjp = GetComponent<RandomJokePrinter>();
        }
    }

    void Start()
    {
        PrinterOff();
    }
    // Update is called once per frame
    void Update()
    {
        if(isShown)
        {
            currentTime += Time.deltaTime;
            if(currentTime > 5f)
            {
                PrinterOff();
            }
        }
    }

    public void SetText(int type, string contents)
    {
        switch(type)
        {
            case 0:
                setup.text = contents;
                break;
            case 1:
                punchline.text = contents;
                break;
            case 2:
                effect.text = contents;
                break;
            default:
                break;
        }
    }

    public void PrinterOn()
    {
        this.gameObject.SetActive(true);
        currentTime = 0;
        isShown = true;
    }

    private void PrinterOff()
    {
        setup.text = "";
        punchline.text = "";
        effect.text = "";
        currentTime = 0;
        isShown = false;
        this.gameObject.SetActive(false);
    }
}
