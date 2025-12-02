using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Data
{
    public static class ResourceSaveLoader
    {
        public static bool LoadData()
        {
            if (Repository.TryGetData("GameResources", out ResourceData data))
            {
                Player.Resources.Gold = data.Gold;
                Player.Resources.Food = data.Food;
                Player.Resources.PeopleSatisfaction = data.PeopleSatisfaction;
                Player.Resources.CastleStrength = data.CastleStrength;
                Debug.Log("Loaded resources data");
                return true;
            }
            return false;
        }

        public static void LoadDefaultData()
        {
            TextAsset textAsset = Resources.Load<TextAsset>("Data/DefaultResources");
            if (textAsset == null)
            {
                Debug.LogError("Default resources file not found!");
                return;
            }

            try
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Error
                };

                var defaultData = JsonConvert.DeserializeObject<ResourceData>(textAsset.text, settings);
                Player.Resources.Gold = defaultData.Gold;
                Player.Resources.Food = defaultData.Food;
                Player.Resources.PeopleSatisfaction = defaultData.PeopleSatisfaction;
                Player.Resources.CastleStrength = defaultData.CastleStrength;    
            }
            catch (JsonException ex)
            {
                Debug.LogError($"JSON error: {ex.Message}");
            }
        }

        public static void SaveData()
        {
            var data = new ResourceData
            {
                Gold = Player.Resources.Gold,
                Food = Player.Resources.Food,
                PeopleSatisfaction = Player.Resources.PeopleSatisfaction,
                CastleStrength = Player.Resources.CastleStrength
            };
            Repository.SetData("GameResources", data);
            Debug.Log("Saved resources data");
        }
    }

    [Serializable]
    public class ResourceData
    {
        public int Gold;
        public int Food;
        public int PeopleSatisfaction;
        public int CastleStrength;
    }
}