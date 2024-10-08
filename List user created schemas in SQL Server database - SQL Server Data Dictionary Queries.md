Query below lists user schemas in SQL Server database, **excluding** default **db\_\*** , **sys**, **information\_schema** and **guest** schemas.

If you want to list all schemas use [this script](https://dataedo.com/kb/query/sql-server/list-schemas-in-database).

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select s.name as schema_name, 
    s.schema_id,
    u.name as schema_owner
from sys.schemas s
    inner join sys.sysusers u
        on u.uid = s.principal_id
where u.issqluser = 1
    and u.name not in ('sys', 'guest', 'INFORMATION_SCHEMA')
```

## Columns

-   **schema\_name** - schema name
-   **schema\_id** - schema id, unique within the database
-   **schema\_owner** - principal that owns this schema

## Rows

-   **One row** represents one schema in a database
-   **Scope of rows:** all schemas in a database, excluding default db\_\* , sys, information\_schema and guest schemas
-   **Ordered by** schema name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/user_schemas_in_database.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)