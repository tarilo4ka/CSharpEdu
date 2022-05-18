using Microsoft.AspNetCore.Mvc;
using AdvertisementApi.Models;

namespace AdvertisementApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SystemController : ControllerBase
{
    private readonly DataContext _context;

    public SystemController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("signup")]
    public async Task<ActionResult<User>> SignUp(User user)
    {
        if (_context.Users != null)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return BadRequest("User with the same username already exists");
            }
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return BadRequest("User with the same email already exists");
            }
        }

        _context.Users?.Add(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(string username, string password)
    {
        if (_context.Users != null)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                _context.CurrentUser?.Add(user);
                return Ok(user);
            }
        }
        await _context.SaveChangesAsync();
        return BadRequest("User not found");
    }
    

    [HttpDelete("deletecurrentuser")]
    public async Task<ActionResult<User>> Logout()
    {
        if (_context.Users == null)
        {
            return BadRequest("Users not found");
        }

        if (_context.CurrentUser == null)
        {
            return BadRequest("User not logged in");
        }
        // clear the CurrentUser
        User? current = _context.CurrentUser.FirstOrDefault();
        if (current != null)
        {
            _context.CurrentUser.Remove(current);
        }
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("currentuser")]
    public async Task<ActionResult<User>> GetCurrentUser()
    {
        
        if (_context.Users == null)
        {
            return BadRequest("Users not found");
        }

        if (_context.CurrentUser?.ToList().Count == 0)
        {
            return BadRequest("User not logged in");
        }

        int? ind = _context.CurrentUser?.ToList().Count -1;
        if (ind == null)
        {
            return BadRequest("User not logged in");
        }

        await _context.SaveChangesAsync();
        
        return Ok(_context.CurrentUser?.ToList()[(int)ind]);

    }

    [HttpGet("users")]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        if (_context.Users == null || _context.Users.ToList().Count == 0)
        {
            return BadRequest("Users not found");
        }
        await _context.SaveChangesAsync();
        return Ok(_context.Users);
    }

    [HttpGet("advertisements")]
    public async Task<ActionResult<List<Advertisement>>> GetAdvertisements()
    {
        if (_context.CurrentUser?.ToList().Count == 0)
        {
            return BadRequest("You are not logged in");
        }
        string? currentUserEmail = _context.CurrentUser?.ToList()[0].Email;
        if (_context.Advertisements == null || _context.Advertisements.ToList().Count == 0)
        {
            return BadRequest("Advertisements not found");
        }
        var advertisements = await _context.Advertisements.ToListAsync();

        advertisements = _context.Advertisements.Where(a => 
                   a.UserEmail == currentUserEmail
                ).ToList();

        await _context.SaveChangesAsync();
        return Ok(advertisements);
    }

    [HttpPost("advertisements")]
    public async Task<ActionResult<Advertisement>> AddAdvertisement(Advertisement advertisement)
    {
        if (_context.CurrentUser?.ToList().Count == 0)
        {
            return BadRequest("You are not logged in");
        }
        advertisement.UserEmail = _context.CurrentUser?.ToList()[0].Email;
        _context.Advertisements?.Add(advertisement);

        await _context.SaveChangesAsync();
        return Ok(advertisement);
    }
}
