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
    #endregion // Unity Inspector Variables

    #region Unity Message Handlers
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
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
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        // Always update the agent position to match the current object position
        _agent.nextPosition = transform.position;
    }
    #endregion // Unity Message Handlers
}
