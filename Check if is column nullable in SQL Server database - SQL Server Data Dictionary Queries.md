Query below check nullability attribute for the column.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select schema_name(t.schema_id) as schema_name,
       t.name as table_name,
       c.name as column_name,
       case is_nullable
            when 0 then 'NOT NULLABLE'
            else 'NULLABLE'
            end as nullable
from sys.columns c
join sys.tables t
     on t.object_id = c.object_id
order by schema_name,
         table_name,
         column_name;
```

## Columns

-   **schema\_name** - schema name
-   **table\_name** - table name
-   **column\_name** - column name
-   **nullable** - nullability attribute for the column:
    -   **NULLABLE** - is nullable
    -   **NOT NULLABLE** - is not nullable

## Rows

-   **One row** represents one column in the database
-   **Scope of rows:** all columns in the database
-   **Ordered by** schema name, table name, column name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/column_nullable.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)