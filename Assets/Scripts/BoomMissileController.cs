using System;
using UnityEngine;

public class BoomMissileController : MonoBehaviour
{
    public float speed;
    float time;

    void Start()
    {
        speed = 35.0f;
        time = 0;  // 죽을때
    }

    // Update is called once per frame
    void Update()
    {
        MoveBoom();  // 위로 이동
        DestroyBoom();  // 3초 뒤 Destroy
    }

    private void MoveBoom()  // 위로 이동
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void DestroyBoom()  // 3초 뒤 Destroy
    {
        time += Time.deltaTime;
        if (time > 3.0f)
        {
            Destroy(gameObject);
        }
    }

}
