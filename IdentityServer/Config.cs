using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.DataProtection;
using Secret = IdentityServer4.Models.Secret;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> GetClients() =>
           new List<Client>
           {
                new Client
                {
                    ClientId = "client_id_mvc",
                    ClientSecrets = {new Secret("client_secret_mvc".ToSha256())},

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },

                    RedirectUris = {"https://localhost:5001/signin-oidc"},
                    PostLogoutRedirectUris = {"https://localhost:5001/signout-callback-oidc"},

                    RequireConsent = false,

                    AccessTokenLifetime = 3600,

                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true
                }
           };


        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Profile();
        }
    }
}

