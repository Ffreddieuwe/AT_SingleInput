using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DescriptionManager : MonoBehaviour
{
    [SerializeField]
    private string[] m_descriptions;

    [SerializeField]
    private GameObject m_linkedClock;

    [SerializeField]
    private int m_clockSegments;

    private float m_currentHandRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        m_currentHandRotation = m_linkedClock.GetComponent<ClockManager>().currentRotation;

        if (m_clockSegments == 3)
        {
            Update3();
        }
        else
        {
            Update4();
        }
    }

    void UpdateDescription(int clockPanel)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = m_descriptions[clockPanel];
    }

    private void Update4()
    {
        if (m_currentHandRotation < 45 || m_currentHandRotation > 315)
        {
            UpdateDescription(0);
        }
        else if (m_currentHandRotation < 135)
        {
            UpdateDescription(1);
        }
        else if (m_currentHandRotation < 215)
        {
            UpdateDescription(2);
        }
        else
        {
            UpdateDescription(3);
        }
    }

    private void Update3()
    {
        if (m_currentHandRotation < 120)
        {
            UpdateDescription(0);
        }
        else if (m_currentHandRotation < 240)
        {
            UpdateDescription(1);
        }
        else
        {
            UpdateDescription(2);
        }
    }
}
