// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace IdentityServerAspNetIdentity
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();
                    
                    // recuperation des services users et roles
                    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    // creation du role admin
                    var roleName = roleMgr.FindByNameAsync("admin").Result;
                    if (roleName == null)
                    {
                        var adminRole = roleMgr.CreateAsync(new IdentityRole
                        {
                            ConcurrencyStamp = DateTime.Now.ToString(),
                            Name = "admin",
                            NormalizedName = "admin"
                        }).Result;
                    }
                    // creation du role manager
                    roleName = roleMgr.FindByNameAsync("manager").Result;
                    if (roleName == null)
                    {
                        var adminRole = roleMgr.CreateAsync(new IdentityRole
                        {
                            ConcurrencyStamp = DateTime.Now.ToString(),
                            Name = "manager",
                            NormalizedName = "manager"
                        }).Result;
                    }
                    // creation user admin
                    var admin = userMgr.FindByNameAsync("admin").Result;
                    if (admin == null)
                    {
                        admin = new ApplicationUser
                        {
                            UserName = "admin",
                            Email = "admin@cegeplimoilou.ca",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(admin, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        result = userMgr.AddToRoleAsync(admin, "admin").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        result = userMgr.AddClaimsAsync(admin, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "bond james bond"),
                            new Claim(JwtClaimTypes.GivenName, "james"),
                            new Claim(JwtClaimTypes.FamilyName, "bond"),
                            new Claim(JwtClaimTypes.WebSite, "http://bondjamesbond.com"),
                            //new Claim(JwtClaimTypes.Role, "admin"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("admin created");
                    }
                    else
                    {
                        Log.Debug("admin already exists");
                    }
                    // creation user manager
                    var manager = userMgr.FindByNameAsync("manager").Result;
                    if (manager == null)
                    {
                        admin = new ApplicationUser
                        {
                            UserName = "manager",
                            Email = "manager@email.com",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(manager, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        result = userMgr.AddToRoleAsync(manager, "manager").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        result = userMgr.AddClaimsAsync(manager, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Benny"),
                            new Claim(JwtClaimTypes.GivenName, "Ben"),
                            new Claim(JwtClaimTypes.FamilyName, "Benito"),
                            new Claim(JwtClaimTypes.WebSite, "http://Benito.com"),
                            //new Claim(JwtClaimTypes.Role, "manager"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("manager created");
                    }
                    else
                    {
                        Log.Debug("manager already exists");
                    }
                    ///////////////////////////////////////////////////////////////////////
                    var bob = userMgr.FindByNameAsync("bob").Result;
                    if (bob == null)
                    {
                        bob = new ApplicationUser
                        {
                            UserName = "bob",
                            Email = "BobSmith@email.com",
                            EmailConfirmed = true
                        };
                        var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("location", "somewhere")
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("bob created");
                    }
                    else
                    {
                        Log.Debug("bob already exists");
                    }
                }
            }
        }
    }
}
