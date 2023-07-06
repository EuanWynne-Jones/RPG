using System.Collections;
using UnityEngine;

public class Thunder: MonoBehaviour
{
    public Light directionalLight;   // Reference to the directional light in the scene
    public AudioClip[] thunderSounds;   // Array of thunder sound effects

    public float minIntensity = 0.5f;   // Minimum intensity of the light
    public float maxIntensity = 3f;   // Maximum intensity of the light
    public float intensityIncreaseSpeed = 5f;   // Speed at which the light intensity increases
    public float intensityDecreaseSpeed = 2f;   // Speed at which the light intensity decreases
    public float minFlickerIntensity = 0.2f;   // Minimum intensity for flickering effect
    public float maxFlickerIntensity = 0.5f;   // Maximum intensity for flickering effect
    public float flickerSpeed = 10f;   // Speed of the flickering effect
    public float minWaitTime = 1f;   // Minimum time to wait between lightning strikes
    public float maxWaitTime = 5f;   // Maximum time to wait between lightning strikes
    public int simultaneousStrikes = 3;   // Number of simultaneous lightning strikes

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

                // Wait for random time between lightning strikes
                yield return new WaitForSeconds(waitTime);

                // Start lightning flash
                isFlashing = true;
                targetIntensity = Random.Range(minIntensity, maxIntensity);
                PlayThunderSound();

                // Flicker effect
                while (currentIntensity < targetIntensity)
                {
                    currentIntensity += intensityIncreaseSpeed * Time.deltaTime;
                    directionalLight.intensity = currentIntensity;

                    // Apply flickering effect
                    float flickerIntensity = Mathf.Lerp(maxFlickerIntensity, minFlickerIntensity, Mathf.PingPong(Time.time * flickerSpeed, 1f));
                    directionalLight.intensity += flickerIntensity;

                    yield return null;
                }

                // Wait for a short duration to simulate lightning flash
                yield return new WaitForSeconds(0.2f);

                // Decrease light intensity gradually
                while (currentIntensity > minIntensity)
                {
                    currentIntensity -= intensityDecreaseSpeed * Time.deltaTime;
                    directionalLight.intensity = currentIntensity;
                    yield return null;
                }

                // End lightning flash
                isFlashing = false;
            }
        }
    }

    private void Update()
    {
        // You can add additional logic to control the intensity of the light based on game conditions here
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
