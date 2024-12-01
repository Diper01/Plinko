using UnityEngine;
using Zenject;
public class Ball : MonoBehaviour
{
    public float ballBet;
    [SerializeField] private Rigidbody2D rb;
    private readonly float speed = 5f;
    public BallPool _ballPool;

  
    private void Start()
    {
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), -speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pin"))
        {
            float randomBounce = Random.Range(-0.5f, 0.5f);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x + randomBounce, rb.linearVelocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            float f = other.gameObject.GetComponent<Target>().value;
            CurrencyManager.Instance.CurrentTarget(ballBet, f);
            ReturnToPool();
        }
    }

    private void ReturnToPool() => _ballPool.ReturnBallToPool(gameObject);


    public void ActivateBall(Vector3 position, Color color, float ballBetValue)
    {
        transform.position = position;
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), -speed);
        GetComponent<Renderer>().material.color = color;
        ballBet = ballBetValue;
        gameObject.SetActive(true);
    }
}