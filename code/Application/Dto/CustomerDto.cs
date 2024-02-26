namespace Application.Dto;

public class CustomerDto
{
    public string RUT { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string FirtsSurname { get; set; }
    public string SecondSurname { get; set; }
    public string ThirdSurname { get; set; }
    public string FullName { get; set; }
    public string Status { get; set; }
    public DateTime? Transfer_DateMinSuscription { get; set; }
    public DateTime? Transfer_DateMinBalance { get; set; }


    public string ClientNumber { get; set; }
    public string Profile { get; set; }
    public string CellPhone { get; set; }
    public string PersonType { get; set; }
    public string Alias { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int? Age { get; set; }
    public DateTime? DateOfDeath { get; set; }
    public string Gender { get; set; }
    public string MaritalStatus { get; set; }
    public string Contact { get; set; }
    public string PhoneContact { get; set; }
    public string Indafili { get; set; }
    public string Indrelcu { get; set; }
    public DateTime? Fecafil { get; set; }
    public string Indsilab { get; set; }
    public DateTime? Feinlabo { get; set; }
    public DateTime? Fejubil { get; set; }
    //public decimal Secdomi { get; set; }
    public DateTime? Fealspen { get; set; }
    public string Indestado { get; set; }
    public string Petdomic { get; set; }
    public string Tipopert { get; set; }
    public DateTime? Fedocact { get; set; }
    public string Street1 { get; set; }
    public string Street2 { get; set; }
    public string Number { get; set; }
    public string Floor { get; set; }
    public string Aparment { get; set; }
    public string NeighborHood { get; set; }
    public string Municipality { get; set; }
    public string Nation { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string PhoneType1 { get; set; }
    public string PhonePrefix1 { get; set; }
    public string PhoneNumber1 { get; set; }
    public string PhoneExt1 { get; set; }
    public string PhoneType2 { get; set; }
    public string PhonePrefix2 { get; set; }
    public string PhoneNumber2 { get; set; }
    public string Email { get; set; }
    public DateTimeOffset? LastAccess { get; set; }
}
