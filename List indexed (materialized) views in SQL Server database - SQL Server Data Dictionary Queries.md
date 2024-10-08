Query below lists indexed views, with their definition

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> schema_name(v.schema_id) <span>as</span> schema_name,
       v.name <span>as</span> view_name,
       i.name <span>as</span> index_name,
       m.definition
<span>from</span> sys.views v
<span>join</span> sys.indexes i
     <span>on</span> i.object_id = v.object_id
     <span>and</span> i.index_id = <span>1</span>
     <span>and</span> i.ignore_dup_key = <span>0</span>
<span>join</span> sys.sql_modules m
     <span>on</span> m.object_id = v.object_id
<span>order</span> <span>by</span> schema_name,
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