#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CarParkingSystem.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;


namespace CarParkingSystem.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    public bool IsAccountActive {  get; set; }  

    public long idNumber { get; set; }
    public long orgNum { get; set; }
    public static async Task<ApplicationUser> FindByOrgNumAsync(UserManager<ApplicationUser> userManager, long orgNum)
    {
        return await userManager.Users.FirstOrDefaultAsync(u => u.orgNum == orgNum);
    }
}
