using UnityEngine;

public class CamFlower : Singleton<CamFlower>
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 camRotation;

    private int space = 20;
    private int minSpace = 20;
    private int maxSpace = 30;
    private Vector3 offset;

    void LateUpdate()
    {
        transform.position = target.position + offset;
        transform.rotation = Quaternion.Euler(camRotation);
    }

    public void OnInit()
    {
        SetSpace(minSpace);
    }

    public void SetSpace(int newSpace)
    {
        space = Mathf.Clamp(newSpace, minSpace, maxSpace);
        offset = new Vector3(0, space, -space);
    }

    public void SpaceUp()
    {
        SetSpace(space + 1);
    }
}
