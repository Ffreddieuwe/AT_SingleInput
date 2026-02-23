using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_options;
    private int m_highlightedOption;

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
        m_highlightedOption = 0;
        OnHighlightChange(0, state);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.FindFirstObjectByType<Scanner>().UpdateScanner(m_options);
    }

    public void OnHighlightChange(int newHighlight, MenuState state)
    {
        m_highlightedOption = newHighlight;

        switch (state)
        {
            case MenuState.Settings:
            case MenuState.Play:
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
