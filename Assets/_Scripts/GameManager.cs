using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class GameManager : MonoBehaviour
{
    [Space(10)] [Header("Buttons")] [SerializeField]
    private Button _butG;

    [SerializeField] private Button _butY;
    [SerializeField] private Button _butR;
    [SerializeField] private Button _settingsButton;

    [Space(10)] [Header("Panels")] [SerializeField]
    private RectTransform _settingsPanel;

    [SerializeField] private RectTransform _outMenuPanel;

    private bool _isSettingsOn;
    private bool _isOutMenuOn;

    private BallPool _ballPool;
    private TweenManager _tweenManager;

    private readonly List<Button> _buttons = new();

    [Inject]
    public void Construct(BallPool ballPool, TweenManager tweenManager)
    {
        _ballPool = ballPool;
        _tweenManager = tweenManager;
    }

    private void Start()
    {
        _buttons.AddRange(new[] { _butG, _butY, _butR });
        _settingsButton.onClick.AddListener(SettingsShow);
        _butG.onClick.AddListener(() => ExecuteFunctionAsync(() => _ballPool.SpawnGreenBall()));
        _butY.onClick.AddListener(() => ExecuteFunctionAsync(() => _ballPool.SpawnYellowBall()));
        _butR.onClick.AddListener(() => ExecuteFunctionAsync(() => _ballPool.SpawnRedBall()));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isSettingsOn)
            {
                _tweenManager.TogglePanel(_settingsPanel, false);
                {
                    _isSettingsOn = true;
                }
            }

            _tweenManager.TogglePanel(_outMenuPanel, _isOutMenuOn);
            _isOutMenuOn = !_isOutMenuOn;
        }
    }

    private async Task ExecuteFunctionAsync(System.Func<Task> func)
    {
        _buttons.ForEach(b => b.interactable = false);
        await func.Invoke();
        CurrencyManager.Instance.ChangeMoney(-CurrencyManager.Instance.bet);
        _buttons.ForEach(b => b.interactable = true);
    }

    #region PopMenu

    

   
    private void SettingsShow()
    {
        _tweenManager.TogglePanel(_settingsPanel, _isSettingsOn);
        _isSettingsOn = !_isSettingsOn;
    }
    
    public void CloseQuitPopup()
    {
        _tweenManager.TogglePanel(_outMenuPanel, false);
        _isOutMenuOn = true;
    }

    public void CloseProgram()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
    #endregion
}