using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_m : MonoBehaviour
{
    public GameObject Enemy;
    public float x = 0;
    public float x2 = -1;
    public float y = -1.5f;
    public float y2 = -1.5f;
    public float Enemy_Speed_x = 0.05f;
    public float Enemy_Speed_y = 0.05f;
    public bool Enemy_1_L_ON  = true;
    public bool Enemy_1_R_ON;
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Enemy.transform.position = new Vector3(0, -1.5f, 1.0f);
        Enemy_1_L_ON = true;
    }

    // Update is called once per frame
    void Update()
    {
        EnemymoveL();
        EnemymoveR();
    }

    void OnCollisionEnter2D(Collision2D enemy)
    {
        if (enemy.gameObject.CompareTag("bullet"))
        {
            Destroy(gameObject);
        }
    }

    void EnemymoveL()
    { 
        Enemy_Speed_x = 0.05f;
        Enemy.transform.position = new Vector3(x+5, -1.5f, 1.0f);
        if (Enemy_1_L_ON)
        {
            x -= Enemy_Speed_x;
            if (x <= 0)
            {
                Enemy_1_L_ON = false;
                Enemy_1_R_ON = true;
            }
        }


    }
    
    void EnemymoveR()
    {
        Enemy.transform.position = new Vector3(x, -1.5f, 1f);
        if (Enemy_1_R_ON)
        {
            x += Enemy_Speed_x;
            if (x >= 10)
            {
                Enemy_1_L_ON = true;
                Enemy_1_R_ON = false;
            }
        }


    }

}
