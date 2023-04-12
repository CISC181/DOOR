using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DOOR.EF.Models;

namespace DOOR.EF.Data
{
    public partial class DOOROracleContext : DbContext
    {
        public DOOROracleContext()
        {
        }

        public DOOROracleContext(DbContextOptions<DOOROracleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<GradeConversion> GradeConversions { get; set; } = null!;
        public virtual DbSet<GradeType> GradeTypes { get; set; } = null!;
        public virtual DbSet<GradeTypeWeight> GradeTypeWeights { get; set; } = null!;
        public virtual DbSet<Instructor> Instructors { get; set; } = null!;
        public virtual DbSet<OraTranslateMsg> OraTranslateMsgs { get; set; } = null!;
        public virtual DbSet<School> Schools { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Zipcode> Zipcodes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("UD_COLEJM")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => new { e.CourseNo, e.SchoolId })
                    .HasName("COURSE_PK");

                entity.Property(e => e.CourseNo).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("COURSE_FK2");

                entity.HasOne(d => d.PrerequisiteNavigation)
                    .WithMany(p => p.InversePrerequisiteNavigation)
                    .HasForeignKey(d => new { d.Prerequisite, d.PrerequisiteSchoolId })
                    .HasConstraintName("COURSE_FK1");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => new { e.SectionId, e.StudentId, e.SchoolId })
                    .HasName("ENROLLMENT_PK");

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ENROLLMENT_FK3");

                entity.HasOne(d => d.S)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => new { d.SectionId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ENROLLMENT_FK1");

                entity.HasOne(d => d.SNavigation)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => new { d.StudentId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ENROLLMENT_FK2");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => new { e.SchoolId, e.StudentId, e.SectionId, e.GradeTypeCode, e.GradeCodeOccurrence })
                    .HasName("GRADE_PK");

                entity.Property(e => e.GradeTypeCode).IsFixedLength();

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GRADE_FK1");

                entity.HasOne(d => d.GradeTypeWeight)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => new { d.SchoolId, d.SectionId, d.GradeTypeCode })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GRADE_FK3");

                entity.HasOne(d => d.S)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => new { d.SectionId, d.StudentId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GRADE_FK2");
            });

            modelBuilder.Entity<GradeConversion>(entity =>
            {
                entity.HasKey(e => new { e.SchoolId, e.LetterGrade })
                    .HasName("GRADE_CONVERSION_PK");

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.GradeConversions)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GRADE_CONVERSION_FK1");
            });

            modelBuilder.Entity<GradeType>(entity =>
            {
                entity.HasKey(e => new { e.SchoolId, e.GradeTypeCode })
                    .HasName("GRADE_TYPE_PK");

                entity.Property(e => e.GradeTypeCode).IsFixedLength();

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.GradeTypes)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GRADE_TYPE_FK1");
            });

            modelBuilder.Entity<GradeTypeWeight>(entity =>
            {
                entity.HasKey(e => new { e.SchoolId, e.SectionId, e.GradeTypeCode })
                    .HasName("GRADE_TYPE_WEIGHT_PK");

                entity.Property(e => e.GradeTypeCode).IsFixedLength();

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.GradeTypeWeights)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GRADE_TYPE_WEIGHT_FK1");

                entity.HasOne(d => d.GradeType)
                    .WithMany(p => p.GradeTypeWeights)
                    .HasForeignKey(d => new { d.SchoolId, d.GradeTypeCode })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GRADE_TYPE_WEIGHT_FK2");

                entity.HasOne(d => d.S)
                    .WithMany(p => p.GradeTypeWeights)
                    .HasForeignKey(d => new { d.SectionId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GRADE_TYPE_WEIGHT_FK3");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.HasKey(e => new { e.SchoolId, e.InstructorId })
                    .HasName("INSTRUCTOR_PK");

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("INSTRUCTOR_FK1");

                entity.HasOne(d => d.ZipNavigation)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.Zip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("INSTRUCTOR_FK2");
            });

            modelBuilder.Entity<OraTranslateMsg>(entity =>
            {
                entity.Property(e => e.OraTranslateMsgId)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("sys_guid()");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.Property(e => e.SchoolId).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => new { e.SectionId, e.SchoolId })
                    .HasName("SECTION_PK");

                entity.Property(e => e.SectionId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SECTION_FK2");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => new { d.CourseNo, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SECTION_FK1");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => new { d.SchoolId, d.InstructorId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SECTION_FK3");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.SchoolId })
                    .HasName("STUDENT_PK");

                entity.Property(e => e.StudentId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("STUDENT_FK1");
            });

            modelBuilder.Entity<Zipcode>(entity =>
            {
                entity.HasKey(e => e.Zip)
                    .HasName("ZIP_PK");

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.State).IsFixedLength();
            });

            modelBuilder.HasSequence("COURSE_SEQ");

            modelBuilder.HasSequence("INSTRUCTOR_SEQ");

            modelBuilder.HasSequence("ORA_TRANSLATE_MSG_SEQ");

            modelBuilder.HasSequence("SECTION_SEQ");

            modelBuilder.HasSequence("STUDENT_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
