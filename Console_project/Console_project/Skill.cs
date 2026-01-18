public class Skill
{
    public string Name;      // 스킬명
    public int MpCost;       // 소비 MP
    public string Desc;      // 설명 (UI용)

    public Action<character, Enemy> Effect; 

}
