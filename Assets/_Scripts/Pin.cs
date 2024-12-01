using UnityEngine;
using Random = UnityEngine.Random;

public class Pin : MonoBehaviour
{
    private AudioSource pinSound;
    private float forceMagnitude = 0.1f;

    void Start()
    {
        pinSound = GetComponent<AudioSource>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var rb = collision.gameObject.GetComponent<Rigidbody2D>();
        float randX = Random.Range(-forceMagnitude, forceMagnitude);
        float randY = Random.Range(-forceMagnitude, forceMagnitude);
        rb.AddForce(new Vector2(randX, randY));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        pinSound.Play();
    }
}