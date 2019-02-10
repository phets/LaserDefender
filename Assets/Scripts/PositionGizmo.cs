﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGizmo : MonoBehaviour {
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 1.0f);
    }
}
