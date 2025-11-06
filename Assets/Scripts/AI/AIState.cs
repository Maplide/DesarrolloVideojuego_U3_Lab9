using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Clase base abstracta para todos los estados de la IA
/// </summary>
public abstract class AIState
{
    // Usamos 'protected' para que las clases hijas puedan acceder.
    // El prefijo 'm_' es una convención común para miembros protegidos.
    protected AIController m_controller;
    protected NavMeshAgent m_agent;
    protected Transform m_playerTransform;

    public AIState(AIController controller)
    {
        m_controller = controller;
        m_agent = controller.GetComponent<NavMeshAgent>();
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public abstract void OnEnter();
    public abstract void UpdateState();
    public abstract void OnExit();

    protected bool EnsureRefs()
    {
        if (m_controller == null) return false;

        if (m_agent == null)
            m_agent = m_controller.GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (m_playerTransform == null)
        {
            var go = GameObject.FindGameObjectWithTag("Player");
            if (go != null) m_playerTransform = go.transform;
            // si no hay Player, igual seguimos; StunState no lo necesita
        }

        return m_agent != null; // para StunState basta con tener agent
    }
}