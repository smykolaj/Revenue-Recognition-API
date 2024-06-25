namespace Project.DTOs.Get;

public class CompanyGetDto
{
    public long IdCompany { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; }= string.Empty;
    public string PhoneNumber { get; set; }= string.Empty;
    public string Krs { get; set; }= string.Empty;
}