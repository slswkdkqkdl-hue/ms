using System;
using System.Formats.Asn1;
using System.Text;
class Program
{
    static void Main()
    {
     // 학생 3명, 과목 4개 (국어, 영어, 수학, 과학)
        int[,] scores = new int[3, 4]
        {
            { 85, 90, 88, 92 },  // 학생 1
            { 78, 85, 90, 87 },  // 학생 2
            { 92, 88, 95, 90 }   // 학생 3
        };
        
        string[] students = { "김철수", "이영희", "박민수" };
        string[] subjects = { "국어", "영어", "수학", "과학" };
        int a1;
        int a2;
        int a3;

        Console.WriteLine("=== 성적표 ===\n");
        
        // 헤더 출력
        Console.Write("이름\t");
        foreach (string subject in subjects)
        {
            Console.Write($"{subject}\t");
        }
        Console.WriteLine("평균");
        Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");



        for (int i = 0; i < scores.GetLength(0); i++)
        {
            Console.Write($"{students[i]}\t");
            
            int sum = 0;
            for (int j = 0; j < scores.GetLength(1); j++)
            {
                Console.Write($"{scores[i, j]}\t");
                sum += scores[i, j];
            
            }
            
            double average = (double)sum / scores.GetLength(1);
            Console.WriteLine($"{average:F1}");
        }
        Console.WriteLine("=== 과목별 평균 ===");
        
              Console.WriteLine("\n=== 과목별 평균 ===");
        for (int subject = 0; subject < scores.GetLength(1); subject++)
        {
            int sum = 0;
            for (int student = 0; student < scores.GetLength(0); student++)
            {
                sum += scores[student, subject];
            }
            double avg = (double)sum / scores.GetLength(0);
            Console.WriteLine($"{subjects[subject]}: {avg:F1}점");
        }
    }
}
