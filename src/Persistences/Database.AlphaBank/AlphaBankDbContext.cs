using Data.AlphaBank;
using Microsoft.EntityFrameworkCore;

namespace Database.AlphaBank;
public partial class AlphaBankDbContext(DbContextOptions<AlphaBankDbContext> options) : DbContext(options) {

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; }

    public virtual DbSet<CollectionHistory> CollectionHistories { get; set; }

    public virtual DbSet<CollectionStatus> CollectionStatuses { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerStatus> CustomerStatuses { get; set; }

    public virtual DbSet<Deadline> Deadlines { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Interest> Interests { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<LoanApplication> LoanApplications { get; set; }

    public virtual DbSet<LoanStatement> LoanStatements { get; set; }

    public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Occupation> Occupations { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SalaryCategory> SalaryCategories { get; set; }

    public virtual DbSet<TypeAccount> TypeAccounts { get; set; }

    public virtual DbSet<TypeContract> TypeContracts { get; set; }

    public virtual DbSet<TypeCurrency> TypeCurrencies { get; set; }

    public virtual DbSet<TypeIdentification> TypeIdentifications { get; set; }

    public virtual DbSet<TypeLoan> TypeLoans { get; set; }

    public virtual DbSet<TypeNotification> TypeNotifications { get; set; }

    public virtual DbSet<TypePhone> TypePhones { get; set; }

    public virtual DbSet<User> Users { get; set; }

    //Detailed description of the structure of the database and
    //the relationships between the different entities in the application
    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<Account>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__account__3213E83F58228B44");

            entity.ToTable("account");

            entity.Property(e => e.Id)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Balance)
                .HasColumnType("money")
                .HasColumnName("balance");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DateOpening).HasColumnName("date_opening");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TypeAccountId).HasColumnName("type_account_id");
            entity.Property(e => e.TypeCurrencyId).HasColumnName("type_currency_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__account__custome__5FB337D6");

            entity.HasOne(d => d.TypeAccount).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.TypeAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__account__type_ac__60A75C0F");

            entity.HasOne(d => d.TypeCurrency).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.TypeCurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__account__type_cu__619B8048");
        });

        modelBuilder.Entity<ApplicationStatus>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__applicat__3213E83F1ADE7365");

            entity.ToTable("application_status");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<CollectionHistory>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__collecti__3213E83FF7860148");

            entity.ToTable("collection_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.CollectionStatusId).HasColumnName("collection_status_id");
            entity.Property(e => e.DateDeposit).HasColumnName("date_deposit");
            entity.Property(e => e.Deadline).HasColumnName("deadline");
            entity.Property(e => e.DepositMount)
                .HasColumnType("money")
                .HasColumnName("deposit_mount");
            entity.Property(e => e.LoanId).HasColumnName("loan_id");
            entity.Property(e => e.MoratoriumInterest)
                .HasColumnType("money")
                .HasColumnName("moratorium_interest");
            entity.Property(e => e.QuotaNumber).HasColumnName("quota_number");

            entity.HasOne(d => d.CollectionStatus).WithMany(p => p.CollectionHistories)
                .HasForeignKey(d => d.CollectionStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__collectio__colle__02084FDA");

            entity.HasOne(d => d.Loan).WithMany(p => p.CollectionHistories)
                .HasForeignKey(d => d.LoanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__collectio__loan___02FC7413");
        });

        modelBuilder.Entity<CollectionStatus>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__collecti__3213E83F532CE6D9");

            entity.ToTable("collection_status");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Contract>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__contract__3213E83FE3686120");

            entity.ToTable("contract");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCompletion).HasColumnName("date_completion");
            entity.Property(e => e.DateStart).HasColumnName("date_start");
            entity.Property(e => e.DateUpdate).HasColumnName("date_update");
            entity.Property(e => e.LoanApplicationId).HasColumnName("loan_application_id");
            entity.Property(e => e.PathFile)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("path_file");
            entity.Property(e => e.TypeContractId).HasColumnName("type_contract_id");

            entity.HasOne(d => d.LoanApplication).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.LoanApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__contract__loan_a__76969D2E");

            entity.HasOne(d => d.TypeContract).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.TypeContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__contract__type_c__778AC167");
        });

        modelBuilder.Entity<Customer>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__customer__3213E83FFD59AB79");

            entity.ToTable("customer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AverageMonthlySalary)
                .HasColumnType("money")
                .HasColumnName("average_monthly_salary");
            entity.Property(e => e.CustomerStatusId).HasColumnName("customer_status_id");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("email_address");
            entity.Property(e => e.OccupationId).HasColumnName("occupation_id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.CustomerStatus).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CustomerStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__customer__custom__59063A47");

            entity.HasOne(d => d.Occupation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.OccupationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__customer__occupa__5812160E");

            entity.HasOne(d => d.Person).WithMany(p => p.Customers)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__customer__person__571DF1D5");
        });

        modelBuilder.Entity<CustomerStatus>(entity =>  {
            entity.HasKey(e => e.Id).HasName("PK__customer__3213E83FA4BA4A84");

            entity.ToTable("customer_status");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Deadline>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__deadline__3213E83F6C23E869");

            entity.ToTable("deadline");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Employee>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__employee__3213E83F12C79C37");

            entity.ToTable("employee");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateEntry).HasColumnName("date_entry");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.SalaryCategoryId).HasColumnName("salary_category_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Person).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__employee__person__49C3F6B7");

            entity.HasOne(d => d.SalaryCategory).WithMany(p => p.Employees)
                .HasForeignKey(d => d.SalaryCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__employee__salary__4AB81AF0");
        });

        modelBuilder.Entity<Interest>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__interest__3213E83F64745547");

            entity.ToTable("interest");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Loan>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__loan__3213E83FC43C81A0");

            entity.ToTable("loan");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LoanApplicationId).HasColumnName("loan_application_id");
            entity.Property(e => e.LoanStatementId).HasColumnName("loan_statement_id");
            entity.Property(e => e.RemainingQuotas).HasColumnName("remaining_quotas");

            entity.HasOne(d => d.LoanApplication).WithMany(p => p.Loans)
                .HasForeignKey(d => d.LoanApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__loan__loan_appli__7C4F7684");

            entity.HasOne(d => d.LoanStatement).WithMany(p => p.Loans)
                .HasForeignKey(d => d.LoanStatementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__loan__loan_state__7D439ABD");
        });

        modelBuilder.Entity<LoanApplication>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__loan_app__3213E83FFDF7E5B8");

            entity.ToTable("loan_application");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("account_id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.ApplicationStatusId).HasColumnName("application_status_id");
            entity.Property(e => e.DateApplication).HasColumnName("date_application");
            entity.Property(e => e.DeadlineId).HasColumnName("deadline_id");
            entity.Property(e => e.InterestId).HasColumnName("interest_id");
            entity.Property(e => e.Justification)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("justification");
            entity.Property(e => e.PathPatronalOrder)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("path_patronal_order");
            entity.Property(e => e.PathProofSalary)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("path_proof_salary");
            entity.Property(e => e.TypeCurrencyId).HasColumnName("type_currency_id");
            entity.Property(e => e.TypeLoanId).HasColumnName("type_loan_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Account).WithMany(p => p.LoanApplications)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__loan_appl__accou__6E01572D");

            entity.HasOne(d => d.ApplicationStatus).WithMany(p => p.LoanApplications)
                .HasForeignKey(d => d.ApplicationStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__loan_appl__appli__70DDC3D8");

            entity.HasOne(d => d.Deadline).WithMany(p => p.LoanApplications)
                .HasForeignKey(d => d.DeadlineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__loan_appl__deadl__6EF57B66");

            entity.HasOne(d => d.Interest).WithMany(p => p.LoanApplications)
                .HasForeignKey(d => d.InterestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__loan_appl__inter__71D1E811");

            entity.HasOne(d => d.TypeCurrency).WithMany(p => p.LoanApplications)
                .HasForeignKey(d => d.TypeCurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__loan_appl__type___6D0D32F4");

            entity.HasOne(d => d.TypeLoan).WithMany(p => p.LoanApplications)
                .HasForeignKey(d => d.TypeLoanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__loan_appl__type___6FE99F9F");

            entity.HasOne(d => d.User).WithMany(p => p.LoanApplications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__loan_appl__user___6C190EBB");
        });

        modelBuilder.Entity<LoanStatement>(entity =>  {
            entity.HasKey(e => e.Id).HasName("PK__loan_sta__3213E83FD03C5A54");

            entity.ToTable("loan_statement");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<MaritalStatus>(entity =>  {
            entity.HasKey(e => e.Id).HasName("PK__marital___3213E83FD0C2E798");

            entity.ToTable("marital_status");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Message>(entity =>  {
            entity.HasKey(e => e.Id).HasName("PK__message__3213E83FEE529A38");

            entity.ToTable("message");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Nationality>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__national__3213E83FCD951D8A");

            entity.ToTable("nationality");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Notification>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__notifica__3213E83FDE3C688F");

            entity.ToTable("notification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateShipment).HasColumnName("date_shipment");
            entity.Property(e => e.LoanId).HasColumnName("loan_id");
            entity.Property(e => e.TypeNotificationId).HasColumnName("type_notification_id");

            entity.HasOne(d => d.Loan).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.LoanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__notificat__loan___0A9D95DB");

            entity.HasOne(d => d.TypeNotification).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.TypeNotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__notificat__type___0B91BA14");
        });

        modelBuilder.Entity<Occupation>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__occupati__3213E83F88C8A680");

            entity.ToTable("occupation");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Person>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__person__3213E83F4D732686");

            entity.ToTable("person");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.DateBirth).HasColumnName("date_birth");
            entity.Property(e => e.Deceased).HasColumnName("deceased");
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.MaritalStatusId).HasColumnName("marital_status_id");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NationalityId).HasColumnName("nationality_id");
            entity.Property(e => e.SecondName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("second_name");
            entity.Property(e => e.TypeIdentificationId).HasColumnName("type_identification_id");

            entity.HasOne(d => d.MaritalStatus).WithMany(p => p.People)
                .HasForeignKey(d => d.MaritalStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__person__marital___3F466844");

            entity.HasOne(d => d.Nationality).WithMany(p => p.People)
                .HasForeignKey(d => d.NationalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__person__national__3E52440B");

            entity.HasOne(d => d.TypeIdentification).WithMany(p => p.People)
                .HasForeignKey(d => d.TypeIdentificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__person__type_ide__3D5E1FD2");
        });

        modelBuilder.Entity<Phone>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__phone__3213E83F9BBE2173");

            entity.ToTable("phone");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.TypePhoneId).HasColumnName("type_phone_id");

            entity.HasOne(d => d.Person).WithMany(p => p.Phones)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__phone__person_id__440B1D61");

            entity.HasOne(d => d.TypePhone).WithMany(p => p.Phones)
                .HasForeignKey(d => d.TypePhoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__phone__type_phon__44FF419A");
        });

        modelBuilder.Entity<Role>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__role__3213E83F495F3977");

            entity.ToTable("role");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<SalaryCategory>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__salary_c__3213E83F0ABE5A7E");

            entity.ToTable("salary_category");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("money")
                .HasColumnName("description");
        });

        modelBuilder.Entity<TypeAccount>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__type_acc__3213E83F055B1F05");

            entity.ToTable("type_account");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<TypeContract>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__type_con__3213E83F18B25160");

            entity.ToTable("type_contract");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<TypeCurrency>(entity =>  {
            entity.HasKey(e => e.Id).HasName("PK__type_cur__3213E83F1A0C5D22");

            entity.ToTable("type_currency");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<TypeIdentification>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__type_ide__3213E83F503AFEBB");

            entity.ToTable("type_identification");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<TypeLoan>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__type_loa__3213E83F078D96A7");

            entity.ToTable("type_loan");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<TypeNotification>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__type_not__3213E83FDC0453E8");

            entity.ToTable("type_notification");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.MessageId).HasColumnName("message_id");

            entity.HasOne(d => d.Message).WithMany(p => p.TypeNotifications)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__type_noti__messa__07C12930");
        });

        modelBuilder.Entity<TypePhone>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__type_pho__3213E83FFC1D144C");

            entity.ToTable("type_phone");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<User>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83F3A72C6FD");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("email_address");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Password)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Employee).WithMany(p => p.Users)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user__employee_i__5070F446");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user__role_id__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    //Additional configurations to the database model
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}