using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayCanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_descriptionTexts;
    private int m_activeText = 0;

    [SerializeField]
    private GameObject[] m_options;

    [SerializeField]
    private GameObject m_difficultyButtonString;
    private int m_selectedDifficulty = 1;
    private string[] m_difficultyStrings = {"Casual", "Balanced", "Challenging"};

    public bool m_active = false;

    public void Init()
    {
        EnableTextItem(0);
        m_active = true;
        GameObject.FindFirstObjectByType<Scanner>().m_scanningPaused = true;
        GameObject.FindFirstObjectByType<Scanner>().init(m_options[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_active)
        {
            return;
        }

        if (m_activeText == 2)
        {
            GameObject.FindFirstObjectByType<Scanner>().UpdateScanner(m_options);
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                m_activeText++;
                EnableTextItem(m_activeText);

                if (m_activeText == 2)
                {
                    foreach (GameObject button in m_options)
                    {
                        button.SetActive(true);
                    }
                    GameObject.FindFirstObjectByType<Scanner>().m_scanningPaused = false;
                }
            }
        }
    }

    private void EnableTextItem(int textIndex)
    {
        DisableAllText();
        m_descriptionTexts[textIndex].SetActive(true);
    }

    private void DisableAllText()
    {
        foreach (GameObject text in m_descriptionTexts)
        {
            text.SetActive(false);
        }
    }

    public void CycleSettings()
    {
        m_selectedDifficulty = m_selectedDifficulty + 1 > 2 ? 0 : m_selectedDifficulty + 1;
        m_difficultyButtonString.GetComponent<TextMeshProUGUI>().text = m_difficultyStrings[m_selectedDifficulty];
    }


    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
