using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject inGameHudPanel;
    public GameObject optionsPanel;        // <- NUEVO

    // Estados
    private UIState _currentState;
    private Stack<UIState> _stateHistory = new Stack<UIState>(); // <- NUEVO

    public MainMenuState MainMenuState { get; private set; }
    public InGameState InGameState { get; private set; }
    public PauseMenuState PauseMenuState { get; private set; }
    public OptionsState OptionsState { get; private set; }        // <- NUEVO

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        MainMenuState = new MainMenuState(this);
        InGameState   = new InGameState(this);
        PauseMenuState= new PauseMenuState(this);
        OptionsState  = new OptionsState(this); // <- NUEVO
    }

    private void Start()
    {
        ChangeState(MainMenuState, rememberPrevious:false);
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard.escapeKey.wasPressedThisFrame)
        {
            if (_currentState == InGameState)
            {
                ChangeState(PauseMenuState);
            }
            else if (_currentState == PauseMenuState)
            {
                ChangeState(InGameState);
            }
            else if (_currentState == OptionsState)
            {
                GoBack(); // <- volver al menú que te trajo a Opciones
            }
        }
    }

    /// <summary>
    /// Cambia de estado. Si rememberPrevious=true, guarda el estado anterior en el historial.
    /// </summary>
    public void ChangeState(UIState newState, bool rememberPrevious = true)
    {
        if (_currentState != null && rememberPrevious)
            _stateHistory.Push(_currentState);

        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    /// <summary>
    /// Retorna al estado anterior sin volver a apilar.
    /// </summary>
    public void GoBack()
    {
        if (_stateHistory.Count == 0) return;
        var prev = _stateHistory.Pop();
        // Importante: no queremos re-apilar el actual al volver
        _currentState?.Exit();
        _currentState = prev;
        _currentState.Enter();
    }

    // --- Botones ---
    public void OnPlayButtonClicked()  => ChangeState(InGameState);
    public void OnResumeButtonClicked()=> ChangeState(InGameState);
    public void OnExitButtonClicked()  { Debug.Log("Saliendo del juego..."); Application.Quit(); }

    // NUEVOS handlers para Opciones
    public void OnOptionsButtonClicked()       => ChangeState(OptionsState);
    public void OnOptionsBackButtonClicked()   => GoBack();
}
