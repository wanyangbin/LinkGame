using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 用于挂在在方块上的脚本
/// </summary>
public class Btn : MonoBehaviour {
    /// <summary>
    /// 显示方框的数字
    /// </summary>
    public Text btn_Text;
    /// <summary>
    /// 单利 用于引用自身
    /// </summary>
    public static Btn instance;
    /// <summary>
    /// 方框的x，y坐标
    /// </summary>
    public int x;
    public int y;
    /// <summary>
    /// 用于存储方框上的数据
    /// </summary>
    public Data_xxl data;
    /// <summary>
    /// 方块自身按钮
    /// </summary>
    public Button numBtn;
    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }
    void Start () {
        //当点击一个方块时 将方块上的数据传到XXl_main类里
        numBtn.onClick.AddListener(()=>{
            XXL_Main.instance.setPro(data);
        });
	}
    /// <summary>
    /// 用于更新方框上的数据源
    /// </summary>
    /// <param name="data"></param>
    public void setData( Data_xxl data )
    {
        //将data保存
        this.data = data;
        //将自身类保存到Data_XXl类里
        this.data.btn =this;
        if (data.num > 8888)
        {
            //将数字清空
            btn_Text.text =  "";
        }
        else
        {
        btn_Text.text =data.num+"";
        }
    }
}
