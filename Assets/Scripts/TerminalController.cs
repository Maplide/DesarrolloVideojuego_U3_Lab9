using UnityEngine;

public class TerminalController : MonoBehaviour, IInteractable
{
    [Header("Terminal Settings")]
    public Light terminalLight;

    [Tooltip("Si está en true, luego de activarse por primera vez se bloquea y ya no permite más interacciones.")]
    public bool lockAfterFirstActivation = false;

    private bool _isActive = false;        // Para el toggle visual (verde/rojo)
    private bool _hasFiredEvent = false;   // Para no disparar el evento más de una vez
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        // Estado inicial visual (rojo)
        if (terminalLight != null)
            terminalLight.color = Color.red;
    }

    public void Interact()
    {
        // Si está bloqueado, no hacer nada
        if (lockAfterFirstActivation && _hasFiredEvent) return;

        // 1) Comportamiento toggle para la luz (como antes)
        _isActive = !_isActive;
        if (terminalLight != null)
            terminalLight.color = _isActive ? Color.green : Color.red;

        Debug.Log("Estado del sistema: " + (_isActive ? "Activo" : "Inactivo"));

        // 2) Disparar el evento SOLO al pasar de inactivo -> activo (una vez)
        if (_isActive && !_hasFiredEvent)
        {
            Debug.Log("Terminal activado. Disparando evento OnObjectiveActivated.");
            GameEvents.TriggerObjectiveActivated();
            _hasFiredEvent = true;

            // 3) Opcional: bloquear después de la primera activación
            if (lockAfterFirstActivation && _collider != null)
                _collider.enabled = false;
        }
    }
}
