using System;
using System.Text;
using System.Threading;

class Program
{
    const int width = 100;
    const int height = 26;

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
        Console.Clear();

        // Console.SetWindowSize(width, height);
        // Console.SetBufferSize(width, height);

        // DrawBorder();
        // DrawTitle();
        // DrawMenu();
        // DrawCharacters();
        // DrawInputBox();

        // Console.ReadKey();
        Console.WriteLine("");
    
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

        // 좌측 & 우측
        for (int y = 0; y < height; y++)
        {
            Console.SetCursorPosition(0, y);
            Console.Write("■");

            Console.SetCursorPosition(width - 1, y);
            Console.Write("■");
        }
    }

    // ===================== 타이틀 =====================
    static void DrawTitle()
    {
        string title = "Dungeon RPG";
        int x = (width - title.Length) / 2;

        Console.SetCursorPosition(x, 3);
        Console.Write(title);
    }

    // ===================== 메뉴 =====================
    static void DrawMenu()
    {
        int menuX = (width / 2) - 6;

        Console.SetCursorPosition(menuX, 6);
        Console.Write("1. 게임 시작");

        Console.SetCursorPosition(menuX, 7);
        Console.Write("2. 랭킹");

        Console.SetCursorPosition(menuX, 8);
        Console.Write("3. 끝내기");
    }

    // ===================== 캐릭터 출력 공용 =====================
    static void DrawCharacter(string[] shape, int startX, int startY)
    {
        for (int i = 0; i < shape.Length; i++)
        {
            Console.SetCursorPosition(startX, startY + i);
            Console.Write(shape[i]);
        }
    }

    // ===================== 캐릭터 선택 화면 =====================
    static void DrawCharacters()
    {
        int centerX = width / 2;
        int centerY = 11;

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

        DrawCharacter(warrior, centerX - 32, centerY);
        DrawCharacter(mage,     centerX - 6,  centerY);
        DrawCharacter(archer,   centerX + 20, centerY);

        Console.SetCursorPosition(centerX - 30, centerY + 5);
        Console.Write("전사");

        Console.SetCursorPosition(centerX - 3, centerY + 5);
        Console.Write("마법사");

        Console.SetCursorPosition(centerX + 22, centerY + 5);
        Console.Write("궁수");
    }

    // ===================== 입력창 =====================
    static void DrawInputBox()
    {
        Console.CursorVisible = true;

        int y = height - 2;
        Console.SetCursorPosition(2, y);
        Console.Write("입력 : ");

        string input = Console.ReadLine();

        Console.CursorVisible = false;
        Console.SetCursorPosition(2, y - 2);
        Console.Write($"선택값 : {input}");

        // 연출용 대기
        Thread.Sleep(2000);
    }
}
