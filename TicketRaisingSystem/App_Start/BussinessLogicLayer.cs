using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace TicketRaisingSystem.App_Start
{
    public class BussinessLogicLayer
    {
        #region Register User
        public UserCreationResult CreateUser(string UserName, string Password, string Email, string Mobile, string DateOfBirth)
        {
            DataAccessLayer objDL = new DataAccessLayer();
            var Result = new UserCreationResult(); string Role = string.Empty;
            try
            {
                MembershipCreateStatus createStatus;
                MembershipUser user = Membership.CreateUser(Email, Password, Email, UserName, Mobile, true, out createStatus);
                switch (createStatus)
                {
                    case MembershipCreateStatus.Success:
                        if (!Roles.RoleExists(Role = "Admin"))
                        {
                            if (!Roles.IsUserInRole(UserName, Role = "Admin"))
                            {
                                Roles.CreateRole("Admin");
                                Roles.AddUserToRole(UserName, Role = "Admin");
                            }
                            else
                            {
                                Result.Message = "Admin already exists"; Result.Status = false;
                            }
                        }
                        else
                        {
                            if (!Roles.RoleExists(Role = "User"))
                            {
                                Roles.CreateRole("User");
                                Roles.AddUserToRole(UserName, Role = "User");
                            }
                        }
                        string SPName = "Sp_InsertData";
                        string[] ParamNames = { "@UserName", "@Mobile", "@Email", "@Dob", "@Password", "@Role" }; string[] ParamValues = { UserName, Mobile, Email, DateOfBirth, Encrypt(Password), Role };
                        bool Data = objDL.InsertData(SPName, ParamNames, ParamValues);
                        if (Data == true)
                        {
                            Result.Message = "The user account was successfully created"; Result.Status = true;
                        }
                        else
                        {
                            Result.Message = "Something went wrong"; Result.Status = false;
                        }
                        break;
                    case MembershipCreateStatus.DuplicateUserName:
                        Result.Message = "The user with the same UserName already exists!"; Result.Status = false;
                        break;
                    case MembershipCreateStatus.DuplicateEmail:
                        Result.Message = "The user with the same email address already exists!"; Result.Status = false;
                        break;
                    case MembershipCreateStatus.InvalidEmail:
                        Result.Message = "The email address you provided is invalid."; Result.Status = false;
                        break;
                    case MembershipCreateStatus.InvalidAnswer:
                        Result.Message = "The security answer was invalid."; Result.Status = false;
                        break;
                    case MembershipCreateStatus.InvalidPassword:
                        Result.Message = "The password you provided is invalid. It must be 7 characters long and have at least 1 special character."; Result.Status = false;
                        break;
                    default:
                        Result.Message = "There was an unknown error; the user account was NOT created."; Result.Status = false;
                        break;
                }
            }
            catch (Exception Ex)
            {
                new Exception("Something went wrong", Ex);
            }
            return Result;
        }
        #endregion
        #region Password Encryption
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        #endregion
        #region Password Decryption
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        #endregion
    }
    #region UserCreation Class
    public class UserCreationResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
    #endregion
}