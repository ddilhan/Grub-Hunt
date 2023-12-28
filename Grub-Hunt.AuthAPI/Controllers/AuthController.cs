using Grub_Hunt.AuthAPI.DTOs;
using Grub_Hunt.AuthAPI.Implementations;
using Grub_Hunt.AuthAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Grub_Hunt.AuthAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private ResponseDTO _responseDTO;
        private readonly IJWTTokenGenerator _jWTTokenGenerator;
        public AuthController(IAuthService authService, IJWTTokenGenerator jWTTokenGenerator)
        {
            _authService = authService;
            _responseDTO = new();
            _jWTTokenGenerator = jWTTokenGenerator;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO signUpModel)
        {
            try
            {
                var user = await _authService.SignUp(signUpModel);
                _responseDTO.Result = user;

                if (user == null)
                    _responseDTO.Message = "Unable to create user";

                return Ok(_responseDTO);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, _responseDTO);
            }
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO signInModel)
        {
            try
            {
                var user = await _authService.SignIn(signInModel);
                _responseDTO.Result = user;

                if (user != null)
                {
                    _responseDTO.Token = _jWTTokenGenerator.GenerateToken(user);
                }
                else
                    _responseDTO.Message = "Unable to login user";

                return Ok(_responseDTO);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, _responseDTO);
            }
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] SignUpDTO signInModel)
        {
            try
            {
                var result = await _authService.AssignRole(signInModel?.Email, signInModel?.Role?.ToUpper());

                if (result)
                {
                    _responseDTO.Message = "Role assigned";
                    return Ok(_responseDTO);
                }
                else 
                {
                    _responseDTO.Message = "Unable to assign role";
                    return StatusCode((int)HttpStatusCode.InternalServerError, _responseDTO);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, _responseDTO);
            }
        }
    }
}
