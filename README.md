# How to Run & How it Works


## 1. How to Run

1. 准备硬件：Kinect2一边连接电源，一边把USB连接到电脑上
   
2. 准备驱动：下载和安装 [Kinect 2 SDK](https://www.microsoft.com/en-us/download/details.aspx?id=44561)

3. 下载安装 Unity2022.3.21 并用Unity打开项目

4. 运行。然后站在Kinect摄像头前动一动，就可以看到人影拨动粒子的效果了

## 2. How it Works

场景里的 Motion Capture / U_CharacterFront 是目前接收动捕信号之后驱动的人物

具体的驱动方式可以阅读 AvatarController.cs, 这是 Kinect SDK内置的代码

另外，Motion Capture / KinectController 真正负责动捕信号的接收，阅读上面的代码也会有帮助

