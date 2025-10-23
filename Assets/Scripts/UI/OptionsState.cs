using UnityEngine;

public class OptionsState : UIState
{
    public OptionsState(UIManager uiManager) : base(uiManager) {}

    public override void Enter()
    {
        // No tocamos el Time.timeScale para respetar el estado anterior (pausado o no)
        m_uiManager.optionsPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void Exit()
    {
        m_uiManager.optionsPanel.SetActive(false);
    }
}
