using UnityEngine;

public class CocktailManager : MonoBehaviour
{
    public TextAsset textJSON;
    public Cocktails m_cocktails;

    [System.Serializable]
    public class CocktailData
    {
        public string name;
        public int[] ingredients;
        public bool ice;
        public int glass;
    }

    [System.Serializable]
    public class Cocktails
    {
        public CocktailData[] cocktailData;
    }

    private void Start()
    {
        ReadJSON();
    }

    public void ReadJSON()
    {
        m_cocktails = JsonUtility.FromJson<Cocktails>(textJSON.text);
    }
}
