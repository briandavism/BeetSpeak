using Configs;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Button load;

    public string jsonFile = "test";

    public void Awake()
    {
        load.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        var json = Resources.Load(jsonFile) as TextAsset;
        if (json != null)
        {
            var songConfig = JsonConvert.DeserializeObject<SongConfig>(json.text);
            if (songConfig != null)
            {
                MusicCore.Instance.InitScore(songConfig);
                Debug.Log("Init score success");
            }
            else
            {
                Debug.LogError("Failed to parse " + jsonFile + " to config");
            }
        }
        else
        {
            Debug.LogError("No resource with name " + jsonFile + " found");
        }
    }
}