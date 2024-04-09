using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class play_control : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D coll;
    public float speed;
    public Animator anim;
    public float jumpforce;
    public LayerMask ground;
    public int Tian = 0;
    public Text TianNum;
      public AudioClip collectionSound; // 收集音效
    private AudioSource audioSource; 

    // Start is called before the first frame update
    void Start()
    {
          // 获取音频源组件
        audioSource = gameObject.AddComponent<AudioSource>();
        // 设置音频源的clip为收集音效
        audioSource.clip = collectionSound;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement(); 
        SwitchAnim();
        // 检查Tian是否等于9以切换到下一个场景
        if (Tian == 9)
        {
            // 加载下一个场景
            //SceneManager.LoadScene("NextSceneName");
        }
    }

    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
            Debug.Log("moved");
        }
        
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
            Debug.Log("changed");
        }
            //if (Input.GetButtonDown("Jump") )
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce *Time.deltaTime ); 
            anim.SetBool("jumping", true);
        }
    }

    void SwitchAnim()
    {
        anim.SetBool("idle", false);

        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
                
            }
        }
        else if (coll.IsTouchingLayers(ground))
            {
                
                anim.SetBool("falling", false);
                anim.SetBool("idle", true);
                Debug.Log("on th ground");
    
            }
          
        }
    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.tag == "Collection")
    {
        audioSource.Play();
        Destroy(collision.gameObject); // 当触发器碰到tag为"Collection"的物体时，销毁该物体
        Tian += 1; 
        TianNum.text =Tian.ToString();// 增加一个tian的计数
    }
}

}


