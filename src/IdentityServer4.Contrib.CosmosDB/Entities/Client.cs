using System.Collections.Generic;
using IdentityServer4.Contrib.CosmosDB.Abstracts;
using IdentityServer4.Models;
using Newtonsoft.Json;

namespace IdentityServer4.Contrib.CosmosDB.Entities
{
    /// <inheritdoc />
    /// <summary>
    ///     Models an OpenID Connect or OAuth2 client
    /// </summary>
    public class Client : EntityBase
    {
        /// <summary>
        ///     Specifies if client is enabled (defaults to <c>true</c>)
        /// </summary>
        /// <remarks>Default value is true.</remarks>
        [JsonProperty("enabled")]
        public bool Enabled { get; set; } = true;

        /// <summary>
        ///     Unique ID of the client
        /// </summary>
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        /// <summary>
        ///     Gets or sets the protocol type.
        /// </summary>
        /// <value>
        ///     The protocol type.
        /// </value>
        /// <remarks>Default value is OpenIdConnect (`oidc`).</remarks>
        [JsonProperty("protocolType")]
        public string ProtocolType { get; set; } = IdentityServerConstants.ProtocolTypes.OpenIdConnect;

        /// <summary>
        ///     Client secrets - only relevant for flows that require a secret
        /// </summary>
        [JsonProperty("clientSecrets")]
        public List<ClientSecret> ClientSecrets { get; set; }

        /// <summary>
        ///     If set to false, no client secret is needed to request tokens at the token endpoint (defaults to <c>true</c>)
        /// </summary>
        /// <remarks>Default value is true.</remarks>
        [JsonProperty("requireClientSecret")]
        public bool RequireClientSecret { get; set; } = true;

        /// <summary>
        ///     Client display name (used for logging and consent screen)
        /// </summary>
        [JsonProperty("clientName")]
        public string ClientName { get; set; }

        /// <summary>
        ///     Describes the client
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     URI to further information about client (used on consent screen)
        /// </summary>
        [JsonProperty("clientUri")]
        public string ClientUri { get; set; }

        /// <summary>
        ///     URI to client logo (used on consent screen)
        /// </summary>
        [JsonProperty("logoUri")]
        public string LogoUri { get; set; }

        /// <summary>
        ///     Specifies whether a consent screen is required (defaults to <c>true</c>)
        /// </summary>
        /// <remarks>Default value is true.</remarks>
        [JsonProperty("requireConsent")]
        public bool RequireConsent { get; set; } = true;

        /// <summary>
        ///     Specifies whether user can choose to store consent decisions (defaults to <c>true</c>)
        /// </summary>
        /// <remarks>Default value is true.</remarks>
        [JsonProperty("allowRememberConsent")]
        public bool AllowRememberConsent { get; set; } = true;

        /// <summary>
        ///     When requesting both an id token and access token, should the user claims always be added to the id token instead
        ///     of requiring the client to use the userinfo endpoint.
        ///     Defaults to <c>false</c>.
        /// </summary>
        [JsonProperty("alwaysIncludeUserClaimsInIdToken")]
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        /// <summary>
        ///     Specifies the allowed grant types (legal combinations of AuthorizationCode, Implicit, Hybrid, ResourceOwner,
        ///     ClientCredentials).
        /// </summary>
        [JsonProperty("allowedGrantTypes")]
        public List<ClientGrantType> AllowedGrantTypes { get; set; }

        /// <summary>
        ///     Specifies whether a proof key is required for authorization code based token requests (defaults to <c>false</c>).
        /// </summary>
        [JsonProperty("requirePkcs")]
        public bool RequirePkce { get; set; }

        /// <summary>
        ///     Specifies whether a proof key can be sent using plain method (not recommended and defaults to <c>false</c>.)
        /// </summary>
        [JsonProperty("allowPlainTextPkce")]
        public bool AllowPlainTextPkce { get; set; }

