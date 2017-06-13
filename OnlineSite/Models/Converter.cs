using Analyzer.Engine.DataAccessLayer.Data;

namespace OnlineSite.Models
{
    public class Cnv
    {
        public static UserModel MtoD(User d)
        {
            UserModel m = new UserModel();
            m.FirstName = d.Firstname;
            m.LastName = d.Lastname;
            m.Password = d.Passwd;
            m.Username = d.Username;
            return m;
        }
    }
}
