namespace Therapy.Domain.Models {
  public class PaginationFilter {
    public int? PageSize { get; set; }
    public string? After {get; set; }
    public string? Before {get; set; }
    public string? Search { get; set; }
  }
}