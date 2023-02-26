using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using yeaa2;
using yeaa2.Entities;
using yeaa2.Models;

namespace BmiCalculator.Controllers
{
    public class UsersController : ApiController
    {
        private MyDbContext db = new MyDbContext();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/users/register")]
        public IHttpActionResult Register([System.Web.Http.FromBody] UserRegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the username already exists
            if (db.Users.Any(u => u.Username == model.Username))
            {
                return BadRequest("Username already exists.");
            }

            // Generate a salt and hash the password
            byte[] salt = GenerateSalt();
            var hashedPassword = HashPassword(model.Password, salt);


            // Save the user to the database
            var user = new User
            {
                Username = model.Username,
                Password = hashedPassword,
                Salt = salt,
                ImperialMeasurements = new List<ImperialMeasurement>(),
                MetricMeasurements = new List<MetricMeasurement>()
            };
            db.Users.Add(user);
            db.SaveChanges();

            return Ok();
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/users/login")]
        public IHttpActionResult Login([System.Web.Http.FromBody] UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the user from the database
            var user = db.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user == null)
            {
                return BadRequest("Username or password is incorrect.");
            }

            // Hash the password with the user's salt and compare to the hashed password in the database
            var hashedPassword = HashPassword(model.Password, user.Salt);
            if (hashedPassword != user.Password)
            {
                return BadRequest("Username or password is incorrect.");
            }

            // Return the user ID
            return Ok(new { UserId = user.Id });
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/users/{userId}/imperial")]
        public IHttpActionResult CalculateBmiImperial(int userId, [System.Web.Http.FromBody] ImperialMeasurementModel model)
        {
            // Get the user from the database
            var user = db.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Calculate the BMI
            var heightInches = model.HeightFt * 12 + model.HeightIn;
            var bmi = 703 * model.WeightLbs / (heightInches * heightInches);

            // Save the measurement to the database
            var measurement = new ImperialMeasurement
            {
                UserId = userId,
                HeightFt = model.HeightFt,
                HeightIn = model.HeightIn,
                WeightLbs = model.WeightLbs,
                Bmi = bmi,
                CreatedAt = DateTime.UtcNow
            };
            db.ImperialMeasurements.Add(measurement);
            db.SaveChanges();

            return Ok(new { Bmi = bmi });
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/users/{userId}/metric")]
        public IHttpActionResult CalculateBmiMetric(int userId, [System.Web.Http.FromBody] MetricMeasurementModel model)
        {
            var user = db.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            var bmi = model.WeightKg / (model.HeightCm * model.HeightCm / (100 * 100));
            var measurement = new MetricMeasurement
            {
                UserId = userId,
                HeightCm = model.HeightCm,
                WeightKg = model.WeightKg,
                Bmi = bmi,
                CreatedAt = DateTime.UtcNow
            };
            db.MetricMeasurements.Add(measurement);
            db.SaveChanges();

            return Ok(new { Bmi = bmi });
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var passwordData = Encoding.UTF8.GetBytes(password);
                var saltedPasswordData = new byte[passwordData.Length + salt.Length];
                Encoding.UTF8.GetBytes(password, 0, password.Length, saltedPasswordData, 0);
                Array.Copy(salt, 0, saltedPasswordData, passwordData.Length, salt.Length);
                var hash = sha256.ComputeHash(saltedPasswordData);
                return hash;
            }
        }


    }
}
