using TMPro;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public GameObject hand;
    public float speed = 40f;
    private float currentRotation;
    public GameObject testText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentRotation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation += Time.deltaTime * speed;
        if (currentRotation >=360)
        {
            currentRotation = 0;   
        }
        hand.transform.rotation = Quaternion.Euler(0, 0, -currentRotation);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentRotation < 180)
            {
                testText.GetComponent<TextMeshProUGUI>().text = "Right Selected";
            }
            else
            {
                testText.GetComponent<TextMeshProUGUI>().text = "Left Selected";
            }
        }
    }
}
