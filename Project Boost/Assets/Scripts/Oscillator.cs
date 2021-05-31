using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    [SerializeField] [Range(0, 1)] float movementFactor;
    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(cycles * tau);


        movementFactor = (rawSineWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
