using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 此时的摄像机必须转换 2D摄像机 来实现效果（即：摄像机属性Projection --> Orthographic）
        Vector3 dis = new Vector3(Input.GetAxis("Mouse X"),  0,Input.GetAxis("Mouse Y")) + this.transform.position;
        //使用Lerp方法实现 这里的Time.deltaTime是指移动速度可以自己添加变量方便控制
        this.transform.position= Vector3.MoveTowards(this.transform.position,dis,Time.deltaTime*8f);
       
    }
}
