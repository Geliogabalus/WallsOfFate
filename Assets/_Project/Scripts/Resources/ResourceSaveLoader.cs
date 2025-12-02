using Newtonsoft.Json;
using UnityEngine;

namespace Game
{
    public class ResourceSaveLoader : ISaveLoader
    {
        public bool LoadData()
        {
            if (Repository.TryGetData("GameResources", out ResourceData data))
            {
                Resources.Gold = data.Gold;
                Resources.Food = data.Food;
                Resources.PeopleSatisfaction = data.PeopleSatisfaction;
                Resources.CastleStrength = data.CastleStrength;
                ////Debug.Log("Loaded resources data");
                return true;
            }
            return false;
        }

        public void LoadDefaultData()
        {
            TextAsset textAsset = UnityEngine.Resources.Load<TextAsset>("SavsInformation/GameResources/DefaultResources");
            if (textAsset == null)
            {
                //Debug.LogError("Default resources file not found!");
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
                Resources.Gold = defaultData.Gold;
                Resources.Food = defaultData.Food;
                Resources.PeopleSatisfaction = defaultData.PeopleSatisfaction;
                Resources.CastleStrength = defaultData.CastleStrength;    
            }
            catch (JsonException ex)
            {
                //Debug.LogError($"JSON error: {ex.Message}");
            }
        }

        public void SaveData()
        {
            var data = new ResourceData
            {
                Gold = Resources.Gold,
                Food = Resources.Food,
                PeopleSatisfaction = Resources.PeopleSatisfaction,
                CastleStrength = Resources.CastleStrength
            };
            Repository.SetData("GameResources", data);
            ////Debug.Log("Saved resources data");
        }
    }

    [System.Serializable]
    public class ResourceData
    {
        public int Gold;
        public int Food;
        public int PeopleSatisfaction;
        public int CastleStrength;
    }
}