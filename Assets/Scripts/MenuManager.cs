using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuState
{
    Start = 0,
    MainMenu = 1,
    Settings = 2
}

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_methodText;

    [SerializeField]
    private GameObject m_startCanvas;
    [SerializeField]
    private GameObject m_menuCanvas;
    [SerializeField]
    private GameObject m_settingsCanvas;

    private MenuState m_state;
    private GameObject m_currentCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisableAllCanvas();
        m_currentCanvas = m_startCanvas;
        m_currentCanvas.SetActive(true);
        m_currentCanvas.GetComponent<CanvasManager>().Init(m_state);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMethodText(bool automatic)
    {
        m_methodText.GetComponent<TextMeshProUGUI>().text = automatic ? "Automatic" : "Inverse";
    }

    public void ChangeState(int newState)
    {
        DisableAllCanvas();
        m_state = (MenuState) newState;

        switch (m_state)
        {
            case MenuState.Start:
                m_currentCanvas = m_startCanvas;
                break;
            case MenuState.MainMenu:
                m_currentCanvas = m_menuCanvas;
                break;
            case MenuState.Settings:
                m_currentCanvas = m_settingsCanvas;
                gameObject.GetComponent<SettingsManager>().Init();
                break;
        }

        m_currentCanvas.SetActive(true);
        m_currentCanvas.GetComponent<CanvasManager>().Init(m_state);
    }

    private void DisableAllCanvas()
    {
        m_startCanvas.SetActive(false);
        m_menuCanvas.SetActive(false);
        m_settingsCanvas.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void OnHighlightChange (int newHighlight)
    {
        switch (m_state)
        {
            case MenuState.Start:
            case MenuState.MainMenu:
                break;
            case MenuState.Settings:
                m_currentCanvas.GetComponent<CanvasManager>().OnHighlightChange(newHighlight, m_state);
                gameObject.GetComponent<SettingsManager>().OnHighlightChange(newHighlight);
                break;
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
