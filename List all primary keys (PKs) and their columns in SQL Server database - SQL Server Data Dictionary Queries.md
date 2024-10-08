Query below lists all primary keys constraints (**PK**) in the database with their **columns** (one row per column).

See also: [list of all primary keys (one row per PK)](https://dataedo.com/kb/query/sql-server/list-all-primary-keys-in-database).

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> schema_name(tab.schema_id) <span>as</span> [schema_name], 
    pk.[<span>name</span>] <span>as</span> pk_name,
    ic.index_column_id <span>as</span> column_id,
    col.[<span>name</span>] <span>as</span> column_name, 
    tab.[<span>name</span>] <span>as</span> table_name
<span>from</span> sys.tables tab
    <span>inner</span> <span>join</span> sys.indexes pk
        <span>on</span> tab.object_id = pk.object_id 
        <span>and</span> pk.is_primary_key = <span>1</span>
    <span>inner</span> <span>join</span> sys.index_columns ic
        <span>on</span> ic.object_id = pk.object_id
        <span>and</span> ic.index_id = pk.index_id
    <span>inner</span> <span>join</span> sys.columns <span>col</span>
        <span>on</span> pk.object_id = col.object_id
        <span>and</span> col.column_id = ic.column_id
<span>order</span> <span>by</span> schema_name(tab.schema_id),
    pk.[<span>name</span>],
    ic.index_column_id
```

## Columns

-   **schema\_name** - PK schema name
-   **pk\_name** - PK constraint name
-   **column\_id** - id of column in index (1, 2, ...). 2 or higher means key is composite (contains more than one column)
-   **column\_name** - primary key column name
-   **table\_name** - PK table name

## Rows

-   **One row** represents one primary key column
-   **Scope of rows:** columns of all PK constraints in a database
-   **Ordered by** schema, PK constraint name, column id

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/primary_keys_columns.png)

## You could also get this

Get this interactive HTML data dictionary in minutes with [Dataedo](https://dataedo.com/).

![](https://dataedo.com/asset/img/blog/dataedo_export_productmodel.png)

[See live HTML data dictionary sample](https://dataedo.com/samples/html2/AdventureWorks/index.html#/doc/m10t160/adventureworks-database/modules/products/tables/production-productmodel)

[Try for free](http://dataedo.com/free-trial)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)