Query below lists all computed columns in SQL Server database

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(o.schema_id) as schema_name,
       object_name(c.object_id) as table_name,
       column_id,
       c.name as column_name,
       type_name(user_type_id) as data_type,
       definition
from sys.computed_columns c
join sys.objects o on o.object_id = c.object_id
order by schema_name,
         table_name,
         column_id;
```

## Columns

-   **schema\_name** - schema name containing table
-   **table\_name** - table name
-   **column\_id** - id of column in table
-   **column\_name** - name of the column
-   **data\_type** - data type of column
-   **definition** - computing formula

## Rows

-   **One row** represents one column
-   **Scope of rows:** represents all computed columns from all databases
-   **Ordered by** schema name, table name and column id

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/computed_columns.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)