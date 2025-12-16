namespace Program
{
    class Program
    {
        public static int Main()
        {
            CustomerAccount newAccount = new CustomerAccount("Oleg", 100);

            return 0;
        }
    }

    public interface IAccount
    {
        bool SetName(string name);
        bool SaveAccount(string filename);
        Account LoadAccount(string filename);
    }

    public abstract class Account : IAccount
    {
        protected string name;
        protected decimal balance;

        public Account(string inName, decimal inBalance)
        {
            name = inName;
            balance = inBalance;
        }  

        public bool SetName(string inName)
        {
            if (name != null) 
            {
                name = inName;
                return true;
            }else
            {
                return false;
            }
        }

        public abstract bool SaveAccount(string filename);
        public abstract Account LoadAccount(string filename);

   }

    public class CustomerAccount : Account
    {
        public CustomerAccount(string inName, decimal inBalance) : base(inName, inBalance)
        {
            
        } 

        public override bool SaveAccount(string filename)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(filename);
                streamWriter.WriteLine(name);
                streamWriter.WriteLine(balance);
                streamWriter.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public override Account LoadAccount(string filename)
        {
            StreamReader streamReader = null;
            Account result = null;

            try
            {
                streamReader = new StreamReader(filename);
                string accountNameText = streamReader.ReadLine();
                string accountBalanceText = streamReader.ReadLine();

                if (accountBalanceText == null || accountNameText == null)
                {
                    return null;
                }

                decimal accountBalance = decimal.Parse(accountBalanceText);
                result = new CustomerAccount(inName: accountNameText, inBalance: accountBalance);
            }
            catch
            {
                return null;
            }
            finally
            {
                if (streamReader != null) streamReader.Close();
            }
            return result;
        }
    }
}
