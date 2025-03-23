using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserControl.Core.Abstractions.Services;
using UserControl.Core.Dtos.Users;
using UserControl.Core.Exceptions;

namespace UserControl.Api.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
            [SwaggerOperation(Summary = "Registra un nuevo usuario junto con sus teléfonos.")]
        public async Task<ActionResult<UserResponseDto>> RegisterUser([FromBody] CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            try
            {
                var userResponseDto = await _userService.RegisterUser(createUserDto, cancellationToken);
                return CreatedAtAction(nameof(GetUserById), new { id = userResponseDto.Id }, userResponseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Inicia sesión con las credenciales del usuario y devuelve un JWT.
        /// </summary>
        /// <param name="loginDto">Datos de inicio de sesión.</param>
        /// <returns>Token JWT para el usuario.</returns>
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Inicia sesión con las credenciales del usuario y devuelve un JWT.")]
        public async Task<ActionResult<string>> LoginUser([FromBody] UserLoginDto loginDto, CancellationToken cancellationToken)
        {
            try
            {
                var jwtToken = await _userService.LoginUser(loginDto, cancellationToken);
                return Ok(new { token = jwtToken });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza los datos de un usuario.
        /// </summary>
        /// <param name="id">ID del usuario a actualizar.</param>
        /// <param name="updateUserDto">Datos actualizados del usuario.</param>
        /// <returns>Usuario actualizado con éxito.</returns>
        [Authorize]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza los datos de un usuario.")]
        public async Task<ActionResult<UserResponseDto>> UpdateUser(Guid id, [FromBody] UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            try
            {
                var updatedUser = await _userService.UpdateUser(id, updateUserDto, cancellationToken);
                return Ok(updatedUser);
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        /// <returns>Resultado de la eliminación.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un usuario por su ID.")]
        public async Task<ActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.DeleteUser(id, cancellationToken);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene los detalles de un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a obtener.</param>
        /// <returns>Detalles del usuario solicitado.</returns>
        [Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los usuarios")]
        public async Task<ActionResult<UserResponseDto>> GetAllUser(CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetAllUsers();
                return Ok(user);
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
        }

        /// <summary>
        /// Obtiene los detalles de un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a obtener.</param>
        /// <returns>Detalles del usuario solicitado.</returns>
        [Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene los detalles de un usuario por su ID.")]
        public async Task<ActionResult<UserResponseDto>> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetUserById(id, cancellationToken);
                return Ok(user);
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
        }
    }
}
