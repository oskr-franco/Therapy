using Swashbuckle.AspNetCore.Filters;
using Therapy.Domain.DTOs.Account;
namespace TherapyAPI
{
  /// <summary>
  /// Provides an example of a RegisterDTO for Swagger.
  /// </summary>
  public class RegisterDTOExample : IExamplesProvider<RegisterDTO>
  {
    /// <summary>
    /// Returns an example RegisterDTO.
    /// </summary>
    public RegisterDTO GetExamples()
    {
      return new RegisterDTO
      {
        Email = "example@mail.com",
        Password = "Password123",
        ConfirmPassword = "Password123",
        FirstName = "FirstName",
        LastName = "LastName"
      };
    }
  }

  /// <summary>
  /// Provides an example of a LoginDTO for Swagger.
  /// </summary>
  public class LoginDTOExample : IExamplesProvider<LoginDTO>
  {
    /// <summary>
    /// Returns an example LoginDTO.
    /// </summary>
    public LoginDTO GetExamples()
    {
      return new LoginDTO
      {
        Email = "example@email.com",
        Password = "Password123"
      };
    }
  }

  /// <summary>
  /// Provides an example of a RefreshTokenDTO for Swagger.
  /// </summary>
  public class RefreshTokenDTOExample : IExamplesProvider<RefreshTokenDTO>
  {
    public RefreshTokenDTO GetExamples()
    {
      return new RefreshTokenDTO
      {
        RefreshToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
      };
    }
  }
}