using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_centrol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
     public Transform player;
    // Update is called once per frame
    void Update()
    {
    
      transform.position = new Vector3(player.position.x, 0, -10f);
    }
}
