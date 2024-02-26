namespace Application.Dto;

public class PersonByRutDto
{
    public string Rut { get; set; }
    public string FirstName { get; set; }
    public string ClientNumber { get; set; }
    public int Status { get; set; }
    public String Profile { get; set; }
    public DateTime Transfer_DateMinSuscription { get; set; }
    public DateTime Transfer_DateMinBalance { get; set; }
}
