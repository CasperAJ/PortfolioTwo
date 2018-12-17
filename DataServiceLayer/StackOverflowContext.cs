using System;
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
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }

        //search
        public DbQuery<ExactSearchResult> ExactSearchResults { get; set; }
        public DbQuery<PostTFIDF> BestSearchResultTFIDFs { get; set; }
        public DbQuery<PostRank> BestSearchResultRanks { get; set; }
        public DbQuery<CloudSimple> CloudSimples { get; set; }
        public DbQuery<CloudTFIDF> CloudTfidfs { get; set; }
        public DbQuery<WordAssociation> WordAssociations { get; set; }
        public DbQuery<ForceNode> ForceGraphs { get; set; }

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


            modelBuilder.Entity<Mark>().ToTable("marks");
            modelBuilder.Entity<Mark>().Property(x => x.PostId).HasColumnName("post_id");
            modelBuilder.Entity<Mark>().Property(x => x.UserId).HasColumnName("user_id");
            modelBuilder.Entity<Mark>().Property(x => x.Type).HasColumnName("marktype_id");
            modelBuilder.Entity<Mark>().Property(x => x.Note).HasColumnName("note");
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
            modelBuilder.Entity<User>().Property(x => x.Salt).HasColumnName("salt");
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(x => x.CreationDate).HasColumnName("creationdate");

            // searches
            modelBuilder.Query<ExactSearchResult>().Property(x => x.Id).HasColumnName("id");

            modelBuilder.Query<CloudSimple>().Property(x => x.Text).HasColumnName("word");
            modelBuilder.Query<CloudSimple>().Property(x => x.Rank).HasColumnName("rank");

            modelBuilder.Query<CloudTFIDF>().Property(x => x.Word).HasColumnName("word");
            modelBuilder.Query<CloudTFIDF>().Property(x => x.Rank).HasColumnName("rank");

            modelBuilder.Query<WordAssociation>().Property(x => x.Word2).HasColumnName("word2");
            modelBuilder.Query<WordAssociation>().Property(x => x.Grade).HasColumnName("grade");

            modelBuilder.Query<ForceNode>().Property(x => x.Node).HasColumnName("generate_force_graph_input");

            modelBuilder.Query<PostTFIDF>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Query<PostTFIDF>().Property(x => x.ParentId).HasColumnName("parentid");
            modelBuilder.Query<PostTFIDF>().Property(x => x.AcceptedAnswerId).HasColumnName("acceptedanswerid");
            modelBuilder.Query<PostTFIDF>().Property(x => x.CreationDate).HasColumnName("creationdate");
            modelBuilder.Query<PostTFIDF>().Property(x => x.Score).HasColumnName("score");
            modelBuilder.Query<PostTFIDF>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Query<PostTFIDF>().Property(x => x.ClosedDate).HasColumnName("closeddate");
            modelBuilder.Query<PostTFIDF>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Query<PostTFIDF>().Property(x => x.AuthorId).HasColumnName("author_id");
            modelBuilder.Query<PostTFIDF>().Property(x => x.LinkPostId).HasColumnName("linkpostid");
            modelBuilder.Query<PostTFIDF>().Property(x => x.Rank).HasColumnName("rank");

            modelBuilder.Query<PostRank>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Query<PostRank>().Property(x => x.ParentId).HasColumnName("parentid");
            modelBuilder.Query<PostRank>().Property(x => x.AcceptedAnswerId).HasColumnName("acceptedanswerid");
            modelBuilder.Query<PostRank>().Property(x => x.CreationDate).HasColumnName("creationdate");
            modelBuilder.Query<PostRank>().Property(x => x.Score).HasColumnName("score");
            modelBuilder.Query<PostRank>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Query<PostRank>().Property(x => x.ClosedDate).HasColumnName("closeddate");
            modelBuilder.Query<PostRank>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Query<PostRank>().Property(x => x.AuthorId).HasColumnName("author_id");
            modelBuilder.Query<PostRank>().Property(x => x.LinkPostId).HasColumnName("linkpostid");
            modelBuilder.Query<PostRank>().Property(x => x.Rank).HasColumnName("rank");

        }

         
    }
}
