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
        Game.Resources.GoldChanged += OnGoldChanged;
        Game.Resources.FoodChanged += OnFoodChanged;
        Game.Resources.PeopleSatisfactionChanged += OnPeopleSatisfactionChanged;
        Game.Resources.CastleStrengthChanged += OnCastleStrengthChanged;

        foreach (var textField in textsFields) {
            if (textField.key == "gold") textField.value.text = Game.Resources.Gold.ToString();
            else if (textField.key == "food") textField.value.text = Game.Resources.Food.ToString();
            else if (textField.key == "satisfaction") textField.value.text = Game.Resources.PeopleSatisfaction.ToString();
            else if (textField.key == "strength") textField.value.text = Game.Resources.CastleStrength.ToString();
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
