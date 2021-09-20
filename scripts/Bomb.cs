using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    private void OnTriggerEnter2D(Collider2D col)
    {
       if(col.tag == "Enemy")
        {
            anim.SetBool("bag", true);
            StartCoroutine(BigBang());
        }
        if (col.tag == "Pig")
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator BigBang()
    {
        yield return new WaitForSeconds(1.0f);
        anim.SetBool("bag", false);
        gameObject.SetActive(false);
    }
}
