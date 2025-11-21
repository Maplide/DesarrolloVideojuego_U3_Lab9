using UnityEngine;

public class TouchColorChanger : MonoBehaviour
{
    [Header("Renderer del cubo")]
    public Renderer targetRenderer;

    [Header("Colores")]
    public Color normalColor = Color.red;
    public Color touchedColor = Color.green;

    void Start()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<Renderer>();
        }

        SetColor(normalColor);
    }

    // Estos métodos se llamarán desde los eventos del XR Simple Interactable
    public void OnHoverEnter()
    {
        SetColor(touchedColor);
    }

    public void OnHoverExit()
    {
        SetColor(normalColor);
    }

    private void SetColor(Color color)
    {
        if (targetRenderer == null) return;

        // Crear instancia del material para no cambiar el material compartido
        targetRenderer.material.color = color;
    }
}
