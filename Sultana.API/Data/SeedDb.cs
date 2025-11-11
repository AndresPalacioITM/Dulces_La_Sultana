using Sultana.Shared.Entities;
//using Sultana.API.Helpers;
//using Sultana.Shared.Enums;

namespace Sultana.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        //private readonly IUserHelper _userHerlper;

        public SeedDb(DataContext context)//, IUserHelper userHerlper userHelper)
        {
            _context = context;
            //_userHerlper = userHerlper;
        }

        public async Task SeedDbAsync()
        {
            await _context.Database.EnsureCreatedAsync();
           // await CheckRolesAsync();
           //// await CheckUserAsync();
        }

        //private async Task<Empleado> CheckUserAsync(string documento, string firstName, string lastName, string email, string direccion, UserType userType)
        //{
        //    var user = await _userHerlper.GetUserAsync(email);
        //    if (user == null)
        //    {
        //        user = new Empleado
        //        {
        //            Document = documento
        //            FirstName = firstName,
        //            LastName = lastName,
        //            Email = email,
        //            Direccion = direccion,
        //            UserName = email,
        //            UserType = userType
        //        };

        //        await _userHerlper.AddUserAsync(user, "123");
        //        await _userHerlper.AddUserToRoleAsync(user, userType.ToString());

        //    }
        //    return user;
        //}
        
        //private async Task CheckRolesAsync()
        //{
        //    await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
        //    await _userHelper.CheckRoleAsync(UserType.Empleado.ToString());
        }

        

    }
//}
