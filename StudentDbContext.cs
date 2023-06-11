﻿using Microsoft.EntityFrameworkCore;
using PassTheDataFromModelMVC.Models;

namespace PassTheDataFromModelMVC.Context
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }
        public DbSet<Student> students { get; set; }
    }
}
