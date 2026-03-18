using UnityEngine;

public class DrinkManager : MonoBehaviour
{
    public enum Ingredients
    {
        None = 0,
        Gin = 1,
        Vodka = 2,
        Whiskey = 3,
        Lemonade = 4,
        Cola = 5,
        OrangeJuice = 6,
        Mint = 7,
        Lemon = 8,
        Cherries = 9,
    }

    public enum Glasses
    {
        None = 0,
        Martini = 1,
        Hiball = 2,
        Rocks = 3,
    }

    public GameObject[] m_slots;
    public GameObject[] m_slotsSection2;
    private Ingredients[] m_slotIngredients = { Ingredients.None, Ingredients.None, Ingredients.None };
    public GameObject[] m_glasses;
    public GameObject[] m_glassesSection2;
    private Glasses m_activeGlass;
    [SerializeField]
    private Sprite[] m_ingredientSprites;
    [SerializeField]
    private GameObject m_iceCubes;

    private int m_currentOrderIndex = 0;

    [SerializeField]
    private Sprite[] m_cocktailSprites;

    public bool m_shouldAskComplete;
    public bool m_askingComplete;

    [SerializeField]
    GameObject m_confirmationClock;
    [SerializeField]
    GameObject m_confirmationPanel;
    [SerializeField]
    GameObject[] m_customerOrder;
    [SerializeField]
    GameObject m_success;
    [SerializeField]
    GameObject m_failure;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetGlass();
        m_shouldAskComplete = false;
        m_askingComplete = false;
        m_iceCubes.SetActive(false);
        m_confirmationPanel.SetActive(false);

        m_success.SetActive(false);
        m_failure.SetActive(false);

        NewOrder();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_shouldAskComplete && Camera.main.GetComponent<CameraManager>().m_selectedPanel == CameraManager.Panel.Middle)
        {
            m_shouldAskComplete = false;
            m_askingComplete = true;
            m_confirmationPanel.SetActive(true);
            m_confirmationClock.SetActive(true);
        }
    }

    public void ResetDrink()
    {
        ResetGlass();

        for (int i = 0; i < m_slotIngredients.Length; i++)
        {
            m_slotIngredients[i] = Ingredients.None;
            m_slots[i].GetComponent<SpriteRenderer>().sprite = m_ingredientSprites[(int)Ingredients.None];
            m_slotsSection2[i].GetComponent<SpriteRenderer>().sprite = m_ingredientSprites[(int)Ingredients.None];
            
        }

        m_iceCubes.SetActive(false);
        m_confirmationPanel.SetActive(false);
        m_success.SetActive(false);
        m_failure.SetActive(false);
        foreach (var item in m_customerOrder)
        {
            item.SetActive(true);
        }
    }

    public void ActivateGlass(Glasses glass)
    {
        ResetGlass();
        m_activeGlass = glass;
        Camera.main.GetComponent<DrinkManager>().m_shouldAskComplete = true;
        // Slot 0 is martini, slot 1 is hiball, slot 2 is rocks.

        if (glass == Glasses.Martini)
        {
            m_glasses[0].SetActive(true);
            m_glassesSection2[0].SetActive(true);
        }
        else if (glass == Glasses.Hiball)
        {
            m_glasses[1].SetActive(true);
            m_glassesSection2[1].SetActive(true);
        }
        else
        {
            m_glasses[2].SetActive(true);
            m_glassesSection2[2].SetActive(true);
        }
    }

    private void ResetGlass()
    {
        m_activeGlass = Glasses.None;
        for (int i = 0; i < m_glasses.Length; i++)
        {
            m_glasses[i].SetActive(false);
            m_glassesSection2[i].SetActive(false);
        }
    }

    public void AddIngredient(Ingredients newIngredient)
    {
        for (int i = 0; i < m_slotIngredients.Length; i++)
        {
            if (m_slotIngredients[i] == Ingredients.None)
            {
                m_slotIngredients[i] = newIngredient;
                m_slots[i].GetComponent<SpriteRenderer>().sprite = m_ingredientSprites[(int)newIngredient];
                m_slotsSection2[i].GetComponent<SpriteRenderer>().sprite = m_ingredientSprites[(int)newIngredient];
                Camera.main.GetComponent<DrinkManager>().m_shouldAskComplete = true;
                break;
            }
        }
    }

    public void ToggleIceCube()
    {
        m_iceCubes.SetActive(!m_iceCubes.activeSelf);
        Camera.main.GetComponent<DrinkManager>().m_shouldAskComplete = true;
        m_shouldAskComplete = true;
    }

    public void ValidateDrink()
    {
        CocktailManager.CocktailData orderData = gameObject.GetComponent<CocktailManager>().m_cocktails.cocktailData[m_currentOrderIndex];

        m_confirmationClock.SetActive(false);
        m_confirmationPanel.SetActive(false);
        foreach (var item in m_customerOrder)
        {
            item.SetActive(false);
        }

        m_askingComplete = false;

        if (m_activeGlass != (Glasses)orderData.glass)
        {
            m_failure.SetActive(true);
            return;
        }

        if (m_iceCubes.activeSelf != orderData.ice)
        {
            m_failure.SetActive(true);
            return;
        }

        if (orderData.ingredients.Length == 0)
        {
            if (m_slotIngredients[0] != Ingredients.None)
            {
                m_failure.SetActive(true);
                return;
            }
        }
        else if (orderData.ingredients.Length == 1)
        {
            if (m_slotIngredients[0] != (Ingredients)orderData.ingredients[0] || m_slotIngredients[1] != Ingredients.None)
            {
                m_failure.SetActive(true);
                return;
            }
        }
        else if (orderData.ingredients.Length > 1)
        {
            bool foundIng1 = false;
            bool foundIng2 = false;
            bool foundIng3 = false;

            for (int i = 0; i < m_slotIngredients.Length; i++)
            {
                if (m_slotIngredients[i] == (Ingredients)orderData.ingredients[0])
                {
                    foundIng1 = true;
                }
                else if (m_slotIngredients[i] == (Ingredients)orderData.ingredients[1])
                {
                    foundIng2 = true;
                }
                else if (m_slotIngredients[i] == (Ingredients)orderData.ingredients[2])
                {
                    foundIng3 = true;
                }
            }

            if (!(foundIng1 && foundIng2 && foundIng3))
            {
                m_failure.SetActive(true);
                return;
            }
        }
        

            m_success.SetActive(true);
    }

    public void CloseConfirm()
    {
        m_confirmationClock.SetActive(false);
        m_confirmationPanel.SetActive(false);
        m_askingComplete = false;
    }

    private void NewOrder()
    {
        gameObject.GetComponent<CocktailManager>().ReadJSON();
        m_currentOrderIndex = Random.Range(0, gameObject.GetComponent<CocktailManager>().m_cocktails.cocktailData.Length);

        foreach(var item in m_customerOrder)
        {
            item.GetComponent<SpriteRenderer>().sprite = m_cocktailSprites[m_currentOrderIndex];
        }
    }
}
