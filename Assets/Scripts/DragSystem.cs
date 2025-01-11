using UnityEngine;

public class DragSystem : MonoBehaviour
{
    /// <summary>
    /// Если касаемся пальцем экрана, то делаем рейкаст в мировую позицию мыши,
    /// если нашли объект с коллайдером то сохранаем его в поле,
    /// если перемещаем палец то двигаем объект за пальцем с небольним смещением вниз,
    /// если отпускае палец во время перемещения объекта , то отпускаем объект
    /// </summary>
    private Transform _selectedObject;
    private readonly Vector3 _offset = new(0, -0.4f, 0);
    private Vector3 _mouseWorldPosition;

    private void Update()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }

        var touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                TakeObject();
                break;

            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                DragObject();
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                DropObject();
                break;
        }
    }

    private void TakeObject()
    {
        UpdateMouseWorldPosition();

        var hit = Physics2D.Raycast(_mouseWorldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            _selectedObject = hit.collider.transform;
        }
    }

    private void DragObject()
    {
        if (_selectedObject == null)
        {
            return;
        }
        UpdateMouseWorldPosition();
        
        _selectedObject.position = _mouseWorldPosition + _offset;
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

    private void UpdateMouseWorldPosition()
    {
        var mousePosition = Input.mousePosition;
        _mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        _mouseWorldPosition.z = 0;
    }
}