using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBurbuja : MonoBehaviour
{
    public GameObject burbujaPrefab;          // El modelo 3D de la burbuja
    public Transform spawnPoint;              // Punto de origen de las burbujas
    public ParticleSystem particleSystem;     // El sistema de partículas para las burbujas
    public AudioClip shootSound;              // El sonido del disparo
    private AudioSource audioSource;          // Fuente de audio para reproducir el sonido

    private bool isDisparando = false;        // Flag para saber si se está disparando
    private bool audioPlayed = false;         // Flag para controlar el audio

    // Start is called before the first frame update
    void Start()
    {
        // Desactiva el sistema de partículas al inicio para que no empiece automáticamente
        if (particleSystem != null)
        {
            particleSystem.Stop();
        }

        // Obtén el AudioSource del objeto o crea uno si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Detecta si se mantiene presionado el clic izquierdo
        if (Input.GetMouseButton(0))  // 0 es el botón izquierdo del ratón
        {
            if (!isDisparando)
            {
                // Comienza a disparar
                StartCoroutine(DispararBurbuja());
            }

            // Activa el sistema de partículas y reproduce el audio una sola vez
            if (particleSystem != null && !particleSystem.isPlaying)
            {
                particleSystem.Play(); // Activa el sistema de partículas
            }

            if (!audioPlayed && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound); // Reproduce el sonido una sola vez
                audioPlayed = true; // Marca que el audio ya se reprodujo
            }
        }
        else
        {
            // Si el clic es liberado, detiene el disparo y las partículas
            isDisparando = false;
            audioPlayed = false; // Resetea el flag para permitir reproducir el audio nuevamente
            if (particleSystem != null && particleSystem.isPlaying)
            {
                particleSystem.Stop(); // Detiene el sistema de partículas
            }
        }
    }

    // Coroutine para disparar las burbujas con un intervalo
    private IEnumerator DispararBurbuja()
    {
        isDisparando = true;

        // Instancia la burbuja en el punto de origen (spawnPoint)
        Instantiate(burbujaPrefab, spawnPoint.position, spawnPoint.rotation);

        // Espera el intervalo antes de disparar nuevamente
        yield return new WaitForSeconds(0.1f);

        isDisparando = false;
    }
}
