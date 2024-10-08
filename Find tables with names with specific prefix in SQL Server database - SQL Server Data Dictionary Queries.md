Query below finds tables which names start with specific prefix, e.g. tables with names starting with 'hr'.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(t.schema_id) as schema_name,
       t.name as table_name
from sys.tables t
where t.name like 'hr%'
order by table_name,
         schema_name;
```

## Columns

-   **schema\_name** - name of schema table was found in
-   **table\_name** - name of found table

## Rows

-   **One row** represents a table
-   **Scope of rows:** all found tables
-   **Ordered by** table name, schema name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/find_tables_prefix.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)