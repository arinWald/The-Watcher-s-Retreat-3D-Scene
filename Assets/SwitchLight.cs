using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLight : MonoBehaviour
{
    public Light lightObject;
    public Renderer emissiveObject;
    public float emissiveIntensityOn = 1f;
    public float emissiveIntensityOff = 0f;
    public AudioClip audioClip;  // Reference to the audio clip
    private AudioSource audioSource;  // Reference to the AudioSource component

    private bool isLightEnabled = true;

    private void Start()
    {
        // Get the AudioSource component attached to the same game object
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip; // Assign the audio clip to the AudioSource
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleLight();
            ToggleEmissiveIntensity();
            PlayAudioClip();  // Call the function to play the audio clip
        }
    }

    private void ToggleLight()
    {
        isLightEnabled = !isLightEnabled;
        lightObject.enabled = isLightEnabled;
    }

    private void ToggleEmissiveIntensity()
    {
        Material emissiveMaterial = emissiveObject.material;
        bool isEmissionEnabled = emissiveMaterial.IsKeywordEnabled("_EMISSION");

        if (isLightEnabled)
        {
            if (!isEmissionEnabled)
                emissiveMaterial.EnableKeyword("_EMISSION");

            emissiveMaterial.SetFloat("_EmissionIntensity", emissiveIntensityOn);
        }
        else
        {
            if (isEmissionEnabled)
                emissiveMaterial.DisableKeyword("_EMISSION");

            emissiveMaterial.SetFloat("_EmissionIntensity", emissiveIntensityOff);
        }
    }

    private void PlayAudioClip()
    {
        audioSource.PlayOneShot(audioClip);  // Play the assigned audio clip
    }
}