using UnityEngine;

public class FieldScript : AOEBase
{
    private static FieldScript instance;

    void Awake()
    {
        weaponData = Instantiate(weaponData);

        if (instance != null)
        {
            Destroy(gameObject); // Already exists
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Optional
    }

    protected override void Start()
    {
        base.Start();
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
