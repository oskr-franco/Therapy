namespace Therapy.Domain.Models {
  public class PaginationResponse<T> {
    public IEnumerable<T> Data { get; set; }
    public string? FirstCursor { get; set; }
    public string? LastCursor { get; set; }
    public bool HasPrevPage { get; set; }
    public bool HasNextPage { get; set; }
  }
}