using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_xxl {
    /// <summary>
    /// 存储 x,y坐标
    /// </summary>
   public int x;
  public  int y;
    /// <summary>
    /// 显示的数字
    /// </summary>
  public  int num;
    /// <summary>
    /// 存储按钮脚本
    /// </summary>
    public Btn btn;

    public Data_xxl(int x,int y,int num)
    {
        this.x = x;
        this.y = y;
        this.num = num;
    }
    public Data_xxl(int x, int y)
    {
        this.x = x;
        this.y = y;
       
    }
}
