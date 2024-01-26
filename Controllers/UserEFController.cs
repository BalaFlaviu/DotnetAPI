using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using DotnetAPI.Data;
using Microsoft.Extensions.Configuration.UserSecrets;
using AutoMapper;
using System.Linq;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    DataContextEF _entityFramework;
    IMapper _mapper;
    public UserEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserToAddDto, User>();
        }));
    }

    [HttpGet("GetUsers")]
    // public IActionResult Test()
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _entityFramework.Users.ToList<User>();
        return users;

    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        User? user = _entityFramework.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();
        if (user != null)
        {
            return user;
        }

        throw new Exception("Failed to get User");
    }

    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo GetUserJobInfo(int userId)
    {
        UserJobInfo? userJobInfo = _entityFramework.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault<UserJobInfo>();
        if (userJobInfo != null)
        {
            return userJobInfo;
        }

        throw new Exception("Failed to get User");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {

        User? userDb = _entityFramework.Users
            .Where(u => u.UserId == user.UserId)
            .FirstOrDefault<User>();

        if (userDb != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            if (_entityFramework.SaveChanges() >0)
            {
                return Ok();
            }
        }

        throw new Exception("Failed to Edit User");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        User? userDb = _mapper.Map<User>(user);

        //userDb.Active = user.Active;
        //userDb.FirstName = user.FirstName;
        //userDb.LastName = user.LastName;
        //userDb.Email = user.Email;
        //userDb.Gender = user.Gender;

        _entityFramework.Add(userDb);
        if (_entityFramework.SaveChanges() >0)
        {
            return Ok();
        }
        throw new Exception("Failed to Add User");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        User? userDb = _entityFramework.Users
            .Where(u => u.UserId == userId)
            .FirstOrDefault<User>();

        if (userDb != null)
        {
            _entityFramework.Users.Remove(userDb);
            if (_entityFramework.SaveChanges() >0)
            {
                return Ok();
            }
        }

        throw new Exception("Failed to Delete User");
    }
}
