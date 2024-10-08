**Large objects** in SQL Server are columns with following data types: **varchar(max)**, **nvarchar(max)**, **text**, **ntext**, **image**, **varbinary(max)**, and **xml**.

Query below lists all columns with LOB data types.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> t.table_schema <span>as</span> schema_name,
    t.table_name, 
    c.column_name,
    c.data_type
<span>from</span> information_schema.columns c
    <span>inner</span> <span>join</span> information_schema.tables t
        <span>on</span> c.table_schema = t.table_schema
        <span>and</span> c.table_name = t.table_name
<span>where</span> t.table_type = <span>'BASE TABLE'</span> 
<span>and</span> ((c.data_type <span>in</span> (<span>'VARCHAR'</span>, <span>'NVARCHAR'</span>) <span>and</span> character_maximum_length = <span>-1</span>)
<span>or</span> c.data_type <span>in</span> (<span>'TEXT'</span>, <span>'NTEXT'</span>, <span>'IMAGE'</span>, <span>'VARBINARY'</span>, <span>'XML'</span>, <span>'FILESTREAM'</span>))
<span>order</span> <span>by</span> t.table_schema, 
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