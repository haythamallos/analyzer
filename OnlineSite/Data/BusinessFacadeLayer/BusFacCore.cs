using System;
using Analyzer.Engine.BusinessAccessLayer;
using Analyzer.Engine.Common;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;
using Analyzer.Engine.DataAccessLayer.Data;
using Analyzer.Engine.DataAccessLayer.Enumeration;
using System.Collections;

namespace Analyzer.Engine.BusinessFacadeLayer
{
    public class BusFacCore
    {
        private bool _hasError = false;
        private string _errorMessage = null;
        private string _errorStacktrace = null;

        private Config _config = null;

        public bool HasError
        {
            get { return _hasError; }
        }
        public string ErrorStacktrace
        {
            get { return _errorStacktrace; }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
        }
        public BusFacCore(IOptions<ConfigSettings> settings)
        {
            _config = new Config(settings);
        }

        public SqlConnection getDBConnection()
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(_config.ConnectionString);
                conn.Open();
            }
            catch (Exception ex)
            {
                ErrorCode error = new ErrorCode();
                _hasError = true;
            }

            return conn;
        }
        public bool CloseConnection(SqlConnection pRefConn)
        {
            bool bReturn = true;
            try
            {
                if (pRefConn != null)
                {
                    if (pRefConn.State == ConnectionState.Open)
                    {
                        pRefConn.Close();
                    }
                }
                bReturn = true;
            }
            catch (Exception e)
            {
                _hasError = true;
                _errorStacktrace = e.StackTrace.ToString();
                _errorMessage = e.Message;
                bReturn = false;
            }
            return bReturn;
        }

        /*********************** CUSTOM BEGIN *********************/
        public bool Exist(string username)
        {
            bool bExist = true;
            ArrayList arUser = Find(username);
            if ((!HasError) && (arUser.Count == 0))
            {
                bExist = false;
            }
            return bExist;
        }
        public User UserGet(string username)
        {
            User user = null;
            ArrayList arUser = Find(username);
            if ((!HasError) && (arUser.Count == 1))
            {
                user = (User) arUser[0];
            }
            return user;
        }
        public ArrayList Find(string username)
        {
            ArrayList arUser = null;
            try
            {
                if ((!string.IsNullOrEmpty(username)))
                {
                    EnumUser enumUser = new EnumUser() { Username = username };
                    arUser = UserGetList(enumUser);
                }
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }
            return arUser;
        }

        public User UserCreate(string username, string password, long user_source_id = 0)
        {
            User user = null;
            try
            {
                string passwordEncrypted = UtilsSecurity.encrypt(password);
                User userTmp = new User() { Username = username, Passwd = passwordEncrypted };
                long lID = UserCreateOrModify(userTmp);
                if (lID > 0)
                {
                    user = UserGet(lID);
                }
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return user;
        }

        public User UserAuthenticate(string username, string password)
        {
            User user = null;
            try
            {
                ArrayList arUsers = Find(username);
                if ((arUsers != null) && (arUsers.Count == 1))
                {
                    User userTmp = (User)arUsers[0];
                    string passwordEncrypted = UtilsSecurity.encrypt(password);
                    if (userTmp.Passwd == passwordEncrypted)
                    {
                        user = userTmp;
                    }
                }
            }
            catch (Exception ex)
            {
                _hasError = true;
                _errorMessage = ex.Message;
                _errorStacktrace = ex.StackTrace;
            }

            return user;
        }

        /*********************** CUSTOM END *********************/

        //------------------------------------------
        /// <summary>
        /// DbversionCreateOrModify
        /// </summary>
        /// <param name="">pDbversion</param>
        /// <returns>long</returns>
        /// 
        public long DbversionCreateOrModify(Dbversion pDbversion)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusDbversion busDbversion = null;
                busDbversion = new BusDbversion(conn);
                busDbversion.Save(pDbversion);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pDbversion.DbversionID;
                _hasError = busDbversion.HasError;
                if (busDbversion.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// DbversionGetList
        /// </summary>
        /// <param name="">pEnumDbversion</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList DbversionGetList(EnumDbversion pEnumDbversion)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusDbversion busDbversion = null;
                busDbversion = new BusDbversion(conn);
                items = busDbversion.Get(pEnumDbversion);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busDbversion.HasError;
                if (busDbversion.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// DbversionGet
        /// </summary>
        /// <param name="">pLngDbversionID</param>
        /// <returns>Dbversion</returns>
        /// 
        public Dbversion DbversionGet(long pLngDbversionID)
        {
            Dbversion dbversion = new Dbversion() { DbversionID = pLngDbversionID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusDbversion busDbversion = null;
                busDbversion = new BusDbversion(conn);
                busDbversion.Load(dbversion);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busDbversion.HasError;
                if (busDbversion.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return dbversion;
        }

        /// <summary>
        /// DbversionRemove
        /// </summary>
        /// <param name="">pDbversionID</param>
        /// <returns>void</returns>
        /// 
        public void DbversionRemove(long pDbversionID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Dbversion dbversion = new Dbversion();
                dbversion.DbversionID = pDbversionID;
                BusDbversion bus = null;
                bus = new BusDbversion(conn);
                bus.Delete(dbversion);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }
        //------------------------------------------
        /// <summary>
        /// SyslogCreateOrModify
        /// </summary>
        /// <param name="">pSyslog</param>
        /// <returns>long</returns>
        /// 
        public long SyslogCreateOrModify(Syslog pSyslog)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusSyslog busSyslog = null;
                busSyslog = new BusSyslog(conn);
                busSyslog.Save(pSyslog);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pSyslog.SyslogID;
                _hasError = busSyslog.HasError;
                if (busSyslog.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// SyslogGetList
        /// </summary>
        /// <param name="">pEnumSyslog</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList SyslogGetList(EnumSyslog pEnumSyslog)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusSyslog busSyslog = null;
                busSyslog = new BusSyslog(conn);
                items = busSyslog.Get(pEnumSyslog);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busSyslog.HasError;
                if (busSyslog.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// SyslogGet
        /// </summary>
        /// <param name="">pLngSyslogID</param>
        /// <returns>Syslog</returns>
        /// 
        public Syslog SyslogGet(long pLngSyslogID)
        {
            Syslog syslog = new Syslog() { SyslogID = pLngSyslogID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusSyslog busSyslog = null;
                busSyslog = new BusSyslog(conn);
                busSyslog.Load(syslog);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busSyslog.HasError;
                if (busSyslog.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return syslog;
        }

        /// <summary>
        /// SyslogRemove
        /// </summary>
        /// <param name="">pSyslogID</param>
        /// <returns>void</returns>
        /// 
        public void SyslogRemove(long pSyslogID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                Syslog syslog = new Syslog();
                syslog.SyslogID = pSyslogID;
                BusSyslog bus = null;
                bus = new BusSyslog(conn);
                bus.Delete(syslog);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }
        //------------------------------------------
        /// <summary>
        /// UserCreateOrModify
        /// </summary>
        /// <param name="">pUser</param>
        /// <returns>long</returns>
        /// 
        public long UserCreateOrModify(User pUser)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUser busUser = null;
                busUser = new BusUser(conn);
                busUser.Save(pUser);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pUser.UserID;
                _hasError = busUser.HasError;
                if (busUser.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// UserGetList
        /// </summary>
        /// <param name="">pEnumUser</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList UserGetList(EnumUser pEnumUser)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUser busUser = null;
                busUser = new BusUser(conn);
                items = busUser.Get(pEnumUser);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUser.HasError;
                if (busUser.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// UserGet
        /// </summary>
        /// <param name="">pLngUserID</param>
        /// <returns>User</returns>
        /// 
        public User UserGet(long pLngUserID)
        {
            User user = new User() { UserID = pLngUserID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUser busUser = null;
                busUser = new BusUser(conn);
                busUser.Load(user);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUser.HasError;
                if (busUser.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return user;
        }

        /// <summary>
        /// UserRemove
        /// </summary>
        /// <param name="">pUserID</param>
        /// <returns>void</returns>
        /// 
        public void UserRemove(long pUserID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                User user = new User();
                user.UserID = pUserID;
                BusUser bus = null;
                bus = new BusUser(conn);
                bus.Delete(user);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }
        //------------------------------------------
        /// <summary>
        /// UserRoleCreateOrModify
        /// </summary>
        /// <param name="">pUserRole</param>
        /// <returns>long</returns>
        /// 
        public long UserRoleCreateOrModify(UserRole pUserRole)
        {
            long lID = 0;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUserRole busUserRole = null;
                busUserRole = new BusUserRole(conn);
                busUserRole.Save(pUserRole);
                // close the db connection
                bConn = CloseConnection(conn);
                lID = pUserRole.UserRoleID;
                _hasError = busUserRole.HasError;
                if (busUserRole.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return lID;
        }

        /// <summary>
        /// UserRoleGetList
        /// </summary>
        /// <param name="">pEnumUserRole</param>
        /// <returns>ArrayList</returns>
        /// 
        public ArrayList UserRoleGetList(EnumUserRole pEnumUserRole)
        {
            ArrayList items = null;
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUserRole busUserRole = null;
                busUserRole = new BusUserRole(conn);
                items = busUserRole.Get(pEnumUserRole);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUserRole.HasError;
                if (busUserRole.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return items;
        }

        /// <summary>
        /// UserRoleGet
        /// </summary>
        /// <param name="">pLngUserRoleID</param>
        /// <returns>UserRole</returns>
        /// 
        public UserRole UserRoleGet(long pLngUserRoleID)
        {
            UserRole user_role = new UserRole() { UserRoleID = pLngUserRoleID };
            bool bConn = false;
            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                BusUserRole busUserRole = null;
                busUserRole = new BusUserRole(conn);
                busUserRole.Load(user_role);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = busUserRole.HasError;
                if (busUserRole.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
            return user_role;
        }

        /// <summary>
        /// UserRoleRemove
        /// </summary>
        /// <param name="">pUserRoleID</param>
        /// <returns>void</returns>
        /// 
        public void UserRoleRemove(long pUserRoleID)
        {
            bool bConn = false;

            SqlConnection conn = getDBConnection();
            if (conn != null)
            {
                UserRole user_role = new UserRole();
                user_role.UserRoleID = pUserRoleID;
                BusUserRole bus = null;
                bus = new BusUserRole(conn);
                bus.Delete(user_role);
                // close the db connection
                bConn = CloseConnection(conn);
                _hasError = bus.HasError;
                if (bus.HasError)
                {
                    // error
                    ErrorCode error = new ErrorCode();
                }
            }
        }





    }



}
