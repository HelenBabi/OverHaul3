using Microsoft.AspNetCore.Http;

namespace Overhaul.AdminPanel.Models;

public class ErrorViewModel
{
    public string ErrorMessage { get; set; }
    public string InnerErrorMessage { get; set; }
}
public class CustomError
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }
}

