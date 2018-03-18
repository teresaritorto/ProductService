using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using ProductService.Security;
using System;

namespace ProductService
{
    /// <summary>
    /// Auth Startup
    /// </summary>
    public partial class Startup
    {
        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/getsecuretoken"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}