using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/Map")]
public class MapData : ScriptableObject
{
    public string       mapName;
    public int          mapId;
    public GameObject[] playerWeapons;
    public float        cameraFOV;
    public float        timeBeforeBoss;
}
