using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_options;

    [SerializeField]
    private GameObject m_text1;
    [SerializeField]
    private GameObject m_text2;
    [SerializeField]
    private GameObject m_text3;
    [SerializeField]
    private GameObject m_panel;

    public void Init(MenuState state)
    {
        GameObject.FindFirstObjectByType<Scanner>().init(m_options[0]);
        OnHighlightChange(0, state);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.FindFirstObjectByType<Scanner>().UpdateScanner(m_options);
    }

    public void OnHighlightChange(int newHighlight, MenuState state)
    {

        switch (state)
        {
            case MenuState.Settings:
                DisableAllText();
                m_panel.SetActive(true);
                switch (newHighlight)
                {
                    case 0:
                        m_text1.SetActive(true);
                        break;
                    case 1:
                        m_text2.SetActive(true);
                        break;
                    case 2:
                        m_text3.SetActive(true);
                        break;
                    default:
                        m_panel.SetActive(false);
                        break;
                }
                break;
            default: 
                break;
        }
        
    }

    private void DisableAllText()
    {
        m_text1.SetActive(false);
        m_text2.SetActive(false);
        m_text3.SetActive(false);
    }
}
