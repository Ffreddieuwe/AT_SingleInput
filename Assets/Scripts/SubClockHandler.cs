using UnityEngine;

public class SubClockHandler : MonoBehaviour
{

    [SerializeField]
    private GameObject m_parentClock;
    [SerializeField]
    private GameObject m_garnishClock;
    [SerializeField]
    private GameObject m_spiritClock;
    [SerializeField]
    private GameObject m_softClock;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisableAll();
        m_parentClock.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableClock(int clock)
    {
        DisableAll();

        switch (clock)
        {
            case 0:
                m_parentClock.SetActive(true);
                m_parentClock.GetComponent<ClockManager>().ResetRotation();
                break;
            case 1:
                m_garnishClock.SetActive(true);
                m_garnishClock.GetComponent<ClockManager>().ResetRotation();
                break;
            case 2:
                m_spiritClock.SetActive(true);
                m_spiritClock.GetComponent<ClockManager>().ResetRotation();
                break;
            case 3:
                m_softClock.SetActive(true);
                m_softClock.GetComponent<ClockManager>().ResetRotation();
                break;
        }
    }

    private void DisableAll()
    {
        m_parentClock.SetActive(false);
        m_garnishClock.SetActive(false);
        m_spiritClock.SetActive(false);
        m_softClock.SetActive(false);
    }
}
