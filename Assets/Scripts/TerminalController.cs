using UnityEngine;

public class TerminalController : MonoBehaviour, IInteractable
{
    [Header("Terminal Settings")]
    public Light terminalLight;

    private bool _isActive = false;

    public void Interact()
    {
        // Invertimos el estado actual
        _isActive = !_isActive;

        // Cambiamos el color de la luz según el estado
        if (terminalLight != null)
        {
            terminalLight.color = _isActive ? Color.green : Color.red;
        }

        // Mostramos el estado actual en consola
        Debug.Log("Estado del sistema: " + (_isActive ? "Activo" : "Inactivo"));
    }
}
