using UnityEngine;

public class LoadingPanel : AbstractUICanvas
{
    [SerializeField] private Transform imgLoading;

    private float speed = 100;

    void Update()
    {
        imgLoading.Rotate(Vector3.back, speed * Time.deltaTime);
    }
}
