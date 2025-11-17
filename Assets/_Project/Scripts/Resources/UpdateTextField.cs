using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static LocalizationManager;

public class UpdateTextField : MonoBehaviour
{
    [System.Serializable]
    public class TextField {
        public string key;
        public TMPro.TMP_Text value;
    }

    [SerializeField] private List<TextField> textsFields = new List<TextField>();

    private void Awake() {
        GameResources.GameResources.GoldChanged += OnGoldChanged;
        GameResources.GameResources.FoodChanged += OnFoodChanged;
        GameResources.GameResources.PeopleSatisfactionChanged += OnPeopleSatisfactionChanged;
        GameResources.GameResources.CastleStrengthChanged += OnCastleStrengthChanged;

        foreach (var textField in textsFields) {
            if (textField.key == "gold") textField.value.text = GameResources.GameResources.Gold.ToString();
            else if (textField.key == "food") textField.value.text = GameResources.GameResources.Food.ToString();
            else if (textField.key == "satisfaction") textField.value.text = GameResources.GameResources.PeopleSatisfaction.ToString();
            else if (textField.key == "strength") textField.value.text = GameResources.GameResources.CastleStrength.ToString();
        }
    }
    private void OnGoldChanged(int newValue) {
        foreach (var textField in textsFields) {
            if(textField.key == "gold") textField.value.text = newValue.ToString();
        }
    }

    private void OnFoodChanged(int newValue) {
        foreach (var textField in textsFields) {
            if (textField.key == "food") textField.value.text = newValue.ToString();
        }
    }

    private void OnPeopleSatisfactionChanged(int newValue) {
        foreach (var textField in textsFields) {
            if (textField.key == "satisfaction") textField.value.text = newValue.ToString();
        }
    }

    private void OnCastleStrengthChanged(int newValue) {
        foreach (var textField in textsFields) {
            if (textField.key == "strength") textField.value.text = newValue.ToString();
        }
    }
}
