Query below returns all columns from a speficic table in SQL Server database.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select 
    col.column_id as id,
    col.name,
    t.name as data_type,
    col.max_length,
    col.precision,
    col.is_nullable
from sys.tables as tab
    inner join sys.columns as col
        on tab.object_id = col.object_id
    left join sys.types as t
    on col.user_type_id = t.user_type_id
where tab.name = 'Table name' -- enter table name here
-- and schema_name(tab.schema_id) = 'Schema name'
order by tab.name, column_id;
```

**Important:**

-   please edit condition and type in your table name
-   you can also uncomment schema condition and provide your table schema name to further filter tables (in case tables in different schemas have same name).

## Columns

-   **id** - column position in table, starting at 1
-   **name** - column name
-   **data\_type** - column data type
-   **max\_length** - data type max length
-   **precision** - data type precision
-   **is\_nullable** - flag if column is nullable:
    -   0 - not nullable (doesn't allow nulls)
    -   1 - nullable (allows nulls)

## Rows

-   **One row** represents one column in a table
-   **Scope of rows:** all columns in speficic table
-   **Ordered by** column id (position in table)

## Sample results

Columns in **Product** table in **AdventureWorks** database.

![](https://dataedo.com/asset/img/kb/query/sql-server/specific_table_columns.png)

## You could also get this

Get this interactive HTML data dictionary in minutes with [Dataedo](https://dataedo.com/).

![](https://dataedo.com/asset/img/blog/dataedo_export_productmodel.png)

[See live HTML data dictionary sample](https://dataedo.com/samples/html2/AdventureWorks/index.html#/doc/m10t160/adventureworks-database/modules/products/tables/production-productmodel)

[Try for free](http://dataedo.com/free-trial)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)