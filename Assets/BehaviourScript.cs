using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourScript : MonoBehaviour
{
    public int population;
    public bool owned_by_user;
    public bool owned_by_bot;
    public bool neutral;
    public float counter;

    private Fleet planetFleet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((owned_by_user == true) || (owned_by_bot == true)) {
            counter += Time.deltaTime;
            if (counter >= 1)
            {
                population += 1;
                counter = 0;
            }
        }
    }

    private void OnMouseDown()
    {
        planetFleet = GameObject.Find("Fleet").GetComponent<Fleet>();
        if (owned_by_user == true)
        {
            if (planetFleet.size == 0)
            {
                planetFleet.size = population / 2;
                population -= planetFleet.size;
            } else
            {
                population += planetFleet.size;
                planetFleet.size = 0;
            }
        }
        if (owned_by_bot == true || neutral == true)
        {
            if (population >= planetFleet.size)
            {
                population -= planetFleet.size;
                planetFleet.size = 0;
            } else
            {
                population = planetFleet.size - population;
                planetFleet.size = 0;
                if (owned_by_bot == true)
                {
                    owned_by_bot = false;
                    owned_by_user = true;
                }
                if (neutral == true)
                {
                    neutral = false;
                    owned_by_user = true;
                }
            }
        }
    }
}
