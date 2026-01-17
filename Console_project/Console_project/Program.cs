using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

class Program
{
    const int width = 100;
    const int height = 26;

    public static int Game_Level;

    public static character SelectedCharacter; // 선택 캐릭터 담기
    public static Enemy[] enemy; // 난이도 선택대면 그만큼 몬스터담기

    public static List<Health_Potion> hp_Potion;   
    public static List<Mana_Potion> mp_Potion;   

    static Room currentRoom; // 현재 방
    static Room startRoom; // 시작 방
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
        Console.Clear();

        Console.SetWindowSize(width, height);
        Console.SetBufferSize(width, height);

        hp_Potion = new List<Health_Potion>();
        mp_Potion = new List<Mana_Potion>();
       
        for(int i = 0; i < 2; i++)
        {
            hp_Potion.Add(new Health_Potion());   
            mp_Potion.Add(new Mana_Potion());
        }
        
 

        DrawBorder();
        DrawTitle();
        DrawMenu();
        DrawInputBox(); // 입력 받고 로딩씬까지 보여주고 

        Difficulty_Selection_Scene(); // 난이도 선택씬
        character_selection_Scene(); // 캐릭터 선택씬 
          // 여기까지오면 Game_Level 얘량  SelectedCharacter 얘는 초기화되어있음
        Map_Sceen(); // 맵씬 이동
      
    

     
    }

    // ===================== 테두리 =====================
    static void DrawBorder()
    {
        // 상단 & 하단
        for (int x = 0; x < width; x++)
        {
            Console.SetCursorPosition(x, 0);
            Console.Write("■");

            Console.SetCursorPosition(x, height - 1);
            Console.Write("■");
        }

        // 좌측 & 우측 테두리 그리는 부분 수정
        for (int y = 0; y < height; y++)
        {
            Console.SetCursorPosition(0, y);
            Console.Write("■");

            // width - 1 이 아니라 width - 2 혹은 -3 정도로 여유 있게 땡기세요.
            // 이렇게 하면 전각 문자가 다음 줄로 넘어가는 걸 방지할 수 있습니다.
            Console.SetCursorPosition(width-1, y); 
            Console.Write("■");
        }
    }

    // ===================== 타이틀 =====================
