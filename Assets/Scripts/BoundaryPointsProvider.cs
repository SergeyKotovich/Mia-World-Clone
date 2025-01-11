using UnityEngine;

public class BoundaryPointsProvider : MonoBehaviour
{
    /// <summary>
    /// Границы пола
    /// </summary>
    [SerializeField] private Transform _topBoundary;
    [SerializeField] private Transform _bottomBoundary;
    
    public Transform TopBoundary => _topBoundary;
    public Transform BottomBoundary => _bottomBoundary;
}