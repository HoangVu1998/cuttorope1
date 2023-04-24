using UnityEngine;

public class RopeCutter : MonoBehaviour
{
    Rigidbody2D rb;
    Camera Cam;
    public static Vector3 destroyedColliderPosition;
    public static bool isCutter;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Cam = Camera.main;
    }
    void Update()
    {
        cutLinerer();
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void visible_blade()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    void cutLinerer()
    {
        if (Input.GetMouseButton(0))
        {
            rb.position = Cam.ScreenToWorldPoint(Input.mousePosition);
            Invoke("visible_blade", 0.05f);

            RaycastHit2D hit = Physics2D.Raycast(Cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "node")
                { 
                   Destroy(hit.collider.gameObject); // Hủy collider
                }
            }
        }
    }
}
