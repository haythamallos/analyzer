using System;
using System.Xml;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Analyzer.Engine.Common;

namespace Analyzer.Engine.DataAccessLayer.Data
{
	/// <summary>
	/// Copyright (c) 2017 Haytham Allos.  San Diego, California, USA
	/// All Rights Reserved
	/// 
	/// File:  User.cs
	/// History
	/// ----------------------------------------------------
	/// 001	HA	6/5/2017	Created
	/// 
	/// ----------------------------------------------------
	/// Abstracts the User database table.
	/// </summary>
	public class User
	{
		//Attributes
		/// <summary>UserID Attribute type String</summary>
		private long _lUserID = 0;
		/// <summary>UserRoleID Attribute type String</summary>
		private long _lUserRoleID = 0;
		/// <summary>DateCreated Attribute type String</summary>
		private DateTime _dtDateCreated = dtNull;
		/// <summary>DateModified Attribute type String</summary>
		private DateTime _dtDateModified = dtNull;
		/// <summary>Firstname Attribute type String</summary>
		private string _strFirstname = null;
		/// <summary>Middlename Attribute type String</summary>
		private string _strMiddlename = null;
		/// <summary>Lastname Attribute type String</summary>
		private string _strLastname = null;
		/// <summary>Username Attribute type String</summary>
		private string _strUsername = null;
		/// <summary>Passwd Attribute type String</summary>
		private string _strPasswd = null;
		/// <summary>PictureUrl Attribute type String</summary>
		private string _strPictureUrl = null;
		/// <summary>Picture Attribute type String</summary>
		private byte[] _bytePicture = null;
		/// <summary>IsDisabled Attribute type String</summary>
		private bool? _bIsDisabled = null;
		/// <summary>LastLoginDate Attribute type String</summary>
		private DateTime _dtLastLoginDate = dtNull;

		private ErrorCode _errorCode = null;
		private bool _hasError = false;
		private static DateTime dtNull = new DateTime();

		/// <summary>HasError Property in class User and is of type bool</summary>
		public static readonly string ENTITY_NAME = "User"; //Table name to abstract

		// DB Field names
		/// <summary>ID Database field</summary>
		public static readonly string DB_FIELD_ID = "user_id"; //Table id field name
		/// <summary>user_role_id Database field </summary>
		public static readonly string DB_FIELD_USER_ROLE_ID = "user_role_id"; //Table UserRoleID field name
		/// <summary>date_created Database field </summary>
		public static readonly string DB_FIELD_DATE_CREATED = "date_created"; //Table DateCreated field name
		/// <summary>date_modified Database field </summary>
		public static readonly string DB_FIELD_DATE_MODIFIED = "date_modified"; //Table DateModified field name
		/// <summary>firstname Database field </summary>
		public static readonly string DB_FIELD_FIRSTNAME = "firstname"; //Table Firstname field name
		/// <summary>middlename Database field </summary>
		public static readonly string DB_FIELD_MIDDLENAME = "middlename"; //Table Middlename field name
		/// <summary>lastname Database field </summary>
		public static readonly string DB_FIELD_LASTNAME = "lastname"; //Table Lastname field name
		/// <summary>username Database field </summary>
		public static readonly string DB_FIELD_USERNAME = "username"; //Table Username field name
		/// <summary>passwd Database field </summary>
		public static readonly string DB_FIELD_PASSWD = "passwd"; //Table Passwd field name
		/// <summary>picture_url Database field </summary>
		public static readonly string DB_FIELD_PICTURE_URL = "picture_url"; //Table PictureUrl field name
		/// <summary>picture Database field </summary>
		public static readonly string DB_FIELD_PICTURE = "picture"; //Table Picture field name
		/// <summary>is_disabled Database field </summary>
		public static readonly string DB_FIELD_IS_DISABLED = "is_disabled"; //Table IsDisabled field name
		/// <summary>last_login_date Database field </summary>
		public static readonly string DB_FIELD_LAST_LOGIN_DATE = "last_login_date"; //Table LastLoginDate field name