// ===================== 타이틀 =====================
    static void DrawTitle()
    {
        string[] title =
        {
            "  ⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜                ■",
            "  ⬜🟦🟦🟦⬜🟦⬜⬜⬜🟦🟦🟦⬜🟦⬜⬜⬜🟦🟦🟦⬜⬜⬜🟦🟦🟦⬜⬜⬜🟦🟦🟦⬜                ■",
            "  ⬜🟦⬜⬜🟦🟦⬜⬜⬜⬜🟦⬜🟦🟦⬜⬜⬜🟦⬜⬜🟦⬜⬜🟦⬜⬜🟦⬜🟦⬜⬜🟦⬜                ■",
            "  ⬜🟦🟦🟦⬜🟦⬜⬜⬜🟦⬜🟦⬜🟦⬜⬜⬜🟦⬜⬜🟦⬜⬜🟦⬜⬜🟦⬜🟦⬜⬜⬜⬜                ■",
            "  ⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜🟦🟦🟦⬜⬜⬜🟦🟦🟦⬜⬜🟦⬜🟦🟦⬜                ■",
            "  ⬜⬜🟦⬜⬜⬜⬜⬜⬜⬜🟦⬜⬜⬜⬜⬜⬜🟦⬜⬜🟦⬜⬜🟦⬜⬜⬜⬜🟦⬜⬜🟦⬜                ■",
            "  ⬜⬜🟦🟦🟦🟦⬜⬜⬜⬜🟦🟦🟦🟦⬜⬜⬜🟦⬜⬜🟦⬜⬜🟦⬜⬜⬜⬜⬜🟦🟦🟦⬜                ■",
            "  ⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜⬜                ■"
        };

        int startY = 3;
        // startX 값을 15 정도로 두면 중앙에 비슷하게 맞을 겁니다.
        int startX = 15; 

        for (int i = 0; i < title.Length; i++)
        {
            Console.SetCursorPosition(startX, startY + i);
            Console.Write(title[i]);
        }
        
        // 출력 후 커서 위치 초기화
        Console.SetCursorPosition(0, 0);
    }

    // ===================== 메뉴 =====================
    static void DrawMenu()
    {
        int menuX = (width / 2) - 6;

        Console.SetCursorPosition(menuX, 15);
        Console.Write("1. 게임 시작");

        Console.SetCursorPosition(menuX, 17);
        Console.Write("2. 랭킹");

        Console.SetCursorPosition(menuX, 19);
        Console.Write("3. 끝내기");
    }

    // ===================== 캐릭터 출력 공용 =====================
    public static void DrawCharacter(string[] shape, int startX, int startY)
    {
        for (int i = 0; i < shape.Length; i++)
        {
            Console.SetCursorPosition(startX, startY + i);
            Console.Write(shape[i]);
        }
    }
    // 몬스터 UI
    static string[] monster(int i )
    {

        switch (i)
        {
            case 1:
                string[] slime =
                {
                    "   ★   ",
                    " (  ◉ )",
                    "  ▽ ▽ "
                };
                return slime;
                break;
            case 2:
                string[] Enderman =
                {   "   ▪︎  ",
                    "  ███ ",
                    "  ▓▒▓ ",
                    "   ▒  "
                };
                return Enderman;
                break;

            case 3:
                string[] cat =
                {
                    " /\\    /",
                    "(' )  (",
                    "(  \\  )",
                    "|(__)/ "
                };
                return cat;
        }
        return null;
   
    }

    // ===================== 캐릭터 선택 화면 =====================
    static void DrawCharacters()
    {
        int centerX = width / 2;
        int centerY = 6;

        // 전사
        string[] warrior =
        {
            "  O  |",
            " /|\\ ┼",
            " / \\ "
        };

        // 마법사
        string[] mage =
        {
        
            "   O   ▲",
            "  /|\\  ┬",
            "  / \\  |"
        };

        // 궁수
        string[] archer =
        {
            "   0  \\" ,
            "  /|\\  }▶",
            "  / \\ / "
        };

        DrawCharacter(warrior, centerX - 32, centerY);  //3
        DrawCharacter(mage,     centerX - 6,  centerY); // 3
        DrawCharacter(archer,   centerX + 20, centerY); // 3
 
        Console.SetCursorPosition(centerX - 31, centerY-2); // 5
        Console.Write("전사");

        Console.SetCursorPosition(centerX - 5, centerY-2); // 5
        Console.Write("마법사");

        Console.SetCursorPosition(centerX + 22, centerY-2); //5
        Console.Write("궁수");
    }

    // ===================== 입력창 =====================
    static void DrawInputBox()
    {
        Console.CursorVisible = true;

        int y = height - 4;

        while (true)
        {
            Console.SetCursorPosition(7, y);
            Console.Write("입력 :                                                                                   "); // 이전 입력 지우기
            Console.SetCursorPosition(7, y);
            Console.Write("입력 : ");

            string input = Console.ReadLine();

            Console.SetCursorPosition(5, y - 2);
            Console.Write(new string(' ', 50)); // 메시지 영역 초기화

            switch (input)
            {
                case "1":
                    Console.SetCursorPosition(5, y - 2);
                    Console.Write("환영 합니다");
                    Console.CursorVisible = false;
                    Thread.Sleep(2000);
                    loading_screen();
                    return; // 메뉴 종료

                case "2":
                    Console.SetCursorPosition(5, y - 2);
                    Console.Write(".......");
                    Thread.Sleep(800);
                    break; // 다시 입력 받으러 while 처음으로

                case "3":
                    Console.SetCursorPosition(5, y - 2);
                    Console.Write("게임을 종료합니다");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Environment.Exit(0);
                    return;

                default:
                    Console.SetCursorPosition(5, y - 2);
                    Console.Write("1 ~ 3 중에서 입력하세요!");
                    break;
            }
        }
    }
    // 로딩씬 
    public static void loading_screen()
    {
        Console.Clear();
        DrawBorder();
        
        // 커서 숨기기 (함수 시작 시 다시 한번 설정)
        Console.CursorVisible = false;

        int centerX = (width / 2) - 8;
        int centerY = height / 2;

        int duration = 3500; // 3초 동안 실행
        int interval = 500;  // 0.5초마다 모양 변경
        int elapsed = 0;
        bool toggle = true;

        while (elapsed < duration)
        {
            Console.SetCursorPosition(centerX, centerY);
            
            // 로딩 문구와 애니메이션을 한 줄에 출력
            if (toggle)
            {
                Console.Write("로딩 중 ● ○ ●");
            }
            else
            {
                Console.Write("로딩 중 ○ ● ○");
            }

            toggle = !toggle;
            Thread.Sleep(interval);
            elapsed += interval;
        }

        // 완료 후 문구 변경
        Console.SetCursorPosition(centerX, centerY);
      
    }
    // 로딩 애니메이션 (3초 동안 깜빡임)
    public static void LoadingAnimation(int x, int y)
    {
        int duration = 3000; // 3초 (3000ms)
        int interval = 500;  // 0.5초마다 모양 변경
        int elapsed = 0;

        bool toggle = true;

        while (elapsed < duration)
        {
            Console.SetCursorPosition(x, y);
            
            if (toggle)
            {
                Console.Write("● ○ ●");
            }
            else
            {
                Console.Write("○ ● ○");
            }

            toggle = !toggle; // 모양 뒤집기
            Thread.Sleep(interval);
            elapsed += interval;
        }
        

        Console.SetCursorPosition(x, y);
        Console.Write("      "); 
    }
    public static void Difficulty_Selection_Scene()
    {
        Console.Clear();
        DrawBorder();
        Console.SetCursorPosition(3,23);
        Console.CursorVisible = true;

        Drawlevel_UI();
        while (true)
        {   
                   
            Console.Write("입력 창 : ");
            string str = Console.ReadLine();
            
            if(str == "1") {
                Game_Level = 1;
                enemy = new Enemy[3];
                enemy[0]  = new Slime(Game_Level);
                enemy[1]  = new Monster_cat(Game_Level);
                enemy[2]  = new Endermen(Game_Level);
                break; // 쉬움
            }
            if(str == "2"){
                Game_Level = 2;
                enemy = new Enemy[3];
                enemy[0]  = new Slime(Game_Level);
                enemy[1]  = new Monster_cat(Game_Level);
                enemy[2]  = new Endermen(Game_Level);
                break;
            } // 중간
            if(str == "3") // 어려움
            {   
                Game_Level = 3;
                enemy = new Enemy[3];
                enemy[0]  = new Slime(Game_Level);
                enemy[1]  = new Monster_cat(Game_Level);
                enemy[2]  = new Endermen(Game_Level);
                break;
            }
            else
            {
                Console.SetCursorPosition(3,23);
                Console.Write(new string(' ', 50)); // 지우기
                Console.SetCursorPosition(3,23);
            }

        }
        

    }
    public static void Drawlevel_UI()
    {
        Console.Clear();
        DrawBorder();

        // ===== 타이틀 (Y를 3 -> 5로 변경) =====
        string title = " 난이도 선택";
        // 전각 문자(한글) 보정을 위해 X 좌표 계산 시 길이를 조절함
        Console.SetCursorPosition(((width - (title.Length * 2)) / 2), 4);
        Console.Write(title);

        // 박스 설정
        int boxWidth = 20;
        int boxHeight = 8;
        // ★ startY를 7에서 9로 변경 (전체적으로 2칸 내려감)
        int startY = 9; 

        int[] startX =
        {
            (width / 2) - boxWidth - 12,
            (width / 2) - (boxWidth / 2),
            (width / 2) + 12
        };

        string[] names = { "   쉬움", "   중간", "  어려움" };
        string[] desc1 = { " 적이 약합니다.", "아무런 변화 없음", "적이 강해집니다." };
        string[] desc2 = { " (능력치 감소)", "   (변화 없음)", "  (능력치 증가)" };

        for (int i = 0; i < 3; i++)
        {
            int x = startX[i];

            // 번호 위치 (startY 기준으로 자동 조절)
            Console.SetCursorPosition(x + (boxWidth / 2) - 1, startY - 2);
            Console.Write($" {i + 1}");

            // 박스 그리기
            for (int w = 0; w < boxWidth; w++)
            {
                Console.SetCursorPosition(x + w, startY);
                Console.Write("■");
                Console.SetCursorPosition(x + w, startY + boxHeight);
                Console.Write("■");
            }

            for (int h = 0; h <= boxHeight; h++)
            {
                Console.SetCursorPosition(x, startY + h);
                Console.Write("■");
                Console.SetCursorPosition(x + boxWidth - 1, startY + h);
                Console.Write("■");
            }

            // 텍스트 출력 (startY 기준으로 자동 조절)
            Console.SetCursorPosition(x + 5, startY + 2);
            Console.Write(names[i]);

            Console.SetCursorPosition(x + 2, startY + 4);
            Console.Write(desc1[i]);

            Console.SetCursorPosition(x + 2, startY + 5);
            Console.Write(desc2[i]);
        }

        // 입력 안내 메시지 위치
        Console.SetCursorPosition(3, height - 3);
    }

    public static void character_selection_Scene()
    {   
        Console.CursorVisible = false;
        Console.Clear();
        DrawBorder();
        character_UI();
        Console.SetCursorPosition(3,21); //21
        Console.CursorVisible = true;
        Console.Write("캐릭터를 선택해 주십시오.  1. 전사 2. 마법사 3. 궁수");
        Console.SetCursorPosition(3,23);
      
        

        while (true)
        {   
                   
            Console.Write("입력 창 : ");
            string str_c = Console.ReadLine();
            
            if(str_c == "1") {
                SelectedCharacter = new Warrior();
                break; 
            }
            if(str_c == "2"){
                SelectedCharacter = new Wizard();
                break;
            } 
            if(str_c == "3") 
            {   
                SelectedCharacter = new Archer();
                break;
            }
            else
            {
                Console.SetCursorPosition(3,23);
                Console.Write(new string(' ', 50)); // 지우기
                Console.SetCursorPosition(3,23);
            }

        }
    }
    public static void character_UI()
    {
        DrawCharacters();
        int centerX = width / 2; // 50
        int boxY = 11;           // 캐릭터 바로 아래에 붙도록 약간 위로 조정

        string[] Warrior_border =
        {
            "■■■■■■■■■■■■■■■",
            "■ HP : 20     ■",
            "■ MP : 6      ■",
            "■ Atk : 2     ■",
            "■ Def : 5     ■",
            "■ Speed : 3   ■",
            "■■■■■■■■■■■■■■■"
        };
        string[] Wizard_border =
        {
            "■■■■■■■■■■■■■■■",
            "■ HP : 14     ■",
            "■ MP : 10     ■",
            "■ Atk : 4     ■",
            "■ Def : 2     ■",
            "■ Speed : 2   ■",
            "■■■■■■■■■■■■■■■"
        };
        string[] Archer_border =
        {
            "■■■■■■■■■■■■■■■",
            "■ HP : 16     ■",
            "■ MP : 7      ■",
            "■ Atk : 3     ■",
            "■ Def : 3     ■",
            "■ Speed : 4   ■",
            "■■■■■■■■■■■■■■■"
        };

        // 캐릭터 출력 좌표와 정렬을 맞춤
        // 전사 위치(centerX - 32) 기준 박스 좌표
        DrawCharacter(Warrior_border, centerX - 35, boxY); 
        // 마법사 위치(centerX - 6) 기준 박스 좌표
        DrawCharacter(Wizard_border,  centerX - 8,  boxY); 
        // 궁수 위치(centerX + 20) 기준 박스 좌표
        DrawCharacter(Archer_border,  centerX + 17, boxY); 
    }
    public static void Map_Sceen()
    {   

        

        Console.CursorVisible = false;
        Console.Clear();
        DrawBorder();
        Console.SetCursorPosition(3,23);
        
        

        string[] map_top_border =
        {
            "■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■", //0
            "■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■",//1
        };
        DrawCharacter(map_top_border,0,0);
        string[] map_bottom_border =
        {
            "■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■", 
            "■■■■                        ■■■■■■                               ■■■■■■■                       ■■■■■",
            "■■■■                        ■■■■■■                               ■■■■■■■                       ■■■■■",
            "■■■■                        ■■■■■■                               ■■■■■■■                       ■■■■■",
            "■■■■                        ■■■■■■                               ■■■■■■■                       ■■■■■",
            "■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■"
        };
        DrawCharacter(map_bottom_border,0,20);

        // 맵 하단 왼쪽 UI
        Console.SetCursorPosition(5,21);
        Console.Write("※ 입력 설명 ※");
        Console.SetCursorPosition(8,22);
        Console.Write("  1.위로 가기");
        Console.SetCursorPosition(8,23);
        Console.Write(" 2.아래로 가기");
        Console.SetCursorPosition(6,24);
        Console.Write(" 3.오른쪽으로 가기");


        // 맵 가운데 UI
        Console.SetCursorPosition(46,21); //16
        Console.Write("입력창");
        Console.SetCursorPosition(37,23);
        Console.CursorVisible = true;


        //맵 하단 오른쪽 UI
        Console.SetCursorPosition(73,21);
        Console.Write("아이템 보유 수량");

        Console.SetCursorPosition(74,23);
        Console.Write($" 체력 포션 :  {hp_Potion.Count}개");

        Console.SetCursorPosition(74,24);
        Console.Write($" 마나 포션 :  {mp_Potion.Count}개");

        // 벽 세우기 (오른쪽 능력치)
        for (int i = 0; i < 18; i++)
        {
            Console.SetCursorPosition(72,2+i);
            Console.Write("■");
        }
        for (int i = 0; i < 18; i++)
        {
            Console.SetCursorPosition(71,2+i);
            Console.Write("■");
        }
        Console.SetCursorPosition(78,4);
        Console.Write(" <캐릭터 능력치>");
        Console.SetCursorPosition(77,7);
        Console.Write($"  직업 : {SelectedCharacter.Name}");
        Console.SetCursorPosition(78,9);
        Console.Write($" HP : {SelectedCharacter.hp} / {SelectedCharacter.max_hp}");
        Console.SetCursorPosition(78,11);
        Console.Write($" Mp : {SelectedCharacter.mp}");
        Console.SetCursorPosition(78,13);
        Console.Write($" Atk : {SelectedCharacter.atk}");
        Console.SetCursorPosition(78,15);
        Console.Write($" Def : {SelectedCharacter.def}");
        Console.SetCursorPosition(78,17);
        Console.Write($" Speed : {SelectedCharacter.speed}");
        
        room_UI();

        Console.SetCursorPosition(10,14);
        Console.Write("P"); // 시작 플레이 위치 표시

        Room next = null;

        while (true)
        {   
            Console.SetCursorPosition(37, 23);
            Console.Write("입력 :           "); 
            Console.SetCursorPosition(45, 23); 

            string str_m = Console.ReadLine();

    
            Console.SetCursorPosition(46, 23);
            Console.Write(new string(' ', str_m.Length + 5)); 

            if(str_m == "1")
            {
                break;
            }
            if(str_m == "2")
            {
                break;
            }
            if(str_m == "3")
            {
                break;
            }
            else
            {
                
            }
        }

        static void room_UI()
        {
            string[] normal =
            {
                "■■■■■■■■■",
                "■┌     ┐■",
                "■       ■",
                "■└     ┘■",
                "■■■■■■■■■"
            };
            string[] Boss =
            {
                "■■■■■■■■■■",
                "■┌      ┐■",
                "■        ■",
                "■└      ┘■",
                "■■■■■■■■■■"
            };

        
            DrawCharacter(normal,6,12); // 1번방 
            Line_1();
            DrawCharacter(normal,14,4); // 2번방
            Line_2();
            DrawCharacter(normal,23,13); // 3번방 
            Line_3();
            Line_5();
            DrawCharacter(normal,32,5); // 4번방  
            Line_4();
            DrawCharacter(normal,41,12); // 5번방 
            Line_6();
            DrawCharacter(Boss,54,5); // 보스방 6번방


        }
    }
    static void Line_1()
    {
        for (int i = 0; i < 6; i++)
        {
            Console.SetCursorPosition(10,11-i);
            Console.Write("■");
        }
        Console.SetCursorPosition(11,6);
        Console.Write("■");
        Console.SetCursorPosition(12,6);
        Console.Write("■");
        Console.SetCursorPosition(13,6);
        Console.Write("■");
    }
    static void Line_2()
    {
        Console.SetCursorPosition(18,9);
        Console.Write("■");
        int l = 0;
        while (l != 7)
        {
            Console.SetCursorPosition(18,9+l);
            Console.Write("■");
            l++;
        }
        Console.SetCursorPosition(19,15);
        Console.Write("■");
        Console.SetCursorPosition(20,15);
        Console.Write("■");
        Console.SetCursorPosition(21,15);
        Console.Write("■");
        Console.SetCursorPosition(22,15);
        Console.Write("■");
    }
    static void Line_3()
    {
        Console.SetCursorPosition(23,6);
        Console.Write("■");
        Console.SetCursorPosition(24,6);
        Console.Write("■");
        Console.SetCursorPosition(25,6);
        Console.Write("■");
        Console.SetCursorPosition(26,6);
        Console.Write("■");
        Console.SetCursorPosition(26,7);
        Console.Write("■");
        Console.SetCursorPosition(27,7);
        Console.Write("■");
        Console.SetCursorPosition(28,7);
        Console.Write("■");
        Console.SetCursorPosition(29,7);
        Console.Write("■");
        Console.SetCursorPosition(30,7);
        Console.Write("■");
        Console.SetCursorPosition(31,7);
        Console.Write("■");
    }
    static void Line_4()
    {
        Console.SetCursorPosition(42,7); //55
        Console.Write("■");
        for(int i = 0; i < 13; i++)
        {
            Console.SetCursorPosition(42+i,7); 
            Console.Write("■");
        }
    }
    static void Line_5()
    {
       
        for (int i = 0; i < 4; i++)
        {
            Console.SetCursorPosition(32+i,15); //x 36  //42
            Console.Write("■");
        }
        Console.SetCursorPosition(36,15);
        Console.Write("■");
        Console.SetCursorPosition(36,14);
        Console.Write("■");
        Console.SetCursorPosition(37,14);
        Console.Write("■");
        Console.SetCursorPosition(38,14);
        Console.Write("■");
        Console.SetCursorPosition(39,14);
        Console.Write("■");
        Console.SetCursorPosition(40,14);
        Console.Write("■");
        Console.SetCursorPosition(41,14);
        Console.Write("■");
    }
    static void Line_6()
    {
            //(50,14,)
        for (int i =0 ;i <11; i++)
        {   
            Console.SetCursorPosition(49+i,14); //65
            Console.Write("■");
        }
        for (int i = 0; i <8; i++)
        {
            Console.SetCursorPosition(59,14-i);
            Console.Write("■");
        }
    }
}

//////////// ⚔ ☠ ⚠
/// ///////// P : 플레이어 위치 & 플레이어가 지나간곳
/// ///////// E : 몬스터 방
/// ///////// B : 보스 방