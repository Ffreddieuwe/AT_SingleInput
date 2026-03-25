using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int m_resets;
    private int m_drinksCompleted;
    private float m_playtime;
    private int m_ingredientsUsed;

    [SerializeField]
    private GameObject m_customer;
    [SerializeField]
    private GameObject m_orderBubble;
    [SerializeField]
    private GameObject[] m_statTexts;
    [SerializeField]
    GameObject m_success;
    [SerializeField]
    GameObject m_failure;
    [SerializeField]
    private GameObject m_timer;

    public bool orderActive;

    private float m_successWaitTimer;

    public float m_rotation = 0;

    public float speed = 80;

    private bool m_gameOver = false;

    [SerializeField]
    private GameObject m_gameoverPanel;
    [SerializeField]
    private GameObject m_stats1;
    [SerializeField]
    private GameObject m_stats2;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_resets = 0;
        m_drinksCompleted = 0;
        m_playtime = 0;
        m_ingredientsUsed = 0;
        m_successWaitTimer = 0;

        orderActive = false;

        m_timer.GetComponent<TimerManager>().m_active = false;

        foreach (GameObject item in m_statTexts)
        {
            item.SetActive(false);
        }

        m_success.SetActive(false);
        m_failure.SetActive(false);
        m_orderBubble.SetActive(false);

        m_customer.GetComponent<CustomerManager>().ResetCustomer();
        m_customer.GetComponent<CustomerManager>().WalkIn();

        m_gameoverPanel.SetActive(false);
        m_stats1.SetActive(false);
        m_stats2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Main_Menu");
            }
        }

        m_playtime += Time.deltaTime;

        if (m_successWaitTimer > 0)
        {
            m_successWaitTimer -= Time.deltaTime;
            if (m_successWaitTimer <= 0)
            {
                CustomerLeave();
            }
        }

        if (orderActive)
        {
            m_rotation += Time.deltaTime * speed;
            if (m_rotation >= 360)
            {
                m_rotation = 0;
            }
        }
    }

    public void DrinkSuccess()
    {
        m_success.SetActive(true);
        m_failure.SetActive(false);
        orderActive = false;
        m_successWaitTimer = 2f;
        IncrementStat(1);
        m_timer.GetComponent<TimerManager>().m_active = false;
        Camera.main.GetComponent<AudioManager>().PlaySound(0);
    }

    public void DrinkFailure()
    {
        m_failure.SetActive(true);
        Camera.main.GetComponent<AudioManager>().PlaySound(1);
    }

    public void OutOfTime()
    {
        Camera.main.GetComponent<AudioManager>().PlaySound(2);

        m_gameOver = true;
        m_gameoverPanel.SetActive(true);

        m_stats1.GetComponent<TextMeshProUGUI>().text = "Drinks Made: " + m_drinksCompleted;
        m_stats1.SetActive(true);
        System.TimeSpan ts = TimeSpan.FromSeconds(m_playtime);
        m_stats2.GetComponent<TextMeshProUGUI>().text = "Playtime: " + string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        m_stats2.SetActive(true);

        orderActive = false;
    }

    private void CustomerLeave()
    {
        orderActive = false;
        m_orderBubble.SetActive(false);
        m_customer.GetComponent<CustomerManager>().WalkOut();
        Camera.main.GetComponent<DrinkManager>().ResetDrink();
        m_timer.GetComponent<TimerManager>().SetTimerText();
        m_success.SetActive(false);
    }

    public void CustomerReady()
    {
        Camera.main.GetComponent<DrinkManager>().NewOrder();
        m_orderBubble.SetActive(true);
        orderActive = true;
        m_timer.GetComponent<TimerManager>().m_active = true;
    }

    public void IncrementStat(int stat)
    {
        switch (stat)
        {
            case 0:
                m_resets++;
                m_success.SetActive(false);
                m_failure.SetActive(false);
                m_timer.GetComponent<TimerManager>().MistakeMade();
                break;
            case 1:
                m_drinksCompleted++;
                break;
            case 3:
                m_ingredientsUsed++;
                break;

        }

        m_statTexts[0].GetComponent<TextMeshProUGUI>().text = "Resets: " + m_resets;
    }

    public void SetStatText()
    {
        m_statTexts[0].GetComponent<TextMeshProUGUI>().text = "Resets: " + m_resets;
        m_statTexts[1].GetComponent<TextMeshProUGUI>().text = "Drinks Made: " + m_drinksCompleted;
        System.TimeSpan ts = TimeSpan.FromSeconds(m_playtime);
        m_statTexts[2].GetComponent<TextMeshProUGUI>().text = "Playtime: " + string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        m_statTexts[3].GetComponent<TextMeshProUGUI>().text = "Ingredients Used: " + m_ingredientsUsed;

        foreach (GameObject item in m_statTexts)
        {
            item.SetActive(true);
        }
    }

    public void HideStatText()
    {
        foreach (GameObject item in m_statTexts)
        {
            item.SetActive(false);
        }
    }

    public void PauseTimer(bool pause)
    {
        m_timer.GetComponent<TimerManager>().m_active = !pause;
    }
}
