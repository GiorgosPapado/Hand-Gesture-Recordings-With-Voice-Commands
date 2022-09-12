using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LightEffects : MonoBehaviour
{
    public Light lightToFade;
    private float eachFadeTime = 5f;
    private float fadeWaitTime = 1f;
    public DataPersistenceManager manager;
    private FrameData data = new();
    public VoiceRecord record;


    private void OnEnable()
    {
        StartCoroutine(FadeInAndOutRepeat(lightToFade, eachFadeTime, fadeWaitTime));
    }

    private void OnDisable()
    {
        StopCoroutine(FadeInAndOutRepeat(lightToFade, eachFadeTime, fadeWaitTime));
    }

    IEnumerator FadeInAndOut(Light lightToFade, bool fadeIn, float duration)
    {
        float minLuminosity = 0; // min intensity
        float maxLuminosity = 10; // max intensity

        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;

        if (fadeIn)
        {
            a = minLuminosity;
            b = maxLuminosity;
        }
        else
        {
            a = maxLuminosity;
            b = minLuminosity;
        }

        //float currentIntensity = lightToFade.intensity;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);
            if (lightToFade.intensity == 0)
            {
                if (PlayerStats.Rounds > 0)
                    PlayerStats.Rounds -= 1;
                record.Stop();
            }

            yield return null;
        }
    }
  
  //Fade in and out forever
  IEnumerator FadeInAndOutRepeat(Light lightToFade, float duration, float waitTime)
    {
        WaitForSeconds waitForXSec = new WaitForSeconds(waitTime);

        while (true)
        {
            //Fade-in 
            yield return FadeInAndOut(lightToFade, true, duration);

            //Fade out
            yield return FadeInAndOut(lightToFade, false, duration);

            //Wait
            yield return waitForXSec;
            yield return null; ;
        }
    }
}
