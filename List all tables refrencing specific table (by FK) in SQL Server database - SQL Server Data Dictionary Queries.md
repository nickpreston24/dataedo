Query below lists all tables that refrence specific table with foregin keys.

Check out this [summary article of FK queries for SQL Server](https://dataedo.com/kb/query/sql-server/list-foreign-keys-sql-queries).

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> <span>distinct</span> 
    schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name <span>as</span> foreign_table,
    <span>'&gt;-'</span> <span>as</span> rel,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name <span>as</span> primary_table
<span>from</span> sys.foreign_keys fk
    <span>inner</span> <span>join</span> sys.tables fk_tab
        <span>on</span> fk_tab.object_id = fk.parent_object_id
    <span>inner</span> <span>join</span> sys.tables pk_tab
        <span>on</span> pk_tab.object_id = fk.referenced_object_id
<span>where</span> pk_tab.[<span>name</span>] = <span>'Your table'</span> <span>-- enter table name here</span>
<span>--  and schema_name(pk_tab.schema_id) = 'Your table schema name'</span>
<span>order</span> <span>by</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name
```

## Columns

-   **foreign\_table** - foreign tables schemas and names - the table you are looking for
-   **foreign\_table** - relationship symbol implicating FK and direction
-   **primary\_table** - primary (rerefenced) tables names with schema name - the table you provided as a parameter

## Rows

-   **One row** represents one referencing table
-   **Scope of rows:** all tables referencing table with provided name (and optionally schema)
-   **Ordered by** referencing table schema and name

## Notes

-   There can be more tables with the same name. If that's the case, uncomment where clause and provide schema name

## Sample results

All tables referencing with FK **Production.Product** table in **AdventureWorks** database.

![](https://dataedo.com/asset/img/kb/query/sql-server/referencing_tables.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)