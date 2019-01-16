﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlaneBuilder : MonoBehaviour {
	public static void Build (GameObject baseTarget, Transform targetPlane, int numberOfTargets, float targetWidth, float targetDistance) {
        if (numberOfTargets % 2 == 0) // If even number
        { 
            numberOfTargets++; // number of targets must be an odd number
        }

        float thetaStep = 2 * Mathf.PI / numberOfTargets;
        int startTargetIndex = 0;
        int finalTargetIndex = (int) numberOfTargets / 2 + 1;
        for (int i = 0; i < numberOfTargets; i++) {
            int targetPositionIndex;
            if (i % 2 == 0) {
                targetPositionIndex = startTargetIndex;
                startTargetIndex++;
            }
            else {
                targetPositionIndex = finalTargetIndex;
                finalTargetIndex++;
            }
            GameObject newTarget = Instantiate(baseTarget, targetPlane);
            newTarget.name = "Target " + i;
            newTarget.GetComponent<TargetBehaviour>().targetId = targetPositionIndex;
            newTarget.transform.localPosition = targetDistance/2 * (new Vector3(Mathf.Sin(targetPositionIndex * thetaStep), 0, Mathf.Cos(targetPositionIndex * thetaStep)));
            newTarget.transform.localScale = Vector3.one * targetWidth;
            newTarget.SetActive(true);
        }
	}
}