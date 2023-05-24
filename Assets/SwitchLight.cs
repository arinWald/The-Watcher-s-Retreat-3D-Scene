using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLight : MonoBehaviour
{
    public Light lightObject;
    public Renderer emissiveObject;
    public GameObject raycastObject;
    public float emissiveIntensityOn = 1f;
    public float emissiveIntensityOff = 0f;
    public float raycastDistance = 100f; // Adjust the raycast distance here
    public AudioClip audioClip;
    private AudioSource audioSource;
    private bool isLightEnabled = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 1f);

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.collider.gameObject == raycastObject)
                {
                    Debug.Log("Hit object: " + hit.collider.gameObject.name);
                    ToggleLight();
                    ToggleEmissiveIntensity();
                    PlayAudioClip();
                }
            }
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
        audioSource.PlayOneShot(audioClip);
    }
}