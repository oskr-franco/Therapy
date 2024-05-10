using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Therapy.Core.Services.AuthAccessor {
  public class AuthAccessorService: IAuthAccessorService {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClaimsPrincipal _cp;
    public AuthAccessorService(IHttpContextAccessor haccess) {
      _httpContextAccessor = haccess;
      _cp = _httpContextAccessor.HttpContext.User;
    }

    public int? GetId() {
      var id = _cp.FindFirst(c=> c.Type == ClaimTypes.NameIdentifier)?.Value;
      return id == null ? null : int.Parse(id);
    }
  }
}