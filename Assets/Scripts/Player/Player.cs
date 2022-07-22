using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Pode Andar");
        }
    }
}
