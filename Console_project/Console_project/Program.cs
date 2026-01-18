using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
public enum StatType
{

    MP,
    ATK,
    DEF,
    HP,
    SPEED,
}
class Program
{
    const int width = 100;
    const int height = 26;

    public static int Game_Level;

    public static character SelectedCharacter; // 선택 캐릭터 담기
    public static Enemy[] enemy; // 난이도 선택대면 그만큼 몬스터담기
    public static Enemy Choice_enemy; // 선택된 몬스터

    public static List<Health_Potion> hp_Potion;   
    public static List<Mana_Potion> mp_Potion;   

    public static Room currentRoom; // 현재 방
    public static Room startRoom; // 시작 방
    public static Room next = null; // 처음 방초기화
    public static int atk_w; // 전사용
    public static int count= 1; //턴 수 잴거임이건
    public static int mp_save; // mp 만저장 왜? 왜난 쌈끝날때마다 계속다시채워줄거임
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
        
 
        mp_save = 0;
        DrawBorder();
        DrawTitle();
        DrawMenu();
        DrawInputBox(); // 입력 받고 로딩씬까지 보여주고 

        Difficulty_Selection_Scene(); // 난이도 선택씬
        character_selection_Scene(); // 캐릭터 선택씬 
          // 여기까지오면 Game_Level 얘량  SelectedCharacter 얘는 초기화되어있음
        CreateMap(); // 맵 생성
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
    static string[] monster(Enemy enemy)
    {

        switch (enemy.name)
        {
            case "슬라임":
                string[] slime =
                {
                    "   ★   ",
                    " (  ◉ )",
                    "  ▽ ▽ "
                };
                return slime;
                break;
            case "엔더맨":
                string[] Enderman =
                {   "   ▪︎  ",
                    "  ███ ",
                    "  ▓▒▓ ",
                    "   ▒  "
                };
                return Enderman;
                break;

            case "고양이":
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

        int duration = 3000; // 3초 동안 실행
        int interval = 400;  // 0.5초마다 모양 변경
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
        int duration = 2400; // 3초 (3000ms)
        int interval = 400;  // 0.5초마다 모양 변경
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
    static void CreateMap()
    {
        Room r1 = new Room { Id = 1, Type = RoomType.Start }; // R1
        Room r2 = new Room { Id = 2, Type = RoomType.Monster }; // R2
        Room r3 = new Room { Id = 3, Type = RoomType.Normal }; // R3
        Room r4 = new Room { Id = 4, Type = RoomType.Monster };// R4
        Room r5 = new Room { Id = 5, Type = RoomType.Normal };//R5
        Room r6 = new Room { Id = 6, Type = RoomType.Boss };//R6
        r1.IsCleared = true;
        // 연결
        r1.Up = r2;

        r2.Down = r4;
        r2.Right = r3;

        r3.Right = r6;
        r4.Right = r5;
        r5.Right = r6;


        // 시작 위치
        startRoom = r1;
        currentRoom = r1;
    }
    public static void Room_chek()
    {
        int i = currentRoom.Id;
        if(i == 1 && currentRoom.IsCleared) //R1
        {
            Console.SetCursorPosition(10,14); ///18,22
            Console.Write("P"); 
        }else if (i == 2 && currentRoom.IsCleared) // R2
        {
            Console.SetCursorPosition(18,6);
            Console.Write("P");
        }else if (i == 3&&currentRoom.IsCleared)  //R3 
        {
            Console.SetCursorPosition(36,7);
            Console.Write("P");
        }
        else if (i == 4&&currentRoom.IsCleared) //R4
        {
            Console.SetCursorPosition(27,15);
            Console.Write("P");
        }
        else if (i == 5&&currentRoom.IsCleared) //R5
        {
            Console.SetCursorPosition(45,14);
            Console.Write("P");
        }

        
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
        Console.SetCursorPosition(46,21); // 
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
        Room_chek();


      
        

        while (true)
        {   
            Console.SetCursorPosition(37, 23);
            Console.Write("입력 :           "); 
            Console.SetCursorPosition(45, 23); 

            string str_m = Console.ReadLine();

    
            Console.SetCursorPosition(46, 23);
            Console.Write(new string(' ', str_m.Length + 5)); 

            switch (str_m)
            {
                case "1": // 위
                    next = currentRoom.Up;
                    break;
                case "2": // 아래
                    next = currentRoom.Down;
                    break;
                case "3": // 오른쪽
                    next = currentRoom.Right;
                    break;
                default:
                    break;
            }
            if (next != null)
            {   
               Console.CursorVisible = false;
               Console.SetCursorPosition(36,22); 
               Console.Write("                          ");
               Console.SetCursorPosition(36,22); 
               Console.Write("⚔ 방으로 이동합니다.⚔");
               Thread.Sleep(2000); // 2초후에 이동
               currentRoom.IsCleared = true; // 방들어가기전에 그냥 그방은 클리어로해버림
               currentRoom = next;
               next.IsCleared = true;
              
               loading_screen();
               Battle_Sceen();
               break;
            }
            else
            {
               Console.SetCursorPosition(36,22); 
               Console.Write("그곳으로는 갈수 없습니다..");   
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
                "■  Boss  ■",
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
    public static void Battle_Sceen() 
    {
        Console.Clear();
        DrawBorder();
        mp_save = SelectedCharacter.mp; // mp저장 끝나고 채울려고
        atk_w = SelectedCharacter.atk;
        count = 1; // 전투 시작 시 1턴으로 초기화
        Battle_Sceen_UI();
        //여기서부터 배틀 로직작성

        // 각방에 나올 몬스터 설정
        if(currentRoom.Id == 2) Choice_enemy =enemy[0];
        if(currentRoom.Id == 3) Choice_enemy =enemy[1];
        if(currentRoom.Id == 4) Choice_enemy =enemy[1];
        if(currentRoom.Id == 5) Choice_enemy =enemy[0];
        if(currentRoom.Id == 6) Choice_enemy =enemy[2];


        Renewal(); // 모든 UI 갱신
        Tun_set(); // "1" 출력


        DrawCharacter(Character_Draw(),17,9); // 캐릭터그리기 이건고정
        DrawCharacter(monster(Choice_enemy),50,8); // 몬스터 그리기 1 이슬라임
        Renewal();
        
        Tun_set();
        Console.SetCursorPosition(39, 23);
        Console.Write("입력 :           ");

        // if(SelectedCharacter.speed < Choice_enemy.speed)
        // {
        //     Enemy_Tun();
        // }

        // 입력 처리
        while (true)
        {   

 

            Console.SetCursorPosition(39, 23);
            Console.Write("입력 :           "); 
            Console.SetCursorPosition(46, 23); 

            string str_m = Console.ReadLine();

    
            Console.SetCursorPosition(46, 23);
            Console.Write(new string(' ', str_m.Length + 5)); 
            
            switch (str_m)
            {
                case "1":
                    SelectedCharacter.Attack(Choice_enemy);
                    Renewal();
                    Console.ReadKey();
                    Clear_Output();
                    if(Choice_enemy.hp <= 0)
                    {
                        Win_Sceen();
                        break;
                    }
                    Enemy_Tun();
                    break;
                case "2":

                    if (hp_Potion.Count <= 0)
                    {
                        break;
                    }

                    if (SelectedCharacter.hp >= SelectedCharacter.max_hp)
                    {
                        break;
                    }

                    Health_Potion potion = hp_Potion[0];
                    SelectedCharacter.hp += potion.hp_heel;

                    if (SelectedCharacter.hp > SelectedCharacter.max_hp)
                        SelectedCharacter.hp = SelectedCharacter.max_hp;

                    hp_Potion.RemoveAt(0);

                    Renewal();
                    Console.SetCursorPosition(5,24);
                    Console.Write("체력 포션 사용");
                    Console.ReadKey();
                    Clear_Output();
                    Enemy_Tun();
                    break;

                case "3": 
                    if (mp_Potion.Count <= 0)
                    {
                        break;
                    }
                    Mana_Potion potion_m = mp_Potion[0];
                    SelectedCharacter.mp += potion_m.mp_heel;
                    mp_Potion.RemoveAt(0); 
                    Renewal();
                    Console.SetCursorPosition(5,24);
                    Console.Write("마나 회복 포션 사용");
                    Console.ReadKey();
                    Clear_Output();
                    Enemy_Tun();
                    break;
                case "a":
                    if (SelectedCharacter.mp < SelectedCharacter.Skills[0].MpCost)
                    {
                        break; 
                    }
                    UseSkill(0);
                    Renewal();
                    Console.ReadKey();
                    Clear_Output();
                    if(Choice_enemy.hp <= 0)
                    {
                        Win_Sceen();
                        break;
                    }
                    Enemy_Tun();
                    break;
                case "b":
                    if (SelectedCharacter.mp < SelectedCharacter.Skills[1].MpCost)
                    {
                        // Console.SetCursorPosition(36, 22);
                        break; // ← 중요
                    }
                    UseSkill(1);
                    Renewal();
                    Console.ReadKey();
                    Clear_Output();
                    if(Choice_enemy.hp <= 0)
                    {
                        Win_Sceen();
                        break;
                    };
                    Enemy_Tun();
                    break;
            
                default:
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(38,23); 
                    Console.Write("                          ");
                    break;
            }
            
            Tun_set();
            
        }

        //플레이어가 승리했을시 
        // Win_Sceen();
        //플레이어가 패배했을시
        // Lose_Sceen();
    }
    public static void Tun_set()
    {
        Console.SetCursorPosition(36, 3);
        // 뒤에 공백을 넣어 자릿수 변경 시 잔상 제거
        Console.Write($"{count}   "); 
    }
    public static void Enemy_Tun()
    {
        // 1. 적 턴 시작 메시지
        Console.SetCursorPosition(5, 24);
        Console.Write("적의 턴입니다...");
        Thread.Sleep(1000);
        
        // 2. 적 행동 수행
        Choice_enemy.DoAction(SelectedCharacter);
        Renewal(); // 행동 후 스탯 갱신 (내 HP가 깎인 것을 보여줌)

        // 3. 플레이어 사망 체크
        if (SelectedCharacter.hp <= 0)
        {
            Console.ReadKey();
            Lose_Sceen();
            return;
        }

        // 4. 한 라운드가 완전히 끝났으므로 턴 수 증가
        count++; 
        Tun_set(); // 증가된 턴 수 화면에 즉시 반영

        Console.ReadKey();
        Clear_Output();
    }
    public static void Renewal()
    {
        Enemy_hp_view(Choice_enemy); // 몬스터 체력 갱신
        Enemy_status_View(Choice_enemy); // 몬스터 능력 갱신
        Player_hp_View(); // 플레이어 체력 갱신
        Player_status_View(); // 플레이어 스테이스 갱신
        pot_set(); // 포션수 갱신
    }
    public static void pot_set()
    {
        Console.SetCursorPosition(74,23);
        Console.Write($" 체력 포션 :  {hp_Potion.Count}개");

        Console.SetCursorPosition(74,24);
        Console.Write($" 마나 포션 :  {mp_Potion.Count}개");
    }
    public  static void Clear_Output()
    {
        Console.SetCursorPosition(5,21);
        Console.Write("                     ");
        Console.SetCursorPosition(5,22);
        Console.Write("                     ");
        Console.SetCursorPosition(5,23);
        Console.Write("                     ");
        Console.SetCursorPosition(5,24);
        Console.Write("                     ");
    }
    static void Battle_Sceen_UI()
    {
            string[] Battle_bottom_border =
        {
            "■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■", 
            "■■■■                          ■■■■■■                             ■■■■■■■                       ■■■■■",
            "■■■■                          ■■■■■■                             ■■■■■■■                       ■■■■■",
            "■■■■                          ■■■■■■                             ■■■■■■■                       ■■■■■",
            "■■■■                          ■■■■■■                             ■■■■■■■                       ■■■■■",
            "■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■"
        };
        DrawCharacter(Battle_bottom_border,0,20);
        // 벽 세우기
        for (int i = 0; i <= 18; i++)
        {
            Console.SetCursorPosition(72,1+i);
            Console.Write("■");
        } //71
        int c =1;
        while(c != 72)
        {
            Console.SetCursorPosition(c,18);   
            Console.Write("■"); 
            Console.SetCursorPosition(c,19);
            Console.Write("■");
            c++;
        }
        Console.SetCursorPosition(83,2);
        Console.Write("<행동>");
        
        Console.SetCursorPosition(77,5);
        Console.Write($"1.공격하기");
        Console.SetCursorPosition(77,6);
        Console.Write("2.체력 포션 사용");
        Console.SetCursorPosition(77,7);
        Console.Write("3.마나 포션 사용");

        int t = 1;
        while (t != 27)
        {
            Console.SetCursorPosition(72+t,11);
            Console.Write("■");
            t++;
        }
        // 여기서 스킬이름 넣어주면댐
        Console.SetCursorPosition(78,14);
        Console.Write($"a. {SelectedCharacter.Skills[0].Name} MP : {SelectedCharacter.Skills[0].MpCost}");
        Console.SetCursorPosition(78,17);
        Console.Write($"b. {SelectedCharacter.Skills[1].Name} MP : {SelectedCharacter.Skills[1].MpCost}");


        // 아이템 칸 UI 
        Console.SetCursorPosition(73,21);
        Console.Write("아이템 보유 수량");

        Console.SetCursorPosition(74,23);
        Console.Write($" 체력 포션 :  {hp_Potion.Count}개");

        Console.SetCursorPosition(74,24);
        Console.Write($" 마나 포션 :  {mp_Potion.Count}개");

        int s1=0;
        while (s1 != 72)
        {
            Console.SetCursorPosition(1+s1,5);//72
            Console.Write("■");
            s1++;
        }
   
        for(int i = 1; i <=5; i++)
        {
            Console.SetCursorPosition(30,i);
            Console.Write("■");
            Console.SetCursorPosition(42,i);
            Console.Write("■");
        }
        Console.SetCursorPosition(33,1); // 36 중앙
        Console.Write("턴  수");

        Console.SetCursorPosition(36,10);
        Console.Write("VS");        
    }
    static bool UseSkill(int index)
    {
        Skill skill = SelectedCharacter.Skills[index];

        if (SelectedCharacter.mp < skill.MpCost)
        {
            return false; // 실패
        } 

        SelectedCharacter.mp -= skill.MpCost;
        skill.Effect(SelectedCharacter, Choice_enemy);

        return true; // 성공
    }


    public static string[] Character_Draw()
    {
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

        switch (SelectedCharacter.Name)
        {
            case "전사":
                return warrior;
                break;
            case "마법사":
                return mage;
                break;
            case "궁수":
                return archer;
                break;
        }
        return null;
    }
    public static void Enemy_hp_view(Enemy enemy)
    {
        Console.SetCursorPosition(50,13); // 몬스터
        int max = 0;
        if(enemy.hp <= 0)
        {
           Console.Write($"HP : {max}/{enemy.max_hp} ");
        }
        else
        {
           Console.Write($"HP : {enemy.hp}/{enemy.max_hp} "); 
        }
        
        
    }   
    // public static void Player_hp_View()
    // {
    //     Console.SetCursorPosition(14,13); // 플레이어
    //     Console.Write($"HP : {SelectedCharacter.hp}/{SelectedCharacter.max_hp}");
    //     Console.SetCursorPosition(14,14); // 플레이어
    //     Console.Write($"MP : {SelectedCharacter.mp}");
    // }
    public static void Player_hp_View()
    {
        Console.SetCursorPosition(14, 13);
        Console.Write($"HP : {SelectedCharacter.hp}/{SelectedCharacter.max_hp}   "); 
        Console.SetCursorPosition(14, 14);
        Console.Write($"MP : {SelectedCharacter.mp}   "); 
    }
    public static void Enemy_status_View(Enemy enemy)
    {
        Console.SetCursorPosition(63,1);
        Console.Write($"{enemy.name}");

        Console.SetCursorPosition(63,2);
        Console.Write($"ATK : {enemy.atk}");

        Console.SetCursorPosition(54,2);
        Console.Write($"Def : {enemy.def}");

        Console.SetCursorPosition(61,3);
        Console.Write($"Speed : {enemy.speed}");
    }
    public static void Player_status_View()
    {   
        Console.SetCursorPosition(4,1);
        Console.Write($"{SelectedCharacter.Name}");

        Console.SetCursorPosition(4,2);
        Console.Write($"Atk : {SelectedCharacter.atk}");

        Console.SetCursorPosition(14,2);
        Console.Write($"Def : {SelectedCharacter.def}");

        Console.SetCursorPosition(4,3);
        Console.Write($"Speed : {SelectedCharacter.speed}");   
    }
    public static void Tun_change_Enemy()
    {
        Console.SetCursorPosition(34,3);
        Console.Write("Enemy");
    }
    public static void Tun_change_Player()
    {
        Console.SetCursorPosition(33,3);
        Console.Write("Player");
    }

    static void ApplyRandomStatReward()
    {
        Random rand = new Random();

        // 강화할 스탯 개수 (1~3)
        int statCount = rand.Next(1, 4);

        //스탯 목록 생성
        List<StatType> stats = new List<StatType>
        {
            StatType.MP,
            StatType.ATK,
            StatType.DEF,
            StatType.SPEED,
            StatType.HP
            
        };

        // 스탯 섞기 (Shuffle)
        for (int i = 0; i < stats.Count; i++)
        {
            int j = rand.Next(i, stats.Count);
            (stats[i], stats[j]) = (stats[j], stats[i]);
        }

        // 앞에서 statCount 개 선택
        for (int i = 0; i < statCount; i++)
        {
            int value = rand.Next(1, 2); // 증가 수치 1~2

            switch (stats[i])
            {
                
                case StatType.HP:

                    Console.SetCursorPosition(76,9);
                    Console.Write($"+{value}");
                    SelectedCharacter.hp += value;
                    SelectedCharacter.max_hp +=value;
                    break;
                case StatType.MP:

                    Console.SetCursorPosition(73,10);
                    Console.Write($"+{value}");
                    SelectedCharacter.mp += value;
                    break;

                case StatType.ATK:
                    Console.SetCursorPosition(74,11);
                    Console.Write($"+{value}");
                    SelectedCharacter.atk += value;
                    break;

                case StatType.DEF:
                     Console.SetCursorPosition(74,12);
                     Console.Write($"+{value}");
                    SelectedCharacter.def += value;
                    break;
                case StatType.SPEED:
                    Console.SetCursorPosition(75,13);
                    Console.Write($"+{value}");
                    SelectedCharacter.speed += value;
                    break;
            }
        }
    }

    public static void Win_Sceen()
    {
        Console.Clear();
        DrawBorder();
        SelectedCharacter.mp = mp_save; //싸움끝나고 돌아왔을때 마나 다시충전
        SelectedCharacter.atk = atk_w; // 싸움끝나고 다시 공격력 원상복귀
        string[] ability =
        {
            "■■■■■■■■■■■■■■■■■■",
            "■                ■",
            "■  HP :          ■",
            "■  MP :          ■",
            "■  Atk :         ■",
            "■  Def :         ■",
            "■  Speed :       ■",
            "■                ■",
            "■■■■■■■■■■■■■■■■■■"    
        }; 
       
       if(currentRoom.Id != 6)
        {
            Console.SetCursorPosition(width/2-10,3);
            Console.Write("★  승리를 축하 합니다 ★");

            Console.SetCursorPosition(23,6);
            Console.Write("변경 전");

            Console.SetCursorPosition(62,6);
            Console.Write("변경 후");
            
            Console.SetCursorPosition(width/2+3,height/2-2); // 50 //15
            Console.Write("▶"); 
            Console.SetCursorPosition(width/2+2,height/2-2); // 50 //15
            Console.Write("▶"); 
            Console.SetCursorPosition(width/2+1,height/2-2); // 50 //15
            Console.Write("▶");
            Console.SetCursorPosition(width/2,height/2-2); // 50 //15
            Console.Write("▶");
            Console.SetCursorPosition(width/2-1,height/2-2); // 50 //15
            Console.Write("▶");
            Console.SetCursorPosition(width/2-2,height/2-2); // 50 //15
            Console.Write("▶");

            DrawCharacter(ability,23,7); 
            DrawCharacter(ability,62,7);

            //변경전 능력치 UI
            Console.SetCursorPosition(31,9);
            Console.Write($"{SelectedCharacter.hp}/{SelectedCharacter.max_hp}"); // 
            Console.SetCursorPosition(32,10);
            Console.Write($"{SelectedCharacter.mp}"); // 
            Console.SetCursorPosition(33,11);
            Console.Write($"{SelectedCharacter.atk}");
            Console.SetCursorPosition(33,12);
            Console.Write($"{SelectedCharacter.def}");
            Console.SetCursorPosition(34,13);
            Console.Write($"{SelectedCharacter.speed}");


            //변경 후 능력치 UI
            Console.SetCursorPosition(70,9); // 77
            Console.Write($"{SelectedCharacter.hp}/{SelectedCharacter.max_hp}"); // 
            Console.SetCursorPosition(71,10);
            Console.Write($"{SelectedCharacter.mp}"); // 
            Console.SetCursorPosition(72,11);
            Console.Write($"{SelectedCharacter.atk}");
            Console.SetCursorPosition(72,12);
            Console.Write($"{SelectedCharacter.def}");
            Console.SetCursorPosition(73,13);
            Console.Write($"{SelectedCharacter.speed}");

            ApplyRandomStatReward();
        
            Console.SetCursorPosition(3,21);
            Console.Write("0 입력시 맵으로 돌아가기");
            
            Console.SetCursorPosition(3,23);
            while (true)
            {   
                Console.CursorVisible = true;    
                Console.Write("입력 창 : ");
                string str_w = Console.ReadLine();
                
                if(str_w == "0") 
                {   
                    Map_Sceen();
                    break;
                }
                else
                {   
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(3,23);
                    Console.Write(new string(' ', 50)); // 지우기
                    Console.SetCursorPosition(3,23);
                }

            }
         
        }
        else if(currentRoom.Id == 6)
        {   
            Console.CursorVisible = false;
            Console.Clear();
            DrawBorder();
            int i =1;
            int r = 2;
            while (i !=25)
            {
                Console.SetCursorPosition(1,i);
                Console.Write("■");
                Console.SetCursorPosition(98,i);
                Console.Write("■");
                i++;
            
            }
            while(r != 98)
            {
                Console.SetCursorPosition(r,1);
                Console.Write("■");
                Console.SetCursorPosition(r,24);
                Console.Write("■");
                r++;
            }
            Console.SetCursorPosition(width/2-12,(height/2)-2);
            Console.Write("게임이 클리어 되었습니다.");
            Thread.Sleep(4000);

            Console.Clear();
            Environment.Exit(0);
        }
    }
    public static void Lose_Sceen()
    {
        Console.Clear();
        DrawBorder();
            int i1 =1;
            int r1 = 2;
            while (i1 !=25)
            {
                Console.SetCursorPosition(1,i1);
                Console.Write("■");
                Console.SetCursorPosition(98,i1);
                Console.Write("■");
                i1++;
            
            }
            while(r1 != 98)
            {
                Console.SetCursorPosition(r1,1);
                Console.Write("■");
                Console.SetCursorPosition(r1,24);
                Console.Write("■");
                r1++;
            }
        Console.SetCursorPosition(width/2-12,(height/2)-2);
        Console.Write("당신은 패배 하였습니다..");
        Thread.Sleep(4000);
        Console.Clear();
        Environment.Exit(0);
    }
}

//////////// ⚔ ☠ ⚠
/// ///////// P : 플레이어 위치 & 플레이어가 지나간곳
/// ///////// E : 몬스터 방
/// ///////// B : 보스 방