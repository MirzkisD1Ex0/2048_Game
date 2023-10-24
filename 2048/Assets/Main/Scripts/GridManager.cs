using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game2048
{
  public class GridManager : MonoBehaviour
  {
    public static GridManager Instance;

    public GameObject NodeGrids; // 父对象
    public List<GameObject> GridGO = new List<GameObject>();

    public int[,] GridValue = new int[4, 4];

    // ==================================================

    private void Awake()
    {
      Instance = this;
    }

    private void Start()
    {
      Init();
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.W))
      {
        SwipeUp();
      }
    }

    // ==================================================

    private void Init()
    {
      GetGridGO();
      GetGridValue();
      PrintGridValue();
      return;
    }

    /// <summary>
    /// 获取网格对象
    /// </summary>
    public void GetGridGO()
    {
      GridGO.Clear();
      for (int i = 0; i < NodeGrids.transform.childCount; i++)
      {
        GridGO.Add(NodeGrids.transform.GetChild(i).gameObject);
      }
      return;
    }

    /// <summary>
    /// 获取网格数值
    /// </summary>
    public void GetGridValue()
    {
      for (int i = 0; i < 4; i++)
      {
        for (int j = 0; j < 4; j++)
        {
          // 0*4+0 0*4+1 0*4+2 0*4+3
          // 1*4+0 1*4+1 1*4+2 1*4+3
          // 2*4+0
          GridValue[i, j] = int.Parse(GridGO[i * 4 + j].transform.GetChild(0).GetComponent<Text>().text);
        }
      }
      return;
    }

    /// <summary>
    /// DEBUG
    /// 打印数值
    /// </summary>
    public void PrintGridValue()
    {
      string debugMessage = null;
      for (int i = 0; i < GridValue.GetLength(1); i++)
      {
        for (int j = 0; j < GridValue.GetLength(0); j++)
        {
          debugMessage += GridValue[i, j].ToString() + "\t";
        }
        debugMessage += "\n";
      }
      Debug.Log("\n" + debugMessage);
      return;
    }

    // ==================================================


    /// <summary>
    /// 向上滑动
    /// </summary>
    public void SwipeUp()
    {
      // 0002
      // 0020
      // 0200
      // 2000

      for (int x = 0; x < GridValue.GetLength(0); x++) // 列循环
      {
        int[] tempColumn = new int[4];

        for (int i = 0; i < 4; i++) // 获取第一列数值
        {
          tempColumn[i] = GridValue[i, x];
        }

        tempColumn = PutZero2End(tempColumn);

        for (int i = 0; i < 4 - 1; i++) // 加
        {
          if (tempColumn[i] == tempColumn[i + 1])
          {
            tempColumn[i] *= 2;
            tempColumn[i + 1] = 0;
          }
        }

        tempColumn = PutZero2End(tempColumn);

        for (int i = 0; i < 4; i++) // 交还数据
        {
          GridValue[i, x] = tempColumn[i];
        }
      }

      UpdateAllGridsView();
      return;
    }

    /// <summary>
    /// 0排序至数组末尾
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private int[] PutZero2End(int[] value)
    {
      for (int i = 0; i < value.Length; i++)
      {
        for (int j = 0; j < value.Length - 1; j++)
        {
          if (value[j] == 0 && value[j + 1] != 0)
          {
            int tempValue = value[j];
            value[j] = value[j + 1];
            value[j + 1] = tempValue;
          }
        }
      }
      return value;
    }

    // ==================================================
    // View

    /// <summary>
    /// 更新网格视图
    /// </summary>
    /// <param name="value"></param>
    public void UpdateAllGridsView()
    {
      for (int i = 0; i < 4; i++)
      {
        for (int j = 0; j < 4; j++)
        {
          GridGO[i * 4 + j].transform.GetChild(0).GetComponent<Text>().text = GridValue[i, j].ToString();
        }
      }
      return;
    }
  }
}