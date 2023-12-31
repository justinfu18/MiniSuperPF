﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSuperPF.Models;
using MiniSuperPF.Attributes;
using MiniSuperPF.ModelsDTOs;
using MiniSuperPF.Tools;

namespace MiniSuperPF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey]
    public class UsersController : ControllerBase
    {
        private readonly BD_MiniSuperContext _context;


        public Tools.AESEncrytDecry MyCrypto { get; set; }

        public UsersController(BD_MiniSuperContext context)
        {
            _context = context;

            MyCrypto = new Tools.AESEncrytDecry();
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("ValidateUserLogin")]
        public async Task<ActionResult<User>> ValidateUserLogin(String pUserName, string pPassword)
        {

            string EncriptedPassword = AESEncrytDecry.EcryptStringAES(pPassword);

            string EncriptedPassworda = AESEncrytDecry.DecryptStringAES(EncriptedPassword);

            var User = await _context.Users.SingleOrDefaultAsync(e => e.Email == pUserName );
            //string Userpass = AESEncrytDecry.DecryptStringAES(User.LoginPassword.ToString().Trim());

            if (User == null)
            {
                return NotFound();
            }
            return User;


        }



        // GET: api/Users/5
        // Get permite obtener info de usuario recibiendo el email como parametro
        [HttpGet("GetUserData")]
        public ActionResult<IEnumerable<UserDTO>> GetUserData(string email)
        {
            // Sentencias linkU
            var query = (from u in _context.Users
                         join ur in _context.UserRoles on u.UserRoleId equals ur.UserRoleId
                         join us in _context.UserStatuses on u.UserStatusId equals us.UserStatusId
                         where u.Email == email && u.UserStatusId != 2
                         select new

                         {
                             idusuario = u.UserId,
                             nombre = u.Name,
                             correo = u.Email,
                             telefono = u.PhoneNumber,
                             contra = u.LoginPassword,
                             cedula = u.CardId,
                             direccion = u.Address,
                             idrol = ur.UserRoleId,
                             roldescripcion = ur.UserRoleDescription,
                             idestado = us.UserStatusId,
                             estadodesc = us.UserStatusDescription
                         }).ToList();

            // Crear un objeto de dto de retorno
            List<UserDTO> List = new List<UserDTO>();
            foreach (var item in query)
            {
                UserDTO NewItem = new UserDTO()
                {
                    IdUsuario = item.idusuario,
                Nombre = item.nombre,
                Correo = item.correo,
                Cedula = item.cedula,
                NumeroTelefono = item.telefono,
                Contrasennia = item.contra,
                Direccion = item.direccion,
                IdRol = item.idrol,
                RolDescripcion = item.roldescripcion,
                IdEstado = item.idestado,
                EstadoDescripcion = item.estadodesc

            };
                List.Add(NewItem);
            }
            if (List == null)
            {
                return NotFound();

            }
            return List;
        }
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO user)
        {
            if (id != user.IdUsuario)
            {
                return BadRequest();
            }

            string Password = "";
            if (user.Contrasennia.Length <= 60)
            {
                Password = AESEncrytDecry.EcryptStringAES(user.Contrasennia);
            }
            else
            {
                Password = user .Contrasennia;
            }


            User NuevoUsuario = new()
            {
                UserId = user.IdUsuario,
                Name = user.Nombre,
                CardId = user.Cedula,
                Address = user.Direccion,
                PhoneNumber = user.NumeroTelefono,
                LoginPassword = user.Contrasennia,
                Email = user.Correo,
                UserRoleId = user.IdRol,
                UserStatusId = user.IdEstado,
                Services = null,
                UserRole = null,
                UserStatus = null,
            };


            _context.Entry(NuevoUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            string EncriptedPassword = AESEncrytDecry.EcryptStringAES(user.LoginPassword);
            user.LoginPassword = EncriptedPassword;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
