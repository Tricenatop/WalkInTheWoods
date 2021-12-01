using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Watch : MonoBehaviour
{
    public int currentHour = 11;
    public int currentMin = 55;
    public TextMeshPro timeText;


    private void Start()
    {
        timeText.text = currentHour + ":" + currentMin;
        StartCoroutine(min());
    }

    IEnumerator min()
    {
        yield return new WaitForSeconds(60);
        currentMin++;
        if (currentMin > 59)
        {
            currentMin = 0;
            currentHour++;
            if (currentHour > 12)
            {
                currentHour = 1;
            }
        }

        if(currentMin < 10)
        {
            timeText.text = currentHour + ":0" + currentMin;
        }
        else
        {
            timeText.text = currentHour + ":" + currentMin;
        }


        StartCoroutine(min());
    }
}
