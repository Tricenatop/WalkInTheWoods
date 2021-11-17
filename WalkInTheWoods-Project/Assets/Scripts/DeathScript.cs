//Made by Sam Kreimer 2/25/2021
//used to end the game and make the player reset


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    [SerializeField] GameObject realCourse;
    [SerializeField] GameObject deathCourse;

    void Update()
    {

        //test
        if (Input.GetKey("z") )
        {
            setDeathCourse();
        }
        if (Input.GetKey("x"))
        {
            setRealCourse();
        }


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Death")
        {
            setDeathCourse();
            print("death");
        }

        if (other.tag == "Life")
        {
            setRealCourse();
        }
    }

    /// <summary>
    /// turn off RealCourse & turn on DeathCourse
    /// </summary>
    public void setDeathCourse()
    {
        realCourse.SetActive(false);
        deathCourse.SetActive(true);
    }

    /// <summary>
    /// turn on RealCourse & turn off DeathCourse
    /// </summary>
    public void setRealCourse()
    {
        realCourse.SetActive(true);
        deathCourse.SetActive(false);
    }

}
