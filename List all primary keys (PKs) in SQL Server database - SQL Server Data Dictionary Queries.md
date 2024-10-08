Query below lists all primary keys constraints (**PK**) in the database.

See also: [tables with their primary keys](https://dataedo.com/kb/query/sql-server/list-tables-with-their-primary-keys).

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(tab.schema_id) as [schema_name], 
    pk.[name] as pk_name,
    substring(column_names, 1, len(column_names)-1) as [columns],
    tab.[name] as table_name
from sys.tables tab
    inner join sys.indexes pk
        on tab.object_id = pk.object_id 
        and pk.is_primary_key = 1
   cross apply (select col.[name] + ', '
                    from sys.index_columns ic
                        inner join sys.columns col
                            on ic.object_id = col.object_id
                            and ic.column_id = col.column_id
                    where ic.object_id = tab.object_id
                        and ic.index_id = pk.index_id
                            order by col.column_id
                            for xml path ('') ) D (column_names)
order by schema_name(tab.schema_id),
    pk.[name]
```

## Columns

-   **schema\_name** - PK schema name
-   **pk\_name** - PK constraint name
-   **columns** - list of PK columns separated with ','
-   **table\_name** - PK table name

## Rows

-   **One row** represents one primary key (table) in a database
-   **Scope of rows:** all PK constraints in a database
-   **Ordered by** schema, PK constraint name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/primary_keys.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)