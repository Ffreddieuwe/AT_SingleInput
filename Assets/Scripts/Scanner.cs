using System;
using UnityEngine;
using UnityEngine.UI;

public class Scanner : MonoBehaviour
{
    private bool m_usingAutomatic;
    private int m_selectedID;
    private float m_holdTimer;
    private GameObject m_selectedGO;
    private const float m_holdDefault = 1.25f;

    private SettingsManager m_settingsManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_selectedID = 0;
        m_holdTimer = 0f;
    }

    public void init(GameObject firstOption)
    {
        m_holdTimer = 0f;
        m_selectedID = 0;
        m_selectedGO = firstOption;
        firstOption.GetComponent<Button>().Select();

        m_settingsManager = gameObject.GetComponent<SettingsManager>();
        SetAutomatic(m_settingsManager.settings.settingsData.automatic);
    }

    // Update is called once per frame from the menu manager
    public void UpdateScanner(GameObject[] options)
    {
        if (m_usingAutomatic)
        {
            UpdateAutomatic(options);
        }
        else
        {
            UpdateInverse(options);
        }
    }

    private void UpdateInverse(GameObject[] options)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            m_holdTimer += Time.deltaTime * m_settingsManager.settings.settingsData.menuSpeed;

            if (m_holdTimer >= m_holdDefault)
            {
                m_holdTimer = 0f;
                m_selectedID += 1;

                if (m_selectedID >= options.Length)
                {
                    m_selectedID = 0;
                }

                m_selectedGO = options[m_selectedID];
                m_selectedGO.GetComponent<Button>().Select();
                gameObject.GetComponent<MenuManager>().OnHighlightChange(m_selectedID);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            m_holdTimer = 0f;
            m_selectedGO.GetComponent<Button>().onClick.Invoke();
        }
    }

    private void UpdateAutomatic(GameObject[] options)
    {
        m_holdTimer += Time.deltaTime * m_settingsManager.settings.settingsData.menuSpeed;

        if (m_holdTimer >= m_holdDefault)
        {
            m_holdTimer = 0f;
            m_selectedID += 1;

            if (m_selectedID >= options.Length)
            {
                m_selectedID = 0;
            }

            m_selectedGO = options[m_selectedID];
            m_selectedGO.GetComponent<Button>().Select();
            gameObject.GetComponent<MenuManager>().OnHighlightChange(m_selectedID);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_holdTimer = 0f;
            m_selectedGO.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void SetAutomatic(bool automatic)
    {
        m_usingAutomatic = automatic;
        gameObject.GetComponent<MenuManager>().SetMethodText(automatic);
        m_settingsManager.settings.settingsData.automatic = automatic;
        m_settingsManager.UpdateJSON();
    }
}
