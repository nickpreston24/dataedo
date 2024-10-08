Query below lists default constraints defined in the database ordered by constraint name.

[Check this query](https://dataedo.com/kb/query/sql-server/list-table-default-constraints) to see them organized by table.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> con.[<span>name</span>] <span>as</span> constraint_name,
    schema_name(t.schema_id) + <span>'.'</span> + t.[<span>name</span>]  <span>as</span> [<span>table</span>],
    col.[<span>name</span>] <span>as</span> column_name,
    con.[definition]
<span>from</span> sys.default_constraints con
    <span>left</span> <span>outer</span> <span>join</span> sys.objects t
        <span>on</span> con.parent_object_id = t.object_id
    <span>left</span> <span>outer</span> <span>join</span> sys.all_columns <span>col</span>
        <span>on</span> con.parent_column_id = col.column_id
        <span>and</span> con.parent_object_id = col.object_id
<span>order</span> <span>by</span> con.name
```

## Columns

-   **constraint\_name** - name of the constraint in the database
-   **table** - schema and table name constraint is defined for
-   **column\_name** - name of the column
-   **definition** - SQL expression that defines this default constraint

## Rows

-   **One row** represents one default constraint
-   **Scope of rows:** query returns all default constraints in the database (all columns with default constraints)
-   **Ordered by** constraint name

## Sample results

List of default constraints in **AdventureWorks** database.

![](https://dataedo.com/asset/img/kb/query/sql-server/default_constraints.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)