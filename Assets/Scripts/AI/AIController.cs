using UnityEngine;

/// <summary>
/// Controlador principal de la IA. Gestiona el estado actual y las transicciones
/// </summary>
/// 
public class AIController : MonoBehaviour
{
    private AIState _currentState;

    private void Awake()
    {
        // Inicializamos el estado inicial de la IA, por ejemplo, patrullaje.
        changeState(new PatrolState(this));
    }

    private void Update()
    {
        // Delega la lógica de actualización al estado actual
        // Principio de Responsabildiad Única
        _currentState?.UpdateState();
    }

    public void changeState(AIState newState)
    {
        // Maneja la transición entre estados
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }
}
