using UnityEngine;

public class LootChestController : MonoBehaviour, IInteractable
{
    public bool isOpened = false;

    // 👉 Esto es lo que los tests están buscando:
    public bool IsOpened => isOpened;

    public void Interact()
    {
        // ❌ BUG QUE CAUSÓ LA REGRESIÓN (LO DEJAMOS COMENTADO COMO DOCUMENTACIÓN)
        /*
        bool playerHasPermission = false;
        if (IsOpened || !playerHasPermission)
        {
            Debug.Log("Este cofre ya ha sido abierto o el jugador no tiene permiso.");
            return;
        }

        isOpened = true;
        Debug.Log("¡Has abierto el cofre y encontrado un tesoro (con bug)!");
        */

        // ✅ LÓGICA CORRECTA RESTAURADA
        if (isOpened)
        {
            Debug.Log("Este cofre ya ha sido abierto.");
            return;
        }

        isOpened = true;
        Debug.Log("¡Has abierto el cofre y encontrado un tesoro!");
        // Aquí instanciarías un ítem, añadirías oro al inventario, etc.
    }
}
