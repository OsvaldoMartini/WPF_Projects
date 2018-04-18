using System;

namespace Domains_DLL.Models
{

    //Communiction to/from SQL uses this class for product
    //It has a decimal, not string, definition for UnitCost
    //Consversion routines SqlProduct <--> Product provided
    public class XMLUser
    {
    public int UserId { get; set; }
        public string UserName { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }

        public DateTime StartDate { get; set; }
        public Role Role{ get; set; }
        public Department Depto { get; set; }
        public bool Leaver { get; set; }
        public DateTime? LeavingDate { get; set; }

        public XMLUser() { }

        public XMLUser(int userId, string userName, string forename,string surname, DateTime startDate,int roleId,int dptoId, bool leaver, DateTime? leavingDate)
        {
            UserId = userId;
            UserName = userName;
            Forename = forename;
            Surname = surname;
            StartDate = startDate;
            Role = Role.GetById(roleId);
            Depto = Depto.GetById(dptoId);
            Leaver = leaver;
            LeavingDate = leavingDate;
        }

        public User GetXMLUser()
        {

            return new User();
        } //SqlProduct2Product()
    }
}
