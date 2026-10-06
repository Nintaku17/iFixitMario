using UnityEngine;

public class FixableObject : MonoBehaviour
{

    public SpriteRenderer SR;
    public bool Repaired = false;
    public bool Istouching;



    void Start()
    {
        SR = GetComponent<SpriteRenderer>();




    }





    void Update()
    {
        if(Canfix && Input.GetKey(KeyCode.E))
            Repaired = true;
        

        if (Repaired)
            SR.color = Color.orange;


        if (!Repaired)
            SR.color = Color.orangeRed;

        
        

    }

    

   

    private bool Canfix = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Canfix = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Canfix = false;
        }
    }

    


}
