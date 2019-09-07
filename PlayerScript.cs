using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float MaxHP = 5;
    public float NowHp = 5;
    public float flap = 300f;
    public float moveSpeed = 3f;
    float direction = 0f;
    Rigidbody2D rb2d;
    bool jump = false;
    public GameObject bulletToLeft;
    public GameObject bulletToRight;
    Vector2 bulletPos;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;
    bool facingRight = true;
    public float speed = 5f;
    float velX;
    float velY;
    Slider _slider;

    // Use this for initialization
    void Start()
    {
        //コンポーネント読み込み
        rb2d = GetComponent<Rigidbody2D>();
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
    }


    // Update is called once per frame
    void Update()
    {
        //水平方向への移動
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb2d.velocity.y;
        rb2d.velocity = new Vector2(velX * moveSpeed, velY);
       

       
        //ジャンプ判定
        if (Input.GetKeyDown("space") && !jump)
        {
            rb2d.AddForce(Vector2.up * flap);
            jump = true;
        }
        //攻撃速度判定
        if (Input.GetKeyDown("q") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            fire();
        }

    }

    void LateUpdate()
    {
        //キーボード操作
        //キャラのy軸のdirection方向にscrollの力をかける
        Vector3 localScale = transform.localScale;
        if(velX > 0)
        {
            facingRight = true;
        }else if(velX < 0)
        {
            facingRight = false;
        }
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;
        
    }
    //衝突判定
    void OnCollisionEnter2D(Collision2D other)
    {
        //地面との衝突判定
        if (other.gameObject.CompareTag("Ground"))
        {
            jump = false;
        }
        //壁との衝突判定
        if (other.gameObject.CompareTag("wall"))
        {
            jump = false;
        }
        //敵との衝突判定
        if (other.gameObject.CompareTag("Enemy"))
        {
            _slider.value -= 1;
            NowHp -= 1;
            if (NowHp <= 0)
            {
                SceneManager.LoadScene("Gameover");
            }
        }
        //GAMECLEAR判定
        if (other.gameObject.CompareTag("Flag"))
        {
            SceneManager.LoadScene("GameClear");
            Debug.Log("goal");
        }
    }

    
    //攻撃判定
        void fire()
        {
            bulletPos = transform.position;
            if (facingRight)
            {

                bulletPos += new Vector2(+1f, 0f);
                Instantiate(bulletToRight, bulletPos, Quaternion.identity);
            }
            else
            {
                bulletPos += new Vector2(-1f, 0f);
                Instantiate(bulletToLeft, bulletPos, Quaternion.identity);
            }
        }

    void HP()
    {        
        // HPゲージに値を設定
        _slider.maxValue = MaxHP;
        _slider.value = NowHp;
    }
}


    
        


            
    
