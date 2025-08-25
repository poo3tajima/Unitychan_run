using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    Animator animator;
    CharacterController cc;
    AudioSource voice;
    public AudioClip[] voiceList;

    Vector3 dir = Vector3.zero;
    public float gravity = 20.0f;
    public float speed = 4.0f;
    public float rotSpeed = 300.0f;
    public float jumpPower = 8.0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        voice = GetComponent<AudioSource>();

    }


    void Update()
    {
        // 前進成分を取得(0～1) バックしない
        float acc = Mathf.Max(Input.GetAxis("Vertical"), 0f);

        // もし地面の上にいるなら
        if (cc.isGrounded)
        {
            // 左右キーで回転できる
            float rot = Input.GetAxis("Horizontal");
            // 前進と回転が入力されていたら、大きいほうの値をspeedにセットする
            animator.SetFloat("speed", Mathf.Max(acc, Mathf.Abs(rot)));
            // 回転は直接トランスフォームを変更する
            transform.Rotate(0, rot * rotSpeed * Time.deltaTime, 0);

            // ジャンプボタンが押されたら
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("jump");
            }
        }

        // 下方向の重力成分
        dir.y -= gravity * Time.deltaTime;

        // CharacterControllerはMove()でキャラを移動させる
        cc.Move((transform.forward * acc * speed + dir) * Time.deltaTime);

        // 地面に着地したら、y値をリセットする
        if (cc.isGrounded)
        {
            dir.y = 0;
        }
    }

    public void OnJumpStart()
    {
        dir.y = jumpPower;
        voice.PlayOneShot(voiceList[1]);
    }
}
