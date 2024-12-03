Query below lists all tables from all databases on SQL Server instance

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
declare @sql nvarchar(max);

select @sql = 
    (select ' UNION ALL
        SELECT ' +  + quotename(name,'''') + ' as database_name,
               s.name COLLATE DATABASE_DEFAULT
                    AS schema_name,
               t.name COLLATE DATABASE_DEFAULT as table_name 
               FROM '+ quotename(name) + '.sys.tables t
               JOIN '+ quotename(name) + '.sys.schemas s
                    on s.schema_id = t.schema_id'
    from sys.databases 
    where state=0
    order by [name] for xml path(''), type).value('.', 'nvarchar(max)');

set @sql = stuff(@sql, 1, 12, '') + ' order by database_name, 
                                               schema_name,
                                               table_name';

execute (@sql);
```

## Columns

-   **database\_name** - name of the database within schema resides
-   **schema\_name** - name of the schema
-   **table\_name** - name of the table

## Rows

-   **One row** represents one table in database
-   **Scope of rows:** all tables from all schemas and all databases on SQL Server instance
-   **Ordered by** database name, schema name, table name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/tables_all_databases.png)

## You could also get this

Get this interactive HTML data dictionary in minutes with [Dataedo](https://dataedo.com/).

![](https://dataedo.com/asset/img/blog/dataedo_export_productmodel.png)

[See live HTML data dictionary sample](https://dataedo.com/samples/html2/AdventureWorks/index.html#/doc/m10t160/adventureworks-database/modules/products/tables/production-productmodel)

[Try for free](http://dataedo.com/free-trial)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)