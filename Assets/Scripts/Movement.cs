using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrust = 1000f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] AudioClip mainEngineAudio;

    [SerializeField] ParticleSystem leftThruster1Particles;
    [SerializeField] ParticleSystem leftThruster2Particles;
    [SerializeField] ParticleSystem rightThruster1Particles;
    [SerializeField] ParticleSystem rightThruster2Particles;
    [SerializeField] ParticleSystem mainThrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;

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
        if (Input.GetKey(KeyCode.Space)) StartThrusting();
        else StopThrusting();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) RotateLeft();
        else if (Input.GetKey(KeyCode.RightArrow)) RotateRight();
        else StopRotation();
    }

    void Rotate(float rotationWithFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationWithFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void RotateRight()
    {
        Rotate(-rotateSpeed);

        if (!leftThruster1Particles.isPlaying) leftThruster1Particles.Play();
        if (!leftThruster2Particles.isPlaying) leftThruster2Particles.Play();
    }

    void RotateLeft()
    {
        Rotate(rotateSpeed);

        if (!rightThruster1Particles.isPlaying) rightThruster1Particles.Play();
        if (!rightThruster2Particles.isPlaying) rightThruster2Particles.Play();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);

        if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngineAudio);
        if (!mainThrusterParticles.isPlaying) mainThrusterParticles.Play();
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainThrusterParticles.Stop();
    }

    void StopRotation()
    {
        rightThruster1Particles.Stop();
        rightThruster2Particles.Stop();
        leftThruster1Particles.Stop();
        leftThruster2Particles.Stop();
    }
}