        /// <summary>
        ///     Controls whether access tokens are transmitted via the browser for this client (defaults to <c>false</c>).
        ///     This can prevent accidental leakage of access tokens when multiple response types are allowed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if access tokens can be transmitted via the browser; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>Default value is false.</remarks>
        [JsonProperty("allowAccessTokensViaBrowser")]
        public bool AllowAccessTokensViaBrowser { get; set; }

        /// <summary>
        ///     Specifies allowed URIs to return tokens or authorization codes to
        /// </summary>
        [JsonProperty("redirectUris")]
        public List<ClientRedirectUri> RedirectUris { get; set; }

        /// <summary>
        ///     Specifies allowed URIs to redirect to after logout
        /// </summary>
        [JsonProperty("postLogoutRedirectUris")]
        public List<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }

        /// <summary>
        ///     Specifies logout URI at client for HTTP front-channel based logout.
        /// </summary>
        [JsonProperty("frontChannelLogoutUri")]
        public string FrontChannelLogoutUri { get; set; }

        /// <summary>
        ///     Specifies is the user's session id should be sent to the FrontChannelLogoutUri. Defaults to <c>true</c>.
        /// </summary>
        /// <remarks>Default value is true.</remarks>
        [JsonProperty("frontChannelLogoutSessionRequired")]
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;

        /// <summary>
        ///     Specifies logout URI at client for HTTP back-channel based logout.
        /// </summary>
        [JsonProperty("backChannelLogoutUri")]
        public string BackChannelLogoutUri { get; set; }

        /// <summary>
        ///     Specifies is the user's session id should be sent to the BackChannelLogoutUri. Defaults to <c>true</c>.
        /// </summary>
        /// <remarks>Default value is true.</remarks>
        [JsonProperty("backChannelLogoutSessionRequired")]
        public bool BackChannelLogoutSessionRequired { get; set; } = true;

        /// <summary>
        ///     Gets or sets a value indicating whether [allow offline access]. Defaults to <c>false</c>.
        /// </summary>
        /// <remarks>Default value is false.</remarks>
        [JsonProperty("allowOfflineAccess")]
        public bool AllowOfflineAccess { get; set; }

        /// <summary>
        ///     Specifies the api scopes that the client is allowed to request. If empty, the client can't access any scope
        /// </summary>
        [JsonProperty("allowedScopes")]
        public List<ClientScope> AllowedScopes { get; set; }

        /// <summary>
        ///     Lifetime of identity token in seconds (defaults to 300 seconds / 5 minutes)
        /// </summary>
        [JsonProperty("identityTOkenLifetime")]
        public int IdentityTokenLifetime { get; set; } = 300;

        /// <summary>
        ///     Lifetime of access token in seconds (defaults to 3600 seconds / 1 hour)
        /// </summary>
        [JsonProperty("accessTokenLifetime")]
        public int AccessTokenLifetime { get; set; } = 3600;

        /// <summary>
        ///     Lifetime of authorization code in seconds (defaults to 300 seconds / 5 minutes)
        /// </summary>
        [JsonProperty("authorizationCodeLifetime")]
        public int AuthorizationCodeLifetime { get; set; } = 300;

        /// <summary>
        ///     Lifetime of a user consent in seconds. Defaults to null (no expiration)
        /// </summary>
        [JsonProperty("consentLifetime")]
        public int? ConsentLifetime { get; set; }

        /// <summary>
        ///     Maximum lifetime of a refresh token in seconds. Defaults to 2592000 seconds / 30 days
        /// </summary>
        [JsonProperty("absoluteRefreshTokenLifetime")]
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;

        /// <summary>
        ///     Sliding lifetime of a refresh token in seconds. Defaults to 1296000 seconds / 15 days
        /// </summary>
        [JsonProperty("slidingRefreshTokenLifetime")]
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;

