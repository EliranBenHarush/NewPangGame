using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public static Action bubbleExplos;
    [SerializeField]
    private Rigidbody2D ar;
    private bool isBrowm;
    
    
    void Update()
    {
        ar.velocity = new Vector2(0, 6);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Brown_Ceiling"))
        {
            isBrowm = true;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (!isBrowm) bubbleExplos?.Invoke();
    }
}
