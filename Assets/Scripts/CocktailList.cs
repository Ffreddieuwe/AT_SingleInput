using UnityEngine;

public class CocktailList : MonoBehaviour
{
    public struct Cocktail
    {
        public string name;
        public DrinkManager.Ingredients[] ingredients;
        public bool ice;
        public DrinkManager.Glasses glass;
    }
}
