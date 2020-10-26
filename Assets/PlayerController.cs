using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 0.25f;

    [SerializeField]
    float rayLenght = 0.1f;
    [SerializeField]
    float rayLenght2 = 0.5f;

    Vector3 targetpos;
    Vector3 startpos;
    bool moving;
    bool up, down, left, right;

    List<GameObject> objects;
    List<GameObject> Colors;
    public GameObject ColorObject;
    public GameObject BigColorObject;
    public LayerMask layer,layer2;

    public check ch;

    int total = 0;

    bool weout=true;
    bool start = false;


    IEnumerator gamestart()
    {
        yield return new WaitForSeconds(1.5f);
        start = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        objects = new List<GameObject>();
        Colors = new List<GameObject>();
        InvokeRepeating("outcheck", 0f, 0.01f);
        StartCoroutine(gamestart());
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {

       
        if (moving)
        {
            if (Vector3.Distance(startpos,transform.position)>1f)
            {
                transform.position = targetpos;
                moving = false;
                return;
            }
            transform.position += (targetpos - startpos) * moveSpeed * Time.deltaTime;
            return;
        }

       
        if (Input.GetKey(KeyCode.UpArrow))
        {
            up = true;
            down = false;
            left = false;
            right = false;
            
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            down = true;
            up = false;
            left = false;
            right = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            right = true;
            down = false;
            up = false;
            left = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            left = true;
            right = false;
            down = false;
            up = false;
        }


        if (up)
        {
            if (!Physics.Raycast(transform.position, Vector3.forward, rayLenght,layer) )
            {


                targetpos = transform.position + Vector3.forward;
                startpos = transform.position;
                moving = true;
              
                    
                        CreateColor();
                
                   
               
            }
            else  
            {
                up = false;
                
                    CreateColor();
                    EndColor();
                
            }
        }
        else if (down)
        {
            if (!Physics.Raycast(transform.position, Vector3.back, rayLenght, layer) )
            {
                targetpos = transform.position + Vector3.back;
                startpos = transform.position;
                moving = true;


                    CreateColor();
                

            }
            else
            {
                down = false;
                
                    CreateColor();
                    EndColor();
               
            }
        }
        else if (right)
        {
            if (!Physics.Raycast(transform.position, Vector3.right, rayLenght, layer))
            {
                targetpos = transform.position + Vector3.right;
                startpos = transform.position;
                moving = true;


              

                    CreateColor();
                

            }
            else 
            {
                right = false;
               
                    CreateColor();
                    EndColor();
                

            }
        }
        else if (left)
        {
            if (!Physics.Raycast(transform.position, Vector3.left, rayLenght, layer) )
            {
                targetpos = transform.position + Vector3.left;
                startpos = transform.position;
                moving = true;

               

                    CreateColor();
                


            }
            else 
            {
                left = false;
                
                    CreateColor();
                    EndColor();
                
            }
        }
        }
    }

  
    private void CreateColor()
    {
       // print("end");
       /* Debug.DrawRay(transform.position, Vector3.forward, Color.green, 5, false);
        Debug.DrawRay(transform.position, Vector3.left, Color.green, 5, false);
        Debug.DrawRay(transform.position, Vector3.right, Color.green, 5, false);
        Debug.DrawRay(transform.position, Vector3.back, Color.green, 5, false);*/
        RaycastHit[] hits;
        // SADECE FRONT YAPTIĞIN İÇİN HATA OLUYOR HER YÖNE YAPMANIN BİR YOULUNU BUL
        hits = Physics.RaycastAll(new Vector3(transform.position.x-0.2f, transform.position.y, transform.position.z), Vector3.forward, rayLenght2, layer2);
        print("hittt:"+hits.Length);
         /*if (hits.Length==0)
         {*/
      
            print("OLUŞTURULDU");
            GameObject Co = Instantiate(ColorObject, new Vector3(transform.position.x, 0.10f, transform.position.z), Quaternion.identity);
            objects.Add(Co);
      
            
       // }
        
      /*  if (Physics.RaycastAll(transform.position, Vector3.forward , rayLenght2, layer2) || Physics.RaycastAll(transform.position, Vector3.back, rayLenght2, layer2) || Physics.RaycastAll(transform.position, Vector3.left, rayLenght2, layer2) || Physics.RaycastAll(transform.position, Vector3.right, rayLenght2, layer2))
        {
            
               
           

        }
        else
        {*/
           
       // }
      
       
     
    }
    private void EndColor()
    {

        //print("end");

        int x = 0;
        int countt = 0;
        Vector3[] pos;
        pos = new Vector3[2];
        while (x < 2)
        {
            if (Physics.Raycast(objects[countt].transform.position, Vector3.forward, rayLenght, layer))
            {
                pos[x] = objects[countt].transform.position;
            }
            if (Physics.Raycast(objects[countt].transform.position, Vector3.back, rayLenght, layer))
            {
                pos[x] = objects[countt].transform.position;
            }
            if (Physics.Raycast(objects[countt].transform.position, Vector3.left, rayLenght, layer))
            {
                pos[x] = objects[countt].transform.position;
            }
            if (Physics.Raycast(objects[countt].transform.position, Vector3.right, rayLenght, layer))
            {
                pos[x] = objects[countt].transform.position;
            }

            countt = objects.Count - 1;
            x++;
        }

        if (pos[0].x != pos[1].x)
        {


            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].transform.localScale = new Vector3(1, 1, 1);
                objects[i].transform.position += new Vector3(0, 0.2f, 0);

            }
        
        print(pos[0]);
        print(pos[1]);
        bool left = true;
         for (int i = 0; i < objects.Count; i++)//objects.Count
         {
             float lenght=1;
           
            //    float step =Mathf.Abs( objects[objects.Count - 1].transform.position.x) - Mathf.Abs( objects[i].transform.position.x);
            float istouch = 1;

            if (pos[1].x > pos[0].x)
            {
                lenght = 1;
                left = true;
                
            }
            else if (pos[1].x < pos[0].x)
            {
                left = false;
                lenght = -1;
            }
           
            for (int j = 0; j < 50; j++)
            {
               


                if (!Physics.Raycast(objects[i].transform.position, new Vector3(lenght, 0, 0), Mathf.Abs(lenght), layer) && weout )
                {
                   
                    Debug.DrawRay(objects[i].transform.position, new Vector3(lenght, 0, 0), Color.green, 5, false);
                    GameObject cl = Instantiate(BigColorObject, new Vector3(objects[i].transform.position.x+lenght, objects[i].transform.position.y, objects[i].transform.position.z), Quaternion.identity);
                    Colors.Add(cl);
                    if (left)
                    {
                        lenght++;
                    }
                    else
                    {
                        lenght--;
                    }
                }
                else
                {
                lenght = 0;
                print("değidi");
                    break;
                }
            }

            /*  for (int j = 0; j < step; j++)
              {



                  if (pos[1].x == objects[i].transform.position.x || pos[1].z == objects[i].transform.position.z)
                  {
                      lenght = 0;
                      break;
                  }
                  else if (pos[1].x > objects[i].transform.position.x)
                  {
                      lenght+= objects[i].transform.position.x + 1;
                      print("+++");
                  }
                  else if(pos[1].x < objects[i].transform.position.x)
                  {
                      print("----");
                      lenght-=objects[i].transform.position.x + 1;
                  }



                  if (!Physics.Raycast(objects[i].transform.position, new Vector3(lenght, 0, 0), Mathf.Abs( lenght) , layer)  && !Physics.Raycast(objects[i].transform.position, new Vector3(lenght, 0, 0), Mathf.Abs(lenght), layer2))
                  {
                      Debug.DrawRay(objects[i].transform.position,new Vector3(lenght, 0, 0), Color.green, 5, false);
                      GameObject cl = Instantiate(BigColorObject, new Vector3(lenght, objects[i].transform.position.y, objects[i].transform.position.z), Quaternion.identity);
                  }
                  else
                  {
                      print("değiyirrr");
                  }
              }*/
        }
        objects = new List<GameObject>();
        for (int i = 0; i < Colors.Count; i++)
        {
            Colors[i].layer = 9;
        }
            total += Colors.Count;
            
        Colors = new List<GameObject>();

        weout = false;
            ch.checkcolor();
        }
      
    }


    private void outcheck()
    {
       

       //print(Physics.Raycast(transform.position, Vector3.forward, rayLenght2, layer2));
       /*
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, Vector3.forward, rayLenght2, layer2);
        if (hits.Length == 0)
        {
            weout = true;
        }
        else
        {
            weout = false;
        }
            */
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward,out hit,rayLenght2,layer2) || Physics.Raycast(transform.position, Vector3.back, out hit, rayLenght2, layer2) || Physics.Raycast(transform.position, Vector3.left, out hit, rayLenght2, layer2)|| Physics.Raycast(transform.position, Vector3.right, out hit, rayLenght2, layer2))
            {
                print(hit.transform.gameObject.layer);
                weout = false;
                return;
            }
            else 
            {
                print("weouttttt");
                weout = true;
                return;
            }


          

            print(weout);

    }
}
