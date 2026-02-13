using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Button1;
    [SerializeField]
    private GameObject Button2;
    [SerializeField]
    private GameObject Button3;
    [SerializeField]
    private GameObject MenuText;

    private int selectedButton = 1;
    private float moveTimer = 2f;

    private float holdTimer = 0f;

    public bool useAutomatic = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedButton = 1;
        Button1.GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (useAutomatic)
        {
            UpdateAutomatic();
        }
        else
        {
            UpdateInverse();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            useAutomatic = !useAutomatic;
        }
    }

    private void UpdateInverse()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= 0.75f)
            {
                holdTimer = 0f;
                switch (selectedButton)
                {
                    case 1:
                        selectedButton++;
                        Button2.GetComponent<Button>().Select();
                        break;
                    case 2:
                        selectedButton++;
                        Button3.GetComponent<Button>().Select();
                        break;
                    case 3:
                        selectedButton = 1;
                        Button1.GetComponent<Button>().Select();
                        break;
                }
            } 
        }
        else if (Input.GetKeyUp(KeyCode.Space)) 
        {
            holdTimer = 0f;

            switch (selectedButton)
            {
                case 1:
                    MenuText.GetComponent<TextMeshProUGUI>().text = "Option 1 pressed";
                    break;
                case 2:
                    MenuText.GetComponent<TextMeshProUGUI>().text = "Option 2 pressed";
                    break;
                case 3:
                    MenuText.GetComponent<TextMeshProUGUI>().text = "Option 3 pressed";
                    break;
            }
        }
    }

    private void UpdateAutomatic()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer < 0)
        {
            moveTimer = 2f;

            switch (selectedButton)
            {
                case 1:
                    selectedButton++;
                    Button2.GetComponent<Button>().Select();
                    break;
                case 2:
                    selectedButton++;
                    Button3.GetComponent<Button>().Select();
                    break;
                case 3:
                    selectedButton = 1;
                    Button1.GetComponent<Button>().Select();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (selectedButton)
            {
                case 1:
                    MenuText.GetComponent<TextMeshProUGUI>().text = "Option 1 pressed";
                    break;
                case 2:
                    MenuText.GetComponent<TextMeshProUGUI>().text = "Option 2 pressed";
                    break;
                case 3:
                    MenuText.GetComponent<TextMeshProUGUI>().text = "Option 3 pressed";
                    break;
            }
        }
    }
}
