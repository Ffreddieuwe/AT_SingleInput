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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedButton = 1;
        Button1.GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
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
