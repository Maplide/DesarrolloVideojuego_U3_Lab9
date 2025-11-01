using UnityEngine;

public class MainMenuState : UIState
{
    public MainMenuState(UIManager uiManager) : base(uiManager) { }

    public override void Enter()
    {
        Debug.Log("Entrando al estado de Menú Principal");
        if (m_uiManager.mainMenuPanel != null)
            m_uiManager.mainMenuPanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void Exit()
    {
        Debug.Log("Saliendo del estado de Menú Principal");
        if (m_uiManager.mainMenuPanel != null)
            m_uiManager.mainMenuPanel.SetActive(false);
    }
}
