using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;

namespace PRN221_DataAccess;

public partial class Prn221Context : DbContext
{
    public Prn221Context()
    {
    }

    public Prn221Context(DbContextOptions<Prn221Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountConversation> AccountConversations { get; set; }

    public virtual DbSet<BookingService> BookingServices { get; set; }

    public virtual DbSet<CategoryNews> CategoryNews { get; set; }

    public virtual DbSet<CategoryPost> CategoryPosts { get; set; }

    public virtual DbSet<CategoryService> CategoryServices { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<Follow> Follows { get; set; }

    public virtual DbSet<LikePost> LikePosts { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<NewsImage> NewsImages { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostImage> PostImages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceRating> ServiceRatings { get; set; }

    public virtual DbSet<SharePost> SharePosts { get; set; }

    public virtual DbSet<View> Views { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-5B3AKMH;Initial Catalog=PRN221;Persist Security Info=True;User ID=sa;Password=khoa31102003;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__46A222CDDA4DC2DA");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Username, "UQ__Account__F3DBC572EC0B8408").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.Avatar)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.DegreeUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("degree_url");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EmailConfirmed).HasColumnName("email_confirmed");
            entity.Property(e => e.FacebookId)
                .HasMaxLength(255)
                .HasColumnName("facebook_id");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Major)
                .HasMaxLength(100)
                .HasColumnName("major");
            entity.Property(e => e.Otp).HasColumnName("otp");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.PhoneConfirmed).HasColumnName("phone_confirmed");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<AccountConversation>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.ConversationId }).HasName("PK__AccountC__F5B3C5241C7EFF31");

            entity.ToTable("AccountConversation");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.ConversationId).HasColumnName("conversation_id");
            entity.Property(e => e.IsOut).HasColumnName("is_out");
            entity.Property(e => e.OutAt).HasColumnName("out_at");
        });

        modelBuilder.Entity<BookingService>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__BookingS__5DE3A5B133B84482");

            entity.ToTable("BookingService");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BookingAt).HasColumnName("booking_at");
            entity.Property(e => e.BookingBy).HasColumnName("booking_by");
            entity.Property(e => e.BookingStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("booking_status");
            entity.Property(e => e.IsDeletedExpert).HasColumnName("is_deleted_expert");
            entity.Property(e => e.IsDeletedFarmer).HasColumnName("is_deleted_farmer");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
        });

        modelBuilder.Entity<CategoryNews>(entity =>
        {
            entity.HasKey(e => e.CategoryNewsId).HasName("PK__Category__9D9BEED87C17BC64");

            entity.Property(e => e.CategoryNewsId).HasColumnName("category_news_id");
            entity.Property(e => e.CategoryNewsDescription)
                .HasMaxLength(500)
                .HasColumnName("category_news_description");
            entity.Property(e => e.CategoryNewsName)
                .HasMaxLength(100)
                .HasColumnName("category_news_name");
        });

        modelBuilder.Entity<CategoryPost>(entity =>
        {
            entity.HasKey(e => e.CategoryPostId).HasName("PK__Category__02AEB4E3D897F871");

            entity.ToTable("CategoryPost");

            entity.Property(e => e.CategoryPostId).HasColumnName("category_post_id");
            entity.Property(e => e.CategoryPostDescription)
                .HasMaxLength(500)
                .HasColumnName("category_post_description");
            entity.Property(e => e.CategoryPostName)
                .HasMaxLength(200)
                .HasColumnName("category_post_name");
        });

        modelBuilder.Entity<CategoryService>(entity =>
        {
            entity.HasKey(e => e.CategoryServiceId).HasName("PK__Category__8B6132CC014ED78A");

            entity.ToTable("CategoryService");

            entity.Property(e => e.CategoryServiceId).HasColumnName("category_service_id");
            entity.Property(e => e.CategoryServiceDescription)
                .HasMaxLength(500)
                .HasColumnName("category_service_description");
            entity.Property(e => e.CategoryServiceName)
                .HasMaxLength(100)
                .HasColumnName("category_service_name");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__E7957687BFDB8466");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Content)
                .HasMaxLength(200)
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.ConversationId).HasName("PK__Conversa__311E7E9ABB210042");

            entity.ToTable("Conversation");

            entity.Property(e => e.ConversationId).HasColumnName("conversation_id");
            entity.Property(e => e.ConversationName)
                .HasMaxLength(20)
                .HasColumnName("conversation_name");
            entity.Property(e => e.CreateAt).HasColumnName("create_at");
            entity.Property(e => e.CreatorId).HasColumnName("creator_id");
            entity.Property(e => e.DeleteAt).HasColumnName("delete_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsGroup).HasColumnName("is_group");
            entity.Property(e => e.MemberCount).HasColumnName("member_count");
        });

        modelBuilder.Entity<Follow>(entity =>
        {
            entity.HasKey(e => e.FollowId).HasName("PK__Follow__15A691441F97A0BC");

            entity.ToTable("Follow");

            entity.Property(e => e.FollowId).HasColumnName("follow_id");
            entity.Property(e => e.BeFollowedId).HasColumnName("be_followed_id");
            entity.Property(e => e.FollowAt).HasColumnName("follow_at");
            entity.Property(e => e.FollowerId).HasColumnName("follower_id");
        });

        modelBuilder.Entity<LikePost>(entity =>
        {
            entity.HasKey(e => e.LikePostId).HasName("PK__LikePost__8F1D2FE8818D5EB4");

            entity.ToTable("LikePost");

            entity.Property(e => e.LikePostId).HasColumnName("like_post_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.UnLike).HasColumnName("un_like");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Message__0BBF6EE6FB4E2C8E");

            entity.ToTable("Message");

            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.Content)
                .HasMaxLength(200)
                .HasColumnName("content");
            entity.Property(e => e.ConversationId).HasColumnName("conversation_id");
            entity.Property(e => e.CreateAt).HasColumnName("create_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__4C27CCD871254FCF");

            entity.Property(e => e.NewsId).HasColumnName("news_id");
            entity.Property(e => e.CategoryNewsId).HasColumnName("category_news_id");
            entity.Property(e => e.Content)
                .HasMaxLength(100)
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<NewsImage>(entity =>
        {
            entity.HasKey(e => e.NewsImageId).HasName("PK__NewsImag__4C7E25D8A31F2978");

            entity.ToTable("NewsImage");

            entity.Property(e => e.NewsImageId).HasColumnName("news_image_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.NewsId).HasColumnName("news_id");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__3ED787663D2FFA7D");

            entity.ToTable("Post");

            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CategoryPostId).HasColumnName("category_post_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.PostContent)
                .HasMaxLength(1000)
                .HasColumnName("post_content");
            entity.Property(e => e.UpdateAt).HasColumnName("update_at");
        });

        modelBuilder.Entity<PostImage>(entity =>
        {
            entity.HasKey(e => e.PostImageId).HasName("PK__PostImag__CD0DD560A8C093F6");

            entity.ToTable("PostImage");

            entity.Property(e => e.PostImageId).HasColumnName("post_image_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.PostId).HasColumnName("post_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CC1906DA07");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__3E0DB8AFDB37D731");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.AverageRating)
                .HasColumnType("decimal(2, 1)")
                .HasColumnName("average_rating");
            entity.Property(e => e.Content)
                .HasMaxLength(1000)
                .HasColumnName("content");
            entity.Property(e => e.CreateAt).HasColumnName("create_at");
            entity.Property(e => e.CreatorId).HasColumnName("creator_id");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.IsEnable).HasColumnName("is_enable");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RatingCount).HasColumnName("rating_count");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<ServiceRating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__ServiceR__D35B278B3036FE54");

            entity.ToTable("ServiceRating");

            entity.Property(e => e.RatingId).HasColumnName("rating_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.RatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("rated_at");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(2, 1)")
                .HasColumnName("rating");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<SharePost>(entity =>
        {
            entity.HasKey(e => e.SharePostId).HasName("PK__SharePos__3B880F32ECD90F29");

            entity.ToTable("SharePost");

            entity.Property(e => e.SharePostId).HasColumnName("share_post_id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.ShareAt).HasColumnName("share_at");
            entity.Property(e => e.SharerId).HasColumnName("sharer_id");
        });

        modelBuilder.Entity<View>(entity =>
        {
            entity.HasKey(e => e.CountViewId).HasName("PK__Views__C5F7EC927A5A56D8");

            entity.Property(e => e.CountViewId).HasColumnName("count_view_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.NewsId).HasColumnName("news_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}