        /// <summary>
        ///     ReUse: the refresh token handle will stay the same when refreshing tokens
        ///     OneTime: the refresh token handle will be updated when refreshing tokens
        /// </summary>
        /// <remarks>Default value is OneTimeOnly (1).</remarks>
        [JsonProperty("refreshTokenUsage")]
        public int RefreshTokenUsage { get; set; } = (int) TokenUsage.OneTimeOnly;

        /// <summary>
        ///     Gets or sets a value indicating whether the access token (and its claims) should be updated on a refresh token
        ///     request.
        ///     Defaults to <c>false</c>.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the token should be updated; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("updateAccessTokenClaimsOnRefresh")]
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        /// <summary>
        ///     Absolute: the refresh token will expire on a fixed point in time (specified by the AbsoluteRefreshTokenLifetime)
        ///     Sliding: when refreshing the token, the lifetime of the refresh token will be renewed (by the amount specified in
        ///     SlidingRefreshTokenLifetime). The lifetime will not exceed AbsoluteRefreshTokenLifetime.
        /// </summary>
        /// <remarks>Default value is Absolute (1).</remarks>
        [JsonProperty("refreshTokenExpiration")]
        public int RefreshTokenExpiration { get; set; } = (int) TokenExpiration.Absolute;

        /// <summary>
        ///     Specifies whether the access token is a reference token or a self contained JWT token (defaults to Jwt).
        /// </summary>
        [JsonProperty("accessTokenType")]
        public int AccessTokenType { get; set; } // AccessTokenType.Jwt;

        /// <summary>
        ///     Gets or sets a value indicating whether the local login is allowed for this client. Defaults to <c>true</c>.
        /// </summary>
        /// <value>
        ///     <c>true</c> if local logins are enabled; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("enableLocalLogin")]
        public bool EnableLocalLogin { get; set; } = true;

        /// <summary>
        ///     Specifies which external IdPs can be used with this client (if list is empty all IdPs are allowed). Defaults to
        ///     empty.
        /// </summary>
        [JsonProperty("identityProviderRestrictions")]
        public List<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether JWT access tokens should include an identifier. Defaults to <c>false</c>.
        /// </summary>
        /// <value>
        ///     <c>true</c> to add an id; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("includeJwtId")]
        public bool IncludeJwtId { get; set; }

        /// <summary>
        ///     Allows settings claims for the client (will be included in the access token).
        /// </summary>
        /// <value>
        ///     The claims.
        /// </value>
        [JsonProperty("claims")]
        public List<ClientClaim> Claims { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether client claims should be always included in the access tokens - or only for
        ///     client credentials flow.
        ///     Defaults to <c>false</c>
        /// </summary>
        /// <value>
        ///     <c>true</c> if claims should always be sent; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("alwaysSendClientClaims")]
        public bool AlwaysSendClientClaims { get; set; }

        /// <summary>
        ///     Gets or sets a value to prefix it on client claim types. Defaults to <c>client_</c>.
        /// </summary>
        /// <value>
        ///     Any non empty string if claims should be prefixed with the value; otherwise, <c>null</c>.
        /// </value>
        /// <remarks>Default value is `client_`.</remarks>
        [JsonProperty("clientClaimsPrefix")]
        public string ClientClaimsPrefix { get; set; } = "client_";

        /// <summary>
        ///     Gets or sets a salt value used in pair-wise subjectId generation for users of this client.
        /// </summary>
        [JsonProperty("pairWiseSubjectSalt")]
        public string PairWiseSubjectSalt { get; set; }

        /// <summary>
        ///     Gets or sets the allowed CORS origins for JavaScript clients.
        /// </summary>
        /// <value>
        ///     The allowed CORS origins.
        /// </value>
        [JsonProperty("allowedCorsOrigins")]
        public List<ClientCorsOrigin> AllowedCorsOrigins { get; set; }

        /// <summary>
        ///     Gets or sets the custom properties for the client.
        /// </summary>
        /// <value>
        ///     The properties.
        /// </value>
        [JsonProperty("properties")]
        public List<ClientProperty> Properties { get; set; }
    }
}