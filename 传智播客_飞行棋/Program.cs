using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 传智播客_飞行棋
{
    class Program
    {
        static int[] map = new int[100];// 静态字段，模拟全局变量
        static int[] PlayerPos = new int[2]; //静态数组，存储玩家A和B的坐标
        static string[] PlayerNames = new string[2];// 存储两个游戏玩家的姓名
        static bool[] Flags=new bool[2];//两个玩家的标记，默认为false
        static void Main(string[] args)
        {
            GameShow();


            //为了提高泡妞的针对性，请不要忘记改名字
            string girl = "";
            Console.WriteLine("{0}小姐姐可以和你玩个游戏吗？", girl);
            Console.WriteLine("请选择1-->可以，2-->不拒绝，3-->Of course，4-->无情且没有一点留恋地拒绝");
            string c = Console.ReadLine();
            while (c != "1" && c != "2" && c != "3")
            {
                if (c == "4")
                    Console.WriteLine("你想好了昂，再给你一次机会！");
                else
                    Console.WriteLine("别瞎写，请输入1或2或3或4  （>_<)");   
                c = Console.ReadLine();
            }
            Console.WriteLine("Take easy！");
            Console.ReadKey(true);



            InputPlayerNames();// 输入玩家姓名之后，清屏

            Console.Clear();// 清屏
            GameShow();

            Console.WriteLine("{0}的士兵用①表示。", PlayerNames[0]);
            Console.WriteLine("{0}的士兵用②表示。", PlayerNames[1]);
            //Console.ReadKey(false);
            InitialMap();// 画地图前，初始化地图
            DrawMap();

            //两个玩家都没到终点时玩游戏
            while (PlayerPos[0] < 99 && PlayerPos[1] < 99)
            {
                if (Flags[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    Flags[0] = false;
                }
                if (PlayerPos[0] >= 99)
                {
                    Console.WriteLine("玩家{0}靠着不要脸的精神无耻地赢了玩家{1}", PlayerNames[0], PlayerNames[1]);
                    break;
                }

                if (Flags[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    Flags[1] = false;
                }
                if (PlayerPos[1] >= 99)
                {
                    Console.WriteLine("玩家{0}靠着不要脸的精神无耻地赢了玩家{1}", PlayerNames[1], PlayerNames[0]);
                    break;
                }
            }//while

            Win();





            Console.ReadKey();
        }
        /// <summary>
        /// 游戏开头
        /// </summary>
        public static void GameShow()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor=ConsoleColor.DarkRed;
            Console.WriteLine("*****************************************************************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**********************************飞行棋大战*********************************");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("*****************************************************************************");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// 初始化地图，分配各形状的坐标
        /// </summary>
        public static void InitialMap()
        {
            int[] luckyturn = { 6, 23, 40, 55, 69, 83 };//幸运轮盘●
            for (int i = 0; i < luckyturn.Length; i++)
                map[luckyturn[i]] = 1;

            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//地雷※
            for (int i = 0; i < landMine.Length; i++)
                map[landMine[i]] = 2;

            int[] pause = { 9, 27, 60, 93 };//暂停▲
            for (int i = 0; i < pause.Length; i++)
                map[pause[i]] = 3;

            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };//时空隧道▓
            for (int i = 0; i < timeTunnel.Length; i++)
                map[timeTunnel[i]] = 4;

        }
        /// <summary>
        /// 绘制地图
        /// </summary>
        public static void DrawMap()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0:30}{1:30}{2:30}{3:30}{4:30}", "图例：普通方格：□ 什么也不发生\n", "      幸 运 盘：● 选择\n", "      地    雷：※ 退后6格\n ", "     暂    停：▲ 暂停一回合\n", "      时空隧道：▓ 前进10格");
            // 第一横行
            for (int i = 0; i < 30; i++)
            {
                Console.Write(DrawStringMap(i));
            }//for
            Console.WriteLine();
            // 第一竖列
            for (int i = 30; i < 35; i++)
            {
                for (int j = 0; j < 29; j++)
                {
                   // Console.Write("++");
                    Console.Write("  ");
                }
                Console.Write(DrawStringMap(i));
                Console.WriteLine();
            }
            // 第二横行
            for (int i = 64; i > 34; i--)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
            //第二竖列
            for (int i = 65; i < 70; i++)
            {
                Console.Write(DrawStringMap(i));
                for (int j = 1; j < 30; j++)
                {
                    Console.Write("  "); 
                }
                Console.WriteLine();
            }
            // 第三横行
            for (int i = 70; i < 100; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
        }//函数
        /// <summary>
        /// 判断每格应该用哪个形状填充
        /// </summary>
        /// <param name="i"></param>
        public static string DrawStringMap(int i) 
        {
            string str = "";
            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[0] == i)//两个玩家坐标相同&&都在地图上
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                str = "<>";
            }
            else if (PlayerPos[0] == i)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                str = "①";
            }
            else if (PlayerPos[1] == i)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                str = "②";
            }
            else
            {
                switch (map[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        str = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        str = "●";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        str = "※";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Green;
                        str = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Red;
                        str = "▓";
                        break;
                }//switch
            }//else   
            return str;
        }
        /// <summary>
        /// 输入玩家姓名
        /// </summary>
        public static void InputPlayerNames()
        {
            Console.WriteLine("请输入第一个玩家A的游戏名称：");
            PlayerNames[0] = Console.ReadLine();
            while (true)
            {
                if (PlayerNames[0] == "")
                {
                    Console.WriteLine("游戏名称不能为空！请重新输入玩家A的游戏名称：");
                    PlayerNames[0] = Console.ReadLine();
                }
                else
                    break;
            }
            Console.WriteLine("请输入第二个玩家B的游戏名称：");
            PlayerNames[1] = Console.ReadLine();
            while (true)
            {
                if (PlayerNames[0] == PlayerNames[1])
                {
                    Console.WriteLine("游戏名称不能与玩家A的游戏名称重复！请重新输入玩家B的游戏名称：");
                    PlayerNames[1] = Console.ReadLine();
                }
                else if (PlayerNames[1] == "")
                {
                    Console.WriteLine("游戏名称不能为空！请重新输入玩家B的游戏名称：");
                    PlayerNames[1] = Console.ReadLine();
                }
                else
                    break;
            }
     
        }

        /// <summary>
        /// 玩游戏
        /// </summary>
        public static void PlayGame(int playerNumber)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);

            Console.WriteLine("玩家{0}按任意键掷骰子", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("玩家{0}掷出了{1}", PlayerNames[playerNumber], rNumber);
            PlayerPos[playerNumber] += rNumber;
           
            ChangePos();//坐标改变，调用
            Console.ReadKey(true);
            Console.WriteLine("玩家{0}按任意键开始行动", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("玩家{0}行动完了", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            // 判断玩家A踩到了什么类型的方块
            if (PlayerPos[playerNumber] == PlayerPos[1 - playerNumber])//如果玩家A踩到玩家B，B退六格
            {
                Console.WriteLine("玩家{0}踩到了玩家{1}，玩家{2}往后退6格", PlayerNames[playerNumber], PlayerNames[1 - playerNumber], PlayerNames[1 - playerNumber]);
                PlayerPos[1 - playerNumber] -= 6;
                //ChangePos();
                Console.ReadKey(true);
            }
            else // 其他带有效果的方块
            {
                int position = map[PlayerPos[playerNumber]];//也可用switch,case
                if (position == 0)
                {
                    Console.WriteLine("玩家{0}踩到了方块，什么也没发生", PlayerNames[playerNumber]);
                    Console.ReadKey(true);
                }
                else if (position == 1)
                {
                    Console.WriteLine("玩家{0}踩到了幸运盘，请选择1-->交换位置，2-->轰炸对方", PlayerNames[playerNumber]);
                    string choiceStr = Console.ReadLine();
                    while (true)
                    {
                        if (choiceStr == "1")
                        {
                            Console.WriteLine("玩家{0}选择与玩家{1}交换位置", PlayerNames[playerNumber], PlayerNames[1 - playerNumber]);
                            Console.ReadKey(true);
                            int pos = PlayerPos[playerNumber];
                            PlayerPos[playerNumber] = PlayerPos[1 - playerNumber];
                            PlayerPos[1 - playerNumber] = pos;
                            Console.WriteLine("交换完成，按任意键继续！");
                            Console.ReadKey(true);
                            break;
                        }
                        else if (choiceStr == "2")
                        {
                            Console.WriteLine("玩家{0}选择轰炸玩家{1}，玩家{2}退6格", PlayerNames[playerNumber], PlayerNames[1 - playerNumber], PlayerNames[1 - playerNumber]);
                            Console.ReadKey(true);
                            PlayerPos[1 - playerNumber] -= 6;
                            //ChangePos();
                            Console.WriteLine("玩家{0}退了6格", PlayerNames[1 - playerNumber]);
                            Console.ReadKey(true);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("请输入1或2，1-->交换位置，2-->轰炸对方");
                            choiceStr = Console.ReadLine();
                        }
                    }//while
                }//else
                else if (position == 2)
                {
                    Console.WriteLine("玩家{0}踩到了地雷，退6格", PlayerNames[playerNumber]);
                    Console.ReadKey(true);
                    Console.WriteLine("玩家{0}退6格", PlayerNames[playerNumber]);
                    PlayerPos[playerNumber] -= 6;
                    // Console.WriteLine("玩家{0}退了6格", PlayerNames[0]);
                }//else
                else if (position == 3)
                {
                    Console.WriteLine("玩家{0}踩到了暂停，暂停一个回合", PlayerNames[playerNumber]);
                    Console.ReadKey(true);
                    Flags[playerNumber] = true;// 如果踩到地雷，标记为true
                }//else
                else if (position == 4)
                {
                    Console.WriteLine("玩家{0}踩到了时空隧道，前进10格", PlayerNames[playerNumber]);
                    PlayerPos[0] += 10;
                    Console.ReadKey(true);
                }//else
            }//if
            ChangePos();//保证玩家坐标在地图之上
            Console.Clear();
            DrawMap();
        }
        /// <summary>
        /// 当玩家坐标发生改变的时候调用，保证玩家坐标在地图上
        /// </summary>
        public static void ChangePos()
        {
            if (PlayerPos[0] < 0)
                PlayerPos[0] = 0;
            if (PlayerPos[0] >= 99)
                PlayerPos[0] = 99;
            if (PlayerPos[1] < 0)
                PlayerPos[1] = 0;
            if (PlayerPos[1] >= 99)
                PlayerPos[1] = 99;
        }
        /// <summary>
        /// 胜利效果展示
        /// </summary>
        public static void Win()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                          ◆                      ");
            Console.WriteLine("                    ■                  ◆               ■        ■");
            Console.WriteLine("      ■■■■  ■  ■                ◆■         ■    ■        ■");
            Console.WriteLine("      ■    ■  ■  ■              ◆  ■         ■    ■        ■");
            Console.WriteLine("      ■    ■ ■■■■■■       ■■■■■■■   ■    ■        ■");
            Console.WriteLine("      ■■■■ ■   ■                ●■●       ■    ■        ■");
            Console.WriteLine("      ■    ■      ■               ● ■ ●      ■    ■        ■");
            Console.WriteLine("      ■    ■ ■■■■■■         ●  ■  ●     ■    ■        ■");
            Console.WriteLine("      ■■■■      ■             ●   ■   ■    ■    ■        ■");
            Console.WriteLine("      ■    ■      ■            ■    ■         ■    ■        ■");
            Console.WriteLine("      ■    ■      ■                  ■               ■        ■ ");
            Console.WriteLine("     ■     ■      ■                  ■           ●  ■          ");
            Console.WriteLine("    ■    ■■ ■■■■■■             ■              ●         ●");
            Console.ResetColor();
        }
    }
}
