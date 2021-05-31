using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 5f;
    [SerializeField] float rotationThrust = 2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    AudioSource audioSource;
    Rigidbody rb;
    bool isAlive;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() 
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            leftThruster();
            ApplyRotation(rotationThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rightThruster();
            ApplyRotation(-rotationThrust);
        }
    }

    void rightThruster()
    {
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
        else
        {
            rightThrusterParticles.Stop();
        }
    }

    void leftThruster()
    {
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
        else
        {
            leftThrusterParticles.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
