using APIClientes.Model.Dto;
using APIClientes.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APIClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepositorio _userRepositorio;
        protected ResponseDto _response;

        public UsersController(IUserRepositorio userRepositorio)
        {
            _userRepositorio = userRepositorio;
            _response = new ResponseDto();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserDto userDto)
        {
            var respuesta = await _userRepositorio.Register(
                    new Model.User
                    {
                        UserName = userDto.UserName
                    }, userDto.Password);

            if (respuesta == -1)
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "Usuario ya Existe";
                return BadRequest(_response);
            }

            if (respuesta == -500)
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "Error al Crear el Usuario";
                return BadRequest(_response);
            }

            _response.IsSucces = true;
            _response.DisplayMessage = "Usuario Creado con Exito";
            return Ok(_response);

        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDto user)
        {
            var respuesta = await _userRepositorio.Login(user.UserName, user.Password);

            _response.IsSucces = true;
            _response.DisplayMessage = "Usuario Conectado";


            if (respuesta == "nouser")
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "Usuario no Existe";
            } 
            if (respuesta == "wrongpassword")
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "Password Incorrecta";
            }

            return Ok(_response);
        }



    }
}
