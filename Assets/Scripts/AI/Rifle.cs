using UnityEngine;
using UnityEngine.InputSystem; // Nuevo Input System

public class Rifle : MonoBehaviour
{
    [Header("Damage Settings")]
    [Tooltip("Daño por disparo (para Physical/Fire)")]
    public float damage = 10f;

    [Tooltip("Tipo de daño: Stun, Physical o Fire")]
    public string damageType = "Physical"; // <- por defecto PHYSICAL para ver la vida bajar

    [Header("Rifle")]
    [Tooltip("Tiempo mínimo entre disparos (segundos)")]
    public float cooldown = 0.2f;

    [Header("Raycast")]
    [Tooltip("Alcance del disparo")]
    public float range = 50f;
    public LayerMask hitLayers = ~0;
    public bool drawDebugRay = true;

    [Header("Opcional (Inspector)")]
    [SerializeField] private Camera cam;    // arrastra tu Main Camera si quieres

    private float _nextTime;
    private Camera _cam;

    private InputAction _fireAction;

    private void Awake()
    {
        // Cámara
        _cam = cam != null ? cam : Camera.main;
        if (_cam == null)
            Debug.LogWarning("[Rifle] No hay Camera.main, usaré transform como origen.");

        // Input (Nuevo Sistema)
        _fireAction = new InputAction("Fire", InputActionType.Button);
        _fireAction.AddBinding("<Mouse>/leftButton");
        _fireAction.AddBinding("<Keyboard>/f");
    }

    private void OnEnable()
    {
        _fireAction?.Enable();
    }

    private void OnDisable()
    {
        _fireAction?.Disable();
    }

    private void Update()
    {
        if (Time.time < _nextTime) return;
        if (!_fireAction.WasPressedThisFrame()) return;

        _nextTime = Time.time + cooldown;

        Vector3 origin, direction;
        if (_cam != null)
        {
            origin = _cam.transform.position;
            direction = _cam.transform.forward;
        }
        else
        {
            origin = transform.position;
            direction = transform.forward;
        }

        if (drawDebugRay) Debug.DrawRay(origin, direction * range, Color.red, 0.5f);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, range, hitLayers, QueryTriggerInteraction.Ignore))
        {
            var dmg = hit.collider.GetComponentInParent<IDamageable>();
            if (dmg != null)
            {
                dmg.TakeDamage(damage, damageType);
                Debug.Log($"[Rifle] Hit '{hit.collider.name}' | damageType={damageType} amount={damage} | point={hit.point}");
            }
            else
            {
                Debug.Log($"[Rifle] Hit '{hit.collider.name}' pero no implementa IDamageable.");
            }
        }
        else
        {
            Debug.Log("[Rifle] Disparo sin impacto.");
        }
    }
}
