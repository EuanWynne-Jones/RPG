using System.Collections;
using UnityEngine;

public class Thunder: MonoBehaviour
{
    public Light directionalLight;   
    public AudioClip[] thunderSounds;  

    public float minIntensity = 0.5f;  
    public float maxIntensity = 3f; 
    public float intensityIncreaseSpeed = 5f;  
    public float intensityDecreaseSpeed = 2f;   
    public float minFlickerIntensity = 0.2f;  
    public float maxFlickerIntensity = 0.5f; 
    public float flickerSpeed = 10f;   
    public float minWaitTime = 1f;  
    public float maxWaitTime = 5f;  
    public int simultaneousStrikes = 3;  

    private AudioSource audioSource;
    private bool isFlashing = false;
    private float targetIntensity;
    private float currentIntensity;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(FlashLightning());
    }

    private IEnumerator FlashLightning()
    {
        while (true)
        {
            for (int i = 0; i < simultaneousStrikes; i++)
            {
                float waitTime = Random.Range(minWaitTime, maxWaitTime);

                yield return new WaitForSeconds(waitTime);

                isFlashing = true;
                targetIntensity = Random.Range(minIntensity, maxIntensity);
                PlayThunderSound();

                while (currentIntensity < targetIntensity)
                {
                    currentIntensity += intensityIncreaseSpeed * Time.deltaTime;
                    directionalLight.intensity = currentIntensity;

                    float flickerIntensity = Mathf.Lerp(maxFlickerIntensity, minFlickerIntensity, Mathf.PingPong(Time.time * flickerSpeed, 1f));
                    directionalLight.intensity += flickerIntensity;

                    yield return null;
                }

                yield return new WaitForSeconds(0.2f);

                while (currentIntensity > minIntensity)
                {
                    currentIntensity -= intensityDecreaseSpeed * Time.deltaTime;
                    directionalLight.intensity = currentIntensity;
                    yield return null;
                }

                isFlashing = false;
            }
        }
    }


    private void PlayThunderSound()
    {
        if (audioSource != null && thunderSounds != null && thunderSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, thunderSounds.Length);
            AudioClip randomThunderSound = thunderSounds[randomIndex];
            audioSource.PlayOneShot(randomThunderSound);
        }
    }
}
