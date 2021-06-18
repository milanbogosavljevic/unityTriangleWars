using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadSystem : MonoBehaviour
{
    private GameData _data;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SaveLoadSystem");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        _data = new GameData();
        LoadFile();
    }

    public GameData GetGameData()
    {
        return _data;
    }

    public void SaveScore(int scoreInt)
    {
        _data.highScore = scoreInt;
        SaveFile();
    }

    public void SaveStats(float bulletsFired, float enemiesHit, float accuracy)
    {
        _data.bulletsFired = bulletsFired;
        _data.enemiesHit = enemiesHit;
        _data.accuracy = accuracy;
        SaveFile();
    }

    public void SaveAchievement(string achievement)
    {
        switch(achievement)
        {
            case "bronzeEnemiesKilled":
                _data.bronzeEnemiesKilled = true;
                break;
            case "silverEnemiesKilled":
                _data.silverEnemiesKilled = true;
                break;    
            case "goldEnemiesKilled":
                _data.goldEnemiesKilled = true;
                break;
            case "bronzeHitsInRow":
                _data.bronzeHitsInRow = true;
                break;
            case "silverHitsInRow":
                _data.silverHitsInRow = true;
                break;    
            case "goldHitsInRow":
                _data.goldHitsInRow = true;
                break;         
        }
        SaveFile();
    }

    public void SaveDontShowInfo(bool dontShow)
    {
        _data.dontShowInfo = dontShow;
        SaveFile();
    }

    public void SaveSoundState(bool isOn)
    {
        _data.soundIsOn = isOn;
        SaveFile();
    }

    public void SaveMusicState(bool isOn)
    {
        _data.musicIsOn = isOn;
        SaveFile();
    }

    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;
 
        if(File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);
 
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, _data);
        file.Close();
    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;
 
        if(File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.Log("File not found");
            return;
        }
 
        BinaryFormatter bf = new BinaryFormatter();
        _data = (GameData) bf.Deserialize(file);
        file.Close();
    }
}
