using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Balls : MonoBehaviour
{
    public static Action PlayerDamage;
    
    [SerializeField]
    private bool moveLeft, moveRight ;
    [SerializeField]
    private Rigidbody2D myBody;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite[] bubbles;
    private float forceX, forecY;
    

    [SerializeField] private GameObject ballMedium, ballSmell;

    private void Awake()
    {
        var rnd = Random.Range(0, bubbles.Length);
        spriteRenderer.sprite = bubbles[rnd];
        myBody = GetComponent<Rigidbody2D>();
        SetBallSpeed();
        if (gameObject.CompareTag("Large"))
        {
            var rndPos = Random.Range(-6, 6);
            transform.localPosition = new Vector3(rndPos, 1.5f, 0);
        }
    }

    private void SetBallSpeed()
    {
        forceX = 2.5f;
        switch (gameObject.tag)
        {
            case "Large":
                forecY = 7.5f;
                break;
            case "Medium":
                forecY = 6f;
                break;
            case "Smell":
                forecY = 6f;
                break;
        }
    }


    private void MoveBall()
    {
        if (moveLeft)
        {
            Vector3 vec = transform.position;
            vec.x -= forceX * Time.deltaTime;
            transform.position = vec;
        }

        if (!moveRight) return;
        {
            Vector3 vec = transform.position;
            vec.x += forceX * Time.deltaTime;
            transform.position = vec;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var tag = col.gameObject.tag;
        switch (tag)
        {
            case "Ground":
                myBody.velocity = new Vector2(0, forecY);
                break;

            case "Brown_Right":
                moveRight = false;
                moveLeft = true;
                break;

            case "Brown_Left":
                moveRight = true;
                moveLeft = false;
                break;
            case "Brown_Ceiling":
                myBody.velocity = new Vector2(0, -2);
                break;
            case "Player":
                PlayerDamage?.Invoke();
                break;

            case "Arrow":
                Destroy(col.gameObject);
                if (gameObject.CompareTag("Large"))
                {
                    CreateBubble(ballMedium);
                }
                if (gameObject.CompareTag("Medium"))
                {
                    CreateBubble(ballSmell);
                }
                if (gameObject.CompareTag("Smell"))
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    private void CreateBubble(GameObject sizeBall)
    {
        Destroy(gameObject);
        for (int i = 0; i < 2; i++)
        {
            var obj = Instantiate(sizeBall, transform.position, Quaternion.identity);
            obj.transform.SetParent(transform.parent);
            if (i == 1)
            {
                obj.GetComponent<Balls>().moveRight = true;
                return;
            }
            obj.GetComponent<Balls>().moveLeft = true;
        }
        
    }

    void Update()
    {
        MoveBall();
    }
}