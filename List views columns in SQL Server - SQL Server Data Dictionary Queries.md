Query below lists all columns in views in SQL Server database

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(v.schema_id) as schema_name,
       object_name(c.object_id) as view_name,
       c.column_id,
       c.name as column_name,
       type_name(user_type_id) as data_type,
       c.max_length,
       c.precision
from sys.columns c
join sys.views v 
     on v.object_id = c.object_id
order by schema_name,
         view_name,
         column_id;
```

## Columns

-   **schema\_name** - view owner, schema name
-   **view\_name** - view name
-   **column\_id** - column number in view
-   **column\_name** - column name
-   **data\_type** - column datatype
-   **max\_length** - data type max length
-   **precision** - data type precision

## Rows

-   **One row** represents one column in a specific view in a database
-   **Scope of rows:** all columns in views in SQL Server database
-   **Ordered by** schema name, view name and column position

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/list-views-columns.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)