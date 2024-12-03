Query below lists views in a database with their references.

## Notes

This query doesn't include cross database references. This query lists only references within database.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select distinct schema_name(v.schema_id) as schema_name,
       v.name as view_name,
       schema_name(o.schema_id) as referenced_schema_name,
       o.name as referenced_entity_name,
       o.type_desc as entity_type
from sys.views v
join sys.sql_expression_dependencies d
     on d.referencing_id = v.object_id
     and d.referenced_id is not null
join sys.objects o
     on o.object_id = d.referenced_id
 order by schema_name,
          view_name;
```

## Columns

-   **schema\_name** - view schema name
-   **view\_name** - view name
-   **referenced\_schema\_name** - schema of the referenced object
-   **referenced\_entity\_name** - name of the referenced object
-   **entity\_type** - type of referenced entity
    -   USER\_TABLE
    -   VIEW

## Rows

-   **One row** represents one reference
-   **Scope of rows:** all references from view to other entities
-   **Ordered by** schema name, view name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/view_references.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)