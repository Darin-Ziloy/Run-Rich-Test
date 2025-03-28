using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevelNumber = 1;
    public Level[] levels;
    public Level currentLevel;
    public static LevelManager instance {get; private set;}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Length)
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.Save();
        }

        currentLevelNumber = PlayerPrefs.GetInt("Level", 1);
        currentLevel = Instantiate(levels[levelIndex], Vector3.zero, Quaternion.identity);
    }
}