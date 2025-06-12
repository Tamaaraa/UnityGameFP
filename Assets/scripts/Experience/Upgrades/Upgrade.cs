[System.Serializable]
public class UpgradeOption
{
    public string title;
    public System.Action Apply;

    public UpgradeOption(string title, System.Action apply)
    {
        this.title = title;
        this.Apply = apply;
    }
}
