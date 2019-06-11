﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialMeasurements {
    private TrialMeasurements lastTrial;

    public int trialId { get; private set; }

    public int initialTargetId { get; private set; }
    public int finalTargetId { get; private set; }

    public Vector3 initialTargetPosition { get; private set; }
    public Vector3 finalTargetPosition { get; private set; }

    public float initialTime { get; private set; }
    public float finalTime { get; private set; }
    public float trialDuration { get; private set; }

    public Vector3 initialPosition { get; private set; }
    public Vector3 finalPosition { get; private set; }
    public double finalPositionProjectedOnMovementAxis { get; private set; }
    public double effectiveAmplitudeOfMovement { get; private set; }
    public double distanceErrorFromTarget { get; private set; }

    public bool missedTarget { get; private set; }
    public bool isMarkedAsOutlier { get; private set; }

    public TrialMeasurements(int trialId, TargetBehaviour initialTarget, TargetBehaviour finalTarget, TrialMeasurements lastTrial = null) {
        this.lastTrial = lastTrial;
        this.trialId = trialId;
        this.initialTargetId = initialTarget.targetId;
        this.finalTargetId = finalTarget.targetId;
        this.initialTargetPosition = initialTarget.position;
        this.finalTargetPosition = finalTarget.position;
        this.initialTime = -1;
        this.finalTime = -1;
        this.trialDuration = -1;
    }

    public void StartTrial(float initialTime, Vector3 initialPosition)
    {
        this.initialTime = initialTime;
        this.initialPosition = initialPosition;
    }

    public void ForceInitialTime(float initialTime)
    {
        this.initialTime = initialTime;
    }

    public void FinishTrial(float finalTime, Vector3 finalPosition, bool missedTarget)
    {
        this.finalTime = finalTime;
        this.trialDuration = this.finalTime - this.initialTime;
        this.finalPosition = finalPosition;
        this.missedTarget = missedTarget;

        this.finalPositionProjectedOnMovementAxis = ResultsMath.Projected3DPointCoordinate(initialTargetPosition, finalTargetPosition, finalPosition);

        double lastProjection = (lastTrial == null) ? 0 : lastTrial.finalPositionProjectedOnMovementAxis;
        this.effectiveAmplitudeOfMovement = ResultsMath.EffectiveAmplitude(
                initialTargetPosition, finalTargetPosition, this.finalPositionProjectedOnMovementAxis, lastProjection);
        this.distanceErrorFromTarget = (finalPosition - finalTargetPosition).magnitude;
    }

    public void MarkAsOutlier(bool isOutlier)
    {
        this.isMarkedAsOutlier = isOutlier;
    }
}