		// Attribute variables
		/// <summary>TAG_ID Attribute type string</summary>
		public static readonly string TAG_ID = "UserID"; //Attribute id  name
		/// <summary>UserRoleID Attribute type string</summary>
		public static readonly string TAG_USER_ROLE_ID = "UserRoleID"; //Table UserRoleID field name
		/// <summary>DateCreated Attribute type string</summary>
		public static readonly string TAG_DATE_CREATED = "DateCreated"; //Table DateCreated field name
		/// <summary>DateModified Attribute type string</summary>
		public static readonly string TAG_DATE_MODIFIED = "DateModified"; //Table DateModified field name
		/// <summary>Firstname Attribute type string</summary>
		public static readonly string TAG_FIRSTNAME = "Firstname"; //Table Firstname field name
		/// <summary>Middlename Attribute type string</summary>
		public static readonly string TAG_MIDDLENAME = "Middlename"; //Table Middlename field name
		/// <summary>Lastname Attribute type string</summary>
		public static readonly string TAG_LASTNAME = "Lastname"; //Table Lastname field name
		/// <summary>Username Attribute type string</summary>
		public static readonly string TAG_USERNAME = "Username"; //Table Username field name
		/// <summary>Passwd Attribute type string</summary>
		public static readonly string TAG_PASSWD = "Passwd"; //Table Passwd field name
		/// <summary>PictureUrl Attribute type string</summary>
		public static readonly string TAG_PICTURE_URL = "PictureUrl"; //Table PictureUrl field name
		/// <summary>Picture Attribute type string</summary>
		public static readonly string TAG_PICTURE = "Picture"; //Table Picture field name
		/// <summary>IsDisabled Attribute type string</summary>
		public static readonly string TAG_IS_DISABLED = "IsDisabled"; //Table IsDisabled field name
		/// <summary>LastLoginDate Attribute type string</summary>
		public static readonly string TAG_LAST_LOGIN_DATE = "LastLoginDate"; //Table LastLoginDate field name

		// Stored procedure names
		private static readonly string SP_INSERT_NAME = "spUserInsert"; //Insert sp name
		private static readonly string SP_UPDATE_NAME = "spUserUpdate"; //Update sp name
		private static readonly string SP_DELETE_NAME = "spUserDelete"; //Delete sp name
		private static readonly string SP_LOAD_NAME = "spUserLoad"; //Load sp name
		private static readonly string SP_EXIST_NAME = "spUserExist"; //Exist sp name

		//properties
		/// <summary>UserID is a Property in the User Class of type long</summary>
		public long UserID 
		{
			get{return _lUserID;}
			set{_lUserID = value;}
		}
		/// <summary>UserRoleID is a Property in the User Class of type long</summary>
		public long UserRoleID 
		{
			get{return _lUserRoleID;}
			set{_lUserRoleID = value;}
		}
		/// <summary>DateCreated is a Property in the User Class of type DateTime</summary>
		public DateTime DateCreated 
		{
			get{return _dtDateCreated;}
			set{_dtDateCreated = value;}
		}
		/// <summary>DateModified is a Property in the User Class of type DateTime</summary>
		public DateTime DateModified 
		{
			get{return _dtDateModified;}
			set{_dtDateModified = value;}
		}
		/// <summary>Firstname is a Property in the User Class of type String</summary>
		public string Firstname 
		{
			get{return _strFirstname;}
			set{_strFirstname = value;}
		}
		/// <summary>Middlename is a Property in the User Class of type String</summary>
		public string Middlename 
		{
			get{return _strMiddlename;}
			set{_strMiddlename = value;}
		}
		/// <summary>Lastname is a Property in the User Class of type String</summary>
		public string Lastname 
		{
			get{return _strLastname;}
			set{_strLastname = value;}
		}
		/// <summary>Username is a Property in the User Class of type String</summary>
		public string Username 
		{
			get{return _strUsername;}
			set{_strUsername = value;}
		}
		/// <summary>Passwd is a Property in the User Class of type String</summary>
		public string Passwd 
		{
			get{return _strPasswd;}
			set{_strPasswd = value;}
		}
		/// <summary>PictureUrl is a Property in the User Class of type String</summary>
		public string PictureUrl 
		{
			get{return _strPictureUrl;}
			set{_strPictureUrl = value;}
		}
		/// <summary>Picture is a Property in the User Class of type byte[]</summary>
		public byte[] Picture 
		{
			get{return _bytePicture;}
			set{_bytePicture = value;}
		}
		/// <summary>IsDisabled is a Property in the User Class of type bool</summary>
		public bool? IsDisabled 
		{
			get{return _bIsDisabled;}
			set{_bIsDisabled = value;}
		}
		/// <summary>LastLoginDate is a Property in the User Class of type DateTime</summary>
		public DateTime LastLoginDate 
		{
			get{return _dtLastLoginDate;}
			set{_dtLastLoginDate = value;}
		}


/*********************** CUSTOM NON-META BEGIN *********************/

/*********************** CUSTOM NON-META END *********************/


