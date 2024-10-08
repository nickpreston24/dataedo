Query below returns list of users in current database.

## Users vs logins

-   **Login** grants access to the server - [List logins in SQL Server](https://dataedo.com/kb/query/sql-server/list-logins-on-server)
-   **User** grants a login access to the database
    
    **One login can be associated with many users but only in different databases**
    

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> <span>name</span> <span>as</span> username,
       create_date,
       modify_date,
       type_desc <span>as</span> <span>type</span>,
       authentication_type_desc <span>as</span> authentication_type
<span>from</span> sys.database_principals
<span>where</span> <span>type</span> <span>not</span> <span>in</span> (<span>'A'</span>, <span>'G'</span>, <span>'R'</span>, <span>'X'</span>)
      <span>and</span> <span>sid</span> <span>is</span> <span>not</span> <span>null</span>
      <span>and</span> <span>name</span> != <span>'guest'</span>
<span>order</span> <span>by</span> username;
```

## Columns

-   **username** - user name
-   **create\_date** - date the account was added
-   **modify\_date** - date the account was last updated
-   **type\_desc** - principal type:
    -   CERTIFICATE\_MAPPED\_USER - User mapped to a certificate
    -   EXTERNAL\_USER - External user from Azure Active Directory
    -   ASYMMETRIC\_KEY\_MAPPED\_USER - User mapped to an asymmetric key
    -   SQL\_USER - SQL user
    -   WINDOWS\_USER - Windows user
-   **authentication\_type** - type of user authentication
    -   NONE : No authentication
    -   INSTANCE : Instance authentication
    -   DATABASE : Database authentication
    -   WINDOWS : Windows Authentication

## Rows

-   **One row** represents one user in the database
-   **Scope of rows:** all users in the database
-   **Ordered by** user name

## Sample results

Those results show that there are 3 users in current database.

![sample results](https://dataedo.com/asset/img/kb/query/sql-server/list-users-in-sql-server-database.png)

## SSMS

You can vew database users using SQL Server Management studio. This option shows also roles and Windows groups.

![ssms](https://dataedo.com/asset/img/kb/query/sql-server/list-users-in-sql-server-database-ssms.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)