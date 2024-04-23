
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

    /// <summary>
    /// Registers a new account.
    /// </summary>
    /// <param name="request">The account to register.</param>
    /// <returns>Ok if the account was created</returns>
    /// <response code="200">Returns Ok if the account was created.</response>
    /// <response code="400">If the account was not created.</response>
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

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="request">The login request.</param>
    /// <returns>The access token.</returns>
    /// <response code="200">Returns the access token.</response>
    /// <response code="400">If the login failed.</response>
    [AllowAnonymous]
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginDTO request)
    {
        var accessToken = await _accountService.LoginAsync(request);
        return Ok(accessToken);
    }

    /// <summary>
    /// Refreshes a token.
    /// </summary>
    /// <param name="request">The refresh token request.</param>
    /// <returns>The access token.</returns>
    /// <response code="200">Returns the access token.</response>
    /// <response code="400">If the token was not refreshed.</response>
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