using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepository userRepository;
        public UserController(UserRepository userRepo)
        {
            this.userRepository = userRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserRepository>> GetUsers()
        {
            try
            {
                var users = this.userRepository.GetUsers();
                return Ok(users);
            }
            catch (Exception error)
            {
                Console.WriteLine("Error while adding user: ", error);
                return BadRequest(error);
            }
        }

        [HttpPost]
        public IActionResult AddNewUser(User user)
        {
            try
            {
                this.userRepository.AddUser(user);
                return Ok(new { message = "User added successfully!" });
            }
            catch (Exception error)
            {
                Console.WriteLine("Error while adding user: ", error);
                return StatusCode(500, new { message = error.Message });
            }
        }

        [HttpPut]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                this.userRepository.UpdateUser(user);
                return Ok(new { message = "User data upadted successfully!" });
            }
            catch (Exception error)
            {
                Console.WriteLine("Error while updating user: ", error);
                return StatusCode(500, new { message = error.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                this.userRepository.DeleteUser(id);
                return Ok(new { message = "User removed...!"});
            }
            catch (Exception error)
            {
                Console.WriteLine("Error while deleting user: ", error);
                return StatusCode(500, new { message = error.Message});
            }
        }
    }
}
