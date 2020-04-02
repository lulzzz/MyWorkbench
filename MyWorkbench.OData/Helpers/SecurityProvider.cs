﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using MyWorkbench.BusinessObjects.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MyWorkbench.OData
{
	public class SecurityProvider {
		public bool InitConnection(string userName, string password, HttpContext httpContext,
			XpoDataStoreProviderService xpoDataStoreProviderService, string connectionString) {
			AuthenticationStandardLogonParameters parameters = new AuthenticationStandardLogonParameters(userName, password);
			SecurityStrategyComplex security = GetSecurity(typeof(AuthenticationStandardProvider).Name, parameters);
			IObjectSpaceProvider objectSpaceProvider = GetObjectSpaceProvider(security, xpoDataStoreProviderService, connectionString);
			try {
				Login(security, objectSpaceProvider);
				SignIn(httpContext, userName);
				return true;
			}
			catch {
				return false;
			}
		}
		private void SignIn(HttpContext httpContext, string userName) {
			List<Claim> claims = new List<Claim>{
				new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
			};
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			ClaimsPrincipal principal = new ClaimsPrincipal(id);
			httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
		}

		public SecurityStrategyComplex GetSecurity(string authenticationName, object parameter) {
			AuthenticationMixed authentication = new AuthenticationMixed();
			authentication.LogonParametersType = typeof(AuthenticationStandardLogonParameters);
			authentication.AddAuthenticationStandardProvider(typeof(Employee));
			authentication.AddIdentityAuthenticationProvider(typeof(Employee));
			authentication.SetupAuthenticationProvider(authenticationName, parameter);
			SecurityStrategyComplex security = new SecurityStrategyComplex(typeof(Employee), typeof(EmployeeRole), authentication);
			security.RegisterXPOAdapterProviders();
			return security;
		}
		public IObjectSpaceProvider GetObjectSpaceProvider(SecurityStrategyComplex security, XpoDataStoreProviderService xpoDataStoreProviderService, string connectionString) {
			SecuredObjectSpaceProvider objectSpaceProvider = new SecuredObjectSpaceProvider(security, xpoDataStoreProviderService.GetDataStoreProvider(connectionString, null, true), true);
			RegisterEntities(objectSpaceProvider);
			return objectSpaceProvider;
		}
		public void Login(SecurityStrategyComplex security, IObjectSpaceProvider objectSpaceProvider) {
			IObjectSpace objectSpace = objectSpaceProvider.CreateObjectSpace();
			security.Logon(objectSpace);
		}
		private void RegisterEntities(SecuredObjectSpaceProvider objectSpaceProvider) {
			objectSpaceProvider.TypesInfo.RegisterEntity(typeof(Employee));
			objectSpaceProvider.TypesInfo.RegisterEntity(typeof(EmployeeRole));
		}
	}
}
