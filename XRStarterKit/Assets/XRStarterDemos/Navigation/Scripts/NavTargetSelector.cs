using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;
using Vuforia;

/// <summary>
/// Interactively updates the target of a NavMesh agent.
/// </summary>
public class NavTargetSelector : MonoBehaviour
{
    #region Unity Inspector Variables
    [Tooltip("The agent to update navigation for.")]
    [SerializeField]
    private NavMeshAgent _agent;

    [Tooltip("Visual that represents the target.")]
    [SerializeField]
    private GameObject _targetVisual;
    #endregion // Unity Inspector Variables


    #region Unity Message Handlers
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        // If the mouse button went down on this frame
        if (Input.GetMouseButtonDown(0))
        {
            // Try to set the target
            TrySetMouseTarget();
        }

    }
    #endregion // Unity Message Handlers


    #region Public Methods
    /// <summary>
    /// Attempts to navigate to the point where the mouse was clicked.
    /// </summary>
    /// <returns>
    /// <c>true</c> if navigation could be set; otherwise <c>false</c>.
    /// </returns>
    public bool TrySetMouseTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);

        // If we have a it, try to target it
        return ((hasHit) && (TrySetTarget(hit.point)));
    }

    /// <summary>
    /// Attempts to navigate to the specified point.
    /// </summary>
    /// <param name="point">
    /// The point to navigate to.
    /// </param>
    /// <returns>
    /// <c>true</c> if navigation could be set; otherwise <c>false</c>.
    /// </returns>
    public bool TrySetTarget(Vector3 point)
    {
        // Make sure the visual is visible
        _targetVisual.SetActive(true);

        // Move to the position
        _targetVisual.transform.position = point;

        // Attempt to update the agent
        return _agent.SetDestination(point);
    }
    #endregion // Public Methods

}
