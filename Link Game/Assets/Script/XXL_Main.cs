using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class XXL_Main : MonoBehaviour
{
    public static XXL_Main instance;
    /// <summary>
    /// 关闭按钮
    /// </summary>
    public Button close;
    /// <summary>
    /// 两个Toggle 用于选择 游戏难度（简单，困难）
    /// </summary>
    public Toggle jd;
    public Toggle kn;
    /// <summary>
    /// 调整 按钮
    /// </summary>
    public Button tz;
    /// <summary>
    /// 游戏得分
    /// </summary>
    public Text gradeNum;
    private int grade = 0;
    /// <summary>
    /// 开始界面
    /// </summary>
    public GameObject startBtn;
    /// <summary>
    /// 开始按钮
    /// </summary>
    public Button sBtn;
    /// <summary>
    /// 再来一次按钮
    /// </summary>
    public Button againBtn;
    /// <summary>
    /// 退出按钮
    /// </summary>
    public Button exitBtn;
    /// <summary>
    /// 网格布局
    /// </summary>
    public GridLayoutGroup glg;
    /// <summary>
    /// 内容的父组件 
    /// </summary>  
    public Transform BJ_GameObject;
    /// <summary>
    /// 预制体
    /// </summary>
    public GameObject pre;
    //第一次点击的data
    public Data_xxl fistBtn;
    //第二次点击的data
    public Data_xxl twoBtn;
    //行列数
    private int hw=5;
    /// <summary>
    /// 用于存储一行的方块（按钮） 行为  x
    /// 列：为y
    /// </summary>
    List<GameObject> gameObj;
    List<List<GameObject>> LLGam = new List<List<GameObject>>();

    private void Awake()
    {
        instance = this;
    }
    /// <summary>
    /// 将btn预制体上的数组传递到这里
    /// </summary>
    /// <param name="data"></param>
    public void setPro(Data_xxl data)
    {
        //如果第一个数据不为空则放入第二个数据变量里
        if (fistBtn != null)
        {
            twoBtn = data;
        }
        else
        {
            fistBtn = data;
        }
        //调用消除方法
        xc();
    }
    //bool isXc = false;
    /// <summary>
    /// 消除方法
    /// </summary>
    public void xc()
    {
        //先判断两个按钮不为空
        if (fistBtn != null && twoBtn != null)
        {
            //第一个按钮上的数和第二个按钮上的数相等并且两个按钮不是一个按钮时
            if (fistBtn.num == twoBtn.num && fistBtn != twoBtn)//&&fistBtn != twoBtn
            {
                //都在第一行或第一列，最后一行或最后一列可以消除
                if ((fistBtn.x == twoBtn.x || fistBtn.y == twoBtn.y) && (fistBtn.x == 0 || fistBtn.x == 9 || fistBtn.y == 0 || fistBtn.y == 9))
                {

                    xcData();
                }

                if (fistBtn.x == twoBtn.x && vertical(fistBtn, twoBtn))
                {
                    //垂直连线
                    Debug.Log("垂直连线");
                    xcData();
                }
                if (fistBtn.y == twoBtn.y && horizon(fistBtn, twoBtn))
                {
                    Debug.Log("水平连线");
                    xcData();
                }
                if (oneCorner(fistBtn, twoBtn))
                {
                    Debug.Log("一个拐角");
                    xcData();
                }
                else
                {
                    Debug.Log("两个拐角");
                    if (twoCorner(fistBtn, twoBtn))
                    {
                        xcData();
                    }

                }

            }
            else
            {
                fistBtn = twoBtn;
            }

            fistBtn = null;
            twoBtn = null;
        }

    }
    /// <summary>
    /// 用于消除
    /// </summary>
    public void xcData()
    {
        //分数+1
        grade += 1;
        //将数字变成空
        gradeNum.text = grade + "";
        //88888时随便取的值 在Btn.cs里判断如果是这个值 就设置为空
        fistBtn.num = 888888;
        //更新第一个按钮
        fistBtn.btn.setData(fistBtn);
        //更新第二个按钮
        twoBtn.btn.setData(fistBtn);
        //将两个按钮设置为空
        LLGam[fistBtn.y][fistBtn.x] = null;
        LLGam[twoBtn.y][twoBtn.x] = null;
     
    }
    #region


    void Start()
    {
        //close.onClick.AddListener(()=> {
        //       Application.
        //});
        //默认选择为true
        jd.GetComponent<Toggle>().isOn = true;
        //困难默认是 false
        kn.GetComponent<Toggle>().isOn = false;
        //网格布局设置为列固定
        glg.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        //设置列固定数
        glg.constraintCount = hw;
        //简单toggle
        jd.onValueChanged.AddListener((v) => {
            //当选择简单时
            if (v)
            {
                hw = 5;
                //苦难设置为false
                kn.GetComponent<Toggle>().isOn = false;
            }
            else
            {//选择困难时
                hw = 10;
                //简单设置为true
                kn.GetComponent<Toggle>().isOn = true;
            }
            glg.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            glg.constraintCount = hw;
        });
        //与简单相反
        kn.onValueChanged.AddListener((v) => {
            if (v)
            {
                hw = 10;
                jd.GetComponent<Toggle>().isOn = false;
            }
            else
            {
                hw = 5;
                jd.GetComponent<Toggle>().isOn = true;
            }
            glg.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            glg.constraintCount = hw;
        });    
       // 调整
        tz.onClick.AddListener(() =>
        {
         //生成0-7的随机数   
            int sz = Random.Range(0, 7);
            #region 随机调整
            if (sz == 0)
            {

                glg.startCorner = GridLayoutGroup.Corner.LowerLeft;
            }else if (sz == 1)
            {
                glg.startCorner = GridLayoutGroup.Corner.LowerRight;
            }
            else if (sz == 2)
            {
                glg.startCorner = GridLayoutGroup.Corner.UpperLeft;
            }
            else if (sz == 3)
            {
                glg.startCorner = GridLayoutGroup.Corner.UpperRight;
            }
            else if (sz == 4)
            {
                glg.startAxis = GridLayoutGroup.Axis.Horizontal;
            }
            else if (sz == 5)
            {
                glg.startAxis = GridLayoutGroup.Axis.Vertical;
            }
            else
            {
                glg.startAxis = GridLayoutGroup.Axis.Vertical;
                glg.startCorner = GridLayoutGroup.Corner.UpperRight;
            }
            #endregion

        });   
        //默认显示开始界面
        startBtn.SetActive(true);
        //当点击开始时
        sBtn.onClick.AddListener(() =>
        {
            //开始界面隐藏
            startBtn.SetActive(false);
            //实例化游戏数据
            slYx();
        });
        //再来一次
        againBtn.onClick.AddListener(() =>
        {
            //清楚游戏数据
            CleanGameData();
            //实例化游戏数据
            slYx();
        });
        //关闭
        exitBtn.onClick.AddListener(() =>
        {
            //开始界面显示
            startBtn.SetActive(true);
            //清楚游戏数据
            CleanGameData();
        });

    }
    /// <summary>
    ///  清除游戏界面所有数据
    /// </summary>
    public void CleanGameData()
    {
        //遍历消除游戏数据
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }
    /// <summary>
    /// 实例化游戏界面数据
    /// </summary>
    public void slYx()
    {
        for (int i = 0; i < hw; i++)
        {
            gameObj = new List<GameObject>();
            for (int j = 0; j < hw; j++)
            {
                //将每行生成的一个按钮放入gameObj这个list中
                gameObj.Add(FwInstantiate(i, j, Random.Range(0, hw+1)));

            }
            //将每行放入LLgam  List中
            LLGam.Add(gameObj);
        }
    }
    /// <summary>
    /// 实例化一个按钮
    /// </summary>
    /// <param name="i">x坐标</param>
    /// <param name="j">y坐标</param>
    /// <param name="num">显示 的数值</param>
    /// <returns></returns>
    GameObject FwInstantiate(int i, int j, int num)
    {
        //实例化一个符文组件并将其转换层gameObject类型
        GameObject games = Instantiate(pre, BJ_GameObject, false) as GameObject;
        Data_xxl data = new Data_xxl(j, i, num);
        //获得符文对象并创建
        games.GetComponent<Btn>().setData(data);
        return games;


    }
    /// <summary>
    /// 垂直连线
    /// </summary>
    /// <param name="a">第一个按钮</param>
    /// <param name="b">第二个按钮</param>
    /// <returns></returns>
    private bool vertical(Data_xxl a, Data_xxl b)
    {
        int start = a.y < b.y ? a.y : b.y;        //获取a,b中较小的y值
        int end = a.y < b.y ? b.y : a.y;          //获取a,b中较大的值
        //遍历a，b之间是否通路 如果有一个不是就返回false
        for (int i = start + 1; i < end; i++)
        {
            Debug.Log("垂直:" + LLGam[i][a.x]);
            if (LLGam[i][a.x] != null)
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 水平连线
    /// </summary>
    /// <param name="a">第一个按钮</param>
    /// <param name="b">第二个按钮</param>
    /// <returns></returns>
    private bool horizon(Data_xxl a, Data_xxl b)
    {
        int start = a.x < b.x ? a.x : b.x;        //获取a,b中较小的y值
        int end = a.x < b.x ? b.x : a.x;          //获取a,b中较大的值
        //遍历a，b之间是否通路 如果有一个不是就返回false
        for (int i = start + 1; i < end; i++)
        {
            Debug.Log("水平:" + LLGam[a.y][i]);
            if (LLGam[a.y][i] != null)
            {

                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 一个拐角
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private bool oneCorner(Data_xxl a, Data_xxl b)
    {
        Data_xxl c = new Data_xxl(a.x, b.y);
        Data_xxl d = new Data_xxl(b.x, a.y);
        Debug.Log(LLGam[c.y][c.x]);
        //判断c点是否有元素
        if (LLGam[c.y][c.x] == null)
        {
            return horizon(b, c) && vertical(a, c);
        }
        //判断d点是否有元素
        if (LLGam[d.y][d.x] == null)
        {
            return horizon(a, d) && vertical(b, d);
        }
        return false;
    }
    /// <summary>
    /// 两个拐角
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    /// 广度优先算法
    private bool twoCorner(Data_xxl a, Data_xxl b)
    {
       
        //当存在一个c点 c点与a垂直或水平切c与b有一个拐角，c与b垂直或水平切c与a有一个拐角 
        foreach (Data_xxl d in listBtnTwoConrner(a, b))
        {
            if (oneCorner(d, b))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 记录一个按钮水平至不为空位置btn之间的按钮
    /// </summary>
    /// <returns></returns>
    public List<Data_xxl> listBtnTwoConrner(Data_xxl a, Data_xxl b)
    {
        List<Data_xxl> lb = new List<Data_xxl>();
       Data_xxl startx = a.x < b.x ? a : b;        //获取a,b中较小的y值                                           
        Data_xxl starty = a.y < b.y ? a : b;        //获取a,b中较小的y值
        Data_xxl endx = a.x > b.x ? a : b;                                           
        Data_xxl endy = a.y > b.y ? a : b;
        if (startx.x < hw)
        {
            for (int i = startx.x + 1; LLGam[startx.y][i] == null && i < hw; i++)
            {
                lb.Add(new Data_xxl(i, startx.y));
            }
        }
        if (starty.y < hw)
        {
            for (int i = starty.y + 1; LLGam[i][starty.x] == null && i < hw; i++)
            {
                lb.Add(new Data_xxl(starty.x, i));
            }
        }
        if (lb != null)
        {
            if (endx.x > 1)
            {
                for (int i = endx.x - 1; LLGam[endx.y][i] == null && i > 0; i--)
                {
                    lb.Add(new Data_xxl(i, startx.y));
                }
            }
            if (endy.y > 1)
            {
                for (int i = endy.y - 1; LLGam[i][starty.x] == null && i > 0; i--)
                {
                    lb.Add(new Data_xxl(starty.x, i));
                }
            }
        }
       
       
        Debug.Log(lb.Count);
   
        return lb;
    }
}
#endregion