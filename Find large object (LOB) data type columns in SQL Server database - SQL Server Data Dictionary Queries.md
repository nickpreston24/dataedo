**Large objects** in SQL Server are columns with following data types: **varchar(max)**, **nvarchar(max)**, **text**, **ntext**, **image**, **varbinary(max)**, and **xml**.

Query below lists all columns with LOB data types.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select t.table_schema as schema_name,
    t.table_name, 
    c.column_name,
    c.data_type
from information_schema.columns c
    inner join information_schema.tables t
        on c.table_schema = t.table_schema
        and c.table_name = t.table_name
where t.table_type = 'BASE TABLE' 
and ((c.data_type in ('VARCHAR', 'NVARCHAR') and character_maximum_length = -1)
or c.data_type in ('TEXT', 'NTEXT', 'IMAGE', 'VARBINARY', 'XML', 'FILESTREAM'))
order by t.table_schema, 
    t.table_name,
    c.column_name
```

## Columns

-   **schema\_name** - schema name
-   **table\_name** - table name
-   **column\_name** - column name
-   **data\_type** - data type

## Rows

-   **One row** represents one column with LOB data type
-   **Scope of rows:** all rows containing LOB data types in current database
-   **Ordered by** schema name, table name and column name

## Sample results

List of LOB columns in AdventureWorks database:

![](https://dataedo.com/asset/img/kb/query/sql-server/lob_columns.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)