using System;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField]
    private float m_standardTime;
    [SerializeField]
    private float m_challengeTime;
    [SerializeField]
    private float m_mistakePunishment;

    private float m_currentTime;


    [SerializeField]
    private GameObject m_settingsObject;

    private int m_gameMode;

    public bool m_active;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_settingsObject.GetComponent<SettingsManager>().ReadJSON();
        m_gameMode = m_settingsObject.GetComponent<SettingsManager>().settings.settingsData.gameMode;
        

        if (m_gameMode == 0)
        {
            gameObject.SetActive(false);
            m_active = false;
            return;
        }

        m_active = true;

        SetTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_gameMode == 0 || !m_active) return;   

        m_currentTime -= Time.deltaTime;

        if (m_currentTime < 0)
        {
            m_currentTime = 0;
            m_active = false;
        }

        System.TimeSpan ts = TimeSpan.FromSeconds(m_currentTime);
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }

    private void SetTimerText()
    {
        if (m_gameMode == 1)
        {
            m_currentTime = m_standardTime;
        }
        else
        {
            m_currentTime = m_challengeTime;
        }

        System.TimeSpan ts = TimeSpan.FromSeconds(m_currentTime);
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }

    private void MistakeMade()
    {
        if (m_gameMode == 2)
        {
            m_currentTime -= m_mistakePunishment;
        }
    }
}
