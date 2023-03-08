using CertificationMS.Models;
using CertificationMS.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace CertificationMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        public IConfiguration config { get; set; }
        public JwtController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JwtModel Model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                var hashpass= Hashgenerator.GetPassHash(Model.Password);
                // var IsSuccess= Hasher.VerifyHashedPassword(user.PasswordHash, model.Password);
                try
                {

                    Storeproc storeproc = new Storeproc();
                    SqlConnection sqlConnection = new SqlConnection(config.GetConnectionString("AppCon"));
                    SqlParameter[] parameters = new SqlParameter[2];
                    SqlParameter Empid = new SqlParameter(parameterName: "@EmployeeID", dbType: System.Data.SqlDbType.NVarChar);
                    Empid.Value = Model.EmployeeId;
                    SqlParameter Password = new SqlParameter(parameterName: "@Password", dbType: System.Data.SqlDbType.NVarChar);
                    Password.Value = hashpass;
                    parameters[0] = Empid;
                    parameters[1] = Password;
                    var result = storeproc.GetDataSet(sqlConnection, "[dbo].[GetUserMenus]", parameters);

                    //LoginResp.EmpMenuList = DtToList.ConvertDataTable<EmpMenus>(result.Tables[0]);
                    var r = DtToList.ConvertDataTable<EmpInfo>(result.Tables[1]);
                    if (r.Count < 1 || r == null)
                    {
                        message = "Invalid Credential";
                        return BadRequest(message);
                    }
                   

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApiConst.key));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


                    var token = new JwtSecurityToken(
                           ApiConst.Issuer,
                           ApiConst.Audience,

                           expires: DateTime.UtcNow.AddMinutes(30),
                           signingCredentials: creds

                        );
                    var jwtresult = new LoggedInModel
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo,
                        EmpId = Model.EmployeeId,
                        empInfo = r[0]
                    };
                    return Created("", jwtresult);
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest();
        }
    }
}
