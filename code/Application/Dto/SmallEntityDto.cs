namespace Application.Dto;

public class SmallEntityDto
{
    public SmallEntityDto() { }
    public SmallEntityDto(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}
