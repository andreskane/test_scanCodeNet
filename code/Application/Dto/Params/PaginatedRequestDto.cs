namespace Application.Dto.Params;

public abstract class PaginatedRequestDto
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
