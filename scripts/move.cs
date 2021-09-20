using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    //select char tape
    private enum CharTap
    {
        player=0,
        enemy=1
    }
    [SerializeField]
    private CharTap charTape;

    //get rigit body
    [SerializeField]
    private Rigidbody2D rb;

    //wait move 
    [SerializeField]
    private float waitTime=2.0f;

    //frize enemy
    private bool dirty;

    //change sprite
    [SerializeField]
    private SpriteRenderer EnemySprite;
    //list clear
    [SerializeField]
    private List<Sprite> ClearEnemy;
    //list dirty
    [SerializeField]
    private List<Sprite> DirtyEnemy;
    //index sprite wasd
    private int setEnemySprite=2;
    void Start()
    {

        if (charTape == CharTap.enemy)
        {
            gameObject.tag = "Enemy";
            StartCoroutine(moveEnemy());
        }
    }

    //move
    void MoveChar(Vector2 selectmove)
    {
        rb.AddForce(selectmove);
    }
    void MoveDown()
    {
        rb.AddForce(new Vector2(0, -1));
    }
    void MoveLeft()
    {
        rb.AddForce(new Vector2(-1, 0));
    }
    void MoveRight()
    {
        rb.AddForce(new Vector2(1, 0));
    }

    //plyer and enemy
    private void Update()
    {
        if (charTape == CharTap.player)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 clickposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                MoveChar(clickposition);
                Debug.Log(clickposition);
            }
        }
        //dirty enemy
    }
    IEnumerator moveEnemy()
    {
        yield return new WaitForSeconds(waitTime);
        if (!dirty)
        {
            Vector2 go = new Vector2(Random.Range(-100, 101), Random.Range(-100, 101));
            //wasd
            if (go.y > 0)
            {
                setEnemySprite = 0;
            }
            if (go.x < 0)
            {
                setEnemySprite = 1;
            }
            if (go.y < 0)
            {
                setEnemySprite = 2;
            }
            if (go.y < 0)
            {
                setEnemySprite = 3;
            }
            EnemySprite.sprite = ClearEnemy[setEnemySprite];
            MoveChar(go);
        }
        else
        {
            EnemySprite.sprite = DirtyEnemy[setEnemySprite];
        }
            yield return StartCoroutine(moveEnemy());
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //anti bug
        if (charTape != CharTap.player)
        {
            //anti bug
            if (col.tag == "Bug")
            {
                transform.position = Vector2.zero;
            }
            //bang
            if (col.tag== "Bang")
            {
                //make dirty
                dirty=true;
                StartCoroutine(AvtoClear());
            }
        }
      

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        //game ower 
        if (col.collider.tag == "Pig")
        {
            col.collider.gameObject.SetActive(false);
        }
        //win
        if (col.collider.tag == "Finish" && charTape == CharTap.player)
        {
            col.collider.gameObject.SetActive(false);
        }

    }
    //avto clear enemy
    IEnumerator AvtoClear()
    {
        yield return new WaitForSeconds(10.0f);
        dirty = false;

    }
}
