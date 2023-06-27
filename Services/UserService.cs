using LoopApi.DTO;
using LoopApi.Interfaz;
using LoopApi.Models;
using LoopApi.Util;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LoopApi.Services
{
    public class UserService : IUser
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(IOptions<UserDatabaseSettings> userDatabaseSettings)
        {
            MongoClient mongoClient = new MongoClient(
            userDatabaseSettings.Value.ConnectionString);

            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
                userDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(
                userDatabaseSettings.Value.UsersCollectionName);
        }

        public bool ExtisteUsuarioLogin(Login login)
        {
            bool existe = false;

            string contrasena = Utils.EncriptarContrasena(login.password);

            if (Validacion.EsEmail(login.username))
                existe = Convert.ToBoolean(_usersCollection.Count(p => p.email == login.username && p.password == contrasena));
            else
                existe = Convert.ToBoolean(_usersCollection.Count(p => p.login == login.username && p.password == contrasena));

            return existe;
        }

        public User Authenticate(Login login)
        {
            User user = null;

            try
            {
                string contrasena = Utils.EncriptarContrasena(login.password);

                if (Validacion.EsEmail(login.username))
                    user = _usersCollection.Find(p => p.email == login.username && p.password == contrasena).First();
                else
                    user = _usersCollection.Find(p => p.login == login.username && p.password == contrasena).First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }

        public bool ExisteUsuario(string userName)
        {
            return Convert.ToBoolean(_usersCollection.Count(p => p.login == userName));
        }

        public void Post(User user)
        {
            user.password = Utils.EncriptarContrasena(user.password);
            _usersCollection.InsertOne(user);
        }

        public List<User> Get()
        {
            return _usersCollection.Find(p => true).ToList().OrderBy(p => p.id).ToList();
        }

        public User Get(long id)
        {
            return _usersCollection.Find(p => p.id == id).FirstOrDefault();
        }

        public User GetUsuarioRegistrado(string username)
        {
            return _usersCollection.Find(p => p.login == username).FirstOrDefault();
        }

        public void Update(User user)
        {
            _usersCollection.ReplaceOne(p => p.id == user.id, user);
        }

        public void Delete(long id)
        {
            _usersCollection.DeleteOne(p => p.id == id);
        }

        public bool ValidarActivo(string userName, string email)
        {
            bool valido = false;

            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    long existe = _usersCollection.Count(p => p.login == userName);

                    if (existe > 0)
                    {
                        User user = _usersCollection.Find(p => p.login == userName).First();

                        if (!user.activated)
                        {
                            _usersCollection.DeleteOne(p => p.login == userName);

                            valido = true;
                        }
                        else
                            valido = false;
                    }
                    else
                        valido = true;
                }

                if (!string.IsNullOrEmpty(email))
                {
                    long existe = _usersCollection.Count(p => p.email == email);

                    if (existe > 0)
                    {
                        User user = _usersCollection.Find(p => p.email == email).First();

                        if (!user.activated)
                        {
                            _usersCollection.DeleteOne(p => p.email == email);
                            valido = true;
                        }
                        else
                            valido = false;
                    }
                    else
                        valido = true;
                }
            }
            catch (Exception)
            {
            }

            return valido;
        }

        public User GetActiveKey(string codigo)
        {
            try
            {
                return _usersCollection.Find(p => p.activationKey == codigo).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ObtenerUltimoRegistro()
        {
            List<User> lUsuario = _usersCollection.Find(p => true).ToList();

            return int.Parse(lUsuario.OrderByDescending(p => p.id).First().id.ToString()) + 1;
        }
    }
}
