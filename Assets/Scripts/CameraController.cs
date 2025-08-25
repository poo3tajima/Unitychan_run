using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        // ユニティちゃんの後ろ方向のベクトル１ｍにカメラが立つように
        transform.position = player.position + (-player.forward * 3.0f) + (player.up * 1.0f);

        // LookAtで注視点をユニティちゃんへ向ける
        transform.LookAt(player.position + Vector3.up);
    }
}
