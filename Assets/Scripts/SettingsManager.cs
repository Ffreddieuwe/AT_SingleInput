using Mono.Cecil.Cil;
using System;
using System.IO;
using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_text1;
    [SerializeField]
    private GameObject m_text2;
    [SerializeField]
    private GameObject m_text3;

    public TextAsset textJSON;
    public SettingsDataList settings;

    private bool m_selectBuffer;

    [System.Serializable]
    public class SettingsData
    {
        public bool automatic;
        public float menuSpeed;
        public float gameSpeed;
    }

    [System.Serializable]
    public class SettingsDataList
    {
        public SettingsData settingsData;
    }

    private void Start()
    {
        ReadJSON();
    }

    public void Init()
    {
        DisableAll();
        m_text1.SetActive(true);
        m_selectBuffer = false;
        SetupText();
    }

    public void OnHighlightChange(int newHighlight)
    {
        DisableAll();

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
                break;
        }
    }

    public void ReadJSON()
    {
        settings = JsonUtility.FromJson<SettingsDataList>(textJSON.text);
    }

    public void UpdateJSON()
    {
        string strOutput = JsonUtility.ToJson(settings);
        if (!Application.isEditor)
        {
            string dataPath = Path.Combine(Application.persistentDataPath, "Settings.json");
            File.WriteAllText(dataPath, strOutput);
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/Data/Settings.json", strOutput);
        }
    }

    private void DisableAll()
    {
        m_text1.SetActive(false);
        m_text2.SetActive(false);
        m_text3.SetActive(false);
    }

    public void ToggleControlMode()
    {
        if (m_selectBuffer)
        {
            m_selectBuffer = false;
            return;
        }

        settings.settingsData.automatic = !settings.settingsData.automatic;
        m_selectBuffer = settings.settingsData.automatic ? false : true;
        gameObject.GetComponent<Scanner>().SetAutomatic(settings.settingsData.automatic);
        m_text1.GetComponent<TextMeshProUGUI>().text = settings.settingsData.automatic ? "Automatic" : "Inverse";
        UpdateJSON();
    }

    public void ToggleMenuControlSpeed()
    {
        switch (settings.settingsData.menuSpeed)
        {
            case 0.75f:
                settings.settingsData.menuSpeed = 1f;
                m_text2.GetComponent<TextMeshProUGUI>().text = "1X speed";
                break;
            case 1f:
                settings.settingsData.menuSpeed = 1.25f;
                m_text2.GetComponent<TextMeshProUGUI>().text = "1.25X speed";
                break;
            case 1.25f:
                settings.settingsData.menuSpeed = 0.75f;
                m_text2.GetComponent<TextMeshProUGUI>().text = "0.75X speed";
                break;
        }
        UpdateJSON();
    }

    public void ToggleGameControlSpeed()
    {
        switch (settings.settingsData.gameSpeed)
        {
            case 0.75f:
                settings.settingsData.gameSpeed = 1f;
                m_text3.GetComponent<TextMeshProUGUI>().text = "1X speed";
                break;
            case 1f:
                settings.settingsData.gameSpeed = 1.25f;
                m_text3.GetComponent<TextMeshProUGUI>().text = "1.25X speed";
                break;
            case 1.25f:
                settings.settingsData.gameSpeed = 0.75f;
                m_text3.GetComponent<TextMeshProUGUI>().text = "0.75X speed";
                break;
        }
        UpdateJSON();
    }

    private void SetupText()
    {
        switch (settings.settingsData.gameSpeed)
        {
            case 0.75f:
                m_text3.GetComponent<TextMeshProUGUI>().text = "1X speed";
                break;
            case 1f:
                m_text3.GetComponent<TextMeshProUGUI>().text = "1.25X speed";
                break;
            case 1.25f:
                m_text3.GetComponent<TextMeshProUGUI>().text = "0.75X speed";
                break;
        }

        switch (settings.settingsData.menuSpeed)
        {
            case 0.75f:
                m_text2.GetComponent<TextMeshProUGUI>().text = "1X speed";
                break;
            case 1f:
                m_text2.GetComponent<TextMeshProUGUI>().text = "1.25X speed";
                break;
            case 1.25f:
                m_text2.GetComponent<TextMeshProUGUI>().text = "0.75X speed";
                break;
        }
    }
}
