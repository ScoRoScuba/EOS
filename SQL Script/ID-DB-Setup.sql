USE [EOS.Identity]
GO

INSERT INTO [dbo].[Scopes] ([Enabled],[Name],[DisplayName],[Description],[Required],[Emphasize],[Type],[IncludeAllClaimsForUser],[ClaimsRule],[ShowInDiscoveryDocument])
VALUES (1,'openid',null,null,1,0,0,0,null,1)
INSERT INTO [dbo].[Scopes]([Enabled],[Name],[DisplayName],[Description],[Required],[Emphasize],[Type],[IncludeAllClaimsForUser],[ClaimsRule],[ShowInDiscoveryDocument])
VALUES(1,'profile',null,null,0,0,0,0,null,1)
INSERT INTO [dbo].[Scopes]([Enabled],[Name],[DisplayName],[Description],[Required],[Emphasize],[Type],[IncludeAllClaimsForUser],[ClaimsRule],[ShowInDiscoveryDocument])
VALUES(1,'email',null,null,0,0,0,0,null,1)

INSERT INTO [dbo].[Scopes]([Enabled],[Name],[DisplayName],[Description],[Required],[Emphasize],[Type],[IncludeAllClaimsForUser],[ClaimsRule],[ShowInDiscoveryDocument])
VALUES(1,'read',null,null,0,0,1,0,null,1)
INSERT INTO [dbo].[Scopes]([Enabled],[Name],[DisplayName],[Description],[Required],[Emphasize],[Type],[IncludeAllClaimsForUser],[ClaimsRule],[ShowInDiscoveryDocument])
VALUES(1,'write',null,null,0,0,1,0,null,1)

INSERT INTO [dbo].[Scopes]([Enabled],[Name],[DisplayName],[Description],[Required],[Emphasize],[Type],[IncludeAllClaimsForUser],[ClaimsRule],[ShowInDiscoveryDocument])
VALUES(1,'EOSAPI',null,null,0,0,1,0,null,1)

GO

INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('sub', '' ,1, (select Id from Scopes where Name = 'openid'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('name', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('familiy_name', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('given_name', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('middle_name', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('nickname', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('preferred_username', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('profile', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('picture', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('website', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('gender', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('birthdate', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('zoneinfo', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('locale', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('updated_at', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('phone_number', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('role', '' ,1, (select Id from Scopes where Name = 'profile'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('email', '' ,1, (select Id from Scopes where Name = 'email'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('email_verified', '' ,1, (select Id from Scopes where Name = 'email'))
INSERT INTO [dbo].[ScopeClaims] ([Name],[Description],[AlwaysIncludeInIdToken],[Scope_Id]) VALUES ('api', '' ,1, (select Id from Scopes where Name = 'EOSAPI'))
GO



INSERT INTO [dbo].[Clients]
	([Enabled],[ClientId],[ClientName],[RequireConsent],[AllowRememberConsent],[Flow],[IdentityTokenLifetime],[AccessTokenLifetime],[AuthorizationCodeLifetime],
	[AbsoluteRefreshTokenLifetime],[SlidingRefreshTokenLifetime],[RefreshTokenUsage],[RefreshTokenExpiration],[AccessTokenType],[EnableLocalLogin],[IncludeJwtId],
	[AlwaysSendClientClaims],[PrefixClientClaims])
     VALUES(1,'EOSWebSite','EOS Web Site',0,0,0,360,360,300,1296000,1296000,1,1,1,1,0,1,0)

INSERT INTO [dbo].[Clients]
	([Enabled],[ClientId],[ClientName],[RequireConsent],[AllowRememberConsent],[Flow],[IdentityTokenLifetime],[AccessTokenLifetime],[AuthorizationCodeLifetime],
	[AbsoluteRefreshTokenLifetime],[SlidingRefreshTokenLifetime],[RefreshTokenUsage],[RefreshTokenExpiration],[AccessTokenType],[EnableLocalLogin],[IncludeJwtId],
	[AlwaysSendClientClaims],[PrefixClientClaims])
     VALUES(1,'EOSAPI','EOS Resource API',0,0,4,360,360,300,1296000,1296000,1,1,0,1,0,1,0)	
GO

-- what scopes are supported by what clients
INSERT INTO [dbo].[ClientScopeRestrictions] ([Scope],[Client_Id]) VALUES ( 'openid', (select id from Clients where ClientId='EOSWebSite'))
INSERT INTO [dbo].[ClientScopeRestrictions] ([Scope],[Client_Id]) VALUES ( 'profile', (select id from Clients where ClientId='EOSWebSite'))
INSERT INTO [dbo].[ClientScopeRestrictions] ([Scope],[Client_Id]) VALUES ( 'email', (select id from Clients where ClientId='EOSWebSite'))
INSERT INTO [dbo].[ClientScopeRestrictions] ([Scope],[Client_Id]) VALUES ( 'read', (select id from Clients where ClientId='EOSWebSite'))
INSERT INTO [dbo].[ClientScopeRestrictions] ([Scope],[Client_Id]) VALUES ( 'write', (select id from Clients where ClientId='EOSWebSite'))
INSERT INTO [dbo].[ClientScopeRestrictions] ([Scope],[Client_Id]) VALUES ( 'EOSAPI', (select id from Clients where ClientId='EOSAPI'))
INSERT INTO [dbo].[ClientScopeRestrictions] ([Scope],[Client_Id]) VALUES ( 'api', (select id from Clients where ClientId='EOSAPI'))

GO

-- these are security feature.  and are passed in on call to authenticate
INSERT INTO [dbo].[ClientRedirectUris] ([Uri] ,[Client_Id]) VALUES('https://localhost:44301/callback', (select id from Clients where ClientId='EOSWebSite') )
GO

INSERT INTO [dbo].[ClientPostLogoutRedirectUris] ([Uri],[Client_Id]) VALUES('https://localhost:44301', (select id from Clients where ClientId='EOSWebSite') )
GO

INSERT INTO [dbo].[ClientSecrets] ([Value],[Client_Id]) VALUES ('K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=', (select id from Clients where ClientId='EOSWebSite') )
INSERT INTO [dbo].[ClientSecrets] ([Value],[Client_Id]) VALUES ('K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=', (select id from Clients where ClientId='EOSAPI') )
GO




