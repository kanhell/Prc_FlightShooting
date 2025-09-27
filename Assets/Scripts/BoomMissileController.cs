using System;
using UnityEngine;

public class BoomMissileController : MonoBehaviour
{
    public float speed;
    float time;

    void Start()
    {
        speed = 35.0f;
        time = 0;  // ������
    }

    // Update is called once per frame
    void Update()
    {
        MoveBoom();  // ���� �̵�
        DestroyBoom();  // 3�� �� Destroy
    }

    private void MoveBoom()  // ���� �̵�
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void DestroyBoom()  // 3�� �� Destroy
    {
        time += Time.deltaTime;
        if (time > 3.0f)
        {
            Destroy(gameObject);
        }
    }

}
