The query below lists all tables that was modified in the last 30 days by ALTER statement.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(schema_id) as schema_name,
       name as table_name,
       create_date,
       modify_date
from sys.tables
where modify_date &gt; DATEADD(DAY, -30, CURRENT_TIMESTAMP)
order by modify_date desc;
```

## Columns

-   **schema\_name** - schema name
-   **table\_name** - table name
-   **create\_date** - table creation date
-   **modify\_date** - last update time of table (by ALTER statement)

## Rows

-   **One row:** represents one table in a schema
-   **Scope of rows:** all tables which was last modified in the last 30 days in all schemas
-   **Ordered by:** modify time descending

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/last_update_table.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)