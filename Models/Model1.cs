namespace E_Wallet.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class Model1 : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'E_Wallet.Models.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public Model1()
            : base("name=Model1")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<Scheme> Schemes { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
    }

    public class Bank
    {
        [Key]
        [Required]
        [Display(Name ="Account_Number")]
        public String Account_Number { get; set; }
        [Required]
        [Display(Name = "Phone_Number")]
        public string Phone_Number { get; set; }
        [Required]
        [Display(Name = "Balance")]
        public int Balance { get; set; }
    }

    public class Wallet
    {
        [Key]
        [Required]
        [Display(Name = "Email")]
        public String Email { get; set; }

        [Display(Name = "Account_Number")]
        public string Account_Number { get; set; }

        [Required]
        [Display(Name = "Balance")]
        public int Balance { get; set; }

        
    }

    public class Transaction
    {
        [Key]
        [Required]
        [Display(Name = "Email")]
        public String Email { get; set; }

        [Required]
        [Display(Name = "Transaction_Amount")]
        public int Transaction_Amount { get; set; }

        [Required]
        [Display(Name = "TO_Email")]
        public String TO_Email { get; set; }

        [Display(Name = "Scheme_ID")]
        public int SID { get; set; }       
    }

    public class Scheme
    {
        [Key]
        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Minimum_Transaction")]
        public int Minimum_Transaction { get; set; }

        [Required]
        [Display(Name = "Refund_Amount")]
        public int Refund { get; set; }
    }
}