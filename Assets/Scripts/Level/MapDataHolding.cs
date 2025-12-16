using UnityEngine;

public class MapDataHolding : MonoBehaviour
{

    public static MapDataHolding SINGLETON;
    public        MapData        currentmapData;
    void Start()
    {
        if (SINGLETON == null)
        {
            SINGLETON = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
