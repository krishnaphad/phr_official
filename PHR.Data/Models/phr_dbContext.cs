using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PHR.Data.Models
{
    public partial class phr_dbContext : DbContext
    {
        public phr_dbContext()
        {
        }

        public phr_dbContext(DbContextOptions<phr_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CityMaster> CityMasters { get; set; }
        public virtual DbSet<CompanyMaster> CompanyMasters { get; set; }
        public virtual DbSet<EducationMaster> EducationMasters { get; set; }
        public virtual DbSet<ForgotPassword> ForgotPasswords { get; set; }
        public virtual DbSet<HappyCustomer> HappyCustomers { get; set; }
        public virtual DbSet<JobApplication> JobApplications { get; set; }
        public virtual DbSet<JobsCollection> JobsCollections { get; set; }
        public virtual DbSet<KeySkillMaster> KeySkillMasters { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<LoginDetail> LoginDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseMySQL("server=localhost;port=3306;user=admin;password=Pm2dmin123#;database=phr_db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityMaster>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PRIMARY");

                entity.ToTable("city_master");

                entity.HasIndex(e => e.CityId, "city_id")
                    .IsUnique();

                entity.HasIndex(e => e.CityName, "city_name")
                    .IsUnique();

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.CityAddedDate)
                    .HasColumnType("date")
                    .HasColumnName("city_added_date");

                entity.Property(e => e.CityIsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("city_is_active");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("city_name");
            });

            modelBuilder.Entity<CompanyMaster>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PRIMARY");

                entity.ToTable("company_master");

                entity.HasIndex(e => e.CompanyId, "company_id")
                    .IsUnique();

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CompanyAddedDate)
                    .HasColumnType("date")
                    .HasColumnName("company_added_date");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("company_name");

                entity.Property(e => e.IsCompanyActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_company_active");
            });

            modelBuilder.Entity<EducationMaster>(entity =>
            {
                entity.HasKey(e => e.EducationId)
                    .HasName("PRIMARY");

                entity.ToTable("education_master");

                entity.HasIndex(e => e.EducationId, "education_id")
                    .IsUnique();

                entity.Property(e => e.EducationId).HasColumnName("education_id");

                entity.Property(e => e.EducationAddedDate)
                    .HasColumnType("date")
                    .HasColumnName("education_added_date");

                entity.Property(e => e.EducationName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("education_name");

                entity.Property(e => e.IsEducationActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_education_active");
            });

            modelBuilder.Entity<ForgotPassword>(entity =>
            {
                entity.ToTable("forgot_password");

                entity.Property(e => e.ForgotPasswordId).HasColumnName("forgot_password_id");

                entity.Property(e => e.IsLinkActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_link_active");

                entity.Property(e => e.LinkCreatedDate).HasColumnName("link_created_date");

                entity.Property(e => e.RequestedUserEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("requested_user_email");

                entity.Property(e => e.SystemGeneratedPassword)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("system_generated_password");
            });

            modelBuilder.Entity<HappyCustomer>(entity =>
            {
                entity.ToTable("happy_customers");

                entity.Property(e => e.HappyCustomerId).HasColumnName("happy_customer_id");

                entity.Property(e => e.HappyCustomerComment)
                    .HasMaxLength(200)
                    .HasColumnName("happy_customer_comment");

                entity.Property(e => e.HappyCustomerCompanyLogoName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("happy_customer_company_logo_name");

                entity.Property(e => e.HappyCustomerCompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("happy_customer_company_name");
            });

            modelBuilder.Entity<JobApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationId)
                    .HasName("PRIMARY");

                entity.ToTable("job_application");

                entity.HasIndex(e => e.ApplicationId, "application_id")
                    .IsUnique();

                entity.HasIndex(e => e.JobId, "job_id_idx");

                entity.HasIndex(e => e.UserId, "user_id_idx");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.ApplicantEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("applicant_email");

                entity.Property(e => e.ApplicantExperiance).HasColumnName("applicant_experiance");

                entity.Property(e => e.ApplicantName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("applicant_name");

                entity.Property(e => e.AppliedDate)
                    .HasColumnType("date")
                    .HasColumnName("applied_date");

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("contact_number");

                entity.Property(e => e.CurrentCompany)
                    .HasMaxLength(100)
                    .HasColumnName("current_company");

                entity.Property(e => e.CurrentCtc).HasColumnName("current_ctc");

                entity.Property(e => e.IsCandidateHired)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_candidate_hired");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.NoticePeriod).HasColumnName("notice_period");

                entity.Property(e => e.PositionApplied)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("position_applied");

                entity.Property(e => e.Qualification)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("qualification");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobApplications)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.JobApplications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_id");
            });

            modelBuilder.Entity<JobsCollection>(entity =>
            {
                entity.HasKey(e => e.JobId)
                    .HasName("PRIMARY");

                entity.ToTable("jobs_collection");

                entity.HasIndex(e => e.JobId, "job_id")
                    .IsUnique();

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.JobAddedDate)
                    .HasColumnType("date")
                    .HasColumnName("job_added_date");

                entity.Property(e => e.JobCity)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("job_city");

                entity.Property(e => e.JobCompanyId).HasColumnName("job_company_id");

                entity.Property(e => e.JobDescription)
                    .HasColumnType("varchar(5000)")
                    .HasColumnName("job_description");

                entity.Property(e => e.JobDescriptionFileName)
                    .HasMaxLength(100)
                    .HasColumnName("job_description_file_name");

                entity.Property(e => e.JobEducationRequired)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("job_education_required");

                entity.Property(e => e.JobExperienceRequired)
                    .HasMaxLength(10)
                    .HasColumnName("job_experience_required");

                entity.Property(e => e.JobKeySkills)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("job_key_skills");

                entity.Property(e => e.JobLocationAddress)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("job_location_address");

                entity.Property(e => e.JobSalary)
                    .HasMaxLength(50)
                    .HasColumnName("job_salary");

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("job_title");
            });

            modelBuilder.Entity<KeySkillMaster>(entity =>
            {
                entity.HasKey(e => e.KeySkillId)
                    .HasName("PRIMARY");

                entity.ToTable("key_skill_master");

                entity.HasIndex(e => e.KeySkillId, "key_skill_id")
                    .IsUnique();

                entity.Property(e => e.KeySkillId).HasColumnName("key_skill_id");

                entity.Property(e => e.IsKeySkillActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_key_skill_active");

                entity.Property(e => e.KeySkillAddedDate)
                    .HasColumnType("date")
                    .HasColumnName("key_skill_added_date");

                entity.Property(e => e.KeySkillName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("key_skill_name");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.LogMessage)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .HasColumnName("log_message");

                entity.Property(e => e.LogStack)
                    .HasMaxLength(2000)
                    .HasColumnName("log_stack");
            });

            modelBuilder.Entity<LoginDetail>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("login_details");

                entity.HasIndex(e => e.UserEmail, "user_email")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "user_id")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "user_name")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AddedDate)
                    .HasColumnType("date")
                    .HasColumnName("added_date");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.IsUserActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_user_active");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("mobile_number");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("user_email");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("user_password");

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("user_role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
