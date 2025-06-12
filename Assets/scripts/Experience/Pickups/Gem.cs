public class Gem : PickupBase, IPickupInterface
{
    public int experienceCount;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceCount);
    }
}
