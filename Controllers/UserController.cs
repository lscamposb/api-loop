using LoopApi.DTO;
using LoopApi.Enum;
using LoopApi.Interfaz;
using LoopApi.Models;
using LoopApi.Services;
using LoopApi.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoopApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly UserService _userService;

        public UserController(IConfiguration config, UserService userService)
        {
            _configuration = config;
            _userService = userService;
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public ActionResult<UserExisteDTO> UserExist(string userName, string language)
        {
            try
            {
                bool lExiste = _userService.ExisteUsuario(userName);

                if (lExiste)
                    return BadRequest(new MensajeApi(IdiomaCodeEnum.TITULO_USUARIO, (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_USUARIO_REGISTRADO, language)));

                UserExisteDTO userExisteDTO = new UserExisteDTO();
                userExisteDTO.exist = lExiste;

                return Ok(userExisteDTO);
            }
            catch (Exception ex)
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public ActionResult<JwtToken> Authenticate(Login login)
        {
            try
            {
                bool rememberMe = (login.rememberMe == null) ? false : login.rememberMe;

                if (!_userService.ExtisteUsuarioLogin(login))
                    return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_USUARIO_INGRESAR, login.language), (int)ExceptionCodeEnum.Codigo401, Language.GetMensage(IdiomaCodeEnum.MENSAJE_USUARIO_NO_EXISTE, login.language)));

                User user = _userService.Authenticate(login);

                if (!user.activated)
                    return BadRequest(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_USUARIO_INGRESAR, login.language), (int)ExceptionCodeEnum.Codigo401, Language.GetMensage(IdiomaCodeEnum.MENSAJE_USUARIO_INACTIVO, login.language)));

                var token = TokenGenerator.GenerateTokenJwt(_configuration, user.login, rememberMe);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, login.language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, login.language) + " " + ex.Message));
            }
        }

        // POST api/<UsersController>
        [HttpPost("[action]")]
        [AllowAnonymous]
        public ActionResult<User> Register(User user)
        {
            try
            {
                if (!_userService.ValidarActivo(user.login, ""))
                    return BadRequest(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_USUARIO, user.langKey), (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_USUARIO_REGISTRADO, user.langKey)));

                if (!_userService.ValidarActivo("", user.email))
                    return BadRequest(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_USUARIO, user.langKey), (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_CORREO_REGISTRADO, user.langKey)));

                if (!Validacion.ValidarContrasena(user.password))
                    return BadRequest(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_USUARIO, user.langKey), (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_CONTRASENA_INVALIDA, user.langKey)));

                User userDB = new User();
                userDB.id = _userService.ObtenerUltimoRegistro();
                userDB.login = user.login;
                userDB.firstName = user.firstName;
                userDB.birthDate = user.birthDate;
                userDB.createdDate = user.createdDate.ToString();
                userDB.email = user.email;
                userDB.lastName = user.lastName;
                userDB.password = user.password;
                userDB.activated = true; //Pasar a false cuando funcione lo del envio de SMS
                userDB.langKey = user.langKey;
                userDB.createdBy = user.createdBy;
                userDB.activationKey = Utils.GenerarCodigoActivacion();
                userDB.giftPoints = new List<User.GiftPoint>().ToArray();
                userDB.device = new List<User.Device>().ToArray();

                List<User.Authority> authorities = new List<User.Authority>() { new User.Authority() { name = "ROLE_USER" } };
                userDB.authority = authorities.ToArray();

                _userService.Post(userDB);

                //Envia el mensaje para la activación del usuario
                Utils.EnviarCodigoActivacion(userDB.activationKey, userDB.login);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_USUARIO, user.langKey), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_USUARIO_REGISTRADO, user.langKey)));
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, user.langKey), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, user.langKey)));
            }
        }

        // GET api/<UsersController>/5/es
        [HttpGet("[action]")]
        public ActionResult<UserDTO> Account(string userName, string language)
        {
            try
            {
                User user = _userService.GetUsuarioRegistrado(userName);

                if (user == null)
                    return BadRequest(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_USUARIO, language), (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_USUARIO_SIN_REGISTRAR, language)));

                UserDTO lUser = new UserDTO();
                lUser.id = user.id;
                lUser.login = user.login;
                lUser.firstName = user.firstName;
                lUser.lastName = user.lastName;
                lUser.email = user.email;
                lUser.birthDate = user.birthDate;
                lUser.langKey = user.langKey;

                return Ok(lUser);
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }

        // PUT api/<UsersController>/es
        [HttpPut("[action]")]
        public ActionResult Update(string language, UserDTO user)
        {
            try
            {
                User lUsuario = _userService.Get(user.id);

                if (lUsuario == null)                
                    return BadRequest(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_USUARIO, user.langKey), (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_USUARIO_SIN_REGISTRAR, user.langKey)));
                
                lUsuario.firstName = user.firstName;
                lUsuario.lastName = user.lastName;
                lUsuario.email = user.email;
                lUsuario.birthDate = user.birthDate;
                lUsuario.langKey = user.langKey;

                _userService.Update(lUsuario);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_USUARIO, user.langKey), (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_USUARIO_ACTUALIZADO, user.langKey)));
            }
            catch 
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, user.langKey), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, user.langKey)));
            }
        }

        [HttpGet("[action]")]
        public ActionResult Activate(string codigo, string language)
        {
            try
            {
                User user = _userService.GetActiveKey(codigo);

                if (user == null)
                    return BadRequest(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_USUARIO, language), (int)ExceptionCodeEnum.Codigo400, Language.GetMensage(IdiomaCodeEnum.MENSAJE_USUARIO_NO_VERIFICADO, language)));

                user.activated = true;

                _userService.Update(user);

                return Ok(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_USUARIO, language), (int)ExceptionCodeEnum.Codigo200, Language.GetMensage(IdiomaCodeEnum.MENSAJE_USUARIO_VERIFICADO, language)));
            }
            catch
            {
                return NotFound(new MensajeApi(Language.GetMensage(IdiomaCodeEnum.TITULO_ERROR, language), (int)ExceptionCodeEnum.Codigo404, Language.GetMensage(IdiomaCodeEnum.ERROR_GENERICO, language)));
            }
        }
    }
}
