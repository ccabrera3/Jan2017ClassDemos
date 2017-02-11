﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.AspNet.Identity;
using Chinook.Data.Enitities.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using ChinookSystem.DAL.Security;
using ChinookSystem.DAL;
using Chinook.Data.POCOs;
#endregion

namespace ChinookSystem.BLL.Security
{
    public class UserManager : UserManager<ApplicationUser>
    {
        public UserManager() : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
        }
        #region Constants
        private const string STR_DEFAULT_PASSWORD = "Pa$$word1";
        /// <summary>Requires first intial of FirstName and LastName</summary>
        private const string STR_USERNAME_FORMAT = "{0}.{1}";
        /// <summary>Requires UserName</summary>
        private const string STR_EMAIL_FORMAT = "{0}@Chinook.ca";
        private const string STR_WEBMASTER_USERNAME = "Webmaster";
        #endregion

        public void AddWebMaster()
        {
            //Users accesses all the records on the AspNetUsers table
            //UserName is the user logon user id (dwelch)
            if(!Users.Any(u => u.UserName.Equals(STR_WEBMASTER_USERNAME)))
            {
                //create a new instance that will be used as the data to 
                //   add a new record to the AspNetUsers table
                //dynamically fill two attributes of the instance

                var webMasterAccount = new ApplicationUser()
                {
                    UserName = STR_WEBMASTER_USERNAME,
                    Email = string.Format(STR_EMAIL_FORMAT, STR_WEBMASTER_USERNAME)
                };
                //place the webmaster account on the AspNetUsers table with a password
                this.Create(webMasterAccount, STR_DEFAULT_PASSWORD);
                //place an account role record on the AspNetUserRoles table
                //.Id comes from AspNetUsers table and is the user's sql table pkey
                //role comes from one of the Entities.Security.SecurityRoles
                this.AddToRole(webMasterAccount.Id, SecurityRoles.WebsiteAdmins);
            }
        }

        public void AddEmployees()
        {
            using (var context = new ChinookContext())
            {
                //get all current employees
                //linq query will not execute as yet
                //return datatype will be IQueryable
                var currentEmployees = from x in context.Employees
                                           //where x.Active if such a flag existed
                                       select new EmployeeListPOCO
                                       {
                                           EmployeeId = x.EmployeeId,
                                           FirstName = x.FirstName,
                                           LastName = x.LastName
                                       };
                //get all employees who have a logon
                //Users needs to be in memory therefore use .ToList()
                //The POCO EmployeeId is an int
                //The Users EmployeeId is an int?
                //since we will be only retrieving Users records that have
                //   an EmployeeId value, there will be a value to convert;
                //   change the value to a string and then parse to an int
                //the results will be in memory
                var UserEmployees = from x in Users.ToList()
                                    where x.EmployeeId.HasValue
                                    select new RegisteredEmployeePOCO
                                    {
                                        UserName = x.UserName,
                                        EmployeeId = int.Parse(x.EmployeeId.ToString())
                                    };
                //loop ti see uf auto generaton of new employee Users record is needed
                foreach (var employee in currentEmployees)
                {
                    //does the employee NOT have a logon (no Users record)
                    if (!UserEmployees.Any(ue => ue.EmployeeId == employee.EmployeeId))
                    {
                        //suggested new employee UserName (intitial firstname and lastname : dwelch)
                        var newUserName = employee.FirstName.Substring(0, 1) + employee.LastName;

                        //verify the suggested UserName is not already on the Users Table
                        //if the suggested UserName does exist, the method will suggest
                        //    an alternate UserName
                        newUserName = VerifyNewUserName(newUserName);

                        //create new Users instance
                        var userAccount = new ApplicationUser()
                        {
                            UserName = newUserName,
                            Email = string.Format(STR_EMAIL_FORMAT, newUserName)
                        };
                        userAccount.EmployeeId = employee.EmployeeId;
                        this.Create(userAccount, STR_DEFAULT_PASSWORD);

                        //add to Staff Role
                        this.AddToRole(userAccount.Id, SecurityRoles.Staff);

                    }
                }

            }
        }//eom

        public string VerifyNewUserName(string suggestedUserName)
        {
            //get a list of all current logon names (customers and employees)
            //   that start with the passed suggestedUserName
            //list will be strings
            //will be in memory
            var allUserNames = from x in Users.ToList()
                               where x.UserName.StartsWith(suggestedUserName)
                               orderby x.UserName
                               select x.UserName;

            //will be the verified unique UserName
            var verifiedUserName = suggestedUserName;

            //The following for() loop will current to loop until
            //a unused UserName has been generated.
            //The condition searches all current UserNames for the
            //currently suggested user name reqardless of case.
            //If found the loop will generated a new suggested user name
            //by adding one to the original suggested user name.
            //This will continue until a new verified user name is found.
            for (int i = 1; allUserNames.Any(x => x.Equals(verifiedUserName, StringComparison.OrdinalIgnoreCase)); i++)
            {
                verifiedUserName = suggestedUserName + i.ToString();
            }

            //return the finallized new verified UserName
            return verifiedUserName;
        }
    }
}