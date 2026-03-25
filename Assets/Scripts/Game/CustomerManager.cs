using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private Vector3 m_leftPos = new Vector3(-12f, 1.2f, 0f);
    private Vector3 m_middlePos = new Vector3(-6.75f, 1.2f, 0f);
    private Vector3 m_targetPosition;

    [SerializeField]
    private Sprite[] m_sprites;
    [SerializeField]
    private float m_speed = 6f;
    [SerializeField]
    private GameObject m_gameManager;

    private bool m_moving;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetCustomer();
    }

    public void ResetCustomer()
    {
        m_moving = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = m_sprites[Random.Range(0, m_sprites.Length)];
        transform.position = m_leftPos;
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_moving)
        {
            if (transform.position != m_targetPosition)
            {
                if (Vector3.Distance(transform.position, m_targetPosition) < Time.deltaTime * m_speed)
                {
                    transform.position = m_targetPosition;
                    return;
                }

                Vector3 direction = (m_targetPosition - transform.position).normalized;
                transform.position += direction * Time.deltaTime * m_speed;
            }
            else
            {
                m_moving = false;
                if (m_targetPosition == m_leftPos)
                {
                    ResetCustomer();
                    WalkIn();
                }
                else
                {
                    m_gameManager.GetComponent<GameManager>().CustomerReady();
                }
            }
        }
    }

    public void WalkIn()
    {
        m_targetPosition = m_middlePos;
        m_moving = true;
    }

    public void WalkOut()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        m_targetPosition = m_leftPos;
        m_moving = true;
    }
}
