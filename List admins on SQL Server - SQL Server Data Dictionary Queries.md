Query below returns list of logins in current SQL Server instance with admin privileges.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select mp.name as login,
       case when mp.is_disabled = 1 then 'Disabled'
            else 'Enabled'
            end as status,
      mp.type_desc as type
from sys.server_role_members srp 
join sys.server_principals mp 
     on mp.principal_id = srp.member_principal_id
join sys.server_principals rp 
     on rp.principal_id = srp.role_principal_id
where rp.name = 'sysadmin'
order by mp.name;
```

## Columns

-   **login** - login
-   **status** - status of the login
    -   Enabled
    -   Disabled
-   **type** - principal type

## Rows

-   **One row** represents one user (sysadmin) in the database
-   **Scope of rows:** all admins in the database
-   **Ordered by** login

## Sample results

![sample results](https://dataedo.com/asset/img/kb/query/sql-server/list-admins-in-sql-server.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)