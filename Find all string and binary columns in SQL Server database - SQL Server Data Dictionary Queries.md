In SQL Server string and binary columns are with the following data types: **text**, **ntext**, **varchar**, **nvarchar**, **char**, **nchar**, **binary**, **varbinary**, **image**.

The query below lists all columns with string and binary data types.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> schema_name(t.schema_id) + <span>'.'</span> + t.name <span>as</span> [<span>table</span>],
       c.column_id,
       c.name <span>as</span> column_name,
       type_name(user_type_id) <span>as</span> data_type,
       max_length 
<span>from</span> sys.columns c
<span>join</span> sys.tables t
     <span>on</span> t.object_id = c.object_id
<span>where</span> type_name(user_type_id) <span>in</span> (<span>'text'</span>, <span>'ntext'</span>,
      <span>'varchar'</span>, <span>'nvarchar'</span>, <span>'char'</span>, <span>'nchar'</span>,
      <span>'binary'</span>, <span>'varbinary'</span>, <span>'image'</span>)
<span>order</span> <span>by</span> [<span>table</span>],
         c.column_id;
```

## Columns

-   **table** - name of the schema and table name
-   **column\_id** - column position in table
-   **column\_name** - name of the column
-   **data\_type** - type of data
-   **max\_length** - maximum length in bytes

## Rows

-   **One row** represents one column with a string or binary data type
-   **Scope of rows:** all columns containing string and binary data types in the database (schema)
-   **Ordered by** schema name, table name and position in table

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/string_columns.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)