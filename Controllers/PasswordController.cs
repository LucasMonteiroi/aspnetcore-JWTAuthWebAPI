namespace netCryptWebApi.Controllers
{
    using System;
    using System.Security.Cryptography;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using netCryptWebApi.Classes;

    [Route("api/[controller]")]
    public class PasswordController : Controller
    {
        /// <summary>
        /// Method to generate Hash Password and Salt
        /// </summary>
        /// <param name="password">Password to Hash</param>
        /// <returns>An object with Salt and Hash Password</returns>
        [Authorize("Bearer")]
        [HttpGet("CryptPBKDF2/{password}")]
        public PBKDF2 Get(string password)
        {
            PBKDF2 returnKey = new PBKDF2();
            
            using (var deriveBytes = new Rfc2898DeriveBytes(password, 48))
            {
                byte[] saltBytes = deriveBytes.Salt;
                byte[] itemBytes = deriveBytes.GetBytes(48);

                returnKey.PasswordSalt = Convert.ToBase64String(saltBytes);
                returnKey.CryptPassword = Convert.ToBase64String(itemBytes);
                returnKey.Password = password;
                returnKey.Message = "Password Encrypt with success.";
            }

            return returnKey;
        }

        /// <summary>
        /// Method to validate Hash and Salt
        /// </summary>
        /// <param name="password">Needs to inform: password, salt and password salt</param>
        /// <returns>Return message containing the answer about the validation</returns>
        [Authorize("Bearer")]
        [HttpPost("VerifyPBKDF2")]
        public object Post([FromBody]PBKDF2 password)
        {
            if(password == null || string.IsNullOrEmpty(password.PasswordSalt)|| 
            string.IsNullOrEmpty(password.CryptPassword) || string.IsNullOrEmpty(password.Password)){
                return new {
                    validate = false,
                    message = "Password, Salt and CryptPassword is required to continue operation."};
            }

            byte[] saltBytes = Convert.FromBase64String(password.PasswordSalt);
            byte[] itemBytes = Convert.FromBase64String(password.CryptPassword);

            using (var deriveBytes = new Rfc2898DeriveBytes(password.Password, saltBytes))
            {
                byte[] newItem = deriveBytes.GetBytes(48);

                if (!newItem.SequenceEqual(itemBytes)) 
                    return new {
                    validate = false,
                    message = "Has a mismatch on fields: password, hash and salt."};
            }

            return new {
                    validate = true,
                    message = "The password, salt and hash match."};
        }
    }
}