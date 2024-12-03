**Large objects** in SQL Server are columns with following data types: **varchar(max)**, **nvarchar(max)**, **text**, **ntext**, **image**, **varbinary(max)**, and **xml**.

Query below lists all **tables** that cointain columns with LOB data types.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select t.table_schema as schema_name,
    t.table_name, 
    count(*) as columns
from information_schema.columns c
    inner join INFORMATION_SCHEMA.tables t
        on c.TABLE_SCHEMA = t.TABLE_SCHEMA
        and c.TABLE_NAME = t.TABLE_NAME
where t.TABLE_TYPE = 'BASE TABLE' 
and ((c.data_type in ('VARCHAR', 'NVARCHAR') and c.character_maximum_length = -1)
or data_type in ('TEXT', 'NTEXT', 'IMAGE', 'VARBINARY', 'XML', 'FILESTREAM'))
group by t.table_schema, 
    t.table_name
order by t.table_schema, 
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