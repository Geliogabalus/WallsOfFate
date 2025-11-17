using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour {
    [System.Serializable]
    public class LocalizedText {
        public string key;
        public TMPro.TMP_Text textField;
    }

    [Header("Настройки локализации")]
    [SerializeField] private string localizationFileName = "localization";
    [SerializeField] private List<LocalizedText> localizedTexts = new List<LocalizedText>();
    [SerializeField] private SettingsMenu settingsMenu; 

    private string currentLanguage;

    private void Awake() {
        InitializeLocalization();

        if (settingsMenu != null) {
            settingsMenu.OnLanguageChanged += OnLanguageChanged;
        }
    }

    private void InitializeLocalization() {
        currentLanguage = PlayerPrefs.GetString("CurrentLanguage", "en");

        LoadLocalization();
    }

    private void OnLanguageChanged(string languageValue) {
            string newLanguage = languageValue;

            if (newLanguage != currentLanguage) {
                currentLanguage = newLanguage;
                LoadLocalization();
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
            for (int i = 0; i < localizedTexts.Count; i++) {
                localizedTexts[i].textField.text = localisationData.Find(el => el.key == localizedTexts[i].key).value;
            }

            ////Debug.Log($"Локализация загружена: {localisationData.Count} записей");
        }
        catch (System.Exception e) {
            Debug.LogError($"Ошибка парсинга JSON: {e.Message}");
        }
    }

    private bool ParseLocalisationFile(out List<LocalizationItem> localisationData) {
        localisationData = null;
        if (settingsMenu == null)
            currentLanguage = PlayerPrefs.GetString("CurrentLanguage", "en");
        
        string result = currentLanguage.Replace("\\", "").Replace("/", "");
        string path = $"Localization/{result}/ui/{localizationFileName}";

        TextAsset jsonFile = Resources.Load<TextAsset>(path);

        if (jsonFile == null) {
            //Debug.LogError($"Файл локализации не найден: {path}");

            if (currentLanguage != "en") {
                string fallbackPath = $"Localization/en/ui/{localizationFileName}";
                jsonFile = Resources.Load<TextAsset>(fallbackPath);

                if (jsonFile == null) {
                    Debug.LogError($"Fallback файл локализации не найден: {fallbackPath}");
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

    private void OnDestroy() {
        if (settingsMenu != null) {
            settingsMenu.OnLanguageChanged -= OnLanguageChanged;
        }
    }
}

[System.Serializable]
public class LocalizationItem {
    public string key;
    public string value;
}