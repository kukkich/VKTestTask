using Microsoft.AspNetCore.Mvc;
using VKTestTask.Domain.Dto;
using VKTestTask.Services.Users;

namespace VKTestTask.Controllers;

[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _userService.Get(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userService.GetAll();
        return Ok(result);
    }

    [HttpGet]
    [ActionName("pagination")]
    public async Task<IActionResult> Get(Page page)
    {
        if (!page.IsInValidState)
            return BadRequest();

        var result = await _userService.GetFromPage(page);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] UserCreationModel user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)));

        var result = await _userService.Create(user);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove([FromBody] int userId)
    {
        var result = await _userService.Remove(userId);
        return Ok(result);
    }
}