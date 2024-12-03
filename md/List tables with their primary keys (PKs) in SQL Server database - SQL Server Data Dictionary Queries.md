Query below lists tables and their primary key (**PK**) constraint names. By browsing list you can spot which tables have and which don't have primary keys.

See also: [tables without primary keys](https://dataedo.com/kb/query/sql-server/find-tables-without-primary-keys).

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(tab.schema_id) as [schema_name], 
    tab.[name] as table_name, 
    pk.[name] as pk_name,
    substring(column_names, 1, len(column_names)-1) as [columns]
from sys.tables tab
    left outer join sys.indexes pk
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
    tab.[name]
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