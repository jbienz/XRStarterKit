using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;
using Vuforia;

/// <summary>
/// Visualizes the path of a navigation agent.
/// </summary>
public class NavVisualizer : MonoBehaviour
{
    #region Unity Inspector Variables
    [Tooltip("The agent to visualize navigation for.")]
    [SerializeField]
    private NavMeshAgent _agent;

    [Tooltip("The line renderer used to visualize navigation segments.")]
    [SerializeField]
    private LineRenderer _lineRenderer;
    #endregion // Unity Inspector Variables

    /// <summary>
    /// Draws the current path of the nav agent.
    /// </summary>
    private void DrawPath()
    {
        // If the agent doesn't have a path, or there's only a sigle point, draw nothing
        if ((!_agent.hasPath) || (_agent.path.corners.Length < 2))
        {
            // No points
            _lineRenderer.positionCount = 0;

            // Not enabled
            _lineRenderer.enabled = false;

            // Done
            return;
        }

        // Is enabled
        _lineRenderer.enabled = true;

        // Set the number of positions
        _lineRenderer.positionCount = _agent.path.corners.Length;

        // The first position is the next point of the agent
        _lineRenderer.SetPosition(0, _agent.nextPosition);

        // Draw remaining points
        for (int iPoint = 1; iPoint < _agent.path.corners.Length; iPoint++) 
        {
            _lineRenderer.SetPosition(iPoint, _agent.path.corners[iPoint]);
        }
    }


    #region Unity Message Handlers
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        /*****************
         * Agent
         *****************/

        // We let the camera move freely and not controlled by the agent.
        _agent.updatePosition = false;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        // We don't let the agent move itself
        _agent.acceleration = 0;
        _agent.angularSpeed = 0;
        _agent.autoBraking = false;
        _agent.speed = 0;
        _agent.velocity = Vector3.zero;

        // We do want the agent to constantly repath
        _agent.autoRepath = true;


        /*****************
         * Line Renderer
         *****************/

        // We want the line renderer to start with no segments
        _lineRenderer.positionCount = 0;

        // Want a fairly narrow line
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        // Always update the agent position to match the current object position
        _agent.nextPosition = transform.position;

        // Draw the path
        DrawPath();
    }
    #endregion // Unity Message Handlers
}