		/// <summary>HasError Property in class User and is of type bool</summary>
		public  bool HasError 
		{
			get{return _hasError;}
		}
		/// <summary>Error Property in class User and is of type ErrorCode</summary>
		public ErrorCode Error 
		{
			get{return _errorCode;}
		}

//Constructors
		/// <summary>User empty constructor</summary>
		public User()
		{
		}
		/// <summary>User constructor takes UserID and a SqlConnection</summary>
		public User(long l, SqlConnection conn) 
		{
			UserID = l;
			try
			{
				sqlLoad(conn);
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}

		}
		/// <summary>User Constructor takes pStrData and Config</summary>
		public User(string pStrData)
		{
			Parse(pStrData);
		}
		/// <summary>User Constructor takes SqlDataReader</summary>
		public User(SqlDataReader rd)
		{
			sqlParseResultSet(rd);
		}
		/// <summary>
		///     Dispose of this object's resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(true); // as a service to those who might inherit from us
		}
		/// <summary>
		///		Free the instance variables of this object.
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (! disposing)
				return; // we're being collected, so let the GC take care of this object
		}

		// public methods
		/// <summary>ToString is overridden to display all properties of the User Class</summary>
		public override string ToString() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append(TAG_ID + ":  " + UserID.ToString() + "\n");
			sbReturn.Append(TAG_USER_ROLE_ID + ":  " + UserRoleID + "\n");
			if (!dtNull.Equals(DateCreated))
			{
				sbReturn.Append(TAG_DATE_CREATED + ":  " + DateCreated.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_DATE_CREATED + ":\n");
			}
			if (!dtNull.Equals(DateModified))
			{
				sbReturn.Append(TAG_DATE_MODIFIED + ":  " + DateModified.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_DATE_MODIFIED + ":\n");
			}
			sbReturn.Append(TAG_FIRSTNAME + ":  " + Firstname + "\n");
			sbReturn.Append(TAG_MIDDLENAME + ":  " + Middlename + "\n");
			sbReturn.Append(TAG_LASTNAME + ":  " + Lastname + "\n");
			sbReturn.Append(TAG_USERNAME + ":  " + Username + "\n");
			sbReturn.Append(TAG_PASSWD + ":  " + Passwd + "\n");
			sbReturn.Append(TAG_PICTURE_URL + ":  " + PictureUrl + "\n");
			sbReturn.Append(TAG_PICTURE + ":  " + Picture + "\n");
			sbReturn.Append(TAG_IS_DISABLED + ":  " + IsDisabled + "\n");
			if (!dtNull.Equals(LastLoginDate))
			{
				sbReturn.Append(TAG_LAST_LOGIN_DATE + ":  " + LastLoginDate.ToString() + "\n");
			}
			else
			{
				sbReturn.Append(TAG_LAST_LOGIN_DATE + ":\n");
			}

			return sbReturn.ToString();
		}
		/// <summary>Creates well formatted XML - includes all properties of User</summary>
		public string ToXml() 
		{
			StringBuilder sbReturn = null;

			sbReturn = new StringBuilder();	
			sbReturn.Append("<User>\n");
			sbReturn.Append("<" + TAG_ID + ">" + UserID + "</" + TAG_ID + ">\n");
			sbReturn.Append("<" + TAG_USER_ROLE_ID + ">" + UserRoleID + "</" + TAG_USER_ROLE_ID + ">\n");
			if (!dtNull.Equals(DateCreated))
			{
				sbReturn.Append("<" + TAG_DATE_CREATED + ">" + DateCreated.ToString() + "</" + TAG_DATE_CREATED + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_DATE_CREATED + "></" + TAG_DATE_CREATED + ">\n");
			}
			if (!dtNull.Equals(DateModified))
			{
				sbReturn.Append("<" + TAG_DATE_MODIFIED + ">" + DateModified.ToString() + "</" + TAG_DATE_MODIFIED + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_DATE_MODIFIED + "></" + TAG_DATE_MODIFIED + ">\n");
			}
			sbReturn.Append("<" + TAG_FIRSTNAME + ">" + Firstname + "</" + TAG_FIRSTNAME + ">\n");
			sbReturn.Append("<" + TAG_MIDDLENAME + ">" + Middlename + "</" + TAG_MIDDLENAME + ">\n");
			sbReturn.Append("<" + TAG_LASTNAME + ">" + Lastname + "</" + TAG_LASTNAME + ">\n");
			sbReturn.Append("<" + TAG_USERNAME + ">" + Username + "</" + TAG_USERNAME + ">\n");
			sbReturn.Append("<" + TAG_PASSWD + ">" + Passwd + "</" + TAG_PASSWD + ">\n");
			sbReturn.Append("<" + TAG_PICTURE_URL + ">" + PictureUrl + "</" + TAG_PICTURE_URL + ">\n");
			sbReturn.Append("<" + TAG_PICTURE + ">" + Picture + "</" + TAG_PICTURE + ">\n");
			sbReturn.Append("<" + TAG_IS_DISABLED + ">" + IsDisabled + "</" + TAG_IS_DISABLED + ">\n");
			if (!dtNull.Equals(LastLoginDate))
			{
				sbReturn.Append("<" + TAG_LAST_LOGIN_DATE + ">" + LastLoginDate.ToString() + "</" + TAG_LAST_LOGIN_DATE + ">\n");
			}
			else
			{
				sbReturn.Append("<" + TAG_LAST_LOGIN_DATE + "></" + TAG_LAST_LOGIN_DATE + ">\n");
			}
			sbReturn.Append("</User>" + "\n");

			return sbReturn.ToString();
		}
		/// <summary>Parse accepts a string in XML format and parses values</summary>
		public void Parse(string pStrXml)
		{
			try
			{
				XmlDocument xmlDoc = null;
				string strXPath = null;
				XmlNodeList xNodes = null;

				xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(pStrXml);

				// get the element
				strXPath = "//" + ENTITY_NAME;
				xNodes = xmlDoc.SelectNodes(strXPath);
				foreach (XmlNode xNode in xNodes)
				{
					Parse(xNode);
				}
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}
		}		
		/// <summary>Parse accepts an XmlNode and parses values</summary>
		public void Parse(XmlNode xNode)
		{
			XmlNode xResultNode = null;
			string strTmp = null;

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_ID);
				strTmp = xResultNode.InnerText;
				UserID = (long) Convert.ToInt32(strTmp);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_USER_ROLE_ID);
				UserRoleID = (long) Convert.ToInt32(xResultNode.InnerText);
			}
			catch  
			{
			UserRoleID = 0;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_DATE_CREATED);
				DateCreated = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_DATE_MODIFIED);
				DateModified = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_FIRSTNAME);
				Firstname = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_MIDDLENAME);
				Middlename = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_LASTNAME);
				Lastname = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_USERNAME);
				Username = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_PASSWD);
				Passwd = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_PICTURE_URL);
				PictureUrl = xResultNode.InnerText;
			}
			catch  
			{
				xResultNode = null;
			}
            //Cannot reliably convert byte[] to string.

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_IS_DISABLED);
				IsDisabled = Convert.ToBoolean(xResultNode.InnerText);
			}
			catch  
			{
			IsDisabled = false;
			}

			try
			{
				xResultNode = xNode.SelectSingleNode(TAG_LAST_LOGIN_DATE);
				LastLoginDate = DateTime.Parse(xResultNode.InnerText);
			}
			catch  
			{
			}
		}
		/// <summary>Calls sqlLoad() method which gets record from database with user_id equal to the current object's UserID </summary>
		public void Load(SqlConnection conn)
		{
			try
			{
				sqlLoad(conn);
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}

		}
		/// <summary>Calls sqlUpdate() method which record record from database with current object values where user_id equal to the current object's UserID </summary>
		public void Update(SqlConnection conn)
		{
			bool bExist = false;
			try
			{
				bExist = Exist(conn);
				if (bExist)
				{
					sqlUpdate(conn);
				}
				else
				{
				}
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}
		}
		/// <summary>Calls sqlInsert() method which inserts a record into the database with current object values</summary>
		public void Save(SqlConnection conn)
		{
			try
			{
				bool bExist = false;

				bExist = Exist(conn);
				if (!bExist)
				{
					sqlInsert(conn);
				}
				else
				{
					sqlUpdate(conn);
				}
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}

		}
		/// <summary>Calls sqlDelete() method which delete's the record from database where where user_id equal to the current object's UserID </summary>
		public void Delete(SqlConnection conn)
		{
			try
			{
				sqlDelete(conn);
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}
		}
		/// <summary>Calls sqlExists() returns true if the record exists, false if not </summary>
		public bool Exist(SqlConnection conn)
		{
			bool bReturn = false;
			try
			{
				bReturn = sqlExist(conn);
			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}

			return bReturn;
		}
		/// <summary>Prompt user to enter Property values</summary>
		public void Prompt()
		{
			try 
			{

				Console.WriteLine(User.TAG_USER_ROLE_ID + ":  ");
				UserRoleID = (long)Convert.ToInt32(Console.ReadLine());
				try
				{
					Console.WriteLine(User.TAG_DATE_CREATED + ":  ");
					DateCreated = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateCreated = new DateTime();
				}
				try
				{
					Console.WriteLine(User.TAG_DATE_MODIFIED + ":  ");
					DateModified = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					DateModified = new DateTime();
				}

				Console.WriteLine(User.TAG_FIRSTNAME + ":  ");
				Firstname = Console.ReadLine();

				Console.WriteLine(User.TAG_MIDDLENAME + ":  ");
				Middlename = Console.ReadLine();

				Console.WriteLine(User.TAG_LASTNAME + ":  ");
				Lastname = Console.ReadLine();

				Console.WriteLine(User.TAG_USERNAME + ":  ");
				Username = Console.ReadLine();

				Console.WriteLine(User.TAG_PASSWD + ":  ");
				Passwd = Console.ReadLine();

				Console.WriteLine(User.TAG_PICTURE_URL + ":  ");
				PictureUrl = Console.ReadLine();
             //Cannot reliably convert byte[] to string.

				Console.WriteLine(User.TAG_IS_DISABLED + ":  ");
				IsDisabled = Convert.ToBoolean(Console.ReadLine());
				try
				{
					Console.WriteLine(User.TAG_LAST_LOGIN_DATE + ":  ");
					LastLoginDate = DateTime.Parse(Console.ReadLine());
				}
				catch 
				{
					LastLoginDate = new DateTime();
				}

			}
			catch (Exception e) 
			{
				_hasError = true;
				_errorCode = new ErrorCode();
			}
		}
		
		//protected
		/// <summary>Inserts row of data into the database</summary>
		protected void sqlInsert(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramUserRoleID = null;
			SqlParameter paramDateCreated = null;
			SqlParameter paramFirstname = null;
			SqlParameter paramMiddlename = null;
			SqlParameter paramLastname = null;
			SqlParameter paramUsername = null;
			SqlParameter paramPasswd = null;
			SqlParameter paramPictureUrl = null;
			SqlParameter paramPicture = null;
			SqlParameter paramIsDisabled = null;
			SqlParameter paramLastLoginDate = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_INSERT_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters

			paramUserRoleID = new SqlParameter("@" + TAG_USER_ROLE_ID, UserRoleID);
			paramUserRoleID.DbType = DbType.Int32;
			paramUserRoleID.Direction = ParameterDirection.Input;

				paramDateCreated = new SqlParameter("@" + TAG_DATE_CREATED, DateTime.UtcNow);
			paramDateCreated.DbType = DbType.DateTime;
			paramDateCreated.Direction = ParameterDirection.Input;


			paramFirstname = new SqlParameter("@" + TAG_FIRSTNAME, Firstname);
			paramFirstname.DbType = DbType.String;
			paramFirstname.Size = 255;
			paramFirstname.Direction = ParameterDirection.Input;

			paramMiddlename = new SqlParameter("@" + TAG_MIDDLENAME, Middlename);
			paramMiddlename.DbType = DbType.String;
			paramMiddlename.Size = 255;
			paramMiddlename.Direction = ParameterDirection.Input;

			paramLastname = new SqlParameter("@" + TAG_LASTNAME, Lastname);
			paramLastname.DbType = DbType.String;
			paramLastname.Size = 255;
			paramLastname.Direction = ParameterDirection.Input;

			paramUsername = new SqlParameter("@" + TAG_USERNAME, Username);
			paramUsername.DbType = DbType.String;
			paramUsername.Size = 255;
			paramUsername.Direction = ParameterDirection.Input;

			paramPasswd = new SqlParameter("@" + TAG_PASSWD, Passwd);
			paramPasswd.DbType = DbType.String;
			paramPasswd.Size = 255;
			paramPasswd.Direction = ParameterDirection.Input;

			paramPictureUrl = new SqlParameter("@" + TAG_PICTURE_URL, PictureUrl);
			paramPictureUrl.DbType = DbType.String;
			paramPictureUrl.Size = 255;
			paramPictureUrl.Direction = ParameterDirection.Input;

			paramPicture = new SqlParameter("@" + TAG_PICTURE, Picture);
			paramPicture.DbType = DbType.Binary;
			paramPicture.Size = 2147483647;
			paramPicture.Direction = ParameterDirection.Input;

			paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
			paramIsDisabled.DbType = DbType.Boolean;
			paramIsDisabled.Direction = ParameterDirection.Input;

			if (!dtNull.Equals(LastLoginDate))
			{
				paramLastLoginDate = new SqlParameter("@" + TAG_LAST_LOGIN_DATE, LastLoginDate);
			}
			else
			{
				paramLastLoginDate = new SqlParameter("@" + TAG_LAST_LOGIN_DATE, DBNull.Value);
			}
			paramLastLoginDate.DbType = DbType.DateTime;
			paramLastLoginDate.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramUserRoleID);
			cmd.Parameters.Add(paramDateCreated);
			cmd.Parameters.Add(paramFirstname);
			cmd.Parameters.Add(paramMiddlename);
			cmd.Parameters.Add(paramLastname);
			cmd.Parameters.Add(paramUsername);
			cmd.Parameters.Add(paramPasswd);
			cmd.Parameters.Add(paramPictureUrl);
			cmd.Parameters.Add(paramPicture);
			cmd.Parameters.Add(paramIsDisabled);
			cmd.Parameters.Add(paramLastLoginDate);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			// assign the primary kiey
			string strTmp;
			strTmp = cmd.Parameters["@PKID"].Value.ToString();
			UserID = long.Parse(strTmp);

			// cleanup to help GC
			paramUserRoleID = null;
			paramDateCreated = null;
			paramFirstname = null;
			paramMiddlename = null;
			paramLastname = null;
			paramUsername = null;
			paramPasswd = null;
			paramPictureUrl = null;
			paramPicture = null;
			paramIsDisabled = null;
			paramLastLoginDate = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Check to see if the row exists in database</summary>
		protected bool sqlExist(SqlConnection conn)
		{
			bool bExist = false;

			SqlCommand cmd = null;
			SqlParameter paramUserID = null;
			SqlParameter paramCount = null;

			cmd = new SqlCommand(SP_EXIST_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;

			paramUserID = new SqlParameter("@" + TAG_ID, UserID);
			paramUserID.Direction = ParameterDirection.Input;
			paramUserID.DbType = DbType.Int32;

			paramCount = new SqlParameter();
			paramCount.ParameterName = "@COUNT";
			paramCount.DbType = DbType.Int32;
			paramCount.Direction = ParameterDirection.Output;

			cmd.Parameters.Add(paramUserID);
			cmd.Parameters.Add(paramCount);
			cmd.ExecuteNonQuery();

			string strTmp;
			int nCount = 0;
			strTmp = cmd.Parameters["@COUNT"].Value.ToString();
			nCount = int.Parse(strTmp);
			if (nCount > 0)
			{
				bExist = true;
			}

			// cleanup
			paramUserID = null;
			paramCount = null;
			cmd = null;

			return bExist;
		}
		/// <summary>Updates row of data in database</summary>
		protected void sqlUpdate(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramUserID = null;
			SqlParameter paramUserRoleID = null;
			SqlParameter paramDateModified = null;
			SqlParameter paramFirstname = null;
			SqlParameter paramMiddlename = null;
			SqlParameter paramLastname = null;
			SqlParameter paramUsername = null;
			SqlParameter paramPasswd = null;
			SqlParameter paramPictureUrl = null;
			SqlParameter paramPicture = null;
			SqlParameter paramIsDisabled = null;
			SqlParameter paramLastLoginDate = null;
			SqlParameter paramPKID = null;

			//Create a command object identifying
			//the stored procedure	
			cmd = new SqlCommand(SP_UPDATE_NAME, conn);

			//Set the command object so it knows
			//to execute a stored procedure
			cmd.CommandType = CommandType.StoredProcedure;
			
			// parameters

			paramUserID = new SqlParameter("@" + TAG_ID, UserID);
			paramUserID.DbType = DbType.Int32;
			paramUserID.Direction = ParameterDirection.Input;


			paramUserRoleID = new SqlParameter("@" + TAG_USER_ROLE_ID, UserRoleID);
			paramUserRoleID.DbType = DbType.Int32;
			paramUserRoleID.Direction = ParameterDirection.Input;


				paramDateModified = new SqlParameter("@" + TAG_DATE_MODIFIED, DateTime.UtcNow);
			paramDateModified.DbType = DbType.DateTime;
			paramDateModified.Direction = ParameterDirection.Input;

			paramFirstname = new SqlParameter("@" + TAG_FIRSTNAME, Firstname);
			paramFirstname.DbType = DbType.String;
			paramFirstname.Size = 255;
			paramFirstname.Direction = ParameterDirection.Input;

			paramMiddlename = new SqlParameter("@" + TAG_MIDDLENAME, Middlename);
			paramMiddlename.DbType = DbType.String;
			paramMiddlename.Size = 255;
			paramMiddlename.Direction = ParameterDirection.Input;

			paramLastname = new SqlParameter("@" + TAG_LASTNAME, Lastname);
			paramLastname.DbType = DbType.String;
			paramLastname.Size = 255;
			paramLastname.Direction = ParameterDirection.Input;

			paramUsername = new SqlParameter("@" + TAG_USERNAME, Username);
			paramUsername.DbType = DbType.String;
			paramUsername.Size = 255;
			paramUsername.Direction = ParameterDirection.Input;

			paramPasswd = new SqlParameter("@" + TAG_PASSWD, Passwd);
			paramPasswd.DbType = DbType.String;
			paramPasswd.Size = 255;
			paramPasswd.Direction = ParameterDirection.Input;

			paramPictureUrl = new SqlParameter("@" + TAG_PICTURE_URL, PictureUrl);
			paramPictureUrl.DbType = DbType.String;
			paramPictureUrl.Size = 255;
			paramPictureUrl.Direction = ParameterDirection.Input;

			paramPicture = new SqlParameter("@" + TAG_PICTURE, Picture);
			paramPicture.DbType = DbType.Binary;
			paramPicture.Size = 2147483647;
			paramPicture.Direction = ParameterDirection.Input;

			paramIsDisabled = new SqlParameter("@" + TAG_IS_DISABLED, IsDisabled);
			paramIsDisabled.DbType = DbType.Boolean;
			paramIsDisabled.Direction = ParameterDirection.Input;

			if (!dtNull.Equals(LastLoginDate))
			{
				paramLastLoginDate = new SqlParameter("@" + TAG_LAST_LOGIN_DATE, LastLoginDate);
			}
			else
			{
				paramLastLoginDate = new SqlParameter("@" + TAG_LAST_LOGIN_DATE, DBNull.Value);
			}
			paramLastLoginDate.DbType = DbType.DateTime;
			paramLastLoginDate.Direction = ParameterDirection.Input;

			paramPKID = new SqlParameter();
			paramPKID.ParameterName = "@PKID";
			paramPKID.DbType = DbType.Int32;
			paramPKID.Direction = ParameterDirection.Output;

			//Add parameters to command, which
			//will be passed to the stored procedure
			cmd.Parameters.Add(paramUserID);
			cmd.Parameters.Add(paramUserRoleID);
			cmd.Parameters.Add(paramDateModified);
			cmd.Parameters.Add(paramFirstname);
			cmd.Parameters.Add(paramMiddlename);
			cmd.Parameters.Add(paramLastname);
			cmd.Parameters.Add(paramUsername);
			cmd.Parameters.Add(paramPasswd);
			cmd.Parameters.Add(paramPictureUrl);
			cmd.Parameters.Add(paramPicture);
			cmd.Parameters.Add(paramIsDisabled);
			cmd.Parameters.Add(paramLastLoginDate);
			cmd.Parameters.Add(paramPKID);

			// execute the command
			cmd.ExecuteNonQuery();
			string s;
			s = cmd.Parameters["@PKID"].Value.ToString();
			UserID = long.Parse(s);

			// cleanup
			paramUserID = null;
			paramUserRoleID = null;
			paramDateModified = null;
			paramFirstname = null;
			paramMiddlename = null;
			paramLastname = null;
			paramUsername = null;
			paramPasswd = null;
			paramPictureUrl = null;
			paramPicture = null;
			paramIsDisabled = null;
			paramLastLoginDate = null;
			paramPKID = null;
			cmd = null;
		}
		/// <summary>Deletes row of data in database</summary>
		protected void sqlDelete(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramUserID = null;

			cmd = new SqlCommand(SP_DELETE_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramUserID = new SqlParameter("@" + TAG_ID, UserID);
			paramUserID.DbType = DbType.Int32;
			paramUserID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramUserID);
			cmd.ExecuteNonQuery();

			// cleanup to help GC
			paramUserID = null;
			cmd = null;

		}
		/// <summary>Load row of data from database</summary>
		protected void sqlLoad(SqlConnection conn)
		{
			SqlCommand cmd = null;
			SqlParameter paramUserID = null;
			SqlDataReader rdr = null;

			cmd = new SqlCommand(SP_LOAD_NAME, conn);
			cmd.CommandType = CommandType.StoredProcedure;
			paramUserID = new SqlParameter("@" + TAG_ID, UserID);
			paramUserID.DbType = DbType.Int32;
			paramUserID.Direction = ParameterDirection.Input;
			cmd.Parameters.Add(paramUserID);
			rdr = cmd.ExecuteReader();
			if (rdr.Read())
			{
				sqlParseResultSet(rdr);
			}
			// cleanup
			rdr.Dispose();
			rdr = null;
			paramUserID = null;
			cmd = null;
		}
		/// <summary>Parse result set</summary>
		protected void sqlParseResultSet(SqlDataReader rdr)
		{
			this.UserID = long.Parse(rdr[DB_FIELD_ID].ToString());
			try
			{
			this.UserRoleID = Convert.ToInt32(rdr[DB_FIELD_USER_ROLE_ID].ToString().Trim());
			}
			catch{}
         try
			{
				this.DateCreated = DateTime.Parse(rdr[DB_FIELD_DATE_CREATED].ToString());
			}
			catch 
			{
			}
         try
			{
				this.DateModified = DateTime.Parse(rdr[DB_FIELD_DATE_MODIFIED].ToString());
			}
			catch 
			{
			}
			try
			{
			this.Firstname = rdr[DB_FIELD_FIRSTNAME].ToString().Trim();
			}
			catch{}
			try
			{
			this.Middlename = rdr[DB_FIELD_MIDDLENAME].ToString().Trim();
			}
			catch{}
			try
			{
			this.Lastname = rdr[DB_FIELD_LASTNAME].ToString().Trim();
			}
			catch{}
			try
			{
			this.Username = rdr[DB_FIELD_USERNAME].ToString().Trim();
			}
			catch{}
			try
			{
			this.Passwd = rdr[DB_FIELD_PASSWD].ToString().Trim();
			}
			catch{}
			try
			{
			this.PictureUrl = rdr[DB_FIELD_PICTURE_URL].ToString().Trim();
			}
			catch{}
			try
			{
         if(rdr[rdr.GetOrdinal(DB_FIELD_PICTURE)] != DBNull.Value)
         {
			       this.Picture = (byte[])rdr[rdr.GetOrdinal(DB_FIELD_PICTURE)];
         }
			}
			catch{}
			try
			{
			this.IsDisabled = Convert.ToBoolean(rdr[DB_FIELD_IS_DISABLED].ToString().Trim());
			}
			catch{}
         try
			{
				this.LastLoginDate = DateTime.Parse(rdr[DB_FIELD_LAST_LOGIN_DATE].ToString());
			}
			catch 
			{
			}
		}

	}
}

//END OF User CLASS FILE
