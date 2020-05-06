﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Hexagon
{
  List<Vector3> corners;
  public Vector3 center;
  public List<Vector3> faces;

  public Hexagon(Vector3 center, Vector3[] triCenters)
  {
    corners = new List<Vector3>();
    faces = new List<Vector3>();
    this.center = center;
    this.corners.AddRange(triCenters);

    SortFaces();
    corners.Add(corners[0]);
    Triangulate();
  }

  private void SortFaces()
  {
    // int axis = FindAxis(center);

    List<Vector3> tempVectors = new List<Vector3>();
    // List<Vector3> tempVectors2 = new List<Vector3>();


    // int virtXAxis = 0;
    // int virtYAxis = 2;
    // if (axis == 0)
    // {
    //   virtXAxis = 1;
    //   virtYAxis = 2;
    // }
    // else if (axis == 2)
    // {
    //   virtXAxis = 0;
    //   virtYAxis = 1;
    // }

    // for (int i = 0; i < corners.Count; i++)
    // {
    //   Vector3 newVector;
    //   newVector = corners[i];
    //   newVector[axis] = center[axis];

    //   tempVectors.Add(newVector);
    // }

    tempVectors = corners;
    for (int x = 0; x < tempVectors.Count; x++)
    {
      Vector3 tempValue = tempVectors[x];

      for (int y = x; y < tempVectors.Count; y++)
      {
        float isClockWise = IsClockWise(Vector3.ProjectOnPlane(tempValue, center), Vector3.ProjectOnPlane(tempVectors[y], center));
        if (isClockWise < 0)
        {
          tempValue = tempVectors[x];
          tempVectors[x] = tempVectors[y];
          tempVectors[y] = tempValue;
        }
      }
    }
    corners = tempVectors;


  }

  private float IsClockWise(Vector3 verticeA, Vector3 verticeB)
  {
    float result = Vector3.Dot(center, Vector3.Cross(verticeA - center, verticeB - center));
    return result;
  }

  private void Triangulate()
  {
    for (int i = 0; i < 6; i++)
    {
      //   faces.Add(new HexFace(center, corners[i], corners[i + 1]));
      faces.Add(center);
      faces.Add(corners[i]);
      faces.Add(corners[i + 1]);
    }
  }
}
