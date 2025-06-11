using System.Collections.Generic;
using UnityEngine;

public class DropRates : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject dropPrefab;
        public float dropRate;
    }

    public List<Drops> drops;

    void OnDestroy()
    {
        drops.Sort((x, y) => x.dropRate.CompareTo(y.dropRate));
        float randNum = Random.Range(0f, 100f);
        Debug.Log("Random num gem gave: " + randNum);
        float probability;

        foreach (Drops drop in drops)
        {
            probability = drop.dropRate;
            if (randNum <= probability)
            {
                Instantiate(drop.dropPrefab, transform.position, Quaternion.identity);
                break;
            }
        }
    }
}
