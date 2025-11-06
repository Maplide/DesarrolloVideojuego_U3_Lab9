using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AIController : MonoBehaviour, IInteractable, IDamageable
{
    [Header("AI Settings")]
    public Transform[] waypoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float detectionRadius = 10f;
    public float loseSightRadius = 15f;

    [Header("Stun Settings")]
    [Tooltip("Duración del aturdimiento en segundos")]
    public float stunDuration = 2f;

    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    [Header("Fire DOT Settings (opcional)")]
    public float fireTickDamage = 2f;     // daño por tick
    public float fireTickInterval = 0.5f; // cada cuánto aplica el tick
    public float fireTotalDuration = 3f;  // duración total del DOT

    // Dependencias cacheadas
    public NavMeshAgent Agent { get; private set; }
    public Transform Player { get; private set; }

    private AIState _currentState;

    // Control de Stun
    private Coroutine _stunCoroutine;
    private bool _isStunned;

    // Control de Fuego (DOT)
    private Coroutine _fireCoroutine;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        if (Agent == null)
        {
            Debug.LogError("[AIController] Falta NavMeshAgent. Deshabilitando.");
            enabled = false;
            return;
        }
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    private void Start()
    {
        var playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null) Player = playerGO.transform;
        else Debug.LogWarning("[AIController] No se encontró 'Player'.");

        ChangeState(new PatrolState(this));
    }

    private void Update()
    {
        if (!enabled || _currentState == null) return;
        _currentState.UpdateState();
    }

    public void ChangeState(AIState newState)
    {
        if (newState == null) return;
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }

    // === Compat: IInteractable (si algo externo aún llama Interact)
    public void Interact()
    {
        Stun(stunDuration);
    }

    // === API Stun ===
    public void Stun(float duration)
    {
        if (!_isStunned)
        {
            ChangeState(new StunState(this, duration));
        }
        else
        {
            if (_stunCoroutine != null) StopCoroutine(_stunCoroutine);
            _stunCoroutine = StartCoroutine(StunTimer(duration));
        }
    }

    public void BeginStunWindow(float duration)
    {
        _isStunned = true;
        if (_stunCoroutine != null) StopCoroutine(_stunCoroutine);
        _stunCoroutine = StartCoroutine(StunTimer(duration));
    }

    private IEnumerator StunTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        _isStunned = false;
        if (enabled) ChangeState(new PatrolState(this));
    }

    // === IDamageable (única implementación) ===
    public void TakeDamage(float amount, string damageType)
    {
        Debug.Log($"[AIController:{name}] TakeDamage => type={damageType}, amount={amount}");

        switch (damageType)
        {
            case "Stun":
                Stun(stunDuration);
                ApplyDamage(amount*0.5f);
                break;

            case "Physical":
                ApplyDamage(amount);
                break;

            case "Fire":
                ApplyDamage(amount); // daño inicial
                if (_fireCoroutine != null) StopCoroutine(_fireCoroutine);
                _fireCoroutine = StartCoroutine(FireDotCoroutine());
                break;

            default:
                // Cualquier otro tipo lo tratamos como daño físico
                ApplyDamage(amount);
                break;
        }
    }

    private void ApplyDamage(float amount)
    {
        if (amount <= 0f) return;
        float old = currentHealth;
        currentHealth = Mathf.Max(0f, currentHealth - amount);
        Debug.Log($"[AI HP] {name}: {old} -> {currentHealth}/{maxHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private IEnumerator FireDotCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < fireTotalDuration)
        {
            ApplyDamage(fireTickDamage);
            Debug.Log($"[AI DOT:Fire] {name} tick {fireTickDamage}, HP={currentHealth}/{maxHealth}");
            if (currentHealth <= 0f) yield break;

            yield return new WaitForSeconds(fireTickInterval);
            elapsed += fireTickInterval;
        }
    }

    private void Die()
    {
        Debug.Log($"[AI DIE] {name} murió.");
        enabled = false;

        if (Agent != null)
        {
            Agent.isStopped = true;
            Agent.updatePosition = false;
            Agent.updateRotation = false;
        }

        // Visual rápido: ocultar o destruir
        var rend = GetComponentInChildren<Renderer>();
        if (rend) rend.enabled = false;

        Destroy(gameObject, 0.2f); // opcional
    }
}
