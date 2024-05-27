using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    public List<LevelData> levels;
    private string dataPath;

    void Start()    //FindObjectOfType<GameDataManager>().CompleteLevel(currentLevelNumber, earnedStars);

    {
        dataPath = Application.persistentDataPath + "/gamedata.json";
        LoadGameData();
    }

    public void CompleteLevel(int levelNumber, int starsEarned)
    {
        LevelData level = levels.Find(l => l.levelNumber == levelNumber);
        if (level != null)
        {
            level.starsEarned = Mathf.Max(level.starsEarned, starsEarned); // En yüksek yýldýz sayýsýný saklar
            if (level.starsEarned >= 2 && levelNumber < levels.Count)
            {
                levels[levelNumber].isUnlocked = true; // Bir sonraki seviyeyi açar
            }
            SaveGameData();
        }
    }

    private void SaveGameData()
    {
        List<LevelDataSerializable> serializableData = new List<LevelDataSerializable>();
        foreach (var level in levels)
        {
            serializableData.Add(new LevelDataSerializable(level));
        }

        string json = JsonUtility.ToJson(new GameData { levels = serializableData }, true);
        File.WriteAllText(dataPath, json);
    }

    private void LoadGameData()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            for (int i = 0; i < levels.Count; i++)
            {
                if (i < gameData.levels.Count)
                {
                    levels[i].starsEarned = gameData.levels[i].starsEarned;
                    levels[i].isUnlocked = gameData.levels[i].isUnlocked;
                }
            }
        }
        else
        {
            // Ýlk seviye her zaman açýk olacak
            if (levels.Count > 0)
            {
                levels[0].isUnlocked = true;
            }
        }
    }
}

[System.Serializable]
public class LevelDataSerializable
{
    public int levelNumber;
    public int starsEarned;
    public bool isUnlocked;

    public LevelDataSerializable(LevelData levelData)
    {
        levelNumber = levelData.levelNumber;
        starsEarned = levelData.starsEarned;
        isUnlocked = levelData.isUnlocked;
    }
}

[System.Serializable]
public class GameData
{
    public List<LevelDataSerializable> levels;
}
