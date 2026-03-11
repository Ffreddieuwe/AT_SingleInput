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
        Martini,
        Hiball,
        Rocks,
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

    public bool m_shouldAskComplete;
    public bool m_askingComplete;

    [SerializeField]
    GameObject m_confirmationClock;
    [SerializeField]
    GameObject m_confirmationPanel;
    [SerializeField]
    GameObject m_customerOrder;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (m_shouldAskComplete)
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
        m_customerOrder.SetActive(true);
    }

    public void ActivateGlass(Glasses glass)
    {
        ResetGlass();
        m_activeGlass = glass;
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
                Debug.Log("hello");
                m_slotIngredients[i] = newIngredient;
                m_slots[i].GetComponent<SpriteRenderer>().sprite = m_ingredientSprites[(int)newIngredient];
                m_slotsSection2[i].GetComponent<SpriteRenderer>().sprite = m_ingredientSprites[(int)newIngredient];
                break;
            }
        }
    }

    public void ToggleIceCube()
    {
        m_iceCubes.SetActive(!m_iceCubes.activeSelf);
        m_shouldAskComplete = true;
    }

    public void ValidateDrink()
    {
        m_confirmationClock.SetActive(false);
        m_confirmationPanel.SetActive(false);
        m_customerOrder.SetActive(false);

        m_askingComplete = false;

        if (m_activeGlass != Glasses.Hiball)
        {
            m_failure.SetActive(true);
            return;
        }

        if (!m_iceCubes.activeSelf)
        {
            m_failure.SetActive(true);
            return;
        }

        bool foundVodka = false;
        bool foundOJ = false;
        bool foundLemon = false;

        for (int i = 0; i < m_slotIngredients.Length; i++)
        {
            if (m_slotIngredients[i] == Ingredients.Vodka)
            {
                foundVodka = true;
            }
            else if (m_slotIngredients[i] == Ingredients.Lemon)
            {
                foundLemon = true;
            }
            else if (m_slotIngredients[i] == Ingredients.OrangeJuice)
            {
                foundOJ = true;
            }
        }

        if (!(foundOJ && foundVodka && foundLemon))
        {
            m_failure.SetActive(true);
            return;
        }

        m_success.SetActive(true);
    }

    public void CloseConfirm()
    {
        m_confirmationClock.SetActive(false);
        m_confirmationPanel.SetActive(false);
        m_askingComplete = false;
    }
}
