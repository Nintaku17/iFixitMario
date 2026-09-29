using UnityEngine;

public class FixableObject : MonoBehaviour
{

    public SpriteRenderer SR;
    public bool Repaired;



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

    public void CheackFix(bool Repaired)
    {

       


    }




}
