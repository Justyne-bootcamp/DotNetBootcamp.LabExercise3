using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.LabExercise3
{
    public class Account
    {
        public int AccountId { get; set; }
        public decimal AccountFunds { get; set; }
    }
    public class CheckBalanceService
    {
        public Account account;
        public Account Account {
            get {
                return account;
            }
        }

        public CheckBalanceService(Account account)
        {
            this.account = account;
        }
        public void CheckBalance()
        {
            Console.WriteLine("Current Available Balance: PHP {0}", Account.AccountFunds);
        }
    }
    public class WithrawCashService
    {
        public Account account;
        public Account Account
        {
            get
            {
                return account;
            }
        }

        public Validator validator;
        public Validator Validator
        {
            get
            {
                return validator;
            }
        }

        public WithrawCashService(Account account)
        {
            this.account = account;
            this.validator = new Validator();
        }
        public void WithrawCash(decimal amount)
        {
            bool isValidInput = Validator.WithrawalAmountValidator(amount, Account);
            
            if (isValidInput)
            {
                Console.WriteLine("Successfuly Withrew PHP {0}", amount);
                Account.AccountFunds -= amount;
            }
        }
    }
    public class DepositCashService
    {
        public Account account;
        public Account Account
        {
            get
            {
                return account;
            }
        }
        public Validator validator;
        public Validator Validator
        {
            get
            {
                return validator;
            }
        }
        public DepositCashService(Account account)
        {
            this.account = account;
            this.validator = new Validator();
        }
        public void DepositCash(decimal amount){

            bool isValidInput = Validator.DepositAmountValidator(amount);
            Account.AccountFunds += amount;
        }
    }
    public class Validator
    {
        public bool ServiceChoiceValidator(string input)
        {
            if (input == "1" || input == "2" || input == "3" || input == "4")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ContinueTransactionValidator(string choice)
        {
            return (choice != "y" && choice != "Y");
        }
        public bool WithrawalAmountValidator(decimal amount, Account account)
        {
            if (amount < 0)
            {
                Console.WriteLine("Invalid Withrawal Amount. Must be at least 0");
                return false;
            }
            if (amount % 100 != 0)
            {
                Console.WriteLine("Invalid Withrawal Amount. Must be multiple of 100 (e.g 100 200 300)");
                return false;
            }
            if (amount > account.AccountFunds)
            {
                Console.WriteLine("Insufficient Balance. Your Account only have PHP {0}", account.AccountFunds);
                return false;
            }

            return true;
        }
        public bool DepositAmountValidator(decimal amount)
        {
            if (amount < 0)
            {
                return false;
            }
            return true;
        }
           
    }
    public class ServicesRenderer
    {
        public void RenderServices()
        {
            Console.WriteLine("***** Welcome to ATM Service *****");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Withraw Cash");
            Console.WriteLine("3. Deposit Cash");
            Console.WriteLine("4. Quit");
            Console.WriteLine("**********************************");
        }
    }
    public class AutomatedTellerMachine
    {
        public Validator validator;
        public Validator Validator {
            get {
                    return validator; 
            }
        }
        public AutomatedTellerMachine(){
            this.validator = new Validator();
        }

        public void StartUp()
        {
            Account account = new Account();
            do
            {
                this.RenderServices();
                string choice = this.GetInput();

                switch (choice)
                {
                    case "1":
                        CheckBalance(account);
                        break;
                    case "2":
                        WithrawCash(account);
                        break;
                    case "3":
                        DepositCash(account);
                        break;
                    case "4":
                        Quit();
                        break;
                    default:
                        break;
                }
                Console.Write("Continue Transaction? y/n: ");
                string input = Console.ReadLine();

                if (Validator.ContinueTransactionValidator(input))
                {
                    Quit();
                }
            } while (true);

        }
        public void RenderServices()
        {
            ServicesRenderer servicesRenderer = new ServicesRenderer();
            servicesRenderer.RenderServices();
        }
        public void CheckBalance(Account account)
        {
            CheckBalanceService checkBalanceService = new CheckBalanceService(account);
            checkBalanceService.CheckBalance();
        }
        public void WithrawCash(Account account)
        {
            Console.WriteLine("Enter Amount to Withraw");
            decimal amount = 0;
            try
            {
               amount  = Convert.ToDecimal(Console.ReadLine());
            }catch (Exception ex)
            {
                Console.WriteLine("Invalid Withrawal Amount. Please input numerical characters");
            }
            WithrawCashService withrawCashService = new WithrawCashService(account);
            withrawCashService.WithrawCash(amount);
            CheckBalance(account);
        }
        public void DepositCash(Account account)
        {
            Console.WriteLine("Enter Amount to Deposit");
            decimal amount = 0;
            try
            {
                amount = Convert.ToDecimal(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Deposit Amount. Please input numerical characters");
            }
            DepositCashService depositCashService = new DepositCashService(account);
            depositCashService.DepositCash(amount);
            CheckBalance(account);
        }
        public void Quit()
        {
            Console.WriteLine("Please Come Again");
            Environment.Exit(0);
        }
        public string GetInput()
        {
            do
            {
                Console.WriteLine("Enter your choice");
                string choice = Console.ReadLine();

                if (validator.ServiceChoiceValidator(choice))
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Invalid Input. Please try again");
                }
            
            } while (true);
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            AutomatedTellerMachine automatedTellerMachine = new AutomatedTellerMachine();
            automatedTellerMachine.StartUp();
        }
    }
}
