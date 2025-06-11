using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection instance;
    public PlayerScriptableObject characterData;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static PlayerScriptableObject GetData()
    {
        return instance.characterData;
    }

    public void SelectCharacter(PlayerScriptableObject character)
    {
        characterData = character;
    }

    public void DestroyInstance()
    {
        instance = null;
        Destroy(gameObject);
    }
}
