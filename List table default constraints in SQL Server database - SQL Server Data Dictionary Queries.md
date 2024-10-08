Query below lists table default constraints.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> schema_name(t.schema_id) + <span>'.'</span> + t.[<span>name</span>] <span>as</span> [<span>table</span>],
    col.column_id,
    col.[<span>name</span>] <span>as</span> column_name,
    con.[definition],
    con.[<span>name</span>] <span>as</span> constraint_name
<span>from</span> sys.default_constraints con
    <span>left</span> <span>outer</span> <span>join</span> sys.objects t
        <span>on</span> con.parent_object_id = t.object_id
    <span>left</span> <span>outer</span> <span>join</span> sys.all_columns <span>col</span>
        <span>on</span> con.parent_column_id = col.column_id
        <span>and</span> con.parent_object_id = col.object_id
<span>order</span> <span>by</span> schema_name(t.schema_id) + <span>'.'</span> + t.[<span>name</span>], 
    col.column_id
```

## Columns

-   **table** - schema and table name
-   **column\_id** - column table id (unique within table)
-   **column\_name** - name of the column
-   **definition** - SQL expression that defines this default constraint
-   **constraint\_name** - name of the constraint in the database

## Rows

-   **One row** represents one default constraint
-   **Scope of rows:** query returns all default constraints in the database (all columns with default constraints)
-   **Ordered by** table schema and name, table column id

## Sample results

List of table default constraints in **AdventureWorks** database.

![](https://dataedo.com/asset/img/kb/query/sql-server/table_default_constraints.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)