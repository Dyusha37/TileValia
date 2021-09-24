using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{   [Tooltip ("Game units per second")]
    [SerializeField] float scrollRape = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yMove = scrollRape * Time.deltaTime;
        transform.Translate(new Vector2(0f, yMove));
    }
}
