using UnityEngine;

public class ToothbrushScript : MonoBehaviour

{
    public Texture2D grabCursor;
    private Vector3 offset;
    private bool isDragging = false;

    void OnMouseDown()
    {
        isDragging = true;
        Cursor.SetCursor(grabCursor, new Vector2(16, 16), CursorMode.Auto);
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        Vector3 newPos = GetMouseWorldPos() + offset;
        newPos.z = 0; 
        transform.position = newPos;
    }

    void OnMouseUp()
    {
        isDragging = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 10; 
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
public ParticleSystem bubbleParticles;

void OnTriggerStay2D(Collider2D other)
{
    if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
    {
        if (!bubbleParticles.isPlaying) 
            bubbleParticles.Play();
    }
    else
    {
        bubbleParticles.Stop();
    }
}
}
