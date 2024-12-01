using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Action = System.Action;

public class CurrencyManager : Singleton<CurrencyManager>
{
    [HideInInspector] public float bet;
    [SerializeField] private TextMeshProUGUI _moneyDisplay;
    [SerializeField] private TextMeshProUGUI _betDisplay;
    [SerializeField] private Button minusBet;
    [SerializeField] private Button plusBet;
    private float _money;
    private readonly float[] _bets = { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 1.2f, 2, 4, 10, 20, 50, 100 };
    private int betIndex = 0;

    private void Start()
    {
        _money = PlayerPrefs.GetFloat("money", 3000);
        minusBet.onClick.AddListener(MinusBet);
        plusBet.onClick.AddListener(PlusBet);
        
        ChangeMoney();
        ChangeBet();
    }

    public void ChangeMoney(float value = 0f, Action action = null)
    {
        switch (_money + value)
        {
            case < 0:
                //TODO pop menu
                return;
            case >= 5_000_000:
                _money = 5_000_000;
                break;
            default:
                _money += value;
                break;
        }

        _moneyDisplay.text = _money + "";
        action?.Invoke();
    }

    public void CurrentTarget(float ballBet, float targetValue)
    {
        float money = ballBet * targetValue;
        ChangeMoney(money);
    }

    private void OnApplicationQuit() => PlayerPrefs.SetFloat("money", _money);

    #region Bet
    private void PlusBet()
    {
        if (betIndex + 1 >= _bets.Length)
            return;
        
        betIndex++;
        ChangeBet();
    }

    private void MinusBet()
    {
        if (betIndex - 1 < 0)
            return;

        betIndex--;
        ChangeBet();
    }


    private void ChangeBet()
    {
        bet = _bets[betIndex];
        _betDisplay.text = _bets[betIndex] + "";
    }
    #endregion
    public void GetMoney() => ChangeMoney(300);
    
}