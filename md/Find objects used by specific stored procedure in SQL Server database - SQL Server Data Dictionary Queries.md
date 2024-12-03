Query below return all stored procedures and objects used by them.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select schema_name(obj.schema_id) as schema_name,
       obj.name as procedure_name,
       schema_name(dep_obj.schema_id) as referenced_object_schema,
       dep_obj.name as referenced_object_name,
       dep_obj.type_desc as object_type
from sys.objects obj
left join sys.sql_expression_dependencies dep
          on dep.referencing_id = obj.object_id
left join sys.objects dep_obj
          on dep_obj.object_id = dep.referenced_id
where obj.type in ('P', 'X', 'PC', 'RF')
    --  and obj.name = 'procedure_name'  -- put procedure name here
order by schema_name,
         procedure_name;
```

## Columns

-   **schema\_name** - schema name
-   **procedure\_name** - procedure name
-   **referenced\_object\_schema** - schema name of the referenced object
-   **referenced\_object\_name** - name of the referenced object
-   **object\_type** - type of referenced object

## Rows

-   **One row** represents one referenced object by procedure or only procedure if procedure is not using any objects
-   **Scope of rows:** all objects that are used by procedure in database
-   **Ordered by** schema name and procedure name

## Sample Result

![](https://dataedo.com/asset/img/kb/query/sql-server/procedures_referenced_objects.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)