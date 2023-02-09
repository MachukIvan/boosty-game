using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    Vector3 startingPosition;


    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period; // Continually growing over time
        const float tau = Mathf.PI * 2; // Constant value of 6.283
        float rawSineWave = Mathf.Sin(cycles * tau); // Changes from -1 to 1 and backwards
        float movementFactor = (rawSineWave + 1f) / 2f; // To stay in range from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
