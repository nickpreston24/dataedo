Spatial in SQL Server are columns with the following data types: **geometry**, **geography**

The query below lists all columns with spatial data types.

### Query

```
<span>select</span> schema_name(t.schema_id) + <span>'.'</span> + t.name <span>as</span> [<span>table</span>],
       c.column_id,
       c.name <span>as</span> column_name,
       type_name(user_type_id) <span>as</span> data_type
<span>from</span> sys.columns c
<span>join</span> sys.tables t
     <span>on</span> t.object_id = c.object_id
<span>where</span> type_name(user_type_id) <span>in</span> (<span>'geometry'</span>, <span>'geography'</span>)
<span>order</span> <span>by</span> [<span>table</span>],
         c.column_id;
```

### Columns

-   **table** - name of the (schema and table
-   **column\_id** - column position in table
-   **column\_name** - name of the column
-   **data\_type** - type of spatial data:
    -   GEOMETRY
    -   GEOGRAPHY

### Rows

-   **One row** represents one column with a spatial data type
-   **Scope of rows:** all columns containing spatial data types in the database
-   **Ordered by** schema name, table name and column position in table

### Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/spatial_columns.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)