Query below lists indexed (materialized) views in a database with their references.

## Notes

This query doesn't include cross database references. This query lists only references within database.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
<span>select</span> <span>distinct</span> schema_name(v.schema_id) <span>as</span> schema_name,
       v.name <span>as</span> view_name,
       schema_name(o.schema_id) <span>as</span> referenced_schema_name,
       o.name <span>as</span> referenced_entity_name,
       o.type_desc <span>as</span> entity_type
<span>from</span> sys.views v
<span>join</span> sys.sql_expression_dependencies d
     <span>on</span> d.referencing_id = v.object_id
     <span>and</span> d.referenced_id <span>is</span> <span>not</span> <span>null</span>
<span>join</span> sys.objects o
     <span>on</span> o.object_id = d.referenced_id
<span>join</span> sys.indexes i 
     <span>on</span> v.object_id = i.object_id
     <span>and</span> i.type = <span>1</span>
 <span>order</span> <span>by</span> schema_name,
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
-   **Scope of rows:** all references from indexed view to other entities
-   **Ordered by** schema name, view name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/indexed_view_references.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)