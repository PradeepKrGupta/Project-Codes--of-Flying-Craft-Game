using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float missileSpeed = 20f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up*missileSpeed*Time.deltaTime);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bonous")
        {
            GameObject gm = Instantiate(GameManager.Instance.explosion, transform.position, transform.rotation);
            Destroy(gm, 2f);
          
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}