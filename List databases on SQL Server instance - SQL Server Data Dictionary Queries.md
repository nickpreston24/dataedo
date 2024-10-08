Query below lists databases on SQL Server instance.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select [name] as database_name, 
    database_id, 
    create_date
from sys.databases
order by name
```

## Columns

-   **database\_name** - database name
-   **database\_id** - databalase id, unique within an instance of SQL Server
-   **created\_date** - date the database was created or renamed

## Rows

-   **One row** represents one database
-   **Scope of rows:** all databases on SQL Server instance, including system databases
-   **Ordered by** database name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/server_databases.png)

View of databases in SSMS. Blue rectangle selects system databases.

![](https://dataedo.com/asset/img/kb/query/sql-server/server_databases_ssms.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)