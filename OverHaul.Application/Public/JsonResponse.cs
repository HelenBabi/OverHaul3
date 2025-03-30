using Overhaul.Application.Enums;

namespace Overhaul.Application.Public;

public class JsonResponse
{
    public bool IsSuccess { get; set; }
    public object? Data { get; set; }
    public string? Message { get; set; }
    public MessageTitle ErrorCode { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int PageIndex { get; set; }
}
  