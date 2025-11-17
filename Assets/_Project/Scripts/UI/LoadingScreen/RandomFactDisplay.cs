using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static LocalizationManager;

public class RandomFactDisplay : MonoBehaviour
{
    [Header("Настройки фактов")]
    [Tooltip("Компонент TextMeshPro для отображения факта.")]
    public TMP_Text factText;

    [Tooltip("Массив фактов, из которого выбирается один случайный.")]
    [TextArea(2, 5)]
    public List<string> facts;

    [Header("Настройки локализации")]
    [SerializeField] private string localizationFileName = "localization";

    private string currentLanguage;
    private static int currentIndex = 0;
    private static bool allFactsShown = false;


    private void OnEnable()
    {
        currentLanguage = PlayerPrefs.GetString("CurrentLanguage", "en");
        LoadLocalization();

        RefreshFact();
    }

    public void RefreshFact()
    {
        if (factText != null && facts != null && facts.Count > 0)
        {
            string selectedFact;

            if (!allFactsShown)
            {
                selectedFact = facts[currentIndex];
                currentIndex++;

                if (currentIndex >= facts.Count)
                {
                    allFactsShown = true;
                }
            }
            else
            {
                int randomIndex = Random.Range(0, facts.Count);
                selectedFact = facts[randomIndex];
            }

            factText.text = selectedFact;
        }
    }

    private void LoadLocalization() {
        List<LocalizationItem> localisationData = null;
        bool flowControl = ParseLocalisationFile(out localisationData);
        if (!flowControl) {
            return;
        }

        ChangeLocalisation(localisationData);
    }

    private void ChangeLocalisation(List<LocalizationItem> localisationData) {
        try {
            facts.Clear();

            foreach (var localizedFact in localisationData) {
                if (!localizedFact.key.StartsWith("continue-butt")) { facts.Add(localizedFact.value); }
            }

            ////Debug.Log($"Локализация загружена: {localisationData.Count} записей");
        }
        catch (System.Exception e) {
            //Debug.LogError($"Ошибка парсинга JSON: {e.Message}");
        }
    }

    private bool ParseLocalisationFile(out List<LocalizationItem> localisationData) {
        localisationData = null;
        string result = currentLanguage.Replace("\\", "").Replace("/", "");
        string path = $"Localization/{result}/ui/{localizationFileName}";
        
        TextAsset jsonFile = Resources.Load<TextAsset>(path);

        if (jsonFile == null) {
            //Debug.LogError($"Файл локализации не найден: {path}");

            if (currentLanguage != "en") {
                string fallbackPath = $"Localization/en/ui/{localizationFileName}";
                jsonFile = Resources.Load<TextAsset>(fallbackPath);

                if (jsonFile == null) {
                    //Debug.LogError($"Fallback файл локализации не найден: {fallbackPath}");
                    return false;
                }
            }
            else {
                return false;
            }
        }
        List<LocalizationItem> data = JsonConvert.DeserializeObject<List<LocalizationItem>>(jsonFile.text);
        localisationData = data;

        return true;
    }

}
