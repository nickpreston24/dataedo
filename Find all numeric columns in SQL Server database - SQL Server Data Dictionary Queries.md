Numeric in SQL Server are columns with the following data types: **tinyint**, **smallint**, **int**, **bigint**, **decimal**, **numeric**, **bit**, **float**, **real**, **smallmoney** and **money**. The query below lists all columns with numeric data types.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> schema_name(t.schema_id) + <span>'.'</span> + t.name <span>as</span> [<span>table</span>],
       c.column_id,
       c.name <span>as</span> column_name,
       type_name(user_type_id) <span>as</span> data_type,
       max_length,
       <span>precision</span>,
       scale 
<span>from</span> sys.columns c
<span>join</span> sys.tables t
     <span>on</span> t.object_id = c.object_id
<span>where</span> type_name(user_type_id) <span>in</span> (<span>'bigint'</span>, <span>'int'</span>, 
      <span>'smallint'</span>, <span>'tinyint'</span>, <span>'decimal'</span>, <span>'numeric'</span>,
      <span>'smallmoney'</span>, <span>'money'</span>, <span>'bit'</span>, <span>'float'</span>, <span>'real'</span>)
<span>order</span> <span>by</span> [<span>table</span>],
         c.column_id;
```

## Columns

-   **table** - name of the schema and name of the table
-   **column\_id** - column position in table
-   **column\_name** - name of the column
-   **data\_type** - type of data
-   **max\_length** - maximum length (in bytes)
-   **precision** - precision of the column
-   **scale** - scale of column

## Rows

-   **One row** represents one column with a numeric data type
-   **Scope of rows:** all columns containing numeric data types in the database
-   **Ordered by** schema name and table name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/numeric_columns.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)