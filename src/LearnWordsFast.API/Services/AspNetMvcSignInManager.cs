﻿using System.Threading.Tasks;
using LearnWordsFast.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace LearnWordsFast.API.Services
{
    public class AspNetMvcSignInManager : ISignInManager
    {
        private readonly SignInManager<User> _signInManager;

        public AspNetMvcSignInManager(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public Task<SignInResult> PasswordSignInAsync(string login, string password)
        {
            return _signInManager.PasswordSignInAsync(login, password, true, false);
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }

        public Task SignInAsync(User user)
        {
            return _signInManager.SignInAsync(user, true);
        }
    }
}