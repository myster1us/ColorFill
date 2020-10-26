using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class check : MonoBehaviour
{
    public int size = 5;
    int totalcolors=0;
    public LayerMask layer2;
    public int total ;
    Vector3 startpos;
    public Animator fn;
    void Start()
    {

        startpos = transform.position;

        StartCoroutine(gamestart());
    }
    public void checkcolor()
    {
        totalcolors = 0;
        while(transform.position.z!=0)
        {

            float xs = 1;
            for (int i = 0; i <= size; i++)
            {
                if (Physics.Raycast(new Vector3(transform.position.x + xs, transform.position.y, transform.position.z), Vector3.back, 1, layer2))
                {
                    totalcolors++;
                }
                Debug.DrawRay(new Vector3(transform.position.x + xs, transform.position.y, transform.position.z), Vector3.back, Color.green, 5, false);
                xs++;
            }
            transform.position-= new Vector3(0, 0, 1);
        }

        print(totalcolors);
        transform.position = startpos;
        StartCoroutine(finish());
    }
    IEnumerator gamestart()
    {
        yield return new WaitForSeconds(1.5f);
        fn.enabled = false;
    }

    IEnumerator finish()
    {
       
        if (totalcolors >= total)
        {
            fn.enabled = true;
            fn.SetBool("finish", true);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        yield return new WaitForSeconds(0f);
    }
}
