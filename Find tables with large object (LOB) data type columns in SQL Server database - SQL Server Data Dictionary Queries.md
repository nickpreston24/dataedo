**Large objects** in SQL Server are columns with following data types: **varchar(max)**, **nvarchar(max)**, **text**, **ntext**, **image**, **varbinary(max)**, and **xml**.

Query below lists all **tables** that cointain columns with LOB data types.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> t.table_schema <span>as</span> schema_name,
    t.table_name, 
    <span>count</span>(*) <span>as</span> <span>columns</span>
<span>from</span> information_schema.columns c
    <span>inner</span> <span>join</span> INFORMATION_SCHEMA.tables t
        <span>on</span> c.TABLE_SCHEMA = t.TABLE_SCHEMA
        <span>and</span> c.TABLE_NAME = t.TABLE_NAME
<span>where</span> t.TABLE_TYPE = <span>'BASE TABLE'</span> 
<span>and</span> ((c.data_type <span>in</span> (<span>'VARCHAR'</span>, <span>'NVARCHAR'</span>) <span>and</span> c.character_maximum_length = <span>-1</span>)
<span>or</span> data_type <span>in</span> (<span>'TEXT'</span>, <span>'NTEXT'</span>, <span>'IMAGE'</span>, <span>'VARBINARY'</span>, <span>'XML'</span>, <span>'FILESTREAM'</span>))
<span>group</span> <span>by</span> t.table_schema, 
    t.table_name
<span>order</span> <span>by</span> t.table_schema, 
    t.table_name
```

## Columns

-   **schema\_name** - schema name
-   **table\_name** - table name
-   **columns** - number of LOB columns in a table

## Rows

-   **One row** represents one table
-   **Scope of rows:** all tables containing columns with LOB data types in current database
-   **Ordered by** schema name, table name

## Sample results

List of tables with LOB columns in AdventureWorks database:

![](https://dataedo.com/asset/img/kb/query/sql-server/lob_tables.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)