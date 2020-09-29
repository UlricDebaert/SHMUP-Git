using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

namespace DeepSea.Scoreboards
{
    public class Scoreboard : MonoBehaviour
    {
        [SerializeField] private int maxScoreboardEntries = 5;
        [SerializeField] private Transform highscoresHolderTransform = null;
        [SerializeField] private GameObject scoreboardEntryObject = null;

        [Header("Test")]
        [SerializeField] ScoreboardEntryData testEntrydata = new ScoreboardEntryData();

        private string SavePath => $"{Application.persistentDataPath}/highscores.json";

        public TextMeshProUGUI nameText;

        //placeholder
        int score;

        private void Start()
        {
            nameText = GameObject.FindGameObjectWithTag("nameText").GetComponent<TextMeshProUGUI>();

            ScoreboardSaveData savedScores = GetSavedScore();
            UpdateUI(savedScores);

            SaveScore(savedScores);

        }

        private void Update()
        {
            testEntrydata.entryName = nameText.text;
        }

        [ContextMenu("Add Test Entry")]
        public void AddTestEntry()
        {
            AddEntry(testEntrydata);
           
        }

        private ScoreboardSaveData GetSavedScore()
        {
            if (!File.Exists(SavePath))
            {
                File.Create(SavePath).Dispose();
                return new ScoreboardSaveData();
            }

            using (StreamReader stream = new StreamReader(SavePath))
            {
                string json = stream.ReadToEnd();

                return JsonUtility.FromJson<ScoreboardSaveData>(json);
            }        
        }

        private void SaveScore(ScoreboardSaveData scoreboardSaveData)
        {
            using (StreamWriter stream = new StreamWriter(SavePath))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json);
            }
        }

        private void UpdateUI(ScoreboardSaveData savedScores)
        {
            foreach(Transform child in highscoresHolderTransform)
            {
                Destroy(child.gameObject);

            }

            foreach(ScoreboardEntryData highscore in savedScores.highscores)
            {
                Instantiate(scoreboardEntryObject, highscoresHolderTransform).GetComponent<ScoreboardEntryUI>().Initialise(highscore);
            }
        }

        public void AddEntry(ScoreboardEntryData scoreboardEntryData)
        {
            ScoreboardSaveData savedScores = GetSavedScore();

            bool scoreAdded = false;

            for (int i = 0; 1 < savedScores.highscores.Count; i++)
            {
                if (scoreboardEntryData.entryScore > savedScores.highscores[i].entryScore)
                {
                    savedScores.highscores.Insert(i, scoreboardEntryData);
                    scoreAdded = true;
                    break;
                }
            }

            if(!scoreAdded && savedScores.highscores.Count < maxScoreboardEntries)
            {
                savedScores.highscores.Add(scoreboardEntryData);
            }

            if(savedScores.highscores.Count > maxScoreboardEntries)
            {
                savedScores.highscores.RemoveRange(maxScoreboardEntries, savedScores.highscores.Count - maxScoreboardEntries);
            }

            UpdateUI(savedScores);

            SaveScore(savedScores);
        }
    }
}

