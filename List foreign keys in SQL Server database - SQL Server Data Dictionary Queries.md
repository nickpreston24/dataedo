Query below returns foreign key constrants defined in a database.

Check out this [summary article of FK queries for SQL Server](https://dataedo.com/kb/query/sql-server/list-foreign-keys-sql-queries).

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name <span>as</span> foreign_table,
    <span>'&gt;-'</span> <span>as</span> rel,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name <span>as</span> primary_table,
    <span>substring</span>(column_names, <span>1</span>, <span>len</span>(column_names)<span>-1</span>) <span>as</span> [fk_columns],
    fk.name <span>as</span> fk_constraint_name
<span>from</span> sys.foreign_keys fk
    <span>inner</span> <span>join</span> sys.tables fk_tab
        <span>on</span> fk_tab.object_id = fk.parent_object_id
    <span>inner</span> <span>join</span> sys.tables pk_tab
        <span>on</span> pk_tab.object_id = fk.referenced_object_id
    <span>cross</span> <span>apply</span> (<span>select</span> col.[<span>name</span>] + <span>', '</span>
                    <span>from</span> sys.foreign_key_columns fk_c
                        <span>inner</span> <span>join</span> sys.columns <span>col</span>
                            <span>on</span> fk_c.parent_object_id = col.object_id
                            <span>and</span> fk_c.parent_column_id = col.column_id
                    <span>where</span> fk_c.parent_object_id = fk_tab.object_id
                      <span>and</span> fk_c.constraint_object_id = fk.object_id
                            <span>order</span> <span>by</span> col.column_id
                            <span>for</span> <span>xml</span> <span>path</span> (<span>''</span>) ) D (column_names)
<span>order</span> <span>by</span> schema_name(fk_tab.schema_id) + <span>'.'</span> + fk_tab.name,
    schema_name(pk_tab.schema_id) + <span>'.'</span> + pk_tab.name
```

## Columns

-   **foreign\_table** - foreign table name with schema name
-   **rel** - relationship symbol implicating direction
-   **primary\_table** - primary (rerefenced) table name with schema name
-   **fk\_columns** - list of FK colum names, separated with ","
-   **fk\_constraint\_name** - foreign key constraint name

## Rows

-   **One row** represents one foreign key. If foreign key consists of multiple columns (composite key) it is still represented as one row.
-   **Scope of rows:** all foregin keys in a database
-   **Ordered by** foreign table schema name and table name

## Sample results

Foreign keys in AdventureWorks database:

![](https://dataedo.com/asset/img/kb/query/sql-server/foreign_keys.png)

## You could also get this

Get this interactive HTML data dictionary in minutes with [Dataedo](https://dataedo.com/).

![](https://dataedo.com/asset/img/blog/dataedo_export_productmodel.png)

[See live HTML data dictionary sample](https://dataedo.com/samples/html2/AdventureWorks/index.html#/doc/m10t160/adventureworks-database/modules/products/tables/production-productmodel)

[Try for free](http://dataedo.com/free-trial)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)