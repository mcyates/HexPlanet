﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Pentagon
{
  List<Vector3> corners;
  public Vector3 center;
  public List<Vector3> faces;
  public int pentagonIndex;

  public Pentagon(Vector3 center, Vector3[] triCenters, int pentagonIndex)
  {
    corners = new List<Vector3>();
    faces = new List<Vector3>();

    this.center = center;
    this.corners.AddRange(triCenters);
    this.pentagonIndex = pentagonIndex;

    // if (pentagonIndex == 0)
    // {
    SortFaces();
    // }
    corners.Add(corners[0]);

    Triangulate();
  }

  private void SortFaces()
  {

    List<Vector3> tempVectors = new List<Vector3>();

    tempVectors = corners;
    for (int x = 0; x < tempVectors.Count; x++)
    {
      Vector3 tempValue = tempVectors[x];

      for (int y = x; y < tempVectors.Count; y++)
      {
        float isClockWise = IsClockWise(tempValue, tempVectors[y]);
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
    for (int i = 0; i < 5; i++)
    {

      faces.Add(center);
      faces.Add(corners[i]);
      faces.Add(corners[i + 1]);
    }
  }
}
