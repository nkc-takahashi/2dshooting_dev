using UnityEngine;

public class BulletEnemy : MonoBehaviour {

    public float tamaSpeed;
    public GameObject player;
    private Vector2 relativePos;
    private Vector2 tamaPos;

    public bool mouserotateswitch;
    public float DestroyTiming;
    
    private void Rotate()                                                   //マウス方向を向く
    {
        if (mouserotateswitch)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.localPosition);
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - pos);
            transform.rotation = rotation;
        }
        else
        {
            if (gameObject.transform.rotation.z != 0) { }
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void ByeBye()
    {
        DestroyTiming -= Time.deltaTime;
        if (DestroyTiming<=0)
        {
            Destroy(gameObject);
        }
    }

    #region Unity Callback

    // Use this for initialization
    private void Start ()
    {
        GetComponent<Rigidbody2D>().velocity = (transform.up * tamaSpeed);
    }

    // Update is called once per frame
    private void Update ()
    {
        ByeBye();   //消える
        //Rotate();   //追尾    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Destroy(gameObject);
    }

    #endregion
}
