using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private JoyStick js;
    [SerializeField]
    private float speed;

    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;   
    Vector3 movement;
    void Start ()
    {
//        js = GameObject.FindObjectOfType<JoyStick> ();

        anim = GetComponent <Animator> ();
        playerRigidbody = GetComponent <Rigidbody> ();

        js.OnJoyStickTouchBegin += OnJoyStickBegin;
        js.OnJoyStickTouchMove += OnJoyStickMove;
        js.OnJoyStickTouchEnd += OnJoyStickEnd;
    }     
    void OnJoyStickBegin(Vector2 vec)
    {
//        Debug.Log("开始触摸虚拟摇杆");
    }
    void OnJoyStickMove (Vector2 vec)
    {
//        Debug.Log("正在移动虚拟摇杆");
        //设置角色朝向
        Quaternion q = Quaternion.LookRotation (new Vector3 (vec.x, 0, vec.y));
//        transform.rotation = q;
        playerRigidbody.MoveRotation (q);

        movement.Set (vec.x, 0f, vec.y);
        Animating (vec.x, vec.y);
        //移动角色并播放奔跑动画
        movement = movement.normalized *  speed * Time.deltaTime;
        playerRigidbody.MovePosition (transform.position + movement);
    }   
    void OnJoyStickEnd ()
    {
//        Debug.Log("触摸移动摇杆结束");
        Animating (0, 0);
    }
    void OnGUI()
    {
//        GUI.Label(new Rect(30,30,200,30),"3D模式下的虚拟摇杆测试");
    }

    void Animating (float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool ("IsWalking", walking);
    }
}