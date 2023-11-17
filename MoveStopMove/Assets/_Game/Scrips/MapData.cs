using UnityEngine;

public class MapData : MonoBehaviour
{
    [SerializeField] private Transform[] listPointSpawn;
    [SerializeField] private Transform pointPlayer;
    [SerializeField] private int maxBot;

    public Transform[] ListPointSpawn => listPointSpawn;
    public Vector3 PointPlayer => pointPlayer.position;
    public int MaxBot => maxBot;
}
