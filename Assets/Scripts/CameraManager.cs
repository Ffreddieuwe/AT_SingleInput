using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public enum Panel 
    {
        Middle = 0,
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4,
    }

    public enum Direction
    {
        Left, 
        Right, 
        Up, 
        Down 
    };

    private Vector3[] m_panelPositions = {
        new Vector3(0f,0f,-10f),
        new Vector3(-17.8f,0f,-10f),
        new Vector3(17.8f,0f,-10f),
        new Vector3(0f,10f,-10f),
        new Vector3(0f,-10f,-10f)
    };

    public Panel m_selectedPanel;
    private Panel m_targetPanel;
    private Vector3 m_targetPos;
    public bool m_moving;

    [SerializeField]
    private float m_speed = 10f;

    [SerializeField]
    private GameObject m_mainBarDescriptionText;

    [SerializeField]
    private GameObject m_pauseDescriptionText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_selectedPanel = Panel.Middle;
        m_targetPanel = Panel.Middle;
        m_targetPos = m_panelPositions[(int)m_selectedPanel];
        m_moving = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Move(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Direction.Right);
        }

        if (m_moving)
        {
            if (transform.position != m_targetPos)
            {
                if (Vector3.Distance(transform.position, m_targetPos) < Time.deltaTime * m_speed)
                {
                    transform.position = m_targetPos;
                    return;
                }

                Vector3 direction = (m_targetPos - transform.position).normalized;
                transform.position += direction * Time.deltaTime * m_speed;
            }
            else
            {
                m_moving = false;
                m_selectedPanel = m_targetPanel;

                if (m_selectedPanel == Panel.Middle)
                {
                    m_mainBarDescriptionText.SetActive(true);
                }
                else if (m_selectedPanel == Panel.Up)
                {
                    m_pauseDescriptionText.SetActive(true);
                }
            }
        }
    }

    public void Move(Direction direction)
    {
        if (m_moving)
        {
            return;
        }

        switch (direction)
        {
            case Direction.Left:
                if (m_selectedPanel == Panel.Middle)
                {
                    StartMovement(Panel.Left);
                    return;
                }
                else if (m_selectedPanel == Panel.Right)
                {
                    StartMovement(Panel.Middle);
                    return;
                }
                break;
            case Direction.Right:
                if (m_selectedPanel == Panel.Middle)
                {
                    StartMovement(Panel.Right);
                    return;
                }
                else if (m_selectedPanel == Panel.Left)
                {
                    StartMovement(Panel.Middle);
                    return;
                }
                break;
            case Direction.Up:
                if (m_selectedPanel == Panel.Middle)
                {
                    StartMovement(Panel.Up);
                    return;
                }
                else if (m_selectedPanel == Panel.Down)
                {
                    StartMovement(Panel.Middle);
                    return;
                }
                break;
            case Direction.Down:
                if (m_selectedPanel == Panel.Middle)
                {
                    StartMovement(Panel.Down);
                    return;
                }
                else if (m_selectedPanel == Panel.Up)
                {
                    StartMovement(Panel.Middle);
                    return;
                }
                break;
        }
    }

    private void StartMovement(Panel panel)
    {
        m_moving = true;
        m_targetPanel = panel;
        m_targetPos = m_panelPositions[(int)m_targetPanel];
    }
}
