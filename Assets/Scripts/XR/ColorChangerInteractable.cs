using System.Collections;
using UnityEngine;

public class ColorChangerInteractable : MonoBehaviour
{
    [Header("Config")]
    [Tooltip("Renderer cuyo material cambiará de color. Si se deja vacío, usará el Renderer del mismo objeto.")]
    public Renderer targetRenderer;

    [Tooltip("Tiempo en segundos antes de volver al color original.")]
    public float resetDelay = 2f;

    private Color _originalColor;
    private bool _hasOriginalColor = false;
    private Coroutine _resetCoroutine;

    private void Awake()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<Renderer>();
        }

        if (targetRenderer != null)
        {
            _originalColor = targetRenderer.material.color;
            _hasOriginalColor = true;
        }
        else
        {
            Debug.LogWarning($"[ColorChangerInteractable] No se encontró Renderer en {gameObject.name}.", this);
        }
    }

    // 👇 Este método lo llamará el evento "Select Entered" del XR Simple Interactable
    public void HandleSelectEntered()
    {
        if (targetRenderer == null || !_hasOriginalColor)
            return;

        // cancelar coroutine anterior si existía
        if (_resetCoroutine != null)
        {
            StopCoroutine(_resetCoroutine);
            _resetCoroutine = null;
        }

        // Color aleatorio
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        targetRenderer.material.color = randomColor;

        // Programar retorno al color original
        _resetCoroutine = StartCoroutine(ResetColorAfterDelay(resetDelay));
    }

    private IEnumerator ResetColorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (targetRenderer != null && _hasOriginalColor)
        {
            targetRenderer.material.color = _originalColor;
        }

        _resetCoroutine = null;
    }
}
