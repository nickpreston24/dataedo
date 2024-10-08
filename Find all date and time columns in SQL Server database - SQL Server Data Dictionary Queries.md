Date and time in SQL Server are represented by following data types: **date**, **time**, **datetime**, **datetime2**, **smalldatetime**, **datetimeoffset**, . The query below lists all columns with date/time data types.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> schema_name(t.schema_id) + <span>'.'</span> + t.name <span>as</span> [<span>table</span>],
       c.column_id,
       c.name <span>as</span> column_name,
       type_name(user_type_id) <span>as</span> data_type,
       scale <span>as</span> second_scale
<span>from</span> sys.columns c
<span>join</span> sys.tables t
     <span>on</span> t.object_id = c.object_id
<span>where</span> type_name(user_type_id) <span>in</span> (<span>'date'</span>, <span>'datetimeoffset'</span>, 
      <span>'datetime2'</span>, <span>'smalldatetime'</span>, <span>'datetime'</span>, <span>'time'</span>)
<span>order</span> <span>by</span> [<span>table</span>],
         c.column_id;
```

## Columns

-   **database\_name** - name of the schema
-   **table\_name** - name of the table
-   **column\_id** - column position in table
-   **column\_name** - name of the column
-   **data\_type** - type of data
-   **second\_scale** - number of digits for the fractional part of the seconds

## Rows

-   **One row** represents one column with a date/time data type
-   **Scope of rows:** all columns containing date/time data types in the database
-   **Ordered by** schema name, table name and column position in table

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/dateandtime_columns.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)