using System.Net;

namespace Car_Api.Models;

public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; }
    
    public List<string> ErrorMessages { get; set; }
    public object Result { get; set; }
}