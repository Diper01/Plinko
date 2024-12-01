using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BallPool : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Vector3 _launchPos = new(0, 3, 0);
    [SerializeField] private int poolSize = 10;

    private List<GameObject> _ballPool;

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        _ballPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            var ball = Instantiate(_ballPrefab);
            ball.GetComponent<Ball>()._ballPool = this;
            ball.SetActive(false);
            _ballPool.Add(ball);
        }
    }

    private GameObject GetAvailableBall()
    {
        foreach (var ball in _ballPool)
        {
            if (!ball.activeInHierarchy)
            {
                return ball;
            }
        }

        return null;
    }

    private async Task<GameObject> GetBallFromPoolAsync()
    {
        var ball = GetAvailableBall();
        while (ball == null)
        {
            await Task.Yield();
            ball = GetAvailableBall();
        }

        return ball;
    }

    public void ReturnBallToPool(GameObject ball) => ball.SetActive(false);


    private async Task SpawnBall(Color color, int layer)
    {
        var ball = await GetBallFromPoolAsync();
        ball.GetComponent<Ball>()._ballPool = this;
        ball.layer = layer;
        ball.GetComponent<Ball>().ActivateBall(_launchPos, color, CurrencyManager.Instance.bet);
    }

    public async Task SpawnGreenBall() => await SpawnBall(Color.green, 8);
    public async Task SpawnYellowBall() => await SpawnBall(Color.yellow, 9);
    public async Task SpawnRedBall() => await SpawnBall(Color.red, 10);
}