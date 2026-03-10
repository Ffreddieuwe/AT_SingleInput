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
    private bool m_moving;

    [SerializeField]
    private float m_speed = 10f;


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
                Vector3 direction = (m_targetPos - transform.position).normalized;
                transform.position += direction * Time.deltaTime * m_speed;
            }
            else
            {
                if (m_targetPanel == Panel.Middle && m_selectedPanel != Panel.Up)
                {
                    Camera.main.GetComponent<DrinkManager>().m_shouldAskComplete = true;
                }

                m_moving = false;
                m_selectedPanel = m_targetPanel;
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
