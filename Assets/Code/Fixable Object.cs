using UnityEngine;

public class FixableObject : MonoBehaviour
{

    public SpriteRenderer SR;
    public static bool Repaired = false;



    void Start()
    {
        SR = GetComponent<SpriteRenderer>();




    }





    void Update()
    {

        if (Repaired)
            SR.color = Color.orange;

        if (!Repaired)
            SR.color = Color.orangeRed;



    }

    public static void CheackFix()
    {

       
        Repaired = true;

    }




}
