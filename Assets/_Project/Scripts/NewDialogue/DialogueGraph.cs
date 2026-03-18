using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class DialogueGraph : ScriptableObject
    {
        [SerializeField]
        private string DialogueName;

        [SerializeField]
        public List<Node> sentences = new List<Node>();

        [SerializeField]
        public string Portrait;

        public string GetName()
        {
            return DialogueName;
        }

        [Serializable]
        public class Node
        {
            #region GraphVariables 
            public int id;
            public bool IsMainCharacter = false;
            public string CharName;
            public string Text;
            public int NextNodeID = -1;
            public bool isOption = false;
            #endregion

            #region GameVariables
            public bool StartMinigame = false;
            public MiniGame.MiniGameType MiniGameType = MiniGame.MiniGameType.None;
            public string MiniGameSceneName = "";

            [SerializeField, TextArea(3, 5)] private string _parametersJson = "{}";

            private Dictionary<string, object> _cachedParams;

            public Dictionary<string, object> MinigameParams
            {
                get
                {
                    if (_cachedParams == null || _cachedParams.Count == 0)
                    {
                        try
                        {
                            _cachedParams = JsonConvert.DeserializeObject<Dictionary<string, object>>(_parametersJson);
                        }
                        catch
                        {
                            _cachedParams = new Dictionary<string, object>();
                        }
                    }
                    return _cachedParams;
                }
                set
                {
                    _cachedParams = value;
                    _parametersJson = JsonConvert.SerializeObject(value, Formatting.Indented);
                }
            }

            #region Resources
            public int Gold = 0;
            public int Food = 0;
            public int PeopleSatisfaction = 0;
            public int CastleStrength = 0;
            #endregion

            #endregion
            public Node(int _id, bool _IsMainCharacter, string _CharName, string _Text)
            {
                id = _id;
                IsMainCharacter = _IsMainCharacter;
                CharName = _CharName;
                Text = _Text;
            }

            public Node() { }
        }
    }
}
