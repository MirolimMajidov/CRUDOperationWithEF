using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyUser.Models;

namespace MyUser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserContext _context;

        public UserController(ILogger<UserController> logger, UserContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _context.Users.AsTracking().ToList();
        }

        [HttpPut]
        public User Put(User user)
        {
            var _user = _context.Users.FirstOrDefault(u => u.ID == user.ID);
            if (_user is not null)
            {
                if (!string.IsNullOrEmpty(user.LastName))
                    _user.LastName = user.LastName;

                if (!string.IsNullOrEmpty(user.FirstName))
                    _user.FirstName = user.FirstName;

                _context.SaveChanges();
            }

            return _user;
        }

        [HttpPost]
        public User Post(string lastName, string firstName)
        {
            var user = new User()
            {
                LastName = lastName,
                FirstName = firstName,
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        [HttpDelete]
        public User Delete(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.ID == userId);
            if (user is not null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return user;
        }
    }
}