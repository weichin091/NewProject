using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FM3.Models;

namespace FM3.Services
{
    public class UsersServices
    {
        public List<Users> GetUsers(ACESFunc fundata)
        {
            List<Users> users = new List<Users>();

            ACESLib.ACES aces = new ACESLib.ACES();
            string licit = aces.CheckLicit(fundata.UID);
            ACESLib.UserBean userBean = aces.GetUser();

            if (userBean.WorkID != null)
            {
                Users user = new Users
                {
                    Userid = userBean.WorkID,
                    Username = userBean.PerNName,
                };
                users.Add(user);
            }
            else
            {
                Users user = new Users
                {
                    Userid = "00000",
                    Username = "無名氏",
                };
                users.Add(user);
            }

            return users;
        }
    }
}