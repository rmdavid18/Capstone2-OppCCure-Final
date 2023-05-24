using Capstonep2.Infrastructure.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstonep2.Infrastructure.Domain
{

    public class DefaultDBContext : DbContext
    {
        public DefaultDBContext(DbContextOptions<DefaultDBContext> options)
     : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<ConsultationRecord> ConsultationRecords { get; set; }
        public DbSet<Finding> Findings { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Purpose> Purposes { get; set; }
        public DbSet<ApptSymptom> ApptSymptoms { get; set; }
        public DbSet<ApptPurpose> ApptPurposes { get; set; }
        public DbSet<ApptFinding> ApptFindings { get; set; }
        public DbSet<ApptPrecription> ApptPrecriptions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            List<Patient> patients = new List<Patient>();
            List<ConsultationRecord> consultationRecords = new List<ConsultationRecord>();
            List<Finding> findings = new List<Finding>();
            List<User> users = new List<User>();
            List<Role> roles = new List<Role>();
            List<Appointment> appointments = new List<Appointment>();
            List<Prescription> prescriptions = new List<Prescription>();
            List<UserLogin> userLogins = new List<UserLogin>();
            List<UserRole> userRoles = new List<UserRole>();
            List<Symptom> symptoms = new List<Symptom>();
            List<Purpose> purposes = new List<Purpose>();
            List<ApptSymptom> apptSymptoms = new List<ApptSymptom>();
            List<ApptPurpose> apptPurposes = new List<ApptPurpose>();
            List<ApptPrecription> apptPrecriptions = new List<ApptPrecription>();
            List<ApptFinding> apptFindings = new List<ApptFinding>();


            //p
            prescriptions.Add(new Prescription()
            {
                ID = Guid.Parse("3d588288-29d3-4f29-a6e4-45734f748986"),
                GName = "Azelastine eye drops for allergies (Optilast)",
                
            });
            prescriptions.Add(new Prescription()
            {
                ID = Guid.Parse("71bd251b-43a1-4f19-a750-fc04e5e74876"),
                GName = "Atropine eye drops (Minims Atropine)"
               
            });
            prescriptions.Add(new Prescription()
            {
                ID = Guid.Parse("974e4796-cbb3-4bd8-b0a5-f5886cb10d45"),
                GName = "Apraclonidine eye drops (Iopidine)"
               
            });
            prescriptions.Add(new Prescription()
            {
                ID = Guid.Parse("35538073-8775-4efd-ab8a-c37479dc5109"),
                GName = "Antazoline and xylometazoline eye drops (Otrivine-Antistin)"
               
            });
            prescriptions.Add(new Prescription()
            {
                ID = Guid.Parse("cf144c28-7bca-4140-91bd-57983d308c1c"),
                GName = "Aciclovir eye ointment."
                
            });
            prescriptions.Add(new Prescription()
            {
                ID = Guid.Parse("744fa268-a8fd-4d38-a7af-239354d507b4"),
                GName = "Acetylcysteine for dry eyes (Ilube)"
                
            });

            //

            findings.Add(new Finding()
            {
                ID = Guid.Parse("efd1381a-4c3d-4260-aaf2-04a0a26591bc"),
                FName = "age-related macular degeneration"
              
            });
            findings.Add(new Finding()
            {
                ID = Guid.Parse("672a4093-269e-47aa-879c-738cf2bf5e55"),
                FName = "cataract"
                
            });
            findings.Add(new Finding()
            {
                ID = Guid.Parse("332d1fb4-35f1-48d8-ac19-f66472fce607"),
                FName = "diabetic"
                
            });
            findings.Add(new Finding()
            {
                ID = Guid.Parse("629d1da5-bf42-4dd5-9eda-614ba1260f03"),
                FName = "retinopathy"
                
            });
            findings.Add(new Finding()
            {
                ID = Guid.Parse("ab7f6ecf-7e82-4281-b90d-69f4ef72b66a"),
                FName = "glaucoma"
                

            });




            //p
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("32d18f17-4f8f-4534-9394-703261e2390b"),
                Name = "Blurred, clouded or dim vision."
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("10cbac3c-2dbf-45c9-8832-e6d2dd0b2168"),
                Name = "Problem seeing at night."
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("0bd555b4-5d90-4033-abd7-2b19dfce9f41"),
                Name = "Problem seeing through light and glare"
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("e0d9efd5-c988-4692-aafd-c0096b0093ff"),
                Name = "Seeing 'halos' around lights."
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("8f908b6a-618f-486f-9aa3-ecac26ddd539"),
                Name = "Frequently changing contact lens"
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("6e880219-f4fa-4743-bcfd-295fcd602ce3"),
                Name = "Prescription or eyeglasses"
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("b7862274-66a6-4ed1-a193-d1f014fe5ff9"),
                Name = "Faded view of colors"
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("03e03141-ccd4-4d5c-a7d5-2c739c4e8c2a"),
                Name = "Faded view of colors"
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("89fa12ed-89c5-4535-ae64-7544221ac48e"),
                Name = "Cataracts"
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("b7892990-94dd-44b8-a7cb-d142cd10e3d8"),
                Name = "Dry Eyes"
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("b9e8ceda-2511-4f71-9c57-70bf77e128a0"),
                Name = " Eye Pain"
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("cd2345d7-aba2-4228-9303-b647f6d70574"),
                Name = "excessive tearing"
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("dac8c2aa-2a57-40ef-ae4c-9316a3e27d1a"),
                Name = "burning"
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("4807111d-782b-4099-b24a-9d50cdb1093f"),
                Name = "swollen"
            });
            symptoms.Add(new Symptom()
            {
                Id = Guid.Parse("1b89eb12-b4ae-4a4e-af4a-68a8d5f9f998"),
                Name = "greasy or drooping eyelids"
            });
            //

            //Purpose
            purposes.Add(new Purpose()
            {
                Id = Guid.Parse("7f28dca4-e0f4-4798-a823-f44cdcd2a3b0"),
                Name = " CheckUp"
            });
            purposes.Add(new Purpose()
            {
                Id = Guid.Parse("70b4d9b7-e5bf-4da4-a355-a0af2da1d587"),
                Name = "FollowUp"
            });
            purposes.Add(new Purpose()
            {
                Id = Guid.Parse("912f8c3e-3ca7-4703-a858-2b9bc7612096"),
                Name = "EyeGradeCheck"
            });
            purposes.Add(new Purpose()
            {
                Id = Guid.Parse("3042ec44-a9b3-4bee-8a3d-14fd9f5167f7"),
                Name = "Brokeneyeglasses"
            });
            purposes.Add(new Purpose()
            {
                Id = Guid.Parse("9f87d3db-3842-4a4d-8552-b568c7f44620"),
                Name = "Surgery"
            });
            //



            //patientt3
            patients.Add(new Patient()
            {
                ID = Guid.Parse("2b792220-f333-49ec-abe2-3a6fc4edb0c2"),
                FirstName = "Luisa Katrina",
                MiddleName = "Pangilinan",
                LastName = "Reyes",
                Address = "Luakan,Dinalupihan, Bataan",
                BirthDate = DateTime.Parse("23/03/2023"),
                Contact = "09504160614",

                Gender = Models.Enums.Gender.Female
            });

            users.Add(new User()
            {
                ID = Guid.Parse("0352c124-f290-4f99-b1a5-e235cafcd836"),
                PatientID = Guid.Parse("2b792220-f333-49ec-abe2-3a6fc4edb0c2"),
                Email = "luisa@yahoo.com",
                FirstName = "Luisa Katrina",
                LastName = "Pangilinan",
                MiddleName = "Reyes",
                BirthDate = DateTime.Parse("23/01/2001"),
                Gender = Models.Enums.Gender.Female,
                RoleID = Guid.Parse("2afa881f-e519-4e67-a841-e4a2630e8a2a"),
                Address = "Dinalupihan, Orani, Bataan"
            });
            userLogins.AddRange(new List<UserLogin>()
            {
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("0352c124-f290-4f99-b1a5-e235cafcd836"),
                    Type = "General",
                    Key = "Password",
                    Value = BCrypt.Net.BCrypt.EnhancedHashPassword("patient")
                },
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("0352c124-f290-4f99-b1a5-e235cafcd836"),
                    Type = "General",
                    Key = "IsActive",
                    Value = "true"
                },
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("0352c124-f290-4f99-b1a5-e235cafcd836"),
                    Type = "General",
                    Key = "LoginRetries",
                    Value = "0"
                }
            });
            userRoles.Add(new UserRole()
            {
                Id = Guid.NewGuid(),
                UserID = Guid.Parse("0352c124-f290-4f99-b1a5-e235cafcd836"),
                RoleID = Guid.Parse("2afa881f-e519-4e67-a841-e4a2630e8a2a"),
            });
            //pa3
            //patient2
            patients.Add(new Patient()
            {
                ID = Guid.Parse("5a7e7bc3-8816-41df-b44d-eeb60ae99b5b"),
                FirstName = "Clarissa Joy",
                MiddleName = "Gozon",
                LastName = "Flores",
                Address = "Luakan,Dinalupihan, Bataan",
                BirthDate = DateTime.Parse("23/03/2023"),
                Contact = "09504160614",
                

                Gender = Models.Enums.Gender.Female
            });
            //patient2
            //user2
            users.Add(new User()
            {
                ID = Guid.Parse("d7dbd16f-1c71-4415-a147-22a2b428bf95"),
                PatientID = Guid.Parse("5a7e7bc3-8816-41df-b44d-eeb60ae99b5b"),
                Email = "joy@yahoo.com",
                FirstName = "Clarissa Joy",
                LastName = "Gozon",
                MiddleName = "Flores",
                BirthDate = DateTime.Parse("23/01/2001"),
                Gender = Models.Enums.Gender.Male,
                RoleID = Guid.Parse("2afa881f-e519-4e67-a841-e4a2630e8a2a"),
                Address = "Dinalupihan, Orani, Bataan"
            });
            userLogins.AddRange(new List<UserLogin>()
            {
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("d7dbd16f-1c71-4415-a147-22a2b428bf95"),
                    Type = "General",
                    Key = "Password",
                    Value = BCrypt.Net.BCrypt.EnhancedHashPassword("capstone")
                },
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("d7dbd16f-1c71-4415-a147-22a2b428bf95"),
                    Type = "General",
                    Key = "IsActive",
                    Value = "true"
                },
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("d7dbd16f-1c71-4415-a147-22a2b428bf95"),
                    Type = "General",
                    Key = "LoginRetries",
                    Value = "0"
                }
            });
            userRoles.Add(new UserRole()
            {
                Id = Guid.NewGuid(),
                UserID = Guid.Parse("d7dbd16f-1c71-4415-a147-22a2b428bf95"),
                RoleID = Guid.Parse("2afa881f-e519-4e67-a841-e4a2630e8a2a"),
            });
            // user2

            //appts2
       
          

          
           

            patients.Add(new Patient()
            {
                ID = Guid.Parse("8664a4bd-0ec6-4aaa-83e6-7d2bd0315b5a"),
                FirstName = "Raniel",
                MiddleName = "Mallari",
                LastName = "David",
                Address = "Bacong,Hermosa, Bataan",
                BirthDate = DateTime.Parse("23/03/2023"),
                Contact = "09504160614",
                Gender = Models.Enums.Gender.Male
            });


            users.Add(new User()
            {
                ID = Guid.Parse("7e5e4f74-9902-43da-9974-4b2a08814398"),
                PatientID = Guid.Parse("8664a4bd-0ec6-4aaa-83e6-7d2bd0315b5a"),
                Email = "renieldavid@yahoo.com",
                FirstName = "Reniel",
                LastName = "Mallari",
                MiddleName = "David",
                BirthDate = DateTime.Parse("23/01/2001"),
                Gender = Models.Enums.Gender.Male,
                RoleID = Guid.Parse("2afa881f-e519-4e67-a841-e4a2630e8a2a"),
                Address = "Dinalupihan, Orani, Bataan"
            });
            //adminacc
            users.Add(new User()
            {
                ID = Guid.Parse("1bd5f519-b891-4491-9a7c-a86cd0c2a15e"),
                Email = "admin@yahoo.com",
                FirstName = "admin",
                LastName = "admin",
                MiddleName = "admin",
                BirthDate = DateTime.Parse("21/02/2002"),

                Gender = Models.Enums.Gender.Female,
                RoleID = Guid.Parse("54f00f70-72b8-4d34-a953-83321ff6b101"),
                Address = "Dinalupihan, Orani , Bataan"

            });
            userLogins.AddRange(new List<UserLogin>()
            {
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("1bd5f519-b891-4491-9a7c-a86cd0c2a15e"),
                    Type = "General",
                    Key = "Password",
                    Value = BCrypt.Net.BCrypt.EnhancedHashPassword("admin")
                },
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("1bd5f519-b891-4491-9a7c-a86cd0c2a15e"),
                    Type = "General",
                    Key = "IsActive",
                    Value = "true"
                },
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("1bd5f519-b891-4491-9a7c-a86cd0c2a15e"),
                    Type = "General",
                    Key = "LoginRetries",
                    Value = "0"
                }
            });
            userRoles.Add(new UserRole()
            {
                Id = Guid.NewGuid(),
                UserID = Guid.Parse("1bd5f519-b891-4491-9a7c-a86cd0c2a15e"),
                RoleID = Guid.Parse("54f00f70-72b8-4d34-a953-83321ff6b101"),
            });


            //

            users.Add(new User()
            {
                ID = Guid.Parse("00acfb7f-6c90-459a-b53f-bf73ddac85b4"),
                Email = "Janedavid@yahoo.com",
                FirstName = "Jane",
                LastName = "David",
                MiddleName = "Adan",
                BirthDate = DateTime.Parse("21/02/2002"),

                Gender = Models.Enums.Gender.Female,
                RoleID = Guid.Parse("54f00f70-72b8-4d34-a953-83321ff6b101"),
                Address = "Dinalupihan, Orani , Bataan"

            });
            userLogins.AddRange(new List<UserLogin>()
            {
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("00acfb7f-6c90-459a-b53f-bf73ddac85b4"),
                    Type = "General",
                    Key = "Password",
                    Value = BCrypt.Net.BCrypt.EnhancedHashPassword("capstone")
                },
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("00acfb7f-6c90-459a-b53f-bf73ddac85b4"),
                    Type = "General",
                    Key = "IsActive",
                    Value = "true"
                },
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("00acfb7f-6c90-459a-b53f-bf73ddac85b4"),
                    Type = "General",
                    Key = "LoginRetries",
                    Value = "0"
                }
            });
            userRoles.Add(new UserRole()
            {
                Id = Guid.NewGuid(),
                UserID = Guid.Parse("00acfb7f-6c90-459a-b53f-bf73ddac85b4"),
                RoleID = Guid.Parse("54f00f70-72b8-4d34-a953-83321ff6b101"),
            });
            //role
            roles.Add(new Role()
            {
                ID = Guid.Parse("2afa881f-e519-4e67-a841-e4a2630e8a2a"),
                Name = "patient",
                Abbreviation = "Pt",
                Description = "One who receives medical treatment"
            });

            roles.Add(new Role()
            {
                ID = Guid.Parse("54f00f70-72b8-4d34-a953-83321ff6b101"),
                Name = "admin",
                Abbreviation = "Adm",
                Description = "One who manages the system"
            });
            //role

            userLogins.AddRange(new List<UserLogin>()
            {
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("7e5e4f74-9902-43da-9974-4b2a08814398"),
                    Type = "General",
                    Key = "Password",
                    Value = BCrypt.Net.BCrypt.EnhancedHashPassword("capstone")
                },
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("7e5e4f74-9902-43da-9974-4b2a08814398"),
                    Type = "General",
                    Key = "IsActive",
                    Value = "true"
                },
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =Guid.Parse("7e5e4f74-9902-43da-9974-4b2a08814398"),
                    Type = "General",
                    Key = "LoginRetries",
                    Value = "0"
                }
            });
            userRoles.Add(new UserRole()
            {
                Id = Guid.NewGuid(),
                UserID = Guid.Parse("7e5e4f74-9902-43da-9974-4b2a08814398"),
                RoleID = Guid.Parse("2afa881f-e519-4e67-a841-e4a2630e8a2a"),
            });




            modelBuilder.Entity<ConsultationRecord>().HasData(consultationRecords);
            modelBuilder.Entity<Finding>().HasData(findings);
            modelBuilder.Entity<Patient>().HasData(patients);
            modelBuilder.Entity<Prescription>().HasData(prescriptions);
            modelBuilder.Entity<Appointment>().HasData(appointments);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<UserLogin>().HasData(userLogins);
            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<UserRole>().HasData(userRoles);
            modelBuilder.Entity<Symptom>().HasData(symptoms);
            modelBuilder.Entity<Purpose>().HasData(purposes);
            modelBuilder.Entity<ApptSymptom>().HasData(apptSymptoms);
            modelBuilder.Entity<ApptPurpose>().HasData(apptPurposes);
            modelBuilder.Entity<ApptFinding>().HasData(apptFindings);
            modelBuilder.Entity<ApptPrecription>().HasData(apptPrecriptions);
            

        }

    }
}
