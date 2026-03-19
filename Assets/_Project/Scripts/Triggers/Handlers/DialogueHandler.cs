using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
namespace Game
{
    internal class DialogueHandler : MonoBehaviour, ITriggerHandler
    {
        [Header("Dialogue Settings")]
        //[SerializeField] private string defaultDialogue;
        [SerializeField] private List<InfluenceArea> influenceArias;
        private NPCPrefabFactory npcFactory;

        [Inject]
        private void Construct(NPCPrefabFactory npcFActory)
        {
            this.npcFactory = npcFActory;
            foreach (var npc in npcFactory.instances) {
                influenceArias.Add(npc.Value.GetComponentInChildren<InfluenceArea>());
            }
        }

        private void OnEnable()
        {
            foreach (var area in influenceArias)
            {
                area.OnEventTriggered += Handle;   
            }
        }


        private void OnDisable()
        {
            foreach (var area in influenceArias)
            {
                area.OnEventTriggered -= Handle;
            }
        }

        public void Handle(TriggerEvent eventData)
        {
            DialogueManager _dialogueManager = DialogueManager.Instance;
            if (eventData.IsEnteracted && !_dialogueManager.IsInDialogue)
            {

                DialogueGraph dialogueGraph = GetDialogueGraph(eventData.TriggerObj);
                _dialogueManager.StartDialogue(dialogueGraph);

                /*var activeGroups = QuestCollection.GetActiveQuestGroups();
                QuestGroup groupToUpdate = null;
                QuestTask taskToComplete = null;
                DialogueGraph dialogueGraph;

                foreach (var group in activeGroups)
                {
                    taskToComplete = group.Tasks
                        .Where(t => !t.IsDone && t.ForNPS == eventData.TriggerObj.name && t.CanComplete())
                        .OrderBy(t => t.Id)
                        .FirstOrDefault();

                    if (taskToComplete != null)
                    {
                        groupToUpdate = group;
                        break;
                    }
                }

                if (groupToUpdate != null)
                {
                    groupToUpdate.GetCurrentTask().CompleteTask();
                    groupToUpdate = UpdateGroupState(groupToUpdate);
                    var originalGroup = QuestCollection.GetAllQuestGroups()
                        .FirstOrDefault(g => g.Id == groupToUpdate.Id);
                    originalGroup?.CopyFrom(groupToUpdate);

                    dialogueGraph = GetDialogueGraph(taskToComplete.RequeredDialog);
                    _dialogueManager.StartDialogue(dialogueGraph);
                    return;
                }

                // Проверка на старт новых квестов
                var currentDay = QuestCollection.GetCurrentDayData();
                var availableGroups = currentDay != null
                    ? currentDay.Quests.Where(q => q.CheckOpen(eventData.TriggerObj.name)).ToList()
                    : new List<QuestGroup>();

                if (availableGroups.Count > 0)
                {
                    var group = availableGroups.First();
                    group.StartQuest();

                    dialogueGraph = GetDialogueGraph(group.OpenDialog);
                    _dialogueManager.StartDialogue(dialogueGraph);
                    return;
                }

                dialogueGraph = GetDialogueGraph(eventData.TriggerObj);
                _dialogueManager.StartDialogue(dialogueGraph);*/
            }
        }

        private DialogueGraph GetDialogueGraph(GameObject obj)
        {
            string dialoguePath = "Dialogues/NPC/" + obj.name.ToLower();

            TextAsset textAsset = Resources.Load<TextAsset>(dialoguePath);
            if (textAsset == null)
            {
                Debug.LogError($"StartDayDialogueHandler: Failed to load dialogue graph at path: {dialoguePath}");
                return null;
            }

            try
            {
                var dialogueGraph = JsonConvert.DeserializeObject<DialogueGraph>(textAsset.text);
                return dialogueGraph;
            }
            catch (JsonException ex)
            {
                Debug.LogError($"StartDayDialogueHandler: Failed to load dialogue graph at path: {dialoguePath}");
                return null;
            }
        }
    }
}

