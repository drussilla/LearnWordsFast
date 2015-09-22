﻿using System;

namespace LearnWordsFast.DAL.Models
{
    public class Language
    {
        public Language()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid Id { get; set; }  

        public virtual string Name { get; set; }
    }
}