namespace Program
{
    class Program
    {
        public static int Main()
        {
            CustomerAccount firstAccount = new CustomerAccount("Oleg", 100);
            firstAccount.SaveAccount("oleg");

            CustomerAccount secondAccount = Account.LoadAccount("oleg") as CustomerAccount;

            if (secondAccount != null) secondAccount.SetName("grisha");

            secondAccount.SaveAccount("oleg");
            Console.WriteLine(secondAccount.GetName()); 

            DictionaryBank bank = new DictionaryBank();

            bank.SaveBankAccounts("Save.txt");


            return 0;
        }
    }

    public interface IAccount
    {
        bool SetName(string name);
        string GetName();
        bool SaveAccount(string filename);
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

        public string GetName()
        {
            return name; 
        }

        public abstract bool SaveAccount(string filename);
        public static Account LoadAccount(string filename)
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

    public class CustomerAccount : Account
    {
        public CustomerAccount(string inName, decimal inBalance) : base(inName, inBalance)
        {
            
        } 

        public void Save(TextWriter textOut)
        {
            textOut.WriteLine(name);
            textOut.WriteLine(balance);
        }

        public override bool SaveAccount(string filename)
        {
            // text writer instead stram 
            TextWriter textOut = null;
            try
            {
                textOut = new StreamWriter(filename);
                Save(textOut);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (textOut != null) textOut.Close();
            }

            return true;
        }

    }

    public class DictionaryBank
    {
        Dictionary<string, IAccount> accountDictionary = new Dictionary<string, IAccount>();

        public IAccount FindAccount(string key)
        {
            if (accountDictionary.ContainsKey(key)) 
                return accountDictionary[key];
            else
            {
                return null;
            }
        }

        public bool StoreAccount(IAccount account)
        {
            if (accountDictionary.ContainsKey(account.GetName()))
                return false;
            else
            {
                accountDictionary.Add(account.GetName(), account);;
                return true;
            }
        }

        public void SaveBankAccounts(TextWriter textOut)
        {
           textOut.WriteLine(accountDictionary.Count());

           foreach (CustomerAccount accountRecord in accountDictionary.Values)
           {
               if (accountRecord != null) accountRecord.Save(textOut);                
           } 
        }
    }
}
