using System.Collections; // para Corrutinas
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Playing, Victory, Loss }
    private GameState _currentState;

    [Header("Gameplay Settings")]
    [SerializeField] private int _objectivesToWin = 3;

    //Modificado - Guia13
    public GameLogic Logic {get; private set;}//Propiedad pública para acceder a la logica
    
    private int _objectivesCompleted = 0;

    // =========================
    //        TIMER (NUEVO)
    // =========================
    [Header("Timer")]
    [SerializeField] private float _timeLimit = 60f;   // NUEVO: segundos de límite
    private bool _isGameOver = false;                  // NUEVO: para parar la corrutina

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        //Modificado - Guia13
        Logic = new GameLogic(_objectivesToWin);
    }

    private void OnEnable()
    {
        GameEvents.OnObjectiveActivated += HandleObjectiveActivated;
    }

    private void OnDisable()
    {
        GameEvents.OnObjectiveActivated -= HandleObjectiveActivated;
    }

    private void Start()
    {
        ChangeState(GameState.Playing);

        // NUEVO: Iniciar contador al empezar a jugar
        StartCoroutine(CountdownTimer());
    }

    //Modificado - Guia13
    private void HandleObjectiveActivated()
    {
        if (_currentState != GameState.Playing) return;

        // Llamada a completar el objetivo, agregada desde la imagen
        Logic.CompleteObjective();

        // Mostrar el progreso como en la imagen
        Debug.Log($"Objetivo completado. Progreso: {Logic.ObjectivesCompleted}/{Logic.ObjectivesToWin}");

        // Verificar si se ha alcanzado la condición de victoria
        if (Logic.IsVictoryConditionMet)
        {
            ChangeState(GameState.Victory);
        }
    }


    public void ChangeState(GameState newState)
    {
        if (_currentState == newState) return;

        _currentState = newState;
        Debug.Log($"Nuevo estado de juego: {_currentState}");

        switch (_currentState)
        {
            case GameState.Playing:
                _isGameOver = false; // NUEVO: por si reinicias desde menú
                break;

            case GameState.Victory:
                _isGameOver = true;  // NUEVO: detiene la cuenta atrás
                StartCoroutine(VictorySequence());
                break;

            case GameState.Loss:
                _isGameOver = true;  // NUEVO: detiene la cuenta atrás
                StartCoroutine(LossSequence());
                break;
        }
    }

    // =========================
    //   CUENTA REGRESIVA (NUEVO)
    // =========================
    private IEnumerator CountdownTimer()
    {
        // Refrescar HUD al inicio
        UIManager.Instance?.UpdateTimer(_timeLimit);

        while (_timeLimit > 0f && !_isGameOver)
        {
            yield return new WaitForSeconds(1f);
            _timeLimit = Mathf.Max(0f, _timeLimit - 1f);
            UIManager.Instance?.UpdateTimer(_timeLimit);
        }

        // Si llegó a 0 y no se ganó, es derrota
        if (!_isGameOver && _timeLimit <= 0f)
        {
            ChangeState(GameState.Loss);
        }
    }

    // ============== VICTORIA (YA LA TENÍAS) ==============
    private IEnumerator VictorySequence()
    {
        Debug.Log("SECUENCIA DE VICTORIA INICIADA");

        var fpc = FindFirstObjectByType<FirstPersonController>();
        if (fpc != null) fpc.enabled = false;

        yield return new WaitForSeconds(1f);

        Debug.Log("mostrando UI de Victoria...");
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowVictoryPanel();
        }

        yield return new WaitForSeconds(3f);

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ChangeState(UIManager.Instance.MainMenuState, rememberPrevious:false);
            if (UIManager.Instance.victoryPanel != null)
                UIManager.Instance.victoryPanel.SetActive(false);
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Volviendo a la escena de Menú...");
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);

        // Reset básico
        _objectivesCompleted = 0;
        _timeLimit = 60f;        // <- ajusta si quieres otro valor por default
        _currentState = GameState.Playing;
    }

    // ============== DERROTA (NUEVO) ==============
    private IEnumerator LossSequence()
    {
        Debug.Log("SECUENCIA DE DERROTA INICIADA");

        var fpc = FindFirstObjectByType<FirstPersonController>();
        if (fpc != null) fpc.enabled = false;

        yield return new WaitForSeconds(0.5f);

        Debug.Log("mostrando UI de Derrota...");
        UIManager.Instance?.ShowLossPanel();

        yield return new WaitForSeconds(3f);

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ChangeState(UIManager.Instance.MainMenuState, rememberPrevious:false);
            if (UIManager.Instance.lossPanel != null)
                UIManager.Instance.lossPanel.SetActive(false);
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);

        // Reset básico
        _objectivesCompleted = 0;
        _timeLimit = 60f;        // <- ajusta si quieres
        _currentState = GameState.Playing;
    }
}
