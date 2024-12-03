Query below lists indexed views, with their definition

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select schema_name(v.schema_id) as schema_name,
       v.name as view_name,
       i.name as index_name,
       m.definition
from sys.views v
join sys.indexes i
     on i.object_id = v.object_id
     and i.index_id = 1
     and i.ignore_dup_key = 0
join sys.sql_modules m
     on m.object_id = v.object_id
order by schema_name,
         view_name;
```

## Columns

-   **schema\_name** - schema name
-   **view\_name** - indexed view name
-   **index\_name** - name of the unique clustered index
-   **definition** - definition of the view

## Rows

-   **One row** represents one indexed view in a database with its index
-   **Scope of rows:** all indexed views
-   **Ordered by** schema name, view name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/indexed-views.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)