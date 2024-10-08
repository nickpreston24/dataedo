Query below lists table check constraints.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(t.schema_id) + '.' + t.[name] as [table],
    col.column_id,
    col.[name] as column_name,
    con.[definition],
    case when con.is_disabled = 0 
        then 'Active' 
        else 'Disabled' 
        end as [status],
    con.[name] as constraint_name
from sys.check_constraints con
    left outer join sys.objects t
        on con.parent_object_id = t.object_id
    left outer join sys.all_columns col
        on con.parent_column_id = col.column_id
        and con.parent_object_id = col.object_id
order by schema_name(t.schema_id) + '.' + t.[name], 
    col.column_id
```

## Columns

-   **table** - schema and table name
-   **column\_id** - column table id (unique within table)
-   **column\_name** - name of the column for column-level check constraints, null for table-level check constraints
-   **definition** - SQL expression that defines this check constraint
-   **status** - constraint status:
    -   'Active' if constraint is active,
    -   'Disabled' for disabled constraints
-   **constraint\_name** - name of the constraint in the database

## Rows

-   **One row** represents one check constraint
-   **Scope of rows:** query returns all check constraints in the database
-   **Ordered by** table schema and name, table column id

## Sample results

List of table check constraints in **AdventureWorks** database.

![](https://dataedo.com/asset/img/kb/query/sql-server/table_check_constraints.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)