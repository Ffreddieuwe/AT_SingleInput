using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DrinkManager;

public class ClockManager : MonoBehaviour
{
    public GameObject hand;
    public float speed = 80;
    private float currentRotation;

    [SerializeField]
    private CameraManager.Panel m_panel;
    [SerializeField]
    private int m_subClockIndex;
    [SerializeField]
    private bool m_confirmationClock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentRotation = 0;

        if (m_confirmationClock)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation += Time.deltaTime * speed;
        if (currentRotation >= 360)
        {
            currentRotation = 0;
        }
        hand.transform.rotation = Quaternion.Euler(0, 0, -currentRotation);

        if (m_panel != Camera.main.GetComponent<CameraManager>().m_selectedPanel)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (m_panel)
            {
                case CameraManager.Panel.Middle:
                    MiddleUpdate();
                    break;
                case CameraManager.Panel.Left:
                    LeftUpdate();
                    break;
                case CameraManager.Panel.Right:
                    RightUpdate();
                    break;
                case CameraManager.Panel.Up:
                    UpUpdate();
                    break;
                case CameraManager.Panel.Down:
                    DownUpdate();
                    break;

            }
        }
    }

    private void MiddleUpdate()
    {
        if (Camera.main.GetComponent<DrinkManager>().m_askingComplete && !m_confirmationClock)
        {
            return;
        }
        
        if (m_confirmationClock)
        {
            if (currentRotation < 180)
            {
                Camera.main.GetComponent<DrinkManager>().ValidateDrink();
            }
            else
            {
                Camera.main.GetComponent<DrinkManager>().CloseConfirm();
            }

            return;
        }

        if (currentRotation < 45 || currentRotation > 315)
        {
            Camera.main.GetComponent<CameraManager>().Move(CameraManager.Direction.Up);
        }
        else if (currentRotation < 135)
        {
            Camera.main.GetComponent<CameraManager>().Move(CameraManager.Direction.Right);
        }
        else if (currentRotation < 215)
        {
            Camera.main.GetComponent<DrinkManager>().ToggleIceCube();
        }
        else
        {
            Camera.main.GetComponent<CameraManager>().Move(CameraManager.Direction.Left);
        }
    }

    private void LeftUpdate()
    {
        if (m_subClockIndex == 0)
        {

        }

        int selectionIndex = 3;

        if (currentRotation < 45 || currentRotation > 315)
        {
            selectionIndex = 0;
        }
        else if (currentRotation < 135)
        {
            selectionIndex = 1;
        }
        else if (currentRotation < 215)
        {
            selectionIndex = 2;
        }

        switch (m_subClockIndex)
        {
            case 0:
                ParentClockUpdate(selectionIndex);
                break;
            case 1:
                GarnishUpdate(selectionIndex);
                break;
            case 2:
                SpiritUpdate(selectionIndex);
                break;
            case 3: 
                SoftUpdate(selectionIndex);
                break;
        }


        
    }

    private void ParentClockUpdate(int selectionIndex)
    {
        switch (selectionIndex)
        {
            case 0:
                // Garnish
                Camera.main.GetComponent<SubClockHandler>().EnableClock(1);
                break;
            case 1:
                Camera.main.GetComponent<CameraManager>().Move(CameraManager.Direction.Right);
                break;
            case 2:
                // Spirit
                Camera.main.GetComponent<SubClockHandler>().EnableClock(2);
                break;
            case 3:
                // Soft
                Camera.main.GetComponent<SubClockHandler>().EnableClock(3);
                break;
        }
    }

    private void GarnishUpdate(int selectionIndex)
    {
        switch (selectionIndex)
        {
            case 0:
                Camera.main.GetComponent<DrinkManager>().AddIngredient(Ingredients.Cherries);
                break;
            case 1:
                Camera.main.GetComponent<SubClockHandler>().EnableClock(0);
                break;
            case 2:
                Camera.main.GetComponent<DrinkManager>().AddIngredient(Ingredients.Lemon);
                break;
            case 3:
                Camera.main.GetComponent<DrinkManager>().AddIngredient(Ingredients.Mint);
                break;
        }
    }

    private void SpiritUpdate(int selectionIndex)
    {
        switch (selectionIndex)
        {
            case 0:
                Camera.main.GetComponent<DrinkManager>().AddIngredient(Ingredients.Whiskey);
                break;
            case 1:
                Camera.main.GetComponent<SubClockHandler>().EnableClock(0);
                break;
            case 2:
                Camera.main.GetComponent<DrinkManager>().AddIngredient(Ingredients.Gin);
                break;
            case 3:
                Camera.main.GetComponent<DrinkManager>().AddIngredient(Ingredients.Vodka);
                break;
        }
        
    }

    private void SoftUpdate(int selectionIndex)
    {
        switch (selectionIndex)
        {
            case 0:
                Camera.main.GetComponent<DrinkManager>().AddIngredient(Ingredients.Cola);
                break;
            case 1:
                Camera.main.GetComponent<SubClockHandler>().EnableClock(0);
                break;
            case 2:
                Camera.main.GetComponent<DrinkManager>().AddIngredient(Ingredients.OrangeJuice);
                break;
            case 3:
                Camera.main.GetComponent<DrinkManager>().AddIngredient(Ingredients.Lemonade);
                break;
        }
    }

    private void RightUpdate()
    {
        if (currentRotation < 45 || currentRotation > 315)
        {
            Camera.main.GetComponent<DrinkManager>().ActivateGlass(Glasses.Rocks);
        }
        else if (currentRotation < 135)
        {
            Camera.main.GetComponent<DrinkManager>().ActivateGlass(Glasses.Martini);
        }
        else if (currentRotation < 215)
        {
            Camera.main.GetComponent<DrinkManager>().ActivateGlass(Glasses.Hiball);
        }
        else
        {
            Camera.main.GetComponent<CameraManager>().Move(CameraManager.Direction.Left);
        }
    }

    private void UpUpdate()
    {
        if (currentRotation < 120)
        {
            Camera.main.GetComponent<DrinkManager>().ResetDrink();
        }
        else if (currentRotation < 240)
        {
            Camera.main.GetComponent<CameraManager>().Move(CameraManager.Direction.Down);
        }
        else
        {
            SceneManager.LoadScene("Main_Menu");
        }
    }

    private void DownUpdate()
    {
        if (currentRotation < 90 || currentRotation > 270)
        {
            Camera.main.GetComponent<CameraManager>().Move(CameraManager.Direction.Up);
            Debug.Log("Top Selected");
        }
        else
        {
            Debug.Log("Ice Selected");
        }
    }

    public void ResetRotation()
    {
        hand.transform.rotation = Quaternion.Euler(0,0,0);
    }
}
