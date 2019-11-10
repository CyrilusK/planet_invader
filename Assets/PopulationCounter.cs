using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationCounter : MonoBehaviour
{
//    BehaviourScript behaviourScript;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().text = GetComponentInParent<BehaviourScript>().population.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMesh>().text = GetComponentInParent<BehaviourScript>().population.ToString();
    }
}
