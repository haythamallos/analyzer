/******************************************************************************
**		File: DBSchema.sql
**		Desc: Schema implementation.  Creates complete schema for SQLServer db.
**
**		Auth: Haytham Allos
**		Date: 6/4/17
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		6/4/17		HA		Created
**    
*******************************************************************************/



/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		6/4/17		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'user_role')
  BEGIN
      PRINT 'Dropping Table user_role'

      DROP TABLE user_role
  END

go

CREATE TABLE user_role
  (
     user_role_id NUMERIC(10) NOT NULL PRIMARY KEY,
     date_created    DATETIME NULL,
     code            NVARCHAR(255) NULL,
     [description]     NVARCHAR(255) NULL,
     visible_code    NVARCHAR(255) NULL
  )

go

IF Object_id('user_role') IS NOT NULL
  PRINT '<<< CREATED TABLE user_role >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE user_role >>>'
go


/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		6/4/17		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'user')
  BEGIN
      PRINT 'Dropping Table user'

      DROP TABLE [user]
  END

go

CREATE TABLE [user]
  (
     user_id        NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
	 user_role_id [numeric](10, 0) NULL,
     date_created      DATETIME NULL,
     date_modified     DATETIME NULL,
     firstname        NVARCHAR(255) NULL,
     middlename        NVARCHAR(255) NULL,
     lastname        NVARCHAR(255) NULL,
     phone_number        NVARCHAR(255) NULL,
	 username        NVARCHAR(255) NULL,
	 passwd        NVARCHAR(255) NULL,
     picture_url        NVARCHAR(255) NULL,
	 picture           VARBINARY(max) NULL,
	 is_disabled                   BIT NULL,
     last_login_date  DATETIME NULL
 )

go

IF Object_id('user') IS NOT NULL
  PRINT '<<< CREATED TABLE user >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE user >>>'

go

/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/16/16		HA		Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'syslog')
  BEGIN
      PRINT 'Dropping Table syslog'

      DROP TABLE syslog
  END

go

CREATE TABLE syslog
  (
     syslog_id      NUMERIC(10) NOT NULL PRIMARY KEY IDENTITY,
     interaction_id NUMERIC(10) NULL,
     date_created   DATETIME NULL,
     date_modified  DATETIME NULL,
     msgsource      NVARCHAR(255) NULL,
     msgaction      NVARCHAR(255) NULL,
     msgtxt         TEXT NULL
  )

go

IF Object_id('syslog') IS NOT NULL
  PRINT '<<< CREATED TABLE syslog >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE syslog >>>'

go



/******************************************************************************
**		Name:  Lookup Data
**		Desc: Creates the lookup or static for the project
**
**		Auth: Haytham Allos
**		Date: 11/16/16
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/16/16		HA			Created
**    
*******************************************************************************/

/*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:		Description:
**		11/16/16		BH			Created
*******************************************************************************/
IF EXISTS (SELECT *
           FROM   sysobjects
           WHERE  type = 'U'
                  AND name = 'dbversion')
  BEGIN
      PRINT 'Dropping Table dbversion'

      DROP TABLE dbversion
  END

go

CREATE TABLE dbversion
  (
    [dbversion_id] NUMERIC(10) NOT NULL PRIMARY KEY,
	[date_created] [datetime] NULL,
	[major_num] [int] NULL DEFAULT ((0)),
	[minor_num] [int] NULL DEFAULT ((0)),
	[notes] [text] NULL
  )

go

IF Object_id('dbversion') IS NOT NULL
  PRINT '<<< CREATED TABLE dbversion >>>'
ELSE
  PRINT '<<< FAILED CREATING TABLE dbversion >>>'

go

delete from user_role
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (1, GETDATE(), 'USER_ROLE_REGULAR', 'User role regular', 'User role regular')
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (2, GETDATE(), 'USER_ROLE_AFFILIATE', 'User role affiliate', 'User role affiliate')
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (3, GETDATE(), 'USER_ROLE_AFFILIATE_ADMIN', 'User role affiliate admin', 'User role affiliate admin')
INSERT INTO user_role (user_role_id, date_created, code, [description], visible_code) VALUES (4, GETDATE(), 'USER_ROLE_SUPER_ADMIN', 'User role super admin', 'User role super admin')

delete from dbversion
INSERT INTO [dbversion] (dbversion_id, date_created, major_num, minor_num,notes) VALUES (1, GETDATE(), 1, 0,'Inital database')
GO



