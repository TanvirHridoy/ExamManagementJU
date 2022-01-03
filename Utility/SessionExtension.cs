using CertificationMS.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Utility
{
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static EmpMenus GetMenu( this ISession session,string Key,string menuName)
        {
            EmpMenus? menus = new EmpMenus();
            LoggedInModel loggedInModel = new LoggedInModel();
            var value = session.GetString(Key);
            if (value!=null)
            {
                loggedInModel   = JsonConvert.DeserializeObject<LoggedInModel>(value);
            }
            if (loggedInModel!=null)
            {
                menus=loggedInModel.EmpMenuList.Where(e => e.MenuName == menuName).SingleOrDefault();
            }

            return menus;
        }
        public static int GetUserId(this ISession session, string Key)
        {
            LoggedInModel loggedInModel = new LoggedInModel();
            var value = session.GetString(Key);
            if (value!=null)
            {
                loggedInModel   = JsonConvert.DeserializeObject<LoggedInModel>(value);
            }
           return loggedInModel.empInfo.UserId;
        }


    }
}
