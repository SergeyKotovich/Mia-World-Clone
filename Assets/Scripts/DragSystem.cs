using UnityEngine;

public class DragSystem : MonoBehaviour
{
    private Transform _selectedObject;
    private readonly Vector3 _offset = new(0, -0.4f, 0);
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TakeObject();
        }

        if (Input.GetMouseButton(0) && _selectedObject != null)
        {
            DragObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DropObject();
        }
    }

    private void TakeObject()
    {
        var mouseWorldPosition = GetMouseWorldPosition();
        var hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            _selectedObject = hit.collider.transform;
        }
    }

    private void DragObject()
    {
        var mouseWorldPosition = GetMouseWorldPosition();
        
        _selectedObject.position =  mouseWorldPosition + _offset;;
        Debug.Log(_selectedObject.position);
    }

  

    private void DropObject()
    {
        if (_selectedObject == null)
        {
            return;
        }
        
        _selectedObject.GetComponent<InteractableObject>().EnableGravity();

        _selectedObject = null;
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z; 
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}