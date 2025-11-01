using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject inGameHudPanel;
    public GameObject optionsPanel;
    public GameObject victoryPanel;
    public GameObject lossPanel;        // <-- AÑADIDO

    [Header("Loading Screen")]
    public GameObject loadingScreenPanel;
    public Slider loadingBar;

    [Header("HUD")]
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI timerText;

    // Estados
    private UIState _currentState;
    private Stack<UIState> _stateHistory = new Stack<UIState>();

    public MainMenuState MainMenuState { get; private set; }
    public InGameState InGameState { get; private set; }
    public PauseMenuState PauseMenuState { get; private set; }
    public OptionsState OptionsState { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        MainMenuState  = new MainMenuState(this);
        InGameState    = new InGameState(this);
        PauseMenuState = new PauseMenuState(this);
        OptionsState   = new OptionsState(this);
    }

    private void Start()
    {
        ChangeState(MainMenuState, rememberPrevious:false);
    }

    private void OnEnable()
    {
        GameEvents.OnTargetFocused += HandleTargetFocused;
        GameEvents.OnTargetLost    += HandleTargetLost;
    }

    private void OnDisable()
    {
        GameEvents.OnTargetFocused -= HandleTargetFocused;
        GameEvents.OnTargetLost    -= HandleTargetLost;
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame)
        {
            if (_currentState == InGameState)       ChangeState(PauseMenuState);
            else if (_currentState == PauseMenuState) ChangeState(InGameState);
            else if (_currentState == OptionsState) GoBack();
        }
    }

    private void HandleTargetLost(GameObject _)   { if (infoText != null) infoText.gameObject.SetActive(false); }
    private void HandleTargetFocused(GameObject _){ if (infoText != null) infoText.gameObject.SetActive(true);  }

    public void ChangeState(UIState newState, bool rememberPrevious = true)
    {
        if (_currentState != null && rememberPrevious) _stateHistory.Push(_currentState);
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void GoBack()
    {
        if (_stateHistory.Count == 0) return;
        var prev = _stateHistory.Pop();
        _currentState?.Exit();
        _currentState = prev;
        _currentState.Enter();
    }

    // --- Botón Play ---
    public async void OnPlayButtonClicked()
    {
        if (loadingScreenPanel != null) loadingScreenPanel.SetActive(true);
        if (mainMenuPanel != null)      mainMenuPanel.SetActive(false);

        ChangeState(InGameState, rememberPrevious:false);

        AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync("Level_001");
        sceneLoadOperation.allowSceneActivation = false;

        while (!sceneLoadOperation.isDone)
        {
            if (loadingBar != null)
            {
                float progress = Mathf.Clamp01(sceneLoadOperation.progress / 0.9f);
                loadingBar.value = progress;
            }
            if (sceneLoadOperation.progress >= 0.9f)
                sceneLoadOperation.allowSceneActivation = true;

            await Task.Yield();
        }

        if (loadingScreenPanel != null) loadingScreenPanel.SetActive(false);
    }

    public void OnResumeButtonClicked() => ChangeState(InGameState);
    public void OnExitButtonClicked()   { Debug.Log("Saliendo del juego..."); Application.Quit(); }
    public void OnOptionsButtonClicked()     => ChangeState(OptionsState);
    public void OnOptionsBackButtonClicked() => GoBack();

    // --- ÚNICA definición ---
    public void ShowVictoryPanel()
    {
        if (inGameHudPanel != null) inGameHudPanel.SetActive(false);
        if (victoryPanel  != null)  victoryPanel.SetActive(true);
    }

    public void UpdateTimer(float seconds)
    {
        if (timerText == null) return;
        int s = Mathf.Max(0, Mathf.CeilToInt(seconds));
        int m = s / 60;
        int r = s % 60;
        timerText.text = $"{m:00}:{r:00}";
    }

    public void ShowLossPanel()
    {
        if (inGameHudPanel != null) inGameHudPanel.SetActive(false);
        if (lossPanel     != null)  lossPanel.SetActive(true);
    }
}
