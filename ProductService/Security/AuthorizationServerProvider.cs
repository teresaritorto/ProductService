using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using ProductService.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProductService.Security
{
    /// <summary>
    /// Authorization Provider
    /// </summary>
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Validate Authentication
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        /// <summary>
        /// Grant
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //cors
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            
            //hard-coded dummy users for demo purposes, typically you would go to a user store with async await
            if (context.Password != "password" ||
                (context.UserName.ToLower() != "user" && context.UserName.ToLower() != "admin"))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var role = context.UserName.ToLower() == "admin" ? UserRepository.Admin : UserRepository.TestUser;

            //set up identity
            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            context.Validated(new AuthenticationTicket(identity, new AuthenticationProperties { }));
        }
    }
}