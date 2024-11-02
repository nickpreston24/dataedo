Query below returns list of logins in current SQL Server instance.

## Users vs logins

-   **Login** grants access to the server
-   **User** grants a login access to the database [List users in SQL Server database](https://dataedo.com/kb/query/sql-server/list-users-in-database)
    
    **One login can be associated with many users but only in different databases**
    

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select sp.name as login,
       sp.type_desc as login_type,
       sl.password_hash,
       sp.create_date,
       sp.modify_date,
       case when sp.is_disabled = 1 then 'Disabled'
            else 'Enabled' end as status
from sys.server_principals sp
left join sys.sql_logins sl
          on sp.principal_id = sl.principal_id
where sp.type not in ('G', 'R')
order by sp.name;
```

## Columns

-   **login** - user name
-   **login\_type** - principal type:
    -   SQL\_LOGIN - SQL login
    -   WINDOWS\_LOGIN - Windows login
    -   CERTIFICATE\_MAPPED\_LOGIN - Login mapped to a certificate
    -   ASYMMETRIC\_KEY\_MAPPED\_LOGIN - Login mapped to an asymmetric key
-   **password\_hash** - for SQL logins hashed password with SHA-512
-   **create\_date** - date the login was added
-   **modify\_date** - date the login was last updated
-   **status** - status of the login
    -   Enabled
    -   Disabled

## Rows

-   **One row** represents one user in the database
-   **Scope of rows:** all users in the database
-   **Ordered by** user name

## Sample results

![sample results](https://dataedo.com/asset/img/kb/query/sql-server/list-logins-in-sql-server.png)

## SSMS

You can vew logins using SQL Server Management studio. Expand Server -> Security -> Logins branch in Object Explorer.

![ssms](https://dataedo.com/asset/img/kb/query/sql-server/list-logins-in-sql-server-ssms.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)