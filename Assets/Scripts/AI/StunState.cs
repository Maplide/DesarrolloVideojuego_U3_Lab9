using UnityEngine;

public class StunState : AIState
{
    private readonly float _duration;

    public StunState(AIController controller, float duration) : base(controller)
    {
        _duration = Mathf.Max(0f, duration);
    }

    public override void OnEnter()
    {
        if (!EnsureRefs()) return;

        // Detener al agente YA
        m_agent.isStopped = true;
        m_agent.velocity = Vector3.zero;

        // Opcional: animación/flag de stun aquí (si tienes Animator)
        // m_controller.Animator?.SetTrigger("Stun");

        // Iniciar ventana de stun centralizada en el controller
        m_controller.BeginStunWindow(_duration);
    }

    public override void UpdateState()
    {
        // Nada que hacer: solo permanecer detenido
    }

    public override void OnExit()
    {
        if (m_agent != null)
        {
            m_agent.isStopped = false; // Dejar listo para el siguiente estado
        }
    }
}
