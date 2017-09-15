using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace uhack
{
    public class DBase_TLoan
    {
        //private string con = "Server=localhost;Port=3306;database=tloan_db;User Id=root;charset=utf8";
        private string con = "server=10.10.10.223;uid=ulogkiosk;password=uhack;database=tloan_db;";
        protected MySqlConnection dbCon;

        public DBase_TLoan()
        {
            dbCon = new MySqlConnection(con);
        }

        public MySqlConnection DBCon
        {
            get
            {
                return dbCon;
            }
        }

        public void dbOpen()
        {
            dbCon.Open();
        }
        public void dbClose()
        {
            dbCon.Close();
        }
    }

    class Member:DBase_TLoan
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string DateofBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string CivilStatus { get; set; }

        public Member() { }
        public bool SaveMember(string fname, string lname, string mname, string dob, string gender, string address, string contact, string cstatus)
        {
            try
            {
                if (DBCon.State == ConnectionState.Open)
                    dbClose();

                dbOpen();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon;
                cmd.CommandText = "INSERT INTO member(firstname, lastname, middlename, dateofbirth, gender, address, contactno, civilstatus) VALUES ('" + fname + "', '" +lname + "','" + mname + "','" + dob + "','" + gender + "','" + address + "','" + contact + "','" + cstatus + "')";
                cmd.ExecuteNonQuery();
                dbClose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Member GetMemberById(int id)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT * FROM member WHERE idmember=" + id;
            MySqlDataReader dbReader = cmd.ExecuteReader();
            if (dbReader.HasRows)
            {
                dbReader.Read();
                Id = Convert.ToInt32(dbReader["idmember"].ToString());
                FirstName = dbReader["firstname"].ToString();
                LastName = dbReader["lastname"].ToString();
                MiddleName = dbReader["middlename"].ToString();
                dbClose();
                return this;
            }
            else
                return null;
        }

        public Dictionary<int, string> GetAllMember()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT * FROM member";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            Dictionary<int, string> members = new Dictionary<int, string>();
            while(dbReader.Read())
            {               
                members.Add(Convert.ToInt32(dbReader["idmember"].ToString()),dbReader["lastname"].ToString() + ", " + dbReader["firstname"].ToString() + " " + dbReader["middlename"].ToString().Substring(0, 1) + ".");
            }
            dbClose();
            return members;
         }
    }

    class LoanApplication : DBase_TLoan
    {
        public int Id { get; set; }
        public Member MemberInfo { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public string Colateral { get; set; }
        public string Comaker { get; set; }
        public string ComakerAddress { get; set; }
        public DateTime DateApproved { get; set; }

        public LoanApplication() { }

        public LoanApplication GetLoan(int id)
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT * FROM loan WHERE idmember=" + id + " AND balance>0";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            if (dbReader.HasRows)
            {
                dbReader.Read();
                Id = Convert.ToInt32(dbReader["idloan"].ToString());
                MemberInfo = new Member().GetMemberById(id);
                Amount = Convert.ToDouble(dbReader["amount"].ToString());
                dbClose();
                return this;
            }
            else
                return null;
        }

        public bool SaveLoanApplication(int idmember, double amount, string colateral, string comaker, string comakeraddress, string comakercontactno)
        {
            try
            {
                if (GetLoan(idmember) != null) throw new Exception("Loan still active");
                if (DBCon.State == ConnectionState.Open)
                    dbClose();

                dbOpen();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon;
                cmd.CommandText = "INSERT INTO loan(idmember, amount, balance, collateral, comakername, comakeraddress, comakercontactno) VALUES ('" + idmember + "', '" + (amount*1.2) + "','" + (amount*1.2) + "','" + colateral + "','" + comaker + "','" + comakeraddress  + "', '" + comakercontactno + "')";
                cmd.ExecuteNonQuery();
                dbClose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<LoanApplication> GetAllLoan()
        {
            if (DBCon.State == ConnectionState.Open)
                dbClose();

            dbOpen();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DBCon;
            cmd.CommandText = "SELECT * FROM loan WHERE balance>0";
            MySqlDataReader dbReader = cmd.ExecuteReader();
            List<LoanApplication> loans = new List<LoanApplication>();
            while (dbReader.Read())
            {
                LoanApplication l = new LoanApplication();
                l.Id = Convert.ToInt32(dbReader["idloan"].ToString());
                l.MemberInfo = new Member().GetMemberById(Convert.ToInt32(dbReader["idmember"].ToString()));
                l.Amount = Convert.ToDouble(dbReader["amount"].ToString());
                l.DateApproved = Convert.ToDateTime(dbReader["dateofloan"].ToString());

                loans.Add(l);
            }
            dbClose();
            return loans;
        }
    }

    class Payment:DBase_TLoan
    {
        public LoanApplication LoanInfo { get; set; }
        public Member MemberInfo { get; set; }
        public double Amount { get; set; }

        public Payment() { }

        public bool SavePayment(int idmember, double amount)
        {
            try
            {
                LoanApplication loan = new LoanApplication().GetLoan(idmember);
                if (loan == null || loan.Balance < amount) throw new Exception("No loan to pay.");
                if (DBCon.State == ConnectionState.Open)
                    dbClose();

                dbOpen();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DBCon;
                cmd.CommandText = "INSERT INTO payment(idloan, idmember, amountpaid) VALUES(" + loan.Id + ", " + idmember + ", " + amount + ")";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "UPDATE loan SET balance=balance - " + amount;
                cmd.ExecuteNonQuery();
                dbClose();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
