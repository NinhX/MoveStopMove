using System.Collections.Generic;
using UnityEngine;

public class EnemyPanel : AbstractUICanvas
{
    private Camera cam;
    private ObjectPool pool => ObjectPool.Instance;
    private List<Transform> listTranform = new();
    private List<ImagePoint> points = new();

    private float screanWidth;
    private float screanHeight;
    private float screanHalfWidth;
    private float screanHalfHeight;
    private float outScreen;
    private Transform tf => transform;

    void LateUpdate()
    {
        ShowPoint();
    }

    public void AddTranform(Transform transformPoint)
    {
        listTranform.Add(transformPoint);
        ImagePoint imagePoint = pool.Spawn<ImagePoint>(PoolType.ImagePoint, transformPoint.rotation);
        imagePoint.Tf.SetParent(tf, false);
        points.Add(imagePoint);
    }

    protected override void OnShowUI()
    {
        OnInit();
    }

    public override void OnInit()
    {
        cam = Camera.main;
        screanWidth = UIManager.Instance.CanvasWidth;
        screanHeight = UIManager.Instance.CanvasHeight;
        screanHalfWidth = screanWidth / 2;
        screanHalfHeight = screanHeight / 2;
        outScreen = screanHeight * 2;
    }

    private void ShowPoint()
    {
        for (int i = points.Count - 1; i >= 0; i--)
        {
            Transform tf = listTranform[i];
            if (!tf.gameObject.activeInHierarchy)
            {
                RemoveAt(i);
                continue;
            }
            Vector3 viewportPoint = cam.WorldToViewportPoint(tf.position);
            float flip = viewportPoint.z / Mathf.Abs(viewportPoint.z);
            float X = flip * (Mathf.Clamp01(viewportPoint.x) * screanWidth - screanHalfWidth);
            float Y = flip * (Mathf.Clamp01(viewportPoint.y) * screanHeight - screanHalfHeight);

            if (!CheckOutScreen(viewportPoint))
            {
                Y -= outScreen;
            }
            points[i].SetAnchoredPosition3D(new Vector3(X, Y, 0));
        }
    }

    private bool CheckOutScreen(Vector3 viewportPoint)
    {
        return viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1;
    }

    private void RemoveAt(int index)
    {
        ImagePoint image = points[index];
        listTranform.RemoveAt(index);
        points.RemoveAt(index);
        image.OnDesPawn();
    }

    private void ResetAll()
    {
        for (int i = 0; i < points.Count; i++)
        {
            points[i].OnDesPawn();
        }
        points.Clear();
        listTranform.Clear();
    }
}
