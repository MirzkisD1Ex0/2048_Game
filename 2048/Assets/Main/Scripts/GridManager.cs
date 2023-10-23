using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game2048
{
  public class GridManager : MonoBehaviour
  {
    public static GridManager Instance;

    public List<GameObject> GridGO = new List<GameObject>();

    public int[,] GridArray = new int[4, 4];

    // ==================================================

    private void Awake()
    {
      Instance = this;
    }

    private void Start()
    {

    }

    // ==================================================

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void UpdateAllGridsText(int[,] value)
    {

      return;
    }
  }
}