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
        GetComponent<TextMesh>().text = population.ToString();
    }
}
