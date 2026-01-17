public class Room
{
    public int Id;                // 방 번호
    public RoomType Type;         // 일반 / 몬스터 / 보스
    public bool IsCleared;        // 클리어 여부

    public Room Up;
    public Room Down;
    public Room Right;
}
public enum RoomType
{
    Start,
    Normal,
    Monster,
    Boss
}
