using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyUser.Models;
using MyUser.Services;

namespace MyUser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IEntityRepository<User> _repository;

        public UserController(ILogger<UserController> logger, IEntityRepository<User> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _repository.GetAll().ToList();
        }

        [HttpGet("GetById")]
        public User GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        [HttpPost]
        public User Post(string lastName, string firstName)
        {
            var user = new User()
            {
                LastName = lastName,
                FirstName = firstName,
            };
            _repository.Create(user);

            return user;
        }

        [HttpPut]
        public User Put(User user)
        {
            var _user = _repository.GetById(user.Id);
            if (_user is not null)
            {
                if (!string.IsNullOrEmpty(user.LastName))
                    _user.LastName = user.LastName;

                if (!string.IsNullOrEmpty(user.FirstName))
                    _user.FirstName = user.FirstName;

                _repository.Update(_user);
            }

            return _user;
        }

        [HttpDelete]
        public User Delete(Guid userId)
        {
            var _user = _repository.GetById(userId);
            if (_user is not null)
                _repository.Remove(_user);

            return _user;
        }
    }
}