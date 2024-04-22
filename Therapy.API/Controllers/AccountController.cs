
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Therapy.Core.Services.Accounts;
using Therapy.Domain.DTOs.Account;

/// <summary>
/// Controller for managing accounts.
/// </summary>
public class AccountController: ApiController {
    private readonly IAccountService _accountService;
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    /// <param name="accountService">The account service.</param>
    public AccountController(IAccountService accountService) {
        _accountService = accountService;
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDTO request)
    {
        var account = await _accountService.CreateAsync(request);
        if (account==null)
        {
            return BadRequest();
        }
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginDTO request)
    {
        var accessToken = await _accountService.LoginAsync(request);
        return Ok(accessToken);
    }

    [AllowAnonymous]
    [HttpPost("Refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenDTO request)
    {
      var authorizationHeader = Request.Headers["Authorization"].ToString();
      if (authorizationHeader.StartsWith("Bearer "))
      {
        var token = authorizationHeader.Substring("Bearer ".Length).Trim();
        var accessToken = await _accountService.RefreshTokenAsync(token, request.RefreshToken);
        return Ok(accessToken);
      }
      return Unauthorized();
    }
}