﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using DataServiceLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataServiceLayer
{
    public class StackOverflowContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<MarkType> MarkTypes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Search> Searches { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagToPost> TagToPosts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("host=localhost;db=stackoverflow;uid=postgres;pwd=postroot;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().ToTable("authors");
            modelBuilder.Entity<Author>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Author>().Property(x => x.Name).HasColumnName("userdisplayname");
            modelBuilder.Entity<Author>().Property(x => x.CreationDate).HasColumnName("usercreation");
            modelBuilder.Entity<Author>().Property(x => x.Location).HasColumnName("userlocation");
            modelBuilder.Entity<Author>().Property(x => x.Age).HasColumnName("userage");

            modelBuilder.Entity<Comment>().ToTable("comments");
            modelBuilder.Entity<Comment>().Property(x => x.Id).HasColumnName("commentid");
            modelBuilder.Entity<Comment>().Property(x => x.Score).HasColumnName("commentscore");
            modelBuilder.Entity<Comment>().Property(x => x.Text).HasColumnName("commenttext");
            modelBuilder.Entity<Comment>().Property(x => x.CreatedDate).HasColumnName("commentcreateddate");
            modelBuilder.Entity<Comment>().Property(x => x.AuthorId).HasColumnName("author_id");
            modelBuilder.Entity<Comment>().Property(x => x.PostId).HasColumnName("post_id");

            // Note mangler op marks?
            // Mark binding til userid og postid failer i forhjold til db
            modelBuilder.Entity<Mark>().ToTable("marks");
            modelBuilder.Entity<Mark>().ToTable("marks").HasKey(x => new {x.UserId, x.PostId, x.Type});

            modelBuilder.Entity<MarkType>().ToTable("marktypes");
            modelBuilder.Entity<MarkType>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<MarkType>().Property(x => x.Type).HasColumnName("type");




            modelBuilder.Entity<Post>().ToTable("posts");

            //NOTE: set the modelbuilder that we have a value posttypeid of type int, that descriminates into question on value 1 and answer on value 2.
            // hence no mapping on post.
            modelBuilder.Entity<Post>().HasDiscriminator<int>("posttypeid")
                .HasValue<Question>(1)
                .HasValue<Answer>(2);

            modelBuilder.Entity<Question>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Question>().Property(x => x.ParentId).HasColumnName("parentid");
            modelBuilder.Entity<Question>().Property(x => x.AcceptedAnswerId).HasColumnName("acceptedanswerid");
            modelBuilder.Entity<Question>().Property(x => x.CreationDate).HasColumnName("creationdate");
            modelBuilder.Entity<Question>().Property(x => x.Score).HasColumnName("score");
            modelBuilder.Entity<Question>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Entity<Question>().Property(x => x.ClosedDate).HasColumnName("closeddate");
            modelBuilder.Entity<Question>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<Question>().Property(x => x.AuthorId).HasColumnName("author_id");
            modelBuilder.Entity<Question>().Property(x => x.LinkPostId).HasColumnName("linkpostid");
            
            modelBuilder.Entity<Answer>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Answer>().Property(x => x.ParentId).HasColumnName("parentid");
            modelBuilder.Entity<Answer>().Property(x => x.AcceptedAnswerId).HasColumnName("acceptedanswerid");
            modelBuilder.Entity<Answer>().Property(x => x.CreationDate).HasColumnName("creationdate");
            modelBuilder.Entity<Answer>().Property(x => x.Score).HasColumnName("score");
            modelBuilder.Entity<Answer>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Entity<Answer>().Property(x => x.ClosedDate).HasColumnName("closeddate");
            modelBuilder.Entity<Answer>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<Answer>().Property(x => x.AuthorId).HasColumnName("author_id");
            modelBuilder.Entity<Answer>().Property(x => x.LinkPostId).HasColumnName("linkpostid");


            modelBuilder.Entity<Search>().ToTable("searches");
            modelBuilder.Entity<Search>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Search>().Property(x => x.SearchString).HasColumnName("searchstring");
            modelBuilder.Entity<Search>().Property(x => x.UserId).HasColumnName("user_id");

            modelBuilder.Entity<Tag>().ToTable("tags");
            modelBuilder.Entity<Tag>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Tag>().Property(x => x.TagText).HasColumnName("tagtext");

            modelBuilder.Entity<TagToPost>().ToTable("tag_to_posts");
            modelBuilder.Entity<TagToPost>().HasKey(x => new {x.PostId, x.TagId});

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<User>().Property(x => x.UserName).HasColumnName("username");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(x => x.CreationDate).HasColumnName("creationdate");

        }

         
    }
}
