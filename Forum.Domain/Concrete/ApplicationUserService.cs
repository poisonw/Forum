﻿using Forum.Data.Context;
using Forum.Data.Models;
using Forum.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Domain.Concrete
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.Users;
        }

        public ApplicationUser GetById(string id)
        {
            return GetAll().FirstOrDefault(user => user.Id ==id);
        }

        public async Task UpdateUserRating(string userId, Type type)
        {
            var user = GetById(userId);
            user.Rating += CalculateUserRating(type, user.Rating);
            await _context.SaveChangesAsync();
        }

        private int CalculateUserRating(Type type, int userRating)
        {
            var inc = 0;
            if (type == typeof(Post))
            {
                inc = 1;
            }
            if (type == typeof(PostReply))
            {
                inc = 3;
            }
            return userRating + inc;
        }

        public async Task SetProfileImage(string id, string uri)
        {
            
            var user = GetById(id);
            user.ProfileImageUrl = uri;
            _context.Users.AddOrUpdate(user);
            await _context.SaveChangesAsync();
        }
    }
}
