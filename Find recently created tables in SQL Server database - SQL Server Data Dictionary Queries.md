Query below lists all tables in SQL Server database that were created within the last 30 days

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select schema_name(schema_id) as schema_name,
       name as table_name,
       create_date,
       modify_date
from sys.tables
where create_date &gt; DATEADD(DAY, -30, CURRENT_TIMESTAMP)
order by create_date desc;
```

## Columns

-   **schema\_name** - schema name
-   **table\_name** - table name
-   **create\_date** - table creation date
-   **modify\_date** - last update time of table (by ALTER statement)

## Rows

-   **One row** represents one table in a database
-   **Scope of rows:** all tables in database that were created within the last 30 days
-   **Ordered by** create datetime

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/find-recently-created-tables.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)