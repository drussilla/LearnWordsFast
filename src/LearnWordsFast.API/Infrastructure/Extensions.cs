﻿using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace LearnWordsFast.API.Infrastructure
{
    public static class Extensions
    {
        private static readonly Random Random = new Random();

        public static Guid GetId(this ClaimsPrincipal user)
        {
            // TODO: update to support RC2
            return Guid.NewGuid();
        }

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}