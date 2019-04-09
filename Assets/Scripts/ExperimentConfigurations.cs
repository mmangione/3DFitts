﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExperimentConfiguration
{
    public abstract float GetCursorDiameter();
    public abstract ExperimentTask GetExperimentTask();
    public abstract int GetNumBlocksPerTest();
    public abstract int GetNumTargetsPerTest();
    public abstract PlaneOrientation[] GetPlaneOrientationsToTest();
    public abstract IndexOfDifficultyConfiguration[] GetTargetConfigurationsToTest();
}

public class TappingMouseExperimentConfiguration : ExperimentConfiguration
{
    static readonly PlaneOrientation[] planeOrientations = { PlaneOrientation.PlaneYZ };
    static readonly IndexOfDifficultyConfiguration[] configurations = {
        new IndexOfDifficultyConfiguration(0.04f, 0.2f),
        new IndexOfDifficultyConfiguration(0.04f, 0.3f),
        new IndexOfDifficultyConfiguration(0.04f, 0.4f),
        new IndexOfDifficultyConfiguration(0.015f, 0.2f),
        new IndexOfDifficultyConfiguration(0.015f, 0.3f),
        new IndexOfDifficultyConfiguration(0.015f, 0.4f),
        new IndexOfDifficultyConfiguration(0.005f, 0.2f),
        new IndexOfDifficultyConfiguration(0.005f, 0.3f),
        new IndexOfDifficultyConfiguration(0.005f, 0.4f)
    };

    public override float GetCursorDiameter()
    {
        return 0.01f;
    }

    public override ExperimentTask GetExperimentTask()
    {
        return ExperimentTask.ReciprocalTapping;
    }

    public override int GetNumBlocksPerTest()
    {
        return 1;
    }

    public override int GetNumTargetsPerTest()
    {
        return 9;
    }

    public override PlaneOrientation[] GetPlaneOrientationsToTest()
    {
        return planeOrientations;
    }

    public override IndexOfDifficultyConfiguration[] GetTargetConfigurationsToTest()
    {
        return configurations;
    }
}


public class DragMouseExperimentConfiguration : ExperimentConfiguration
{
    static readonly PlaneOrientation[] planeOrientations = { PlaneOrientation.PlaneYZ, PlaneOrientation.PlaneXY, PlaneOrientation.PlaneZX };
    static readonly IndexOfDifficultyConfiguration[] configurations = {
        new IndexOfDifficultyConfiguration(0.04f, 0.2f),
        new IndexOfDifficultyConfiguration(0.04f, 0.3f),
        new IndexOfDifficultyConfiguration(0.04f, 0.4f),
        new IndexOfDifficultyConfiguration(0.02f, 0.2f),
        new IndexOfDifficultyConfiguration(0.02f, 0.3f),
        new IndexOfDifficultyConfiguration(0.02f, 0.4f),
        new IndexOfDifficultyConfiguration(0.01f, 0.2f),
        new IndexOfDifficultyConfiguration(0.01f, 0.3f),
        new IndexOfDifficultyConfiguration(0.01f, 0.4f)
    };

    public override float GetCursorDiameter()
    {
        return 0.01f;
    }

    public override ExperimentTask GetExperimentTask()
    {
        return ExperimentTask.Dragging;
    }

    public override int GetNumBlocksPerTest()
    {
        return 1;
    }

    public override int GetNumTargetsPerTest()
    {
        return 9;
    }

    public override PlaneOrientation[] GetPlaneOrientationsToTest()
    {
        return planeOrientations;
    }

    public override IndexOfDifficultyConfiguration[] GetTargetConfigurationsToTest()
    {
        return configurations;
    }
}