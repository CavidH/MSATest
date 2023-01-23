using IdentityServer4.Models;

namespace AuthServer.Configs;

public static class Config
{
    #region Scopes

//Api lərdəki icazələri təyin edək
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope("Garanti.Write", "Garanti bankası yazma izni"),
            new ApiScope("Garanti.Read", "Garanti bankası okuma izni"),
            new ApiScope("HalkBank.Write", "HalkBank bankası yazma izni"),
            new ApiScope("HalkBank.Read", "HalkBank bankası okuma izni"),
            new ApiScope("HalkBank.www", "HalkBank bankası okuma izni"),
        };
    }

    #endregion

    #region ApiResources

    //api lləri təyin edək

    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
        {
            new ApiResource("Garanti") { Scopes = { "Garanti.Write", "Garanti.Read" } },
            new ApiResource("HalkBank") { Scopes = { "HalkBank.Write", "HalkBank.Read","HalkBank.www" } }
        };
    }

    #endregion


    #region Clients


    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>()
        {
            new Client
            {
                
                ClientId = "GarantiBankasi",
                ClientName = "GarantiBankasi",
                ClientSecrets = { new Secret("garanti".Sha256()) },
                AllowedGrantTypes = { GrantType.ClientCredentials },
                AllowedScopes = { "Garanti.Write", "Garanti.Read" }

            }
            ,
            new Client
            {
                ClientId = "HalkBankasi",
                ClientName = "HalkBankasi",
                ClientSecrets = { new Secret("halkbank".Sha256()) },
                AllowedGrantTypes = { GrantType.ClientCredentials },
                AllowedScopes = {"HalkBank.www", "HalkBank.Write", "HalkBank.Read" }
            }
        };
    }

    #endregion
}