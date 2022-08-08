using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HowToPlay : MonoBehaviour
{
    public GameObject[] Pages; 

    private int index; 

    public GameObject buttons;




    public void NextPage()
    {

        if(index + 1 < Pages.Length)
        { 
            foreach(GameObject page in Pages)
            {
                page.SetActive(false);

            }

            index ++;
            Pages [index].SetActive(true);
        }
    }


    public void PreviousPage()
    {

        if(index> 0)
        { 
            foreach(GameObject page in Pages)
            {
                page.SetActive(false);

            }

            index --;
            Pages [index].SetActive(true);
        }
        else 
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }


    }
    



}
