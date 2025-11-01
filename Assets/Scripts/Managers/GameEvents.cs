using System; // Para la clase "Act2ion"
using UnityEngine;

/// <summary>
/// Contenedor estatico para eventos globales del juego
/// Permite una comunicacion desacoplada entre diferentes sistemas (Patron Observer)
/// </summary>

public static class GameEvents
{
    // evento que se dispara cuando un terminal de objetivo es activado
    public static event Action OnObjectiveActivated;

    public static event Action<GameObject> OnTargetFocused;

    public static event Action<GameObject> OnTargetLost;

    // metodo para invocar el evento desde cualquier lugar, de forma segura
    public static void TriggerObjectiveActivated()
    {
        // el '?' comprueba si hay suscriptores antes de invocar el evento
        OnObjectiveActivated?.Invoke();
    }

    public static void TriggerTargetFocused(GameObject target)
    {
        OnTargetFocused?.Invoke(target);
    }

    public static void TriggerTargetLost(GameObject target)
    {
        OnTargetLost?.Invoke(target);
    }
}