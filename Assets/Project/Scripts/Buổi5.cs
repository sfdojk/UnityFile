using UnityEngine;

public class Buổi5 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LayerMask.GetMask("tenLayer");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity);


    }
    private void OnDrawGizmos()
    {
        
    }
}
