Query below lists tables and their primary key (**PK**) constraint names. By browsing list you can spot which tables have and which don't have primary keys.

See also: [tables without primary keys](https://dataedo.com/kb/query/sql-server/find-tables-without-primary-keys).

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> schema_name(tab.schema_id) <span>as</span> [schema_name], 
    tab.[<span>name</span>] <span>as</span> table_name, 
    pk.[<span>name</span>] <span>as</span> pk_name,
    <span>substring</span>(column_names, <span>1</span>, <span>len</span>(column_names)<span>-1</span>) <span>as</span> [<span>columns</span>]
<span>from</span> sys.tables tab
    <span>left</span> <span>outer</span> <span>join</span> sys.indexes pk
        <span>on</span> tab.object_id = pk.object_id 
        <span>and</span> pk.is_primary_key = <span>1</span>
   <span>cross</span> <span>apply</span> (<span>select</span> col.[<span>name</span>] + <span>', '</span>
                    <span>from</span> sys.index_columns ic
                        <span>inner</span> <span>join</span> sys.columns <span>col</span>
                            <span>on</span> ic.object_id = col.object_id
                            <span>and</span> ic.column_id = col.column_id
                    <span>where</span> ic.object_id = tab.object_id
                        <span>and</span> ic.index_id = pk.index_id
                            <span>order</span> <span>by</span> col.column_id
                            <span>for</span> <span>xml</span> <span>path</span> (<span>''</span>) ) D (column_names)
<span>order</span> <span>by</span> schema_name(tab.schema_id),
    tab.[<span>name</span>]
```

## Columns

-   **schema\_name** - schema name
-   **table\_name** - table name
-   **pk\_name** - primary key constraint name
-   **columns** - list of PK columns separated with ','

## Rows

-   **One row** represents one table in a database
-   **Scope of rows:** all tables in a database
-   **Ordered by** schema, table name

## Sample results

You can see what are the names of PK constraints for each table and which tables don't have PKs at all (in Microsoft System Center Configuration Manager database).

![](https://dataedo.com/asset/img/kb/query/sql-server/tables_with_primary_keys.png)

## You could also get this

Get this interactive HTML data dictionary in minutes with [Dataedo](https://dataedo.com/).

![](https://dataedo.com/asset/img/blog/dataedo_export_productmodel.png)

[See live HTML data dictionary sample](https://dataedo.com/samples/html2/AdventureWorks/index.html#/doc/m10t160/adventureworks-database/modules/products/tables/production-productmodel)

[Try for free](http://dataedo.com/free-trial?cta=QuerySQLTablePKs)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